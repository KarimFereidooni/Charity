namespace Charity.Extensions
{
    public static class AppSetting
    {
        public static string BackendUrl
        {
            get
            {
#if DEBUG
                return "https://localhost:5001";
#else
                return "https://transer.ir";
#endif
            }
        }

        public static string FrontendUrl
        {
            get
            {
#if DEBUG
                return "https://localhost:5001";
#else
                return "https://transer.ir";
#endif
            }
        }
    }
}
