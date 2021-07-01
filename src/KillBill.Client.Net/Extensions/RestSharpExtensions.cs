using System.Linq;
using RestSharp;

namespace KillBill.Client.Net.Extensions
{
    public static class RestSharpExtensions
    {
        public static string GetValue(this IRestResponse response, string key)
        {
            var hdr = response.Headers.FirstOrDefault(x => x.Name.ToString() == key);
            return hdr == null ? null : hdr.Value.ToString();
        }

        public static int ToInt(this string str, int value = 0)
        {
            int.TryParse(str, out value);
            return value;
        }
    }
}