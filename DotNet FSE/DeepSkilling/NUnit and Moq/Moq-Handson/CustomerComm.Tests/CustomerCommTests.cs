using Moq;
using NUnit.Framework;
using CustomerCommLib;

namespace CustomerComm.Tests
{
    /// <summary>
    /// NUnit test class for CustomerComm.
    ///
    /// KEY CONCEPTS:
    ///  • Mock<T>        — creates a fake object that implements T
    ///  • Setup()        — configures what the mock should return
    ///  • It.IsAny<T>()  — wildcard matcher: accept any value of type T
    ///  • Verify()       — asserts the mock method was actually called
    ///
    /// [TestFixture] — marks this class as an NUnit test container
    /// </summary>
    [TestFixture]
    public class CustomerCommTests
    {
        // The mock replaces the real MailSender so no SMTP server is needed.
        private Mock<IMailSender> _mockMailSender;

        // The object we are actually testing.
        private CustomerCommLib.CustomerComm _customerComm;

        /// <summary>
        /// [OneTimeSetUp] runs ONCE before ALL tests in the class.
        /// Use it for expensive one-time setup (e.g. database connections).
        /// Here we just log that the fixture is starting.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestContext.WriteLine("=== CustomerCommTests fixture starting ===");
        }

        /// <summary>
        /// [SetUp] runs before EACH test.
        /// We create fresh mock and CUT instances so tests never share state.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // 1. Create the mock object for IMailSender.
            _mockMailSender = new Mock<IMailSender>();

            // 2. Configure the mock:
            //    • It.IsAny<string>() matches ANY string argument.
            //    • Returns(true)      always returns true.
            //    This means: when SendMail is called with any two strings → return true.
            _mockMailSender
                .Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            // 3. Inject the mock via constructor injection (Dependency Injection).
            _customerComm = new CustomerCommLib.CustomerComm(_mockMailSender.Object);
        }

        /// <summary>
        /// [TearDown] runs after EACH test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _mockMailSender = null;
            _customerComm   = null;
        }

        // ─── Test Cases ───────────────────────────────────────────────────────

        /// <summary>
        /// [TestCase] allows parameterised tests.
        /// Here we pass the expected return value as a parameter.
        /// </summary>
        [TestCase(true)]
        public void SendMailToCustomer_WhenCalled_ReturnsTrue(bool expected)
        {
            // Act — invoke the method under test
            bool result = _customerComm.SendMailToCustomer();

            // Assert — confirm the return value
            Assert.That(result, Is.EqualTo(expected),
                "SendMailToCustomer() should return true when mail is sent successfully.");
        }

        [Test]
        public void SendMailToCustomer_WhenCalled_InvokesMailSenderExactlyOnce()
        {
            // Act
            _customerComm.SendMailToCustomer();

            // Verify the mock's SendMail was called exactly once with any two strings.
            _mockMailSender.Verify(
                m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once,
                "SendMail should be called exactly once per transaction."
            );
        }

        [Test]
        public void SendMailToCustomer_MockNeverThrows_NoRealEmailSent()
        {
            // This test proves that even though MailSender.SendMail()
            // would throw a SmtpException in a real environment,
            // the mock silently returns true — isolation in action.
            Assert.DoesNotThrow(() => _customerComm.SendMailToCustomer(),
                "No exception should be thrown — mock isolates the SMTP dependency.");
        }

        [Test]
        public void SendMailToCustomer_MockSetupWithSpecificArgs_ReturnsTrue()
        {
            // Override the generic setup with a specific-argument setup
            var specificMock = new Mock<IMailSender>();
            specificMock
                .Setup(m => m.SendMail("cust123@abc.com", "Some Message"))
                .Returns(true);

            var comm = new CustomerCommLib.CustomerComm(specificMock.Object);
            bool result = comm.SendMailToCustomer();

            Assert.That(result, Is.True);
        }
    }
}
