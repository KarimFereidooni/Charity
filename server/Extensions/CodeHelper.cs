namespace Charity.Extensions
{
    using System;

    public static class CodeHelper
    {
        private static long LastLongCode { get; set; } = 0L;

        private static long LastSmallCode { get; set; } = 0L;

        private static readonly object LockObject = new object();

        public static long GetUniqueLongCode()
        {
            lock (LockObject)
            {
                System.Threading.Thread.Sleep(1);
                long code = (long)DateTime.UtcNow.Subtract(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                if (code <= 0)
                {
                    throw new Exception("تاریخ سیستم نامعتبر است");
                }

                if (code <= LastLongCode)
                {
                    throw new Exception("تاریخ سیستم تغییر کرده است");
                }

                LastLongCode = code;
                return code;
            }
        }

        public static long GetUniqueSmallCode()
        {
            lock (LockObject)
            {
                System.Threading.Thread.Sleep(1000);
                long code = (long)DateTime.UtcNow.Subtract(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                if (code <= 0)
                {
                    throw new Exception("تاریخ سیستم نامعتبر است");
                }

                if (code <= LastSmallCode)
                {
                    throw new Exception("تاریخ سیستم تغییر کرده است");
                }

                LastSmallCode = code;
                return code;
            }
        }
    }
}
