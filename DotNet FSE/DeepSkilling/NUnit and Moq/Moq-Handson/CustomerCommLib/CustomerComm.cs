namespace CustomerCommLib
{
    /// <summary>
    /// The class under test (CUT).
    ///
    /// DEPENDENCY INJECTION (Constructor Injection):
    /// Instead of creating an IMailSender internally, CustomerComm
    /// RECEIVES it from outside via the constructor.  This means:
    ///   • In production  → pass a real MailSender
    ///   • In unit tests  → pass a Mock<IMailSender> (no SMTP needed)
    ///
    /// This pattern is called "Inversion of Control" (IoC) and is the
    /// foundation of loosely-coupled, testable code.
    /// </summary>
    public class CustomerComm
    {
        private readonly IMailSender _mailSender;

        // Constructor Injection — the dependency is injected here.
        public CustomerComm(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        /// <summary>
        /// Sends a notification email to a customer.
        /// The actual SMTP work is delegated to _mailSender — in tests
        /// this will be a mock object, so no real email is ever sent.
        /// </summary>
        public bool SendMailToCustomer()
        {
            // Real business logic would build the address and message.
            string customerEmail = "cust123@abc.com";
            string messageBody   = "Some Message";

            return _mailSender.SendMail(customerEmail, messageBody);
        }
    }
}
