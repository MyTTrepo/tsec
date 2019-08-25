using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        private static CheckBox chkRemoveOldFiles;
        static void Main(string[] args)
        {
            StaticData.FillStaticData();

            //UCStepUpdate.UpdateClosingPrices();

            Program.GnerateFiles();

            Console.ReadLine();
        }




        public static bool GnerateFiles()
        {
            try {
                Settings settings = new Settings();
                settings.AdjustPricesCondition = 1; // me
                //string path = settings.AdjustPricesCondition != 0 ? settings.AdjustedStorageLocation : settings.StorageLocation;
                string path = "./"; // me
                if ((string.IsNullOrEmpty(path) || !Directory.Exists(path))) { // && this.isVisual
                    Console.WriteLine("\n\tمقدار فیلد محل ذخیره فایل ها صحیح نمی باشد ");
                    return false;
                }

                //settings.StartDate = settings.StartDate.Replace("/", "").ToString(); // unnecessary
                DateTime dateTime = Utility.ConvertJalaliStringToDateTime(settings.StartDate);
                int startDeven = dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
                using (List<string>.Enumerator enumerator = StaticData.SelectedInstruments.GetEnumerator()) { // for each selected instrument
                    while (enumerator.MoveNext()) {
                        string currentItemInscode = enumerator.Current;
                        List<ClosingPriceInfo> cp = FileService.ClosingPrices(Convert.ToInt64(currentItemInscode)); // all closing prices of a selected instrument
                        cp = cp.FindAll((Predicate<ClosingPriceInfo>)(p => p.DEven >= startDeven));
                        if ((settings.AdjustPricesCondition == 1 || settings.AdjustPricesCondition == 2) && cp.Count > 1) { // for both adjust conds
                            List<ClosingPriceInfo> closingPriceInfoList = new List<ClosingPriceInfo>();
                            Decimal num2 = new Decimal(1);
                            closingPriceInfoList.Add(cp[cp.Count - 1]);
                            double gaps = 0.0; // number of price gaps happened in whole history (capital-increase, dividends, intrument open-closes, etc)
                            if (settings.AdjustPricesCondition == 1) { // cond 1
                                for (int index = cp.Count - 2; index >= 0; --index) { // for each cp (2ndlast to first)
                                    if (cp[index].PClosing != cp[index + 1].PriceYesterday) {
                                        ++gaps;
                                    }
                                }
                                // above for loop detects capital increases, dividends or any kind of price gaps
                                // formula: if closing price of previous day is not equal to today's yesterday-price
                            }
                            // (gaps / cp.Count) = I don't understand this yet.
                            if (settings.AdjustPricesCondition == 1 && (gaps / cp.Count < 0.08 || settings.AdjustPricesCondition == 2)) { // cond 2 (kinda)
                                for (int i = cp.Count - 2; i >= 0; --i) { // for each cp (2ndlast to first)
                                    Predicate<TseShareInfo> aShareThatsDifferent = p => {
                                        if (p.InsCode.ToString().Equals(currentItemInscode)) {
                                            return p.DEven == cp[i + 1].DEven;
                                        }
                                        return false;
                                    };

                                    if (settings.AdjustPricesCondition == 1 && cp[i].PClosing != cp[i + 1].PriceYesterday) { // if found gap
                                        num2 = num2 * cp[i + 1].PriceYesterday / cp[i].PClosing; // divide tomorrow's PriceYesterday by today's PClosing
                                    } else if (
                                        settings.AdjustPricesCondition == 2 &&
                                        cp[i].PClosing != cp[i + 1].PriceYesterday &&
                                        StaticData.TseShares.Exists(aShareThatsDifferent)
                                    ) { // do:
                                        var something = StaticData.TseShares.Find(aShareThatsDifferent);
                                        decimal oldShares = something.NumberOfShareOld;
                                        decimal newShares = something.NumberOfShareNew;
                                        num2 = (num2 * oldShares) / newShares;
                                    }

                                    closingPriceInfoList.Add(new ClosingPriceInfo() {
                                        InsCode = cp[i].InsCode,
                                        DEven = cp[i].DEven,
                                        PClosing = Math.Round(num2 * cp[i].PClosing, 2),
                                        PDrCotVal = Math.Round(num2 * cp[i].PDrCotVal, 2),
                                        ZTotTran = cp[i].ZTotTran,
                                        QTotTran5J = cp[i].QTotTran5J,
                                        QTotCap = cp[i].QTotCap,
                                        PriceMin = Math.Round(num2 * cp[i].PriceMin),
                                        PriceMax = Math.Round(num2 * cp[i].PriceMax),
                                        PriceYesterday = Math.Round(num2 * cp[i].PriceYesterday),
                                        PriceFirst = Math.Round(num2 * cp[i].PriceFirst, 2)
                                    });
                                }
                                cp.Clear();
                                for (int index = closingPriceInfoList.Count - 1; index >= 0; --index)
                                    cp.Add(closingPriceInfoList[index]);
                            }
                        } // end of adjusting scenarios
                        InstrumentInfo instrument = StaticData.Instruments.Find((Predicate<InstrumentInfo>)(p => p.InsCode.ToString().Equals(currentItemInscode)));
                        if (!settings.ExcelOutput)
                            //FileService.WriteOutputFile(instrument, cp, !Program.chkRemoveOldFiles.Checked);
                            FileService.WriteOutputFile(instrument, cp, false);
                        else
                            FileService.WriteOutputExcel(instrument, cp);
                    }
                } // end of for each selected instrument
                return true;
            } catch (Exception ex) {
                var x = ex;
                return false;
            }
        }

        private class CheckBox
        {
            public bool Checked { get; internal set; }
        }
    }
}