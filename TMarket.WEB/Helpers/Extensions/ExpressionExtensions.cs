namespace TMarket.WEB.Helpers.Extensions
{
    public static class ExpressionExtensions
    {
        public static bool StartsWithOrNull(this string name, string searchName) =>
            searchName != null ? name.StartsWith(searchName) : true;

        public static bool LessOrEmptyInput(this decimal price, decimal searchPrice) =>
            searchPrice > 0 ? price <= searchPrice : true;
    }
}
