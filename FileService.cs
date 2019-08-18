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
            return FileService.ColumnsInfo("Columns.csv");
        }
        public static List<ColumnInfo> ColumnsInfo(string fileName)
        {
            List<ColumnInfo> columnInfoList = new List<ColumnInfo>();
            try
            {
                using (StreamReader streamReader = new StreamReader((Stream)File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TseClient 2.0\\Files\\" + fileName)))
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
        public static void WriteOutputExcel(InstrumentInfo instrument, List<ClosingPriceInfo> cp)
        {
            return;
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

        public static int OutputFileLastDeven(InstrumentInfo instrument, int indexOfDate, bool isShamsiDate)
        {
            int num = 0;
            try
            {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                Settings settings = new Settings();
                string str1 = settings.StorageLocation;
                if (settings.AdjustPricesCondition == 1 || settings.AdjustPricesCondition == 2)
                    str1 = settings.AdjustedStorageLocation;
                string str2;
                switch (Convert.ToInt32(settings.FileName))
                {
                    case 0:
                        str2 = instrument.CIsin;
                        if (instrument.YMarNSC != "ID")
                        {
                            if (settings.AdjustPricesCondition == 1)
                            {
                                str2 += "-a";
                                break;
                            }
                            if (settings.AdjustPricesCondition == 2)
                            {
                                str2 += "-i";
                                break;
                            }
                            break;
                        }
                        break;
                    case 1:
                        str2 = instrument.LatinName;
                        if (instrument.YMarNSC != "ID")
                        {
                            if (settings.AdjustPricesCondition == 1)
                            {
                                str2 += "-a";
                                break;
                            }
                            if (settings.AdjustPricesCondition == 2)
                            {
                                str2 += "-i";
                                break;
                            }
                            break;
                        }
                        break;
                    case 2:
                        str2 = instrument.LatinSymbol;
                        if (instrument.YMarNSC != "ID")
                        {
                            if (settings.AdjustPricesCondition == 1)
                            {
                                str2 += "-a";
                                break;
                            }
                            if (settings.AdjustPricesCondition == 2)
                            {
                                str2 += "-i";
                                break;
                            }
                            break;
                        }
                        break;
                    case 3:
                        str2 = instrument.Name;
                        if (instrument.YMarNSC != "ID")
                        {
                            if (settings.AdjustPricesCondition == 1)
                            {
                                str2 += "-ت";
                                break;
                            }
                            if (settings.AdjustPricesCondition == 2)
                            {
                                str2 += "-ا";
                                break;
                            }
                            break;
                        }
                        break;
                    case 4:
                        str2 = instrument.Symbol;
                        if (instrument.YMarNSC != "ID")
                        {
                            if (settings.AdjustPricesCondition == 1)
                            {
                                str2 += "-ت";
                                break;
                            }
                            if (settings.AdjustPricesCondition == 2)
                            {
                                str2 += "-ا";
                                break;
                            }
                            break;
                        }
                        break;
                    default:
                        str2 = instrument.CIsin;
                        if (instrument.YMarNSC != "ID")
                        {
                            if (settings.AdjustPricesCondition == 1)
                            {
                                str2 += "-a";
                                break;
                            }
                            if (settings.AdjustPricesCondition == 2)
                            {
                                str2 += "-i";
                                break;
                            }
                            break;
                        }
                        break;
                }
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
            }
            return num;
        }
        public static void WriteOutputFile(InstrumentInfo instrument, List<ClosingPriceInfo> cp, bool appendExistingFile)
        {
            Settings settings = new Settings();
            string str1 = settings.StorageLocation;
            if (settings.AdjustPricesCondition == 1 || settings.AdjustPricesCondition == 2)
                str1 = settings.AdjustedStorageLocation;
            string str2 = settings.Delimeter.ToString();
            string str3; // filename

            /* settings.FileName
            0 Isin کد          
            1 نام لاتین        
            2 نماد لاتین       
            3 نام
            4 نماد
            default: 0

            "IRO1SIPA0001"
            "Saipa"
            "SIPA1"
            "سايپا"
            "خساپا"
            */
            switch (Convert.ToInt32(settings.FileName))
            {
                case 0:
                    str3 = instrument.CIsin;
                    if (instrument.YMarNSC != "ID")
                    {
                        if (settings.AdjustPricesCondition == 1)
                        {
                            str3 += "-a";
                            break;
                        }
                        if (settings.AdjustPricesCondition == 2)
                        {
                            str3 += "-i";
                            break;
                        }
                        break;
                    }
                    break;
                case 1:
                    str3 = instrument.LatinName;
                    if (instrument.YMarNSC != "ID")
                    {
                        if (settings.AdjustPricesCondition == 1)
                        {
                            str3 += "-a";
                            break;
                        }
                        if (settings.AdjustPricesCondition == 2)
                        {
                            str3 += "-i";
                            break;
                        }
                        break;
                    }
                    break;
                case 2:
                    str3 = instrument.LatinSymbol;
                    if (instrument.YMarNSC != "ID")
                    {
                        if (settings.AdjustPricesCondition == 1)
                        {
                            str3 += "-a";
                            break;
                        }
                        if (settings.AdjustPricesCondition == 2)
                        {
                            str3 += "-i";
                            break;
                        }
                        break;
                    }
                    break;
                case 3:
                    str3 = instrument.Name;
                    if (instrument.YMarNSC != "ID")
                    {
                        if (settings.AdjustPricesCondition == 1)
                        {
                            str3 += "-ت";
                            break;
                        }
                        if (settings.AdjustPricesCondition == 2)
                        {
                            str3 += "-ا";
                            break;
                        }
                        break;
                    }
                    break;
                case 4:
                    str3 = instrument.Symbol;
                    if (instrument.YMarNSC != "ID")
                    {
                        if (settings.AdjustPricesCondition == 1)
                        {
                            str3 += "-ت";
                            break;
                        }
                        if (settings.AdjustPricesCondition == 2)
                        {
                            str3 += "-ا";
                            break;
                        }
                        break;
                    }
                    break;
                default:
                    str3 = instrument.CIsin;
                    if (instrument.YMarNSC != "ID")
                    {
                        if (settings.AdjustPricesCondition == 1)
                        {
                            str3 += "-a";
                            break;
                        }
                        if (settings.AdjustPricesCondition == 2)
                        {
                            str3 += "-i";
                            break;
                        }
                        break;
                    }
                    break;
            }
            // replace(char, with)
            // replaces invalid chars for windows file names in the settings.filename (set by user in settings panel)
            string str4 = str3.Replace('\\', ' ').Replace('/', ' ').Replace('*', ' ').Replace(':', ' ').Replace('>', ' ').Replace('<', ' ').Replace('?', ' ').Replace('|', ' ').Replace('^', ' ').Replace('"', ' ');
            int num = 0;
            // str1 storageLocation
            // str4 filename
            if (appendExistingFile)
            {
                if (!File.Exists(str1 + "\\" + str4 + "." + settings.FileExtension))
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
                    num = FileService.OutputFileLastDeven(instrument, indexOfDate, isShamsiDate);
                }
            }
            List<ColumnInfo> columnInfoList = FileService.ColumnsInfo();
            Encoding utF8 = Encoding.UTF8;
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
            TextWriter textWriter = (TextWriter)new StreamWriter(str1 + "\\" + str4 + "." + settings.FileExtension, appendExistingFile, encoding);
            columnInfoList.Sort((Comparison<ColumnInfo>)((s1, s2) => s1.Index.CompareTo(s2.Index)));
            string str5 = "";
            if (settings.ShowHeaders && num == 0)
            {
                foreach (ColumnInfo columnInfo in columnInfoList)
                {
                    if (columnInfo.Visible)
                    {
                        str5 += columnInfo.Header;
                        str5 += str2;
                    }
                }
                string str6 = str5.Substring(0, str5.Length - 1);
                textWriter.WriteLine(str6);
            }
            foreach (ClosingPriceInfo closingPriceInfo in cp)
            {
                if ((!appendExistingFile || closingPriceInfo.DEven > num) && (settings.ExportDaysWithoutTrade || !(closingPriceInfo.ZTotTran == new Decimal(0))))
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
                                    str6 += instrument.LatinName.ToString();
                                    if (instrument.YMarNSC != "ID")
                                    {
                                        if (settings.AdjustPricesCondition == 1)
                                        {
                                            str6 += "-a";
                                            break;
                                        }
                                        if (settings.AdjustPricesCondition == 2)
                                        {
                                            str6 += "-i";
                                            break;
                                        }
                                        break;
                                    }
                                    break;
                                case ColumnType.Symbol:
                                    str6 += instrument.Symbol.Replace(" ", "_").ToString();
                                    if (instrument.YMarNSC != "ID")
                                    {
                                        if (settings.AdjustPricesCondition == 1)
                                        {
                                            str6 += "-ت";
                                            break;
                                        }
                                        if (settings.AdjustPricesCondition == 2)
                                        {
                                            str6 += "-ا";
                                            break;
                                        }
                                        break;
                                    }
                                    break;
                                case ColumnType.Name:
                                    str6 += instrument.Name.Replace(" ", "_").ToString();
                                    if (instrument.YMarNSC != "ID")
                                    {
                                        if (settings.AdjustPricesCondition == 1)
                                        {
                                            str6 += "-ت";
                                            break;
                                        }
                                        if (settings.AdjustPricesCondition == 2)
                                        {
                                            str6 += "-ا";
                                            break;
                                        }
                                        break;
                                    }
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
                            str6 += str2;
                        }
                    }
                    string str7 = str6.Substring(0, str6.Length - 1);
                    textWriter.WriteLine(str7);
                }
            }
            textWriter.Flush();
            textWriter.Dispose();
        }
        public static bool HasAccessToWrite(string path)
        {
            try
            {
                using (File.Create(Path.Combine(path, "Access.txt"), 1, FileOptions.DeleteOnClose))
                    ;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
