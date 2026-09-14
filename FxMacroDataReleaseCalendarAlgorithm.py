# region imports
from AlgorithmImports import *
from QuantConnect.DataSource import *
# endregion


class BlsWithFxMacroDataCalendarAlgorithm(QCAlgorithm):
    def initialize(self):
        self.set_start_date(2026, 1, 1)
        self.set_end_date(2026, 12, 31)
        self.set_cash(100000)

        self.spy = self.add_equity("SPY", Resolution.DAILY).symbol
        self.cpi = self.add_data(BLSEconomicSurveysCpi, "CPI", Resolution.DAILY).symbol
        self.usd_calendar = self.add_data(
            FxMacroDataReleaseCalendar, "USD", Resolution.DAILY
        ).symbol

    def on_data(self, data: Slice):
        event = data.get(FxMacroDataReleaseCalendar, self.usd_calendar)
        cpi = data.get(BLSEconomicSurveysCpi, self.cpi)
        if event is not None and event.market_tier == 1:
            self.debug(f"{self.time.date()} tier-1 release: {event.name}")
            self.set_holdings(self.spy, 0.25)
        elif cpi is not None and cpi.core_cpi is not None:
            self.set_holdings(self.spy, 1)
