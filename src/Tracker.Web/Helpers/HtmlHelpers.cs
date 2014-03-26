namespace Tracker.Web.Helpers
{
    using System;

    public static class HtmlHelpers
    {
        public static string AsHumanizedTimeSpan(this string ts)
        {
            TimeSpan result;
            if (TimeSpan.TryParse(ts, out result))
            {
                return Humanize(result);
            }

            return Humanize(new TimeSpan(0));
        }

        public static string Humanize(this TimeSpan ts)
        {
            // Create an index based on whether the days, hours and minuted are non-zero
            var formats = new [] {
                "∅",             // 000 - zero time
                "{2}m",          // 001
                "{1}h",          // 010
                "{1}h {2}m",     // 011
                "{0}d",          // 100
                "{0}d {2}m",     // 101
                "{0}d {1}h",     // 110
                "{0}d {1}h {2}m" // 111
            };

            var index = ((ts.Days > 0) ? 4 : 0) + ((ts.Hours > 0) ? 2 : 0) + ((ts.Minutes > 0) ? 1 : 0);
            return String.Format(formats[index], ts.Days, ts.Hours, ts.Minutes);            
        }
    }
}