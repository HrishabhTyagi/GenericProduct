namespace Common.ExtendedFunction
{
    public static class ExtendedFunction
    {
        public static bool IsNullOrWhitespaceOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str) && string.IsNullOrWhiteSpace(str))
                return true;
            return false;
        }
    }
}
