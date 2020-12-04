﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = true;

        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }
            return builder.ToString();
        }

        public static string GenerateRandomDay()
        {
            string[] rndDays = { "-", rnd.Next(1, 32).ToString() };

            int index = rnd.Next(rndDays.Length);

            return rndDays[index];
        }

        public static string GenerateRandomMonth()
        {
            string[] rndMonths = { "-", "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December" };

            int index = rnd.Next(rndMonths.Length);

            return rndMonths[index];
        }
    }
}
