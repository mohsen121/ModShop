using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketPlace.Common.Util
{
    public static class  DateTimeTools
    {
        public static DateTime GetKeyWordValidDay(this DateTime date, string dayTemplate, string country)
        {
            if (string.IsNullOrEmpty(dayTemplate))
                return date;

            var countryCode = CountryCode.US;
            try
            {
                countryCode = (CountryCode)Enum.Parse(typeof(CountryCode), country.ToUpper());
            }
            catch { countryCode = CountryCode.US; }
            // check for daytemplate to get best match for sending date
            var days = dayTemplate.Split(',');
            // in this loop if the passed date match with accepted days we send date itself.
            foreach (var d in days)
            {
                if (d == "1")
                {
                    if (date.DayOfWeek == DayOfWeek.Monday)
                    {
                        return date;
                    }
                }
                if (d == "2")
                {
                    if (date.DayOfWeek == DayOfWeek.Tuesday)
                    {
                        return date;
                    }
                }

                if (d == "3")
                {
                    if (date.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        return date;
                    }
                }

                if (d == "4")
                {
                    if (date.DayOfWeek == DayOfWeek.Thursday)
                    {
                        return date;
                    }
                }
                if (d == "5")
                {
                    if (date.DayOfWeek == DayOfWeek.Friday)
                    {
                        return date;
                    }
                }
                if (d == "6")
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        return date;
                    }
                }
                if (d == "7")
                {
                    if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return date;
                    }
                }
                if (d == "8")
                {

                    if (DateSystem.IsPublicHoliday(date, countryCode))
                    {
                        return date;
                    }
                }

            }

            // if we reach this section we should convert date to a valid date according to dayTemplate
            var firstDay = days.OrderBy(x => x).First();
            var validDay = date;
            switch (firstDay)
            {
                case "1":
                    validDay = DateSystem.FindDay(date, DayOfWeek.Monday);
                    break;
                case "2":
                    date = DateSystem.FindDay(date, DayOfWeek.Tuesday);
                    break;
                case "3":
                    validDay = DateSystem.FindDay(date, DayOfWeek.Wednesday);
                    break;
                case "4":
                    validDay = DateSystem.FindDay(date, DayOfWeek.Thursday);
                    break;
                case "5":
                    validDay = DateSystem.FindDay(date, DayOfWeek.Friday);
                    break;
                case "6":
                    validDay = DateSystem.FindDay(date, DayOfWeek.Saturday);
                    break;
                case "7":
                    validDay = DateSystem.FindDay(date, DayOfWeek.Sunday);
                    break;
                case "8":
                    var holidays = DateSystem.GetPublicHoliday(date, date.AddMonths(6), countryCode);
                    if (holidays.Any())
                    {
                        validDay = holidays.Select(x => x.Date).MinBy(x => x);
                    }
                    else
                    {
                        validDay = date;
                    }
                    break;
                default:
                    validDay = date;
                    break;

            }

            return new DateTime(validDay.Year, validDay.Month, validDay.Day, date.Hour, date.Minute, date.Second);
        }
    }
}
