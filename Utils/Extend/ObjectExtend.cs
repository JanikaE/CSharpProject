using Newtonsoft.Json;

namespace Utils.Extend
{
    public static class ObjectExtend
    {
        public static object Clone(this object obj)
        {
            object newObj = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj));
            return newObj;
        }
    }
}
