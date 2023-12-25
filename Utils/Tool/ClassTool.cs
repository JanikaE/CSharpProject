using System.Collections.Generic;
using System.Reflection;

namespace Utils.Tool
{
    public static class ClassTool
    {
        /// <summary>
        /// 获取类中的属性
        /// </summary>
        /// <returns>所有属性名称</returns>
        public static List<string> GetProperties<T>(T t)
        {
            List<string> ListStr = new();
            if (t == null)
            {
                return ListStr;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return ListStr;
            }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object? value = item.GetValue(t, null);

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ListStr.Add(name);
                }
                else
                {
                    GetProperties(value);
                }
            }
            return ListStr;
        }

        /// <summary>         
        ///  获取类中的字段
        /// </summary>
        /// <returns>所有字段名称</returns>
        public static List<string> GetFields<T>(T t)
        {
            List<string> ListStr = new();
            if (t == null)
            {
                return ListStr;
            }
            FieldInfo[] fields = t.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length <= 0)
            {
                return ListStr;
            }
            foreach (FieldInfo item in fields)
            {
                string name = item.Name;
                object? value = item.GetValue(t);

                if (item.FieldType.IsValueType || item.FieldType.Name.StartsWith("String"))
                {
                    ListStr.Add(name);
                }
                else
                {
                    GetFields(value);
                }
            }
            return ListStr;
        }
    }
}
