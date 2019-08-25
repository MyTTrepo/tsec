using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class FileService
    {
        public static int LastDeven(string insCode)
        {
            int num = 0;
            try
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments\\" + insCode + ".csv"))
                    return 0;
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments\\" + insCode + ".csv")))
                {
                    string str = "";
                    while (!streamReader.EndOfStream)
                        str = streamReader.ReadLine();
                    num = Convert.ToInt32(str.Split(',')[1].ToString());
                }
            }
            catch (Exception ex)
            {
            }
            return num;
        }
        public static List<InstrumentInfo> Instruments()
        {
            List<InstrumentInfo> instrumentInfoList = new List<InstrumentInfo>();
            try
            {
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments.csv")))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] strArray = streamReader.ReadLine().Split(',');
                        instrumentInfoList.Add(new InstrumentInfo()
                        {
                            InsCode = Convert.ToInt64(strArray[0].ToString()),
                            InstrumentID = strArray[1].ToString(),
                            LatinSymbol = strArray[2].ToString(),
                            LatinName = strArray[3].ToString(),
                            CompanyCode = strArray[4].ToString(),
                            Symbol = strArray[5].ToString(),
                            Name = strArray[6].ToString(),
                            CIsin = strArray[7].ToString(),
                            DEven = Convert.ToInt32(strArray[8].ToString()),
                            Flow = Convert.ToByte(strArray[9].ToString()),
                            LSoc30 = strArray[10].ToString(),
                            CGdSVal = strArray[11].ToString(),
                            CGrValCot = strArray[12].ToString(),
                            YMarNSC = strArray[13].ToString(),
                            CComVal = strArray[14].ToString(),
                            CSecVal = strArray[15].ToString(),
                            CSoSecVal = strArray[16].ToString(),
                            YVal = strArray[17].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return instrumentInfoList;
        }
        public static List<TseShareInfo> TseShares()
        {
            List<TseShareInfo> tseShareInfoList = new List<TseShareInfo>();
            try
            {
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\TseShares.csv")))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] strArray = streamReader.ReadLine().Split(',');
                        tseShareInfoList.Add(new TseShareInfo()
                        {
                            Idn = Convert.ToInt64(strArray[0].ToString()),
                            InsCode = Convert.ToInt64(strArray[1].ToString()),
                            DEven = Convert.ToInt32(strArray[2].ToString()),
                            NumberOfShareNew = Convert.ToDecimal(strArray[3].ToString()),
                            NumberOfShareOld = Convert.ToDecimal(strArray[4].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tseShareInfoList;
        }
        public static List<string> SelectedInstruments()
        {
            List<string> stringList = new List<string>();
            try
            {
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\SelectedInstruments.csv")))
                //using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead("../../SelectedInstruments.csv"))) // just in case
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] strArray = streamReader.ReadLine().Split(',');
                        stringList.Add(strArray[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stringList;
        }
        public static List<ClosingPriceInfo> ClosingPrices(long insCode)
        {
            List<ClosingPriceInfo> closingPriceInfoList = new List<ClosingPriceInfo>();
            try
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments\\" + insCode.ToString() + ".csv"))
                    return closingPriceInfoList;
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments\\" + insCode.ToString() + ".csv")))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] strArray = streamReader.ReadLine().Split(',');
                        closingPriceInfoList.Add(new ClosingPriceInfo()
                        {
                            InsCode = Convert.ToInt64(strArray[0].ToString()),
                            DEven = Convert.ToInt32(strArray[1].ToString()),
                            PClosing = Convert.ToDecimal(strArray[2].ToString()),
                            PDrCotVal = Convert.ToDecimal(strArray[3].ToString()),
                            ZTotTran = Convert.ToDecimal(strArray[4].ToString()),
                            QTotTran5J = Convert.ToDecimal(strArray[5].ToString()),
                            QTotCap = Convert.ToDecimal(strArray[6].ToString()),
                            PriceMin = Convert.ToDecimal(strArray[7].ToString()),
                            PriceMax = Convert.ToDecimal(strArray[8].ToString()),
                            PriceYesterday = Convert.ToDecimal(strArray[9].ToString()),
                            PriceFirst = Convert.ToDecimal(strArray[10].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return closingPriceInfoList;
        }
        public static List<ColumnInfo> ColumnsInfo()
        {
            List<ColumnInfo> columnInfoList = new List<ColumnInfo>();
            try
            {
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Columns.csv")))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] strArray = streamReader.ReadLine().Split(',');
                        columnInfoList.Add(new ColumnInfo()
                        {
                            Index = Convert.ToInt32(strArray[0].ToString()),
                            Type = (ColumnType)Enum.Parse(typeof(ColumnType), strArray[1].ToString()),
                            Header = strArray[2].ToString(),
                            Visible = strArray[3].ToString().Equals("1")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return columnInfoList;
        }
        public static void WriteInstruments()
        {
            using (TextWriter text = (TextWriter)File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments.csv"))
            {
                foreach (InstrumentInfo instrument in StaticData.Instruments)
                {
                    text.Write(instrument.InsCode);
                    text.Write(',');
                    text.Write(instrument.InstrumentID);
                    text.Write(',');
                    text.Write(instrument.LatinSymbol);
                    text.Write(',');
                    text.Write(instrument.LatinName);
                    text.Write(',');
                    text.Write(instrument.CompanyCode);
                    text.Write(',');
                    text.Write(instrument.Symbol);
                    text.Write(',');
                    text.Write(instrument.Name);
                    text.Write(',');
                    text.Write(instrument.CIsin);
                    text.Write(',');
                    text.Write(instrument.DEven);
                    text.Write(',');
                    text.Write((int)instrument.Flow);
                    text.Write(',');
                    text.Write(instrument.LSoc30);
                    text.Write(',');
                    text.Write(instrument.CGdSVal);
                    text.Write(',');
                    text.Write(instrument.CGrValCot);
                    text.Write(',');
                    text.Write(instrument.YMarNSC);
                    text.Write(',');
                    text.Write(instrument.CComVal);
                    text.Write(',');
                    text.Write(instrument.CSecVal);
                    text.Write(',');
                    text.Write(instrument.CSoSecVal);
                    text.Write(',');
                    text.Write(instrument.YVal);
                    text.Write('\n');
                }
                text.Flush();
            }
        }
        public static void WriteTseShares()
        {
            using (TextWriter text = (TextWriter)File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\TseShares.csv"))
            {
                foreach (TseShareInfo tseShare in StaticData.TseShares)
                {
                    text.Write(tseShare.Idn);
                    text.Write(',');
                    text.Write(tseShare.InsCode);
                    text.Write(',');
                    text.Write(tseShare.DEven);
                    text.Write(',');
                    text.Write(tseShare.NumberOfShareNew);
                    text.Write(',');
                    text.Write(tseShare.NumberOfShareOld);
                    text.Write('\n');
                }
                text.Flush();
            }
        }
        public static void WriteClosingPrices(List<ClosingPriceInfo> input)
        {
            if (input.Count == 0)
                return;
            string str = input[0].InsCode.ToString();
            List<ClosingPriceInfo> closingPriceInfoList1 = new List<ClosingPriceInfo>();
            List<ClosingPriceInfo> closingPriceInfoList2 = new List<ClosingPriceInfo>(); // same as input[0]
            List<ClosingPriceInfo> closingPriceInfoList3 = FileService.ClosingPrices(Convert.ToInt64(str)); // get previous closing prices of currently processing instrument
            foreach (ClosingPriceInfo closingPriceInfo in input)
                closingPriceInfoList2.Add(closingPriceInfo);
            using (List<ClosingPriceInfo>.Enumerator enumerator = closingPriceInfoList3.GetEnumerator()) // go through previous prices
            {
                while (enumerator.MoveNext())
                {
                    ClosingPriceInfo item = enumerator.Current;
                    if (closingPriceInfoList2.Find( (Predicate<ClosingPriceInfo>)(p => p.DEven == item.DEven) ) == null)
                        closingPriceInfoList2.Add(item);
                }
            }
            closingPriceInfoList2.Sort((Comparison<ClosingPriceInfo>)((s1, s2) => s1.DEven.CompareTo(s2.DEven)));
            using (TextWriter text = (TextWriter)File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments\\" + str + ".csv"))
            {
                foreach (ClosingPriceInfo closingPriceInfo in closingPriceInfoList2)
                {
                    text.Write(closingPriceInfo.InsCode);
                    text.Write(',');
                    text.Write(closingPriceInfo.DEven);
                    text.Write(',');
                    text.Write(closingPriceInfo.PClosing);
                    text.Write(',');
                    text.Write(closingPriceInfo.PDrCotVal);
                    text.Write(',');
                    text.Write(closingPriceInfo.ZTotTran);
                    text.Write(',');
                    text.Write(closingPriceInfo.QTotTran5J);
                    text.Write(',');
                    text.Write(closingPriceInfo.QTotCap);
                    text.Write(',');
                    text.Write(closingPriceInfo.PriceMin);
                    text.Write(',');
                    text.Write(closingPriceInfo.PriceMax);
                    text.Write(',');
                    text.Write(closingPriceInfo.PriceYesterday);
                    text.Write(',');
                    text.Write(closingPriceInfo.PriceFirst);
                    text.Write('\n');
                }
                text.Flush();
            }
        }
        public static string GetSuffix(string YMarNSC, int AdjustPricesCondition, bool fa=false)
        {
            string suffx = "";
            if (YMarNSC != "ID")
            {
                if (AdjustPricesCondition == 1)
                {
                    suffx = fa ? "-ت" : "-a";
                }
                else if (AdjustPricesCondition == 2)
                {
                    suffx = fa ? "-ا" : "-i";
                }
            }
            return suffx;
        }
        public static string GetFileName(InstrumentInfo instrument, string filenameType, int AdjustPricesCondition) {
            string YMarNSC = instrument.YMarNSC;
            string filename = "";
            switch (Convert.ToInt32(filenameType))
            {
                case 0:
                    filename = instrument.CIsin + FileService.GetSuffix(YMarNSC, AdjustPricesCondition);
                    break;
                case 1:
                    filename = instrument.LatinName + FileService.GetSuffix(YMarNSC, AdjustPricesCondition);
                    break;
                case 2:
                    filename = instrument.LatinSymbol + FileService.GetSuffix(YMarNSC, AdjustPricesCondition);
                    break;
                case 3:
                    filename = instrument.Name + FileService.GetSuffix(YMarNSC, AdjustPricesCondition, true);
                    break;
                case 4:
                    filename = instrument.Symbol + FileService.GetSuffix(YMarNSC, AdjustPricesCondition, true);
                    break;
                default:
                    filename = instrument.CIsin + FileService.GetSuffix(YMarNSC, AdjustPricesCondition);
                    break;
            }
            return filename;
        }
        public static int OutputFileLastDeven(InstrumentInfo instrument, int indexOfDate, bool isShamsiDate)
        {
            int num = 0;
            try
            {
                Settings settings = new Settings();
                string str1 = settings.StorageLocation;
                if (settings.AdjustPricesCondition == 1 || settings.AdjustPricesCondition == 2)
                    str1 = settings.AdjustedStorageLocation;
                string str2 = FileService.GetFileName(instrument, settings.FileName, settings.AdjustPricesCondition);
                string str3 = str2.Replace('\\', ' ').Replace('/', ' ').Replace('*', ' ').Replace(':', ' ').Replace('>', ' ').Replace('<', ' ').Replace('?', ' ').Replace('|', ' ').Replace('^', ' ').Replace('"', ' ');
                if (!File.Exists(str1 + "\\" + str3 + "." + settings.FileExtension))
                    return 0;
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(str1 + "\\" + str3 + "." + settings.FileExtension)))
                {
                    string str4 = "";
                    while (!streamReader.EndOfStream)
                        str4 = streamReader.ReadLine();
                    string[] strArray = str4.Split(',');
                    num = isShamsiDate ? Utility.ConvertJalaliStringToGregorianInt(strArray[indexOfDate].ToString()) : Convert.ToInt32(strArray[indexOfDate].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return num;
        }
        public static void WriteOutputFile(InstrumentInfo instrument, List<ClosingPriceInfo> cp, bool appendExistingFile)
        {
            Settings settings = new Settings();
            string storageLocation = settings.StorageLocation;
            if (settings.AdjustPricesCondition == 1 || settings.AdjustPricesCondition == 2)
                storageLocation = settings.AdjustedStorageLocation;
            string delimiter = settings.Delimeter.ToString();
            string filename = FileService.GetFileName(instrument, settings.FileName, settings.AdjustPricesCondition);
            // replace(char, with)
            // replaces invalid chars for windows file names in the settings.filename (set by user in settings panel)
            filename = filename.Replace('\\', ' ').Replace('/', ' ').Replace('*', ' ').Replace(':', ' ').Replace('>', ' ').Replace('<', ' ').Replace('?', ' ').Replace('|', ' ').Replace('^', ' ').Replace('"', ' ');
            int outputFileLastDeven = 0;
            if (appendExistingFile)
            {
                if (!File.Exists(storageLocation + "\\" + filename + "." + settings.FileExtension))
                {
                    appendExistingFile = false;
                }
                else
                {
                    int indexOfDate = 0;
                    bool isShamsiDate = false;
                    foreach (ColumnInfo columnInfo in StaticData.ColumnsInfo)
                    {
                        if (columnInfo.Type == ColumnType.Date && columnInfo.Visible)
                        {
                            indexOfDate = columnInfo.Index - 1;
                            isShamsiDate = false;
                            break;
                        }
                        if (columnInfo.Type == ColumnType.ShamsiDate && columnInfo.Visible)
                        {
                            indexOfDate = columnInfo.Index;
                            isShamsiDate = true;
                        }
                    }
                    outputFileLastDeven = FileService.OutputFileLastDeven(instrument, indexOfDate, isShamsiDate);
                }
            }
            List<ColumnInfo> columnInfoList = FileService.ColumnsInfo();
            //Encoding utF8 = Encoding.UTF8; // unnecessary
            Encoding encoding;
            switch (Convert.ToInt32(settings.Encoding))
            {
                case 0:
                    encoding = Encoding.Unicode;
                    break;
                case 1:
                    encoding = Encoding.UTF8;
                    break;
                case 2:
                    encoding = Encoding.GetEncoding(1256);
                    break;
                default:
                    encoding = Encoding.UTF8;
                    break;
            }
            TextWriter textWriter = (TextWriter)new StreamWriter(storageLocation + "\\" + filename + "." + settings.FileExtension, appendExistingFile, encoding);
            columnInfoList.Sort((Comparison<ColumnInfo>)((s1, s2) => s1.Index.CompareTo(s2.Index))); // sort by index I think
            string headerRow = "";
            if (settings.ShowHeaders && outputFileLastDeven == 0)
            {
                foreach (ColumnInfo columnInfo in columnInfoList)
                {
                    if (columnInfo.Visible)
                    {
                        headerRow += columnInfo.Header;
                        headerRow += delimiter;
                    }
                }
                headerRow = headerRow.Substring(0, headerRow.Length - 1);
                textWriter.WriteLine(headerRow);
            }
            // this is where file is generated
            string YMarNSC = instrument.YMarNSC;
            int AdjustPricesCondition = settings.AdjustPricesCondition;
            foreach (ClosingPriceInfo closingPriceInfo in cp)
            {
                if ((!appendExistingFile || closingPriceInfo.DEven > outputFileLastDeven) && (settings.ExportDaysWithoutTrade || !(closingPriceInfo.ZTotTran == new Decimal(0))))
                {
                    string str6 = "";
                    foreach (ColumnInfo columnInfo in columnInfoList)
                    {
                        if (columnInfo.Visible)
                        {
                            switch (columnInfo.Type)
                            {
                                case ColumnType.CompanyCode:
                                    str6 += instrument.CompanyCode.ToString();
                                    break;
                                case ColumnType.LatinName:
                                    str6 += instrument.LatinName.ToString() + FileService.GetSuffix(YMarNSC, AdjustPricesCondition);
                                    break;
                                case ColumnType.Symbol:
                                    str6 += instrument.Symbol.Replace(" ", "_").ToString() + FileService.GetSuffix(YMarNSC, AdjustPricesCondition, true);
                                    break;
                                case ColumnType.Name:
                                    str6 += instrument.Name.Replace(" ", "_").ToString() + FileService.GetSuffix(YMarNSC, AdjustPricesCondition, true);
                                    break;
                                case ColumnType.Date:
                                    str6 += closingPriceInfo.DEven.ToString();
                                    break;
                                case ColumnType.ShamsiDate:
                                    str6 += Utility.ConvertGregorianIntToJalaliInt(closingPriceInfo.DEven).ToString();
                                    break;
                                case ColumnType.PriceFirst:
                                    str6 += closingPriceInfo.PriceFirst.ToString();
                                    break;
                                case ColumnType.PriceMax:
                                    str6 += closingPriceInfo.PriceMax.ToString();
                                    break;
                                case ColumnType.PriceMin:
                                    str6 += closingPriceInfo.PriceMin.ToString();
                                    break;
                                case ColumnType.LastPrice:
                                    str6 += closingPriceInfo.PDrCotVal.ToString();
                                    break;
                                case ColumnType.ClosingPrice:
                                    str6 += closingPriceInfo.PClosing.ToString();
                                    break;
                                case ColumnType.Price:
                                    str6 += closingPriceInfo.QTotCap.ToString();
                                    break;
                                case ColumnType.Volume:
                                    str6 += closingPriceInfo.QTotTran5J.ToString();
                                    break;
                                case ColumnType.Count:
                                    str6 += closingPriceInfo.ZTotTran.ToString();
                                    break;
                                case ColumnType.PriceYesterday:
                                    str6 += closingPriceInfo.PriceYesterday.ToString();
                                    break;
                            }
                            str6 += delimiter;
                        }
                    }
                    string str7 = str6.Substring(0, str6.Length - 1);
                    textWriter.WriteLine(str7);
                }
            }
            textWriter.Flush();
            textWriter.Dispose();
        }
        public static void WriteOutputExcel(InstrumentInfo instrument, List<ClosingPriceInfo> cp)
        {
            return;
        }
        public static string ReadVersionFileContent()
        {
            string str = "";
            try
            {
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Version.txt")))
                    str = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
            }
            return str;
        }
        public static void WriteVersionFileContent(string version)
        {
            using (TextWriter text = (TextWriter)File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Version.txt"))
            {
                text.Write(version);
                text.Flush();
            }
        }
        public static void EraseCurrentFiles()
        {
            foreach (FileSystemInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments").GetFiles())
                file.Delete();
            FileInfo fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\Instruments.csv");
            fileInfo.Delete();
            fileInfo.Create();
        }
    }
}
