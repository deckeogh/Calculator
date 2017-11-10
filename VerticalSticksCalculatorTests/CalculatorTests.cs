using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace VerticalSticksCalculator.Tests
{
	[TestClass()]
	public class CalculatorTests
	{
		[TestMethod()]
		public void RayShotsLengthTest_Incremental()
		{
			//arrange
			var input = new[] { 1, 2, 3, 4 };
			const int expected = 10;

			//act
			var actual = Calculator.RayShotsLength(input.ToList());

			//assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void RayShotsLengthTest_Decremental()
		{
			//arrange
			var input = new[] { 4,3,2,1 };
			const int expected = 4;

			//act
			var actual = Calculator.RayShotsLength(input.ToList());

			//assert
			Assert.AreEqual(expected, actual);
		}
	}
}