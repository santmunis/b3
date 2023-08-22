




using Domain.CDB.Interfaces;

namespace Application.Cdb.Command;
public record struct CalculateCdbCommandContract(decimal InitialInvestment, int DeadlineInMonths) : IRequest<ErrorOr<CalculateCdbCommandResponse>>;

public record struct CalculateCdbCommandResponse(decimal GrossValue, decimal NetValue, string Tax);

public class CalculateCdbCommandHandler : IRequestHandler<CalculateCdbCommandContract, ErrorOr<CalculateCdbCommandResponse>>
{
    private readonly ICdbInvestmentCalculation _cdbInvestmentCalculation;

    public CalculateCdbCommandHandler(ICdbInvestmentCalculation cdbInvestmentCalculation  )
    {
        _cdbInvestmentCalculation = cdbInvestmentCalculation;
    }

    public async Task<ErrorOr<CalculateCdbCommandResponse>> Handle(CalculateCdbCommandContract request, CancellationToken cancellationToken)
    {
        var investiment =
            _cdbInvestmentCalculation.InvestmentCalculation(request.InitialInvestment, request.DeadlineInMonths);

        return new CalculateCdbCommandResponse
        {
            GrossValue = investiment.GrossValue,
            NetValue = investiment.NetValue,
            Tax = investiment.Tax
        };
    }
}

public class CalculateCdbCommandResponseValidator : AbstractValidator<CalculateCdbCommandContract>
{
    public CalculateCdbCommandResponseValidator()
    {
        RuleFor(x => x.InitialInvestment).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(x => x.DeadlineInMonths).NotEmpty().NotNull().GreaterThan(1);
    }
}