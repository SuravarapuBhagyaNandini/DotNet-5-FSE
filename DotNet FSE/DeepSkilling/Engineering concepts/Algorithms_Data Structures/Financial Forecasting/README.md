##  Step 1 – Understanding Recursion

**Recursion** is a programming technique where a function solves a problem by
calling itself with a *simpler* version of the same problem, until it reaches a
**base case** that can be answered directly.

### Why recursion fits financial forecasting

Compound growth is naturally recursive:

```
FV(0) = Principal                      ← base case
FV(n) = FV(n-1) × (1 + rate)          ← recursive step
```

Each period's value depends only on the previous period's value—a perfect
match for recursive thinking.

### Key terms

| Term        | Meaning                                              |
|-------------|------------------------------------------------------|
| Base case   | Condition that stops recursion (periods == 0)        |
| Recursive case | The function calling itself with a smaller input  |
| Call stack  | Memory used to track each pending recursive call     |

---

##  Step 2 – Setup: Recursive Future Value Method

```python
def future_value_recursive(principal: float, rate: float, periods: int) -> float:
    if periods == 0:          # Base case
        return principal
    return future_value_recursive(principal, rate, periods - 1) * (1 + rate)
```

**How it works (unwinding for P=10000, r=0.08, n=3):**

```
fv(10000, 0.08, 3)
  └─ fv(10000, 0.08, 2) × 1.08
       └─ fv(10000, 0.08, 1) × 1.08
            └─ fv(10000, 0.08, 0) × 1.08
                 └─ 10000              ← base case
            = 10800.00
       = 11664.00
  = 12597.12
```

---

##  Step 3 – Implementation: Forecast from Historical Data

```python
def average_growth_rate(past_values):
    rates = [(past_values[i] - past_values[i-1]) / past_values[i-1]
             for i in range(1, len(past_values))]
    return sum(rates) / len(rates)

def predict_future_values(past_values, periods_ahead, custom_rate=None):
    rate    = custom_rate or average_growth_rate(past_values)
    current = past_values[-1]
    return [future_value_recursive(current, rate, p)
            for p in range(1, periods_ahead + 1)]
```

The tool:
1. Computes period-over-period growth rates from the historical series.
2. Averages them to get a representative rate.
3. Applies recursive compounding from the last known value.

---

## Step 4 – Analysis & Optimisation

### 4a. Time Complexity of Plain Recursion

| Metric           | Value  | Explanation                                  |
|------------------|--------|----------------------------------------------|
| Time complexity  | **O(n)** | One multiply per recursive call             |
| Space complexity | **O(n)** | Call stack grows one frame per period        |

**Problem:** Python's default recursion limit is ~1000.  
For n > 1000 (e.g. daily predictions over 3 years) the program raises  
`RecursionError: maximum recursion depth exceeded`.

---

### 4b. Optimisation Strategies

#### Strategy 1 – Memoisation (top-down DP) · `@functools.lru_cache`

```python
@functools.lru_cache(maxsize=None)
def future_value_memoised(principal, rate, periods):
    if periods == 0:
        return principal
    return future_value_memoised(principal, rate, periods - 1) * (1 + rate)
```

- Caches every `(principal, rate, periods)` result.  
- Repeated queries return in **O(1)**.  
- Useful when many forecasts share sub-problems.

#### Strategy 2 – Iterative (bottom-up DP)

```python
def future_value_iterative(principal, rate, periods):
    value = principal
    for _ in range(periods):
        value *= (1 + rate)
    return value
```

- **O(n) time, O(1) space** – no call stack.  
- Safe for arbitrarily large `periods`.

#### Strategy 3 – Closed-form formula (best for single lookups)

```python
def future_value_formula(principal, rate, periods):
    return principal * (1 + rate) ** periods
```

- **O(1) time, O(1) space**.  
- Python's `**` uses fast exponentiation internally.

---

### Complexity Comparison Table

| Algorithm       | Time     | Space  | Notes                                      |
|-----------------|----------|--------|--------------------------------------------|
| Plain Recursive | O(n)     | O(n)   | Simplest; RecursionError risk for large n  |
| Memoised        | O(n)*    | O(n)   | *O(1) for repeated identical queries       |
| Iterative       | O(n)     | O(1)   | No stack overhead; preferred in production |
| Closed-form     | O(1)     | O(1)   | Best for isolated single calculations      |

---

## 🚀 How to Run

```bash
# Run the demo
python financial_forecasting.py

# Run unit tests (requires pytest)
python -m pytest test_financial_forecasting.py -v
```

---

## ✅ Expected Output

```
============================================================
   FINANCIAL FORECASTING TOOL – Recursive Approach
============================================================

[Example 1] Simple Future Value
  Principal : ₹10,000.00
  Rate      : 8.0% per period
  Periods   : 5

  Recursive  result : ₹14,693.28
  Memoised   result : ₹14,693.28
  Iterative  result : ₹14,693.28
  Formula    result : ₹14,693.28

[Example 2] Forecast from Historical Data
  Historical values : [50000, 54000, 58320, 62985, 68024]
  Derived avg rate  : 8.00%

  Forecasted values (5 periods ahead):
    Period +1: ₹73,465.80
    Period +2: ₹79,342.93
    Period +3: ₹85,690.23
    Period +4: ₹92,545.29
    Period +5: ₹99,948.75

[Example 3] Year-by-Year Compound Interest Schedule
  Principal ₹25,000.00 at 10% for 10 years

  Year      Future Value
  ------ ---------------
  1      ₹     27,500.00
  2      ₹     30,250.00
  3      ₹     33,275.00
  4      ₹     36,602.50
  5      ₹     40,262.75
  6      ₹     44,289.03
  7      ₹     48,717.93
  8      ₹     53,589.72
  9      ₹     58,948.69
  10     ₹     64,843.56

============================================================
  COMPLEXITY ANALYSIS
============================================================

  Algorithm        Time       Space   Notes
  ─────────────── ────────   ─────── ──────────────────────────────
  Plain Recursive  O(n)       O(n)    Stack grows with n; risk of
                                      RecursionError for large n
  Memoised         O(n)*      O(n)    * O(1) for repeated queries
  Iterative        O(n)       O(1)    No stack overhead
  Closed-form      O(1)       O(1)    Best for single calculations

============================================================
```

---

## ✅ Test Results (14 / 14 passing)

```
Running tests...
  PASS  zero_periods_returns_principal
  PASS  one_period
  PASS  five_periods
  PASS  zero_rate
  PASS  negative_periods_raises
  PASS  memoised_matches_recursive
  PASS  iterative_matches_recursive
  PASS  formula_matches_recursive
  PASS  constant_growth_rate
  PASS  two_values
  PASS  insufficient_data_raises
  PASS  correct_prediction_count
  PASS  predictions_increase
  PASS  zero_periods_empty

14 passed, 0 failed
```

---

##  Key Concepts Covered

| Concept                     | Where used                                    |
|-----------------------------|-----------------------------------------------|
| Recursion + base case       | `future_value_recursive`                      |
| Deriving rate from data     | `average_growth_rate`                         |
| Multi-period forecasting    | `predict_future_values`                       |
| Memoisation (lru_cache)     | `future_value_memoised`                       |
| Iterative DP (O(1) space)   | `future_value_iterative`                      |
| Closed-form O(1)            | `future_value_formula`                        |
| Time & space complexity     | Step 4 analysis                               |
