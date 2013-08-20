using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("I", RomanNumber.IntToRoman(1));
            Assert.AreEqual("II", RomanNumber.IntToRoman(2));
            Assert.AreEqual("III", RomanNumber.IntToRoman(3));
            Assert.AreEqual("IV", RomanNumber.IntToRoman(4));
            Assert.AreEqual("V", RomanNumber.IntToRoman(5));
            Assert.AreEqual("VI", RomanNumber.IntToRoman(6));
            Assert.AreEqual("XLIX", RomanNumber.IntToRoman(49));
            Assert.AreEqual("DXLIII", RomanNumber.IntToRoman(543));
            Assert.AreEqual("MCMXC", RomanNumber.IntToRoman(1990));
            Assert.AreEqual("CMXIX", RomanNumber.IntToRoman(919));
            Assert.AreEqual("MMMD", RomanNumber.IntToRoman(3500));
        }
    }

    public class RomanNumber
    {
        private static int[] specialValues = new[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        private static string[] romanLetter = new[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        public static string IntToRoman(int numberToTranslate)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < specialValues.Length; i++)
            {
                while (numberToTranslate >= specialValues[i])
                {
                    numberToTranslate -= specialValues[i];
                    sb.Append(romanLetter[i]);
                }
            }

            return sb.ToString();
        }
    }
}