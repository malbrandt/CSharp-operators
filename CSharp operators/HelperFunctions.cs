using System;

namespace CSharp_operators
{
    public static class HelperFunctions
    {

        public static string ToBinaryString(this int input)
        {
            return Convert.ToString(input, 2).PadLeft(32, '0');
            //var prefix = (input).ToString("d8").Length - (Math.Abs((input))).ToString("d8").Length;
            //var binaryNum = "00000000000000000000000000000000".Substring(0, 32 - Convert.ToString(Math.Abs(input), 2).Length) + Convert.ToString(Math.Abs(input), 2);
            //return "1".Substring(0, prefix) + binaryNum.Substring(prefix, 32 - prefix);
        }
    }
}
