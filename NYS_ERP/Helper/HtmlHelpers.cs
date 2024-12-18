namespace NYS_ERP.Helper
{
    public static class HtmlHelpers
    {
        public static string ReadOnlyIf(bool condition)
        {
            return condition ? "readonly" : string.Empty;
        }

        public static string DisabledIf(bool condition)
        {
            return condition ? "disabled" : string.Empty;
        }
    }
}
