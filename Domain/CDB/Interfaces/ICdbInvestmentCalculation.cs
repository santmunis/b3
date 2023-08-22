
using Domain.CDB.Models;

namespace Domain.CDB.Interfaces;

public interface ICdbInvestmentCalculation
{
    public CdbInvestmentCalculation InvestmentCalculation(decimal initialInvestment, int deadlineInMonths);
}