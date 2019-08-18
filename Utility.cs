using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Utility
    {
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
    }
}
