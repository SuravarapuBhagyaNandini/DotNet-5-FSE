namespace CustomerCommLib
{
    /// <summary>
    /// Interface for sending emails.
    /// By programming to an interface rather than a concrete class,
    /// we can swap in a real SMTP sender in production and a mock
    /// sender in unit tests — this is the key to testable design.
    /// </summary>
    public interface IMailSender
    {
        bool SendMail(string toAddress, string message);
    }
}
