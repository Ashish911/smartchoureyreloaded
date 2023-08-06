﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Utilites
{
    public class DateTimeHelper
    {
        public DateTime GetTokyoDate()
        {
            var dateValue = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
            return dateValue;
        }
    }
}
