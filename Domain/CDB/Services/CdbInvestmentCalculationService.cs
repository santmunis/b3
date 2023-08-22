using Domain.CDB.Interfaces;
using Domain.CDB.Models;
using Domain.Commons.Const;

namespace Domain.CDB.Services;

public class CdbInvestmentCalculationService: ICdbInvestmentCalculation
{
    public CdbInvestmentCalculation InvestmentCalculation(decimal initialInvestment, int deadlineInMonths)
    {
        var grossValue = CalculateGrossValue(initialInvestment, deadlineInMonths);
        var netValue = CalculateNetValue(grossValue, deadlineInMonths, initialInvestment);

        return new CdbInvestmentCalculation
        {
            GrossValue = grossValue,
            NetValue = netValue,
            Tax = $"{GetTax(deadlineInMonths) * 100}%"
            
        };
    }
    
    private decimal CalculateGrossValue(decimal initialInvestment, int deadlineInMonths)
    {
        var grossValue = initialInvestment * (1 + TaxesEnum.CDI * TaxesEnum.TB);
        deadlineInMonths--;
        if (deadlineInMonths != 0)
        {
            grossValue = CalculateGrossValue(grossValue, deadlineInMonths);
        }

        return Math.Round(grossValue, 3);
    }
    
    private decimal CalculateNetValue(decimal grossValue, int deadlineInMonths, decimal initialInvestment)
    {
        var tax = GetTax(deadlineInMonths);
        var taxToPay = (grossValue - initialInvestment) * tax;

        return Math.Round(grossValue - taxToPay, 3) ;
    }

    private decimal GetTax(int deadlineInMonths)
    {
        decimal tax;
        switch (deadlineInMonths)
        {
            case <= 6:
                tax = TaxesEnum.SixMonthsTax;
                break;
            case <= 12:
                tax = TaxesEnum.TwelveMonthsTax;
                break;
            case <= 24:
                tax = TaxesEnum.TwentyFourMonthsTax;
                break;
            default:
                tax = TaxesEnum.AboveTwentyFourMonthsTax;
                break;
        }

        return tax;
    }
    
}