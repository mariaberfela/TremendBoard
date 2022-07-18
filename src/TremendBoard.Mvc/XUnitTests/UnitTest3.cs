using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Mvc;

namespace XUnitTests
{
    public class UnitTest3
    {
        [Fact]
        public void Multiplication_ReturnsMultiplicationOfAAndB_EqualToATimesB()
        {
            int num1 = 2;
            int num2 = 3;

            Multiplication mult = new Multiplication(num1, num2);

            int actual = mult.GetMult();
            Assert.Equal(num1*num2, actual);
        }

        [Fact]
        public void Multiplication_ReturnsIfAEqualToB_ReturnsTrueOrFalse()
        {
            int num1 = 2;
            int num2 = 2;

            Multiplication mult = new Multiplication(num1, num2);

            bool equal = true;

            bool actual = mult.AreEqual();
            Assert.Equal(equal, actual);
        }
    }
}
