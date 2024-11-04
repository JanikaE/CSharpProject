using System;

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
    }
}
