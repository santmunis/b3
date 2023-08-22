using Domain.CDB.Services;

namespace TestProject1;

public class InvestmentCalculationTest
{
    private CdbInvestmentCalculationService _service;
    public InvestmentCalculationTest()
    {
        _service = new CdbInvestmentCalculationService();
    }
    [Fact]
    public void SixMonth()
    {
        var sixMonth = _service.InvestmentCalculation(1000, 6);
        Assert.Equal(1059.756M, sixMonth.GrossValue);
        Assert.Equal(1046.311M, sixMonth.NetValue);
    }
    [Fact]
    public void TwelveMonthsTax()
    {
        var twelveMonthsTax = _service.InvestmentCalculation(1000, 12);
        Assert.Equal(1123.082M, twelveMonthsTax.GrossValue);
        Assert.Equal(1098.466M, twelveMonthsTax.NetValue);
    }
    [Fact]
    public void TwentyFourMonthsTax()
    {
        var twentyFourMonthsTax= _service.InvestmentCalculation(1000, 24);
        Assert.Equal(1261.313M, twentyFourMonthsTax.GrossValue);
        Assert.Equal(1215.583M, twentyFourMonthsTax.NetValue);
    }
    [Fact]
    public void AboveTwentyFourMonthsTax()
    {
        var aboveTwentyFourMonthsTax= _service.InvestmentCalculation(1000, 26);
        Assert.Equal(1285.952M, aboveTwentyFourMonthsTax.GrossValue);
        Assert.Equal(1243.059M, aboveTwentyFourMonthsTax.NetValue);
    }
   
}