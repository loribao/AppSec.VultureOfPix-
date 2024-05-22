using Elastic.CommonSchema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSec.Bootstrap;
public static class SerilogExtension
{
    public static void AddSerilog(IConfiguration configuration)
    {

        Serilog.Log.Logger = new LoggerConfiguration()
                                    .ReadFrom
                                    .Configuration(configuration)
                                    .Enrich.FromLogContext()
                                    .Enrich.WithThreadId()
                                    .Enrich.WithMachineName()
                                    .Enrich.WithProperty("Application", "AppSec")
                                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                                    .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(configuration["ElasticsearchSettings:NodeUris"]))
                                    {
                                        AutoRegisterTemplate = true,
                                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                                        IndexFormat = "appsec-{0:yyyy.MM.dd}",
                                        CustomFormatter = new Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter(renderMessage: true, inlineFields: true)
                                    })
                                    .WriteTo.Console()
                                    .CreateLogger(); 
    }
}
