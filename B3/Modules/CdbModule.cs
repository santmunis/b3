using Application.Cdb.Command;
using B3.Interfaces;
using B3.Message;
using Domain.CDB.Interfaces;
using Domain.CDB.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static B3.Message.RestResultMinimalApi;
namespace B3.Modules;

public class CdbModule: IEndpointModule
{
    public IServiceCollection RegisterEndpoints(IServiceCollection services)
    {
        services.AddScoped<ICdbInvestmentCalculation, CdbInvestmentCalculationService>();
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/cdb/investiment/calculate", GetCdbInvestmentCalculation)
            .Produces<RestResult<CalculateCdbCommandResponse>>().Produces(403).Produces(401);
           
        return endpoints;
    }
    private async Task<IResult> GetCdbInvestmentCalculation(
        [FromServices] IMediator mediatr,
        [FromQuery] decimal initialInvestment, [FromQuery] string deadlineInMonths
    )
    {
        var request = new CalculateCdbCommandContract(initialInvestment,int.Parse(deadlineInMonths));    
        var result = await mediatr.Send(request);
        return CreateApiResponse(result);
    }
}