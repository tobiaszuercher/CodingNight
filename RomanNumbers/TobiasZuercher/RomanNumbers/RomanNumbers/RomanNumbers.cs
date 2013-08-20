using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RomanNumbers
{
    [TestClass]
    public class RomanNumbersTests
    {
        [TestMethod]
        public void TestBla()
        {
            Assert.AreEqual("MCMXC", RomanNumber.IntToRoman(1990));
            Assert.AreEqual("CMXIX", RomanNumber.IntToRoman(919));
        }
    }

    public class RomanNumber
    {
        private static int[] BaseValues      = new[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        private static string[] Roman = new[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        public static string IntToRoman(int num)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < BaseValues.Length; i++)
            {
                while (num >= BaseValues[i])
                {
                    num -= BaseValues[i];
                    sb.Append(Roman[i]);
                }
            }

            return sb.ToString();
        }
    }
}