"""
test_financial_forecasting.py
Unit tests for Exercise 7 – Financial Forecasting Tool
"""

import pytest
from financial_forecasting import (
    future_value_recursive,
    future_value_memoised,
    future_value_iterative,
    future_value_formula,
    average_growth_rate,
    predict_future_values,
)

TOLERANCE = 1e-6   # floating-point comparison threshold


# ── future_value_recursive ────────────────────────────────────────

class TestFutureValueRecursive:
    def test_zero_periods_returns_principal(self):
        assert future_value_recursive(10_000, 0.08, 0) == pytest.approx(10_000, rel=TOLERANCE)

    def test_one_period(self):
        assert future_value_recursive(10_000, 0.08, 1) == pytest.approx(10_800, rel=TOLERANCE)

    def test_five_periods(self):
        expected = 10_000 * (1.08 ** 5)
        assert future_value_recursive(10_000, 0.08, 5) == pytest.approx(expected, rel=TOLERANCE)

    def test_zero_rate_returns_principal(self):
        assert future_value_recursive(5_000, 0.0, 10) == pytest.approx(5_000, rel=TOLERANCE)

    def test_negative_periods_raises(self):
        with pytest.raises(ValueError):
            future_value_recursive(1_000, 0.05, -1)

    def test_large_principal(self):
        result = future_value_recursive(1_000_000, 0.05, 10)
        assert result == pytest.approx(1_628_894.63, rel=1e-4)


# ── future_value_memoised ────────────────────────────────────────

class TestFutureValueMemoised:
    def test_matches_recursive(self):
        for n in range(0, 8):
            assert future_value_memoised(1_000, 0.06, n) == pytest.approx(
                future_value_recursive(1_000, 0.06, n), rel=TOLERANCE
            )

    def test_zero_periods(self):
        assert future_value_memoised(500, 0.10, 0) == pytest.approx(500, rel=TOLERANCE)


# ── future_value_iterative ────────────────────────────────────────

class TestFutureValueIterative:
    def test_matches_recursive(self):
        for n in range(0, 10):
            assert future_value_iterative(2_000, 0.07, n) == pytest.approx(
                future_value_recursive(2_000, 0.07, n), rel=TOLERANCE
            )

    def test_zero_rate(self):
        assert future_value_iterative(3_000, 0.0, 5) == pytest.approx(3_000, rel=TOLERANCE)


# ── future_value_formula ─────────────────────────────────────────

class TestFutureValueFormula:
    def test_matches_recursive(self):
        for n in range(0, 10):
            assert future_value_formula(1_500, 0.09, n) == pytest.approx(
                future_value_recursive(1_500, 0.09, n), rel=TOLERANCE
            )


# ── average_growth_rate ──────────────────────────────────────────

class TestAverageGrowthRate:
    def test_constant_growth(self):
        # 10 % growth each period → avg should be exactly 0.10
        values = [100, 110, 121, 133.1]
        assert average_growth_rate(values) == pytest.approx(0.10, rel=1e-4)

    def test_two_values(self):
        assert average_growth_rate([200, 250]) == pytest.approx(0.25, rel=TOLERANCE)

    def test_insufficient_data_raises(self):
        with pytest.raises(ValueError):
            average_growth_rate([1_000])

    def test_known_historical_data(self):
        historical = [50_000, 54_000, 58_320, 62_985, 68_024]
        rate = average_growth_rate(historical)
        assert 0.07 < rate < 0.09    # roughly 8 %


# ── predict_future_values ─────────────────────────────────────────

class TestPredictFutureValues:
    HISTORICAL = [50_000, 54_000, 58_320, 62_985, 68_024]

    def test_returns_correct_number_of_predictions(self):
        preds = predict_future_values(self.HISTORICAL, 5)
        assert len(preds) == 5

    def test_predictions_increase_with_positive_rate(self):
        preds = predict_future_values(self.HISTORICAL, 3)
        assert preds[0] < preds[1] < preds[2]

    def test_custom_rate_overrides_derived_rate(self):
        preds_auto   = predict_future_values(self.HISTORICAL, 3)
        preds_custom = predict_future_values(self.HISTORICAL, 3, custom_rate=0.20)
        assert preds_custom[0] > preds_auto[0]   # 20 % > ~8 %

    def test_first_prediction_matches_manual_calculation(self):
        rate  = average_growth_rate(self.HISTORICAL)
        start = self.HISTORICAL[-1]
        expected_p1 = start * (1 + rate)
        preds = predict_future_values(self.HISTORICAL, 1)
        assert preds[0] == pytest.approx(expected_p1, rel=TOLERANCE)

    def test_zero_periods_returns_empty(self):
        preds = predict_future_values(self.HISTORICAL, 0)
        assert preds == []
