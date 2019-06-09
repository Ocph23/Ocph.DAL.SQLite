using System;
namespace vJine.Core.ORM
{
    public class TypeConverter
    {
        public class sbyte_ /*: IConverter<sbyte>*/
        { public static sbyte CONV_Q(object V) { return (sbyte)V; } }
        public class ushort_ /*: IConverter<ushort>*/
        { public static ushort CONV_Q(object V) { return (ushort)V; } }
        public class uint_ /*: IConverter<uint>*/ { public static uint CONV_Q(object V) { return (uint)V; } }
        public class ulong_ /*: IConverter<ulong>*/ { public static ulong CONV_Q(object V) { return (ulong)V; } }
        public class bool_string /*: IConverter<bool>*/ { public static object CONV_I(object V) { if ((bool)V == true) { return "1"; } else { return "0"; } } public static bool CONV_Q(object V) { if ((string)V == "1") { return true; } else { return false; } } }
        public class sbyte_short /*: IConverter<sbyte>*/ { public static object CONV_I(object V) { return Convert.ToInt16(V); } public static sbyte CONV_Q(object V) { return Convert.ToSByte(V); } }
        public class byte_short /*: IConverter<byte>*/ { public static object CONV_I(object V) { return Convert.ToInt16(V); } public static byte CONV_Q(object V) { return Convert.ToByte(V); } }
        public class short_int /*: IConverter<short>*/ { public static object CONV_I(object V) { return Convert.ToInt32(V); } public static short CONV_Q(object V) { return Convert.ToInt16(V); } }
        public class ushort_int /*: IConverter<ushort>*/ { public static object CONV_I(object V, string Name) { return Convert.ToInt32(V); } public static ushort CONV_Q(object V, string Name) { return Convert.ToUInt16(V); } }
        public class int_long /*: IConverter<int>*/ { public static object CONV_I(object V) { return Convert.ToInt64(V); } public static int CONV_Q(object V) { return Convert.ToInt32(V); } }
        public class uint_long /*: IConverter<uint>*/ { public static object CONV_I(object V) { return Convert.ToInt64(V); } public static uint CONV_Q(object V) { return Convert.ToUInt32(V); } }
        public class long_decimal /*: IConverter<long>*/
        {
            public static object CONV_I(object V) { return decimal.Parse(V.ToString()); }
            public static long CONV_Q(object V) { return long.Parse(V.ToString()); }
        }
        public class ulong_float /*: IConverter<ulong>*/ { public static object CONV_I(object V) { return Convert.ToSingle(V); } public static ulong CONV_Q(object V) { return Convert.ToUInt64(V); } }
        public class ulong_decimal /*: IConverter<ulong>*/ { public static object CONV_I(object V) { return decimal.Parse(V.ToString()); } public static ulong CONV_Q(object V) { return ulong.Parse(V.ToString()); } }
        public class float_double /*: IConverter<float>*/ { public static object CONV_I(object V) { return Convert.ToDouble(V); } public static float CONV_Q(object V) { return Convert.ToSingle(V); } }
        public class char_string /*: IConverter<char>*/
        {
            public static object CONV_I(object V)
            {
                if (V is Char) { return V; }
                else if (V is string)
                {
                    string chars = (string)V;
                    if (chars == "") { return '\0'; } else { return chars[0]; }
                }
                else { throw new OrmException("Fail To Convert String[{0}] To Char", V); }
            }
            public static char CONV_Q(object V) { return ((string)V)[0]; }
        }
        public class DateTime_ /*: IConverter<DateTime>*/
        {
            public static object CONV_I(object V) { return V; }
            public static DateTime CONV_Q(object V) { return (DateTime)V; }
        }



        public static string ConvertType(Type type)
        {

            if(Nullable.GetUnderlyingType(type) != null && type.GenericTypeArguments[0].FullName=="System.Int32")
            {
                return "INTEGER";
            }
            if (type == typeof(String))
                return "TEXT";

            if (type == typeof(Int32))
                return "INTEGER";

            if (type == typeof(DateTime))
                return "datetime default current_timestamp";

            if (type.IsEnum)
                return "TEXT";

            return null;
        }

    }
}