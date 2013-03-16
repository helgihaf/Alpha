using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.ExpressionParsing.Tests
{
    class Person
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Person[] Children { get; set; }

        public int CalculateAge()
        {
            DateTime now = DateTime.Now;

            DateTime birthdayThisYear;
            int daysInBirthMonthThisYear = DateTime.DaysInMonth(now.Year, DateOfBirth.Month);
            if (daysInBirthMonthThisYear < DateOfBirth.Day)
            {
                birthdayThisYear = new DateTime(now.Year, DateOfBirth.Month, daysInBirthMonthThisYear);
            }
            else
            {
                birthdayThisYear = new DateTime(now.Year, DateOfBirth.Month, DateOfBirth.Day);
            }

            if (now < birthdayThisYear)
            {
                return now.Year - DateOfBirth.Year - 1;
            }
            else
            {
                return now.Year - DateOfBirth.Year;
            }
        }
    }
}
