using System;

namespace Data
{
    public static class MyDate
    {
        public static DateTime date = DateTime.Now;
        
        public static DateTime GetDate()
        {
            return date;
        }
    }
}