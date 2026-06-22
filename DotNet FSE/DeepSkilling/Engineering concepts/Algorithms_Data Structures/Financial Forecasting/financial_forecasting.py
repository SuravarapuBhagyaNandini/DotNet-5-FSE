"""
Exercise 7: Financial Forecasting Tool
========================================
Uses recursive algorithms to predict future values based on past growth rates.
"""

import functools


# ─────────────────────────────────────────────────────────────────
# STEP 1 – What is Recursion?
# ─────────────────────────────────────────────────────────────────
# Recursion is a technique where a function calls itself with a
# simpler sub-problem until it reaches a "base case" and returns
# directly.  Each call waits for the result from the next call,
# building the final answer on the way back up the call stack.
#
# For financial forecasting it maps naturally:
#   FV(n) = FV(n-1) * (1 + rate)
# "The value after n periods = the value after n-1 periods grown by rate"


# ─────────────────────────────────────────────────────────────────
# STEP 2 – Basic Recursive Future Value
# ─────────────────────────────────────────────────────────────────

def future_value_recursive(principal: float, rate: float, periods: int) -> float:
    """
    Calculate future value using plain recursion.

    Formula:  FV(n) = P * (1 + r)^n
    Recursive definition:
        base case  : FV(0) = principal
        recursive  : FV(n) = FV(n-1) * (1 + rate)

    Args:
        principal : starting amount (P)
        rate      : growth rate per period as a decimal (e.g. 0.05 = 5 %)
        periods   : number of periods (n)

    Returns:
        Predicted future value after `periods` growth cycles.

    Time Complexity : O(n)  – one multiplication per period
    Space Complexity: O(n)  – call-stack depth equals n
    """
    if periods < 0:
        raise ValueError("periods must be >= 0")
    if periods == 0:          # Base case
        return principal
    # Recursive case
    return future_value_recursive(principal, rate, periods - 1) * (1 + rate)


# ─────────────────────────────────────────────────────────────────
# STEP 3 – Predicting Future Values from Past Growth Rates
# ─────────────────────────────────────────────────────────────────

def average_growth_rate(past_values: list[float]) -> float:
    """
    Derive the average period-over-period growth rate from historical data.

    Args:
        past_values: chronologically ordered list of historical values.

    Returns:
        Average growth rate as a decimal.

    Raises:
        ValueError: if fewer than 2 data points are supplied.
    """
    if len(past_values) < 2:
        raise ValueError("Need at least 2 historical values to compute a growth rate.")

    rates = [
        (past_values[i] - past_values[i - 1]) / past_values[i - 1]
        for i in range(1, len(past_values))
    ]
    return sum(rates) / len(rates)


def predict_future_values(
    past_values: list[float],
    periods_ahead: int,
    custom_rate: float | None = None,
) -> list[float]:
    """
    Predict future values recursively, using either an explicit growth rate
    or one derived automatically from historical data.

    Args:
        past_values    : list of known historical values (at least 2).
        periods_ahead  : how many future periods to forecast.
        custom_rate    : override growth rate; if None the average is used.

    Returns:
        List of `periods_ahead` predicted values.
    """
    rate = custom_rate if custom_rate is not None else average_growth_rate(past_values)
    current = past_values[-1]   # most recent known value

    predictions: list[float] = []
    for period in range(1, periods_ahead + 1):
        predictions.append(future_value_recursive(current, rate, period))
    return predictions


# ─────────────────────────────────────────────────────────────────
# STEP 4a – Optimisation: Memoisation (top-down DP)
# ─────────────────────────────────────────────────────────────────

@functools.lru_cache(maxsize=None)
def future_value_memoised(principal: float, rate: float, periods: int) -> float:
    """
    Memoised recursive future value.

    Python's lru_cache stores results so each (principal, rate, periods)
    triple is computed only once.  Redundant recursive calls return from
    the cache in O(1).

    Time Complexity : O(n) first call; O(1) for cached calls
    Space Complexity: O(n) cache entries + O(n) call stack (first call)
    """
    if periods == 0:
        return principal
    return future_value_memoised(principal, rate, periods - 1) * (1 + rate)


# ─────────────────────────────────────────────────────────────────
# STEP 4b – Optimisation: Iterative (bottom-up DP)
# ─────────────────────────────────────────────────────────────────

def future_value_iterative(principal: float, rate: float, periods: int) -> float:
    """
    Iterative future value – eliminates call-stack overhead entirely.

    Time Complexity : O(n)
    Space Complexity: O(1)  ← best possible for this problem
    """
    value = principal
    for _ in range(periods):
        value *= (1 + rate)
    return value


# ─────────────────────────────────────────────────────────────────
# STEP 4c – Optimisation: Closed-form O(1)
# ─────────────────────────────────────────────────────────────────

def future_value_formula(principal: float, rate: float, periods: int) -> float:
    """
    Direct compound-interest formula: FV = P * (1 + r)^n

    Time Complexity : O(1)  (Python's built-in ** uses fast exponentiation)
    Space Complexity: O(1)
    """
    return principal * (1 + rate) ** periods


# ─────────────────────────────────────────────────────────────────
# Demo / driver
# ─────────────────────────────────────────────────────────────────

def main() -> None:
    print("=" * 60)
    print("   FINANCIAL FORECASTING TOOL – Recursive Approach")
    print("=" * 60)

    # --- Example 1: simple future value ---
    principal = 10_000.0
    rate      = 0.08        # 8 % annual growth
    periods   = 5

    print("\n[Example 1] Simple Future Value")
    print(f"  Principal : ₹{principal:,.2f}")
    print(f"  Rate      : {rate * 100:.1f}% per period")
    print(f"  Periods   : {periods}")

    fv_rec  = future_value_recursive(principal, rate, periods)
    fv_memo = future_value_memoised(principal, rate, periods)
    fv_iter = future_value_iterative(principal, rate, periods)
    fv_form = future_value_formula(principal, rate, periods)

    print(f"\n  Recursive  result : ₹{fv_rec:,.2f}")
    print(f"  Memoised   result : ₹{fv_memo:,.2f}")
    print(f"  Iterative  result : ₹{fv_iter:,.2f}")
    print(f"  Formula    result : ₹{fv_form:,.2f}")

    # --- Example 2: predict from historical data ---
    historical = [50_000, 54_000, 58_320, 62_985, 68_024]
    periods_ahead = 5

    print("\n[Example 2] Forecast from Historical Data")
    print(f"  Historical values : {historical}")
    avg_rate = average_growth_rate(historical)
    print(f"  Derived avg rate  : {avg_rate * 100:.2f}%")

    forecasts = predict_future_values(historical, periods_ahead)
    print(f"\n  Forecasted values ({periods_ahead} periods ahead):")
    for i, val in enumerate(forecasts, start=1):
        print(f"    Period +{i}: ₹{val:,.2f}")

    # --- Example 3: compound interest schedule ---
    print("\n[Example 3] Year-by-Year Compound Interest Schedule")
    p, r = 25_000.0, 0.10
    print(f"  Principal ₹{p:,.2f} at {r*100:.0f}% for 10 years\n")
    print(f"  {'Year':<6} {'Future Value':>15}")
    print(f"  {'-'*6} {'-'*15}")
    for yr in range(1, 11):
        fv = future_value_recursive(p, r, yr)
        print(f"  {yr:<6} ₹{fv:>14,.2f}")

    # --- Complexity summary ---
    print("\n" + "=" * 60)
    print("  COMPLEXITY ANALYSIS")
    print("=" * 60)
    print("""
  Algorithm        Time       Space   Notes
  ─────────────── ────────   ─────── ──────────────────────────────
  Plain Recursive  O(n)       O(n)    Stack grows with n; risk of
                                      RecursionError for large n
  Memoised         O(n)*      O(n)    * O(1) for repeated queries
  Iterative        O(n)       O(1)    No stack overhead
  Closed-form      O(1)       O(1)    Best for single calculations
    """)
    print("=" * 60)


if __name__ == "__main__":
    main()
