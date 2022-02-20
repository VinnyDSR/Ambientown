namespace Assets.Scripts.Helpers
{
    public static class TypeConverter
    {
        public static int boolToInt(bool val)
        {
            if (val)
                return 1;
            else
                return 0;
        }

        public static bool intToBool(int val)
        {
            if (val != 0)
                return true;
            else
                return false;
        }
    }
}