# Assignment 1 — NUnit Unit Testing for a Calculator

## Overview
This assignment demonstrates writing **NUnit unit tests** for a simple `Calculator` class library using key NUnit attributes: `[TestFixture]`, `[SetUp]`, `[TearDown]`, `[Test]`, `[TestCase]`, and `[Ignore]`.

---

## Project Structure

```
Assignment1_NUnit_Calculator/
├── Calculator.sln
├── CalcLibrary/
│   ├── CalcLibrary.csproj
│   └── Calculator.cs          ← Class under test (CUT)
└── CalcLibrary.Tests/
    ├── CalcLibrary.Tests.csproj
    └── CalculatorTests.cs     ← All NUnit test cases
```

---

## Key Concepts Covered

| Concept | Description |
|---|---|
| **Unit Testing** | Testing the smallest piece of code in isolation |
| **Functional Testing** | Testing a complete feature/flow end-to-end |
| **[TestFixture]** | Marks a class as containing NUnit tests |
| **[SetUp]** | Runs **before every** test — used to initialize state |
| **[TearDown]** | Runs **after every** test — used to clean up resources |
| **[Test]** | Marks a single test method |
| **[TestCase]** | Parameterised testing — pass multiple input/output sets to one method |
| **[Ignore]** | Skips a test with a reason (shown in test output) |
| **Assert.That** | Modern NUnit fluent assertion API |

---

## NuGet Packages Required

Install via NuGet Package Manager or `dotnet add package`:

```
NUnit                   3.13.3
NUnit3TestAdapter       4.4.2
Microsoft.NET.Test.Sdk  17.5.0
```

---

## How to Run

### Option A — Visual Studio
1. Open `Calculator.sln`
2. Right-click `CalcLibrary.Tests` → **Run Tests**
3. View results in the **Test Explorer** panel

### Option B — CLI
```bash
cd Assignment1_NUnit_Calculator
dotnet test
```

---

## Expected Output

```
Test Run Summary
----------------
Total tests   : 18
  Passed      : 17
  Skipped     : 1   ← [Ignore] test is skipped with reason shown
  Failed      : 0

SetUp: Calculator instance created.
TearDown: Calculator instance disposed.
... (repeated for each test)
```

### Individual test results

| Test Method | Inputs | Expected | Status |
|---|---|---|---|
| Add_TwoNumbers | (2,3) | 5 | ✅ Pass |
| Add_TwoNumbers | (0,0) | 0 | ✅ Pass |
| Add_TwoNumbers | (-1,-1) | -2 | ✅ Pass |
| Add_TwoNumbers | (-5,10) | 5 | ✅ Pass |
| Add_TwoNumbers | (100,200) | 300 | ✅ Pass |
| Subtract_TwoNumbers | (10,3) | 7 | ✅ Pass |
| Multiply_TwoNumbers | (3,4) | 12 | ✅ Pass |
| Divide_TwoNumbers | (10.0,2.0) | 5.0 | ✅ Pass |
| Divide_ByZero | (10,0) | Exception | ✅ Pass |
| Divide_ByZero_IGNORED | — | — | ⏭ Skipped |

---

## Loose Coupling & Testable Design

The `Calculator` class has **no external dependencies** — it does not depend on a database, file system, or network. This makes it immediately testable without any mocking. This is the foundation of a **testable design**: units that depend only on their inputs and produce predictable outputs.
