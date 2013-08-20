using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void One_equals_I()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("I", romanConverter.Convert(1));
		}

		[TestMethod]
		public void ten_equals_X()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("X", romanConverter.Convert(10));
		}

		[TestMethod]
		public void five_equals_V()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("V", romanConverter.Convert(5));
		}

		[TestMethod]
		public void two_equals_II()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("II", romanConverter.Convert(2));
		}

		[TestMethod]
		public void tree_equals_III()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("III", romanConverter.Convert(3));
		}

		[TestMethod]
		public void four_equals_IV()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("IV", romanConverter.Convert(4));
		}

		[TestMethod]
		public void six_equals_VI()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("VI", romanConverter.Convert(6));
		}

		[TestMethod]
		public void seven_to_10_isOK()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("VII", romanConverter.Convert(7));
			Assert.AreEqual("VIII", romanConverter.Convert(8));
			Assert.AreEqual("IX", romanConverter.Convert(9));
			Assert.AreEqual("X", romanConverter.Convert(10));
		}

		[TestMethod]
		public void fourteen_equals_XIV()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("XIV", romanConverter.Convert(14));
		}

		[TestMethod]
		public void sixteen_equals_XVI()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("XVI", romanConverter.Convert(16));
		}

		[TestMethod]
		public void UpTo20_isOk()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("XVII", romanConverter.Convert(17));
			Assert.AreEqual("XVIII", romanConverter.Convert(18));
			Assert.AreEqual("XIX", romanConverter.Convert(19));
			Assert.AreEqual("XX", romanConverter.Convert(20));
		}

		[TestMethod]
		public void Fifty_isL()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("L", romanConverter.Convert(50));
		}

		[TestMethod]
		public void Fourty_is_XL()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("XL", romanConverter.Convert(40));
		}

		[TestMethod]
		public void FourtyOne_is_XLI()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("XLI", romanConverter.Convert(41));
		}

		[TestMethod]
		public void FourtyNine_is_XLIX()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("XLIX", romanConverter.Convert(49));
		}

		[TestMethod]
		public void Hundered_is_C()
		{
			var romanConverter = new IntToRomanNumberConverter();
			Assert.AreEqual("C", romanConverter.Convert(100));
		}
	}

	public class IntToRomanNumberConverter
	{
		private string ConvertInternal(int i)
		{
			if (i == 0)
			{
				return string.Empty;
			}

			switch (i)
			{
				case 1:
					return "I";
				case 5:
					return "V";
				case 10:
					return "X";
				case 50:
					return "L";
				case 100:
					return "C";
				default:
					if (i >= 40)
					{
						return this.ConvertInternal(50 - i) + "L";
					}
					if (i > 10)
					{
						return "X" + this.ConvertInternal(i - 10);
					}
					if ((i % 5) < 4)
					{
						return this.ConvertInternal(i - 1) + "I";
					}
					else
					{
						return "I" + this.ConvertInternal(i + 1);
					}
			}
		}

		public string Convert(int i)
		{
			return this.ConvertInternal((i / 10) * 10) + this.ConvertInternal(i % 10);
		}
	}
}
