using E_commerce.Core.Interfaces.IServices;
using E_commerce.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services.AllServices
{
    public class DateServices : IDateServices
    {
        public string GetMonthName(int month)
            => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);

        public List<string> GetMonthsNamesFromYear(int Year)
        {
            List<string> Months = new List<string>();

            if(Year == DateTime.Now.Year)
            {
                // IN Case This Year it now the year
                int nowMonth = DateTime.Now.Month;
                for (int i = 1; i <= nowMonth; i++)
                    Months.Add(GetMonthName(i));
            }
            else
            {
                // IN Case this Year it not Now the Year
                for (int i = 1; i <= 12; i++)
                {
                    Months.Add(GetMonthName(i));
                }
            }

            return Months;
        }

        public List<int> GetDaysFromMonth(int month, int year)
        {
            int NumberOfDayes = DateTime.DaysInMonth(year, month);
            List<int> Days = new List<int>();
            
            if (month == DateTime.Now.Month && year == DateTime.Now.Year)
            {
                // IN Case This Month Is Now the Month
                for (int i = 1; i <= DateTime.Now.Day; i++)
                {
                    Days.Add(i);
                }
            }
            else
            {
                // IN Case This Month Is not Now the Month
                for (int i = 1; i <= NumberOfDayes; i++)
                {
                    Days.Add(i);
                }
            }

            return Days;
        }

        public List<int> GetLast10Years()
        {
            int NowYear = DateTime.Now.Year;
            List<int> LastYears = new List<int>();
            LastYears.Add(NowYear);
           
            for (int i = 1; i < 10; i++)
            {
                LastYears.Add(NowYear - i);
            }

            return LastYears;
        }
    }
}