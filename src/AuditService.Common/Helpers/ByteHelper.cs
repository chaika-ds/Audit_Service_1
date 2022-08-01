using System.Runtime.Serialization.Formatters.Binary;
using static System.Text.Encoding;
using static Newtonsoft.Json.JsonConvert;

namespace AuditService.Common.Helpers
{
    public class ByteHelper
    {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            return UTF8.GetBytes(SerializeObject(obj));
        }
    }
}
