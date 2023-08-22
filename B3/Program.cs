using Application;
using B3;
using B3.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebUiServices();
builder.Services.RegisterEndpointModules();
builder.Services.AddCors(o => o.AddPolicy("AcceptAll", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    //.AllowCredentials();
}));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.UseDeveloperExceptionPage();


using var scope = app.Services.CreateScope();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AcceptAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.UseHttpsRedirection();

app.Run();