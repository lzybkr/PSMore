
namespace PSMore.Formatting
{
    internal static class Utils
    {
        // From System.Web.Util.HashCodeCombiner
        internal static int CombineHashCodes(int h1, int h2)
        {
            return unchecked(((h1 << 5) + h1) ^ h2);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2), h3);
        }
    }
}
