// <copyright file="MyCalculatorTest.cs">Copyright ©  2015</copyright>

using System;
using IntelliTest;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntelliTest
{
    [TestClass]
    [PexClass(typeof(MyCalculator))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MyCalculatorTest
    {
        [PexMethod]
        [PexAllowedException(typeof(DivideByZeroException))]
        [PexAllowedException(typeof(OverflowException))]
        public int Run(
            [PexAssumeUnderTest]MyCalculator target,
            int x,
            int y
        )
        {
            int result = target.Run(x, y);
            return result;
            // TODO: add assertions to method MyCalculatorTest.Run(MyCalculator, Int32, Int32)
        }
    }
}
