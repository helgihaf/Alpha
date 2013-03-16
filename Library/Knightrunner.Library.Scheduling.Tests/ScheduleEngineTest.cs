using Knightrunner.Library.Scheduling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Knightrunner.Library.Scheduling.Tests
{
    
    
    /// <summary>
    ///This is a test class for ScheduleEngineTest and is intended
    ///to contain all ScheduleEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ScheduleEngineTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void SecondsIntervalTest()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "*/5"
            };
            ScheduleEngine target = new ScheduleEngine(entry);
            DateTime fromDate = GetFromDateTime();
            DateTime expected = fromDate;
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            fromDate = GetFromDateTime().AddSeconds(1);
            expected = GetFromDateTime().AddSeconds(5);
            actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void SecondsFixedTest()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "15"
            };
            ScheduleEngine target = new ScheduleEngine(entry);
            DateTime fromDate = GetFromDateTime(0);
            DateTime expected = fromDate.AddSeconds(15);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            fromDate = GetFromDateTime(20);
            expected = GetFromDateTime(15).AddMinutes(1);
            actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void SecondsRangeTest()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "30-40"
            };
            ScheduleEngine target = new ScheduleEngine(entry);
            DateTime fromDate = GetFromDateTime();
            DateTime expected = fromDate.AddSeconds(30);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            fromDate = GetFromDateTime(30);
            expected = fromDate;
            actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            fromDate = GetFromDateTime(31);
            expected = fromDate;
            actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            fromDate = GetFromDateTime(40);
            expected = fromDate;
            actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            fromDate = GetFromDateTime(41);
            expected = GetFromDateTime(30).AddMinutes(1);
            actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

        }


        [TestMethod()]
        public void EveryDayForTwoYearsTest()
        {
            // Every day at 00:00:00
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "*",
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            // 2008 is a leap year
            DateTime fromDate = new DateTime(2007, 1, 1);

            while (fromDate.Year < 2009)
            {
                DateTime expected = fromDate;
                DateTime actual = target.NextDateTime(fromDate);
                Assert.AreEqual(expected, actual);

                fromDate = fromDate.AddDays(1);
            }
        }


        [TestMethod()]
        public void NastyBugDateTest()
        {
            // Every day at 00:00:00
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "*",
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2007, 11, 30);
            DateTime expected = new DateTime(2007, 11, 30);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EveryDayAtMidnight()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "*",
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2007, 1, 1);

            DateTime expected = new DateTime(2007, 1, 1);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void DayOfEveryMonth()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "3",
                DaysOfWeek = "*",
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2007, 1, 1);

            DateTime expected = new DateTime(2007, 1, 3);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LastDayOfEveryMonth()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "L",
                DaysOfWeek = "*",
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2007, 1, 1);

            while (fromDate.Year < 2009)
            {
                int daysInMonth = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);
                DateTime expected = new DateTime(fromDate.Year, fromDate.Month, daysInMonth);
                DateTime actual = target.NextDateTime(fromDate);
                Assert.AreEqual(expected, actual, "fromDate = " + fromDate.ToString());

                fromDate = fromDate.AddDays(1);
            }
        }




        [TestMethod()]
        public void WeekStandardTests()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "1",
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2010, 10, 1);
            while (fromDate.Day < 25)
            {
                int day = fromDate.Day;
                int mondayDay = 1;
                if (day >= 1 && day <= 4)
                    mondayDay = 4;
                else if (day >= 5 && day <= 11)
                    mondayDay = 11;
                else if (day >= 12 && day <= 18)
                    mondayDay = 18;
                else if (day >= 19 && day <= 25)
                    mondayDay = 25;
                DateTime expected = new DateTime(fromDate.Year, fromDate.Month, mondayDay);
                DateTime actual = target.NextDateTime(fromDate);
                Assert.AreEqual(expected, actual);

                fromDate = fromDate.AddDays(1);
            }
        }


        [TestMethod()]
        public void WeekLastTests()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "4L",      // last thursday
                Months = "*"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2010, 10, 1);
            DateTime expected = new DateTime(fromDate.Year, fromDate.Month, 28);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);



            entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "5L",      // last friday
                Months = "*"
            };
            target = new ScheduleEngine(entry);

            fromDate = new DateTime(2007, 1, 1);
            while (fromDate.Year < 2009)
            {
                expected = FindLastExpectedWeekday(DayOfWeek.Friday, fromDate);
                actual = target.NextDateTime(fromDate);
                Assert.AreEqual(expected, actual);

                fromDate = fromDate.AddDays(1);
            }

        }



        [TestMethod()]
        public void LastThursdayEveryOtherMonth()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "*",
                DaysOfWeek = "4L",
                Months = "*/2"      // jan, mar, may, ... because 0=jan
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2010, 11, 25);
            DateTime expected = new DateTime(2010, 11, 25);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

            DateTime[] lastThursdays = new DateTime[]
            {
                new DateTime(2010, 1, 28),
                new DateTime(2010, 3, 25),
                new DateTime(2010, 5, 27),
                new DateTime(2010, 7, 29),
                new DateTime(2010, 9, 30),
                new DateTime(2010, 11, 25)
            };

            fromDate = new DateTime(2010,1,1);
            foreach (DateTime lastExpected in lastThursdays)
            {
                actual = target.NextDateTime(fromDate);
                Assert.AreEqual(lastExpected, actual);
                fromDate = actual.AddDays(1);
            }

        }


        [TestMethod()]
        public void SpecificMonth()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "7",
                DaysOfWeek = "*",
                Months = "12"
            };
            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(2010, 12, 7);
            DateTime expected = new DateTime(2010, 12, 7);
            DateTime actual = target.NextDateTime(fromDate);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void EveryOtherYear()
        {
            ScheduleEntry entry = new ScheduleEntry
            {
                Seconds = "0",
                Minutes = "0",
                Hours = "0",
                DaysOfMonth = "1",
                DaysOfWeek = "*",
                Months = "1",
                Years = "*/2"
            };

            ScheduleEngine target = new ScheduleEngine(entry);

            DateTime fromDate = new DateTime(1969,1,1);
            DateTime expected = new DateTime(1970,1,1);
            DateTime actual;

            while (fromDate.Year < 2400)
            {
                actual = target.NextDateTime(fromDate);
                Assert.AreEqual(expected, actual);

                fromDate = actual.AddDays(1);
                expected = expected.AddYears(2);
            }

        }

        private DateTime FindLastExpectedWeekday(DayOfWeek dayOfWeek, DateTime fromDate)
        {
            int day = FindLastWeekday(dayOfWeek, fromDate.Year, fromDate.Month);
            DateTime candidate = new DateTime(fromDate.Year, fromDate.Month, day);
            if (candidate < fromDate)
            {
                fromDate = new DateTime(fromDate.Year, fromDate.Month, 1).AddMonths(1);
                candidate = new DateTime(fromDate.Year, fromDate.Month, FindLastWeekday(dayOfWeek, fromDate.Year, fromDate.Month));
            }
            
            return candidate;
        }

        private int FindLastWeekday(DayOfWeek dayOfWeek, int year, int month)
        {
            int day;
            
            for (day = DateTime.DaysInMonth(year, month); new DateTime(year, month, day).DayOfWeek != dayOfWeek; day--)
                ;
            
            return day;
        }

        private DateTime GetFromDateTime()
        {
            return GetFromDateTime(0);
        }

        private DateTime GetFromDateTime(int second)
        {
            return new DateTime(2009, 10, 21, 10, 34, second);
        }

    }
}
