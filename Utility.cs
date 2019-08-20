using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ConsoleApplication1
{
    class Utility
    {
        public static string Compress(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            MemoryStream memoryStream1 = new MemoryStream();
            using (GZipStream gzipStream = new GZipStream((Stream)memoryStream1, CompressionMode.Compress, true))
                gzipStream.Write(bytes, 0, bytes.Length);
            memoryStream1.Position = 0L;
            MemoryStream memoryStream2 = new MemoryStream();
            byte[] buffer = new byte[memoryStream1.Length];
            memoryStream1.Read(buffer, 0, buffer.Length);
            byte[] inArray = new byte[buffer.Length + 4];
            Buffer.BlockCopy((Array)buffer, 0, (Array)inArray, 4, buffer.Length);
            Buffer.BlockCopy((Array)BitConverter.GetBytes(bytes.Length), 0, (Array)inArray, 0, 4);
            return Convert.ToBase64String(inArray);
        }
        public static DateTime ConvertJalaliStringToDateTime(string input)
        {
            cDate cDate = new cDate(1);
            cDate.fromJalali(input);
            return cDate.ToDateTime();
        }

        public static int ConvertDateTimeToJalaliInt(DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return Convert.ToInt32(persianCalendar.GetYear(dateTime).ToString() + persianCalendar.GetMonth(dateTime).ToString().PadLeft(2, '0') + persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0'));
        }
        public static int ConvertGregorianIntToJalaliInt(int input)
        {
            string str = input.ToString();
            return Utility.ConvertDateTimeToJalaliInt(new DateTime(Convert.ToInt32(str.Substring(0, 4)), Convert.ToInt32(str.Substring(4, 2)), Convert.ToInt32(str.Substring(6, 2))));
        }
        public static int ConvertJalaliStringToGregorianInt(string input)
        {
            cDate cDate = new cDate(1);
            cDate.fromJalali(input);
            DateTime dateTime = cDate.ToDateTime();
            return Convert.ToInt32(dateTime.Year.ToString() + dateTime.Month.ToString("00") + dateTime.Day.ToString("00"));
        }
        public static int ConvertDateTimeToGregorianInt(DateTime dateTime)
        {
            return Convert.ToInt32(dateTime.Year.ToString() + dateTime.Month.ToString("00") + dateTime.Day.ToString("00"));
        }
    }
}
