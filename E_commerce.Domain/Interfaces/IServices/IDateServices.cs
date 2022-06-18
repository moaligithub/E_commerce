using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Interfaces.IServices
{
    public interface IDateServices
    {
        /// <summary>
        ///     This Method it working on Get Month Name By Month Number.
        /// </summary>
        /// <param name="month">This Parameter expresses month number</param>
        /// <returns>month name of string data type</returns>
        string GetMonthName(int month);

        /// <summary>
        ///     This Method It Working on Get All months From Year
        /// </summary>
        /// <param name="Year">This Paramter Expresses Year , And Being Get All Months from This Year</param>
        /// <returns>List of Months of string data type</returns>
        List<string> GetMonthsNamesFromYear(int Year);

        /// <summary>
        ///     This Method it working on Get All Days From Month
        /// </summary>
        /// <param name="month">This Parameter Expresses Month, And Being Get All Days From This Month</param>
        /// <param name="year">This Parameter Expresses Year , And Being Number of Days in month By This Year</param>
        /// <returns>List of Days of int data type</returns>
        List<int> GetDaysFromMonth(int month, int year);

        /// <summary>
        ///     Get Last 10 Years
        /// </summary>
        /// <returns>List of int</returns>
        List<int> GetLast10Years();
    }
}
