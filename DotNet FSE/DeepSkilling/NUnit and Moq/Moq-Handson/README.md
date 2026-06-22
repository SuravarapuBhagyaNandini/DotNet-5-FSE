# Assignment 2 — Mocking with Moq: Mail Sender

## Overview
This assignment demonstrates how to use **Moq** to mock an external email dependency (`IMailSender`) so that unit tests can run without a real SMTP server. It covers **Dependency Injection (DI)**, interface-based design, and the full Moq workflow.

---

## Project Structure

```
Assignment2_Moq_MailSender/
├── CustomerComm.sln
├── CustomerCommLib/
│   ├── CustomerCommLib.csproj
│   ├── IMailSender.cs      ← Interface (the contract)
│   ├── MailSender.cs       ← Real SMTP implementation (NOT testable directly)
│   └── CustomerComm.cs     ← Class Under Test — uses Constructor Injection
└── CustomerComm.Tests/
    ├── CustomerComm.Tests.csproj
    └── CustomerCommTests.cs ← NUnit + Moq test cases
```

---

## Key Concepts

### Why Mocking?
`MailSender.SendMail()` connects to `smtp.gmail.com`. In a unit test we:
- Don't want to send real emails
- Don't want tests to fail due to network issues
- Want fast, deterministic tests

**Solution:** Replace `MailSender` with a **mock object** using Moq.

### Test Doubles Comparison

| Type | Description | Example |
|---|---|---|
| **Mock** | Verifiable fake — can assert calls were made | `Mock<IMailSender>` with `.Verify()` |
| **Stub** | Returns preset values, no verification | `.Setup(...).Returns(true)` only |
| **Fake** | Simplified working implementation | In-memory email list instead of SMTP |

### Dependency Injection (Constructor Injection)
```csharp
// Production: inject the real sender
var comm = new CustomerComm(new MailSender());

// Unit test: inject a mock
var comm = new CustomerComm(mockMailSender.Object);
```

---

## NuGet Packages Required

```
NUnit                   3.13.3
NUnit3TestAdapter       4.4.2
Microsoft.NET.Test.Sdk  17.5.0
Moq                     4.18.4
```

---

## Moq Cheat Sheet Used in This Assignment

```csharp
// 1. Create mock
var mock = new Mock<IMailSender>();

// 2. Setup — wildcard argument match
mock.Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()))
    .Returns(true);

// 3. Pass mock.Object as the dependency
var sut = new CustomerComm(mock.Object);

// 4. Run the method under test
bool result = sut.SendMailToCustomer();

// 5. Assert return value
Assert.That(result, Is.True);

// 6. Verify the mock was actually called
mock.Verify(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
```

---

## How to Run

```bash
cd Assignment2_Moq_MailSender
dotnet test
```

---

## Expected Output

```
Test Run Summary
----------------
Total tests  : 4
  Passed     : 4
  Failed     : 0

Test Results:
✅ SendMailToCustomer_WhenCalled_ReturnsTrue(True)
✅ SendMailToCustomer_WhenCalled_InvokesMailSenderExactlyOnce
✅ SendMailToCustomer_MockNeverThrows_NoRealEmailSent
✅ SendMailToCustomer_MockSetupWithSpecificArgs_ReturnsTrue
```

No real emails are sent. No SMTP connection is made. Tests run in milliseconds.

---

## NUnit Attributes Used

| Attribute | Purpose |
|---|---|
| `[TestFixture]` | Marks class as NUnit test container |
| `[OneTimeSetUp]` | Runs once before all tests in the class |
| `[SetUp]` | Runs before each test |
| `[TearDown]` | Runs after each test |
| `[Test]` | Single test method |
| `[TestCase(true)]` | Parameterised test with expected value |
