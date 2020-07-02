using System.Text;
using Newtonsoft.Json;

namespace PropertyManager.Domain.Convertors
{
    public class ObjectToJson
    {
        public static byte[] ToByteArray(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
