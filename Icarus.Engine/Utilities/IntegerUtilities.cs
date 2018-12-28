namespace Icarus.Engine.Utilities
{
    public static class IntegerUtilities
    {
        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => !value.IsEven();
    }
}