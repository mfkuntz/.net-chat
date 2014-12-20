using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspChat.Models
{
    public class FormattedDateTime
    {
        private DateTime date { get; set; }
        private string format;
        public static explicit operator DateTime(FormattedDateTime input)
        {
            return input.date;
        }

        public override string ToString()
        {
            return date.ToString(format);
        }

        public FormattedDateTime(DateTime d, string format = "g")
        {
            date = d;
            this.format = format;
        }
    }
}