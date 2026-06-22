using NUnit.Framework;
using CalcLibrary;

namespace CalcLibrary.Tests
{
    /// <summary>
    /// [TestFixture] marks this class as a test class for NUnit.
    /// All test methods must reside within a [TestFixture] class.
    /// </summary>
    [TestFixture]
    public class CalculatorTests
    {
        // The instance of the class under test
        private Calculator _calculator;

        /// <summary>
        /// [SetUp] runs BEFORE every single test method.
        /// Used to initialize shared resources — here we create a fresh
        /// Calculator instance so each test starts clean.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
            TestContext.WriteLine("SetUp: Calculator instance created.");
        }

        /// <summary>
        /// [TearDown] runs AFTER every single test method.
        /// Used to release or clean up resources after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _calculator = null;
            TestContext.WriteLine("TearDown: Calculator instance disposed.");
        }

        // ─── Addition Tests ───────────────────────────────────────────────────

        /// <summary>
        /// [TestCase] lets us pass multiple sets of inputs + expected result
        /// in one parameterised test, avoiding code duplication.
        ///
        /// Format: [TestCase(input1, input2, expectedResult)]
        /// </summary>
        [TestCase(2, 3, 5)]
        [TestCase(0, 0, 0)]
        [TestCase(-1, -1, -2)]
        [TestCase(-5, 10, 5)]
        [TestCase(100, 200, 300)]
        public void Add_TwoNumbers_ReturnsCorrectSum(int a, int b, int expected)
        {
            // Act
            int result = _calculator.Add(a, b);

            // Assert — Assert.That is the modern NUnit assertion style.
            // Is.EqualTo checks value equality.
            Assert.That(result, Is.EqualTo(expected),
                $"Expected Add({a}, {b}) to return {expected}, but got {result}.");
        }

        // ─── Subtraction Tests ────────────────────────────────────────────────

        [TestCase(10, 3, 7)]
        [TestCase(0, 0, 0)]
        [TestCase(-5, -3, -2)]
        [TestCase(100, 50, 50)]
        public void Subtract_TwoNumbers_ReturnsCorrectDifference(int a, int b, int expected)
        {
            int result = _calculator.Subtract(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        // ─── Multiplication Tests ─────────────────────────────────────────────

        [TestCase(3, 4, 12)]
        [TestCase(0, 100, 0)]
        [TestCase(-2, 5, -10)]
        [TestCase(-3, -3, 9)]
        public void Multiply_TwoNumbers_ReturnsCorrectProduct(int a, int b, int expected)
        {
            int result = _calculator.Multiply(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        // ─── Division Tests ───────────────────────────────────────────────────

        [TestCase(10.0, 2.0, 5.0)]
        [TestCase(9.0, 3.0, 3.0)]
        [TestCase(7.0, 2.0, 3.5)]
        public void Divide_TwoNumbers_ReturnsCorrectQuotient(double a, double b, double expected)
        {
            double result = _calculator.Divide(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        /// <summary>
        /// [Ignore] skips a test and shows a reason in the test runner output.
        /// Useful when a feature is not yet implemented or temporarily broken.
        /// </summary>
        [Test]
        [Ignore("Divide by zero handling not yet finalised — revisit in Sprint 3")]
        public void Divide_ByZero_ThrowsDivideByZeroException_IGNORED()
        {
            Assert.Throws<System.DivideByZeroException>(() => _calculator.Divide(10, 0));
        }

        // This version IS active and will run:
        [Test]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<System.DivideByZeroException>(() => _calculator.Divide(10, 0));
        }
    }
}
