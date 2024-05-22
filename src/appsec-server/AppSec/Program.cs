using AppSec.Bootstrap;
using Elastic.Apm.AspNetCore;
using Elastic.Apm.NetCoreAll;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
SerilogExtension.AddSerilog(builder.Configuration);
builder.Host.UseSerilog(Log.Logger);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen()
    .ExtendServicesBootStrap(builder.Configuration);



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder.WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowMyOrigin");
}
app.ExtendWebApplicationBootStrap();

app.Run();
