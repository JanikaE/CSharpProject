using System;
using System.ComponentModel;
using System.Reflection;

namespace Utils.Extend
{
    public static class EnumExtend
    {
        public static TEnum? Next<TEnum>(this TEnum basic) where TEnum : struct, Enum
        {
            Array values = Enum.GetValues<TEnum>();
            if (values == null)
                return null;

            int i;
            for (i = 0; i < values.GetLength(0); i++)
            {
                if (values.GetValue(i)?.Equals(basic) == true)
                    break;
            }

            if (i + 1 < values.GetLength(0))
            {
                object? next = values.GetValue(i + 1);
                return next == null ? null : (TEnum)next;
            }
            else
            {
                return null;
            }
        }

        public static TEnum? Previous<TEnum>(this TEnum basic) where TEnum : struct, Enum
        {
            Array values = Enum.GetValues<TEnum>();
            if (values == null)
                return null;

            int i;
            for (i = values.GetLength(0) - 1; i >= 0; i--)
            {
                if (values.GetValue(i)?.Equals(basic) == true)
                    break;
            }

            if (i - 1 >= 0)
            {
                object? next = values.GetValue(i - 1);
                return next == null ? null : (TEnum)next;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从枚举中获取Description
        /// </summary>
        /// <param name="enumName">需要获取枚举描述的枚举</param>
        /// <returns>描述内容</returns>
        public static string? GetDescription(this Enum enumName)
        {
            if (enumName == null)
                return null;

            FieldInfo? fieldInfo = enumName.GetType().GetField(enumName.ToString());
            if (fieldInfo == null)
                return null;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string description;
            if (attributes != null && attributes.Length > 0)
                description = attributes[0].Description;
            else
                description = enumName.ToString();
            return description;
        }

        /// <summary>
        /// 根据属性描述获取枚举值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="description">属性说明</param>
        /// <returns>枚举值</returns>
        public static T GetEnum<T>(string description) where T : struct, IConvertible
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                return default;
            }
            if (!Enum.TryParse(description, out T temp))
            {
                temp = default;
            }

            T[] enums = (T[])Enum.GetValues(type);
            for (int i = 0; i < enums.Length; i++)
            {
                string? name = enums[i].ToString();
                if (name == null)
                    continue;

                FieldInfo? field = type.GetField(name);
                if (field == null)
                    continue;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                    continue;

                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                string edes = descriptionAttribute.Description;
                if (description == edes)
                {
                    temp = enums[i];
                    break;
                }
            }

            return temp;
        }

        #region 位运算

        /// <summary>
        /// 将指定的枚举类型，设置为true
        /// </summary>
        /// <param name="bitEnum">指定具体的类型</param>
        /// <param name="value">数据库存储的值</param>
        public static long SetTrue(this Enum bitEnum, long value)
        {
            return value |= Convert.ToInt64(bitEnum);
        }

        /// <summary>
        /// 将指定的枚举类型，设置为true
        /// </summary>
        /// <param name="bitEnum">指定具体的类型</param>
        /// <param name="value">数据库存储的值</param>
        public static int SetTrue(this Enum bitEnum, int value)
        {
            return value |= Convert.ToInt32(bitEnum);
        }

        /// <summary>
        /// 将指定的枚举类型，设置为false
        /// </summary>
        /// <param name="bitEnum">指定具体的类型</param>
        /// <param name="value">数据库存储的值</param>
        public static long SetFalse(this Enum bitEnum, long value)
        {
            value |= Convert.ToInt64(bitEnum);
            return value -= Convert.ToInt64(bitEnum);
        }

        /// <summary>
        /// 将指定的枚举类型，设置为false
        /// </summary>
        /// <param name="bitEnum">指定具体的类型</param>
        /// <param name="value">数据库存储的值</param>
        public static int SetFalse(this Enum bitEnum, int value)
        {
            value |= Convert.ToInt32(bitEnum);
            return value -= Convert.ToInt32(bitEnum);
        }

        /// <summary>
        /// 判断指定的枚举，true 还是 false
        /// </summary>
        /// <param name="bitEnum">指定具体的类型</param>
        /// <param name="value">数据库存储的值</param>
        /// <returns></returns>
        public static bool Check(this Enum bitEnum, long value)
        {
            return (value & Convert.ToInt64(bitEnum)) > 0;
        }

        /// <summary>
        /// 判断指定的枚举，true 还是 false
        /// </summary>
        /// <param name="bitEnum">指定具体的类型</param>
        /// <param name="value">数据库存储的值</param>
        /// <returns></returns>
        public static bool Check(this Enum bitEnum, int value)
        {
            return (value & Convert.ToInt32(bitEnum)) > 0;
        }

        #endregion
    }
}
