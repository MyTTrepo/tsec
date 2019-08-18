using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class StaticData
    {
        public static string Language;
        public static List<ColumnInfo> ColumnsInfo;
        public static List<InstrumentInfo> Instruments;
        public static List<string> SelectedInstruments;
        public static List<TseShareInfo> TseShares;

        public static void FillStaticData()
        {
            Settings settings = new Settings();
            StaticData.Language = settings.Language;
            StaticData.ColumnsInfo = FileService.ColumnsInfo();
            StaticData.Instruments = FileService.Instruments();
            StaticData.TseShares = FileService.TseShares();
            StaticData.SelectedInstruments = FileService.SelectedInstruments();
        }
    }
}
