using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema;
using Elastic.Serilog.Sinks;
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
                                    .Enrich.WithElasticApmCorrelationInfo()
                                    .WriteTo.ElasticCloud(configuration.GetSection("ElasticsearchSettings:CloudId").Value??"", configuration.GetSection("ElasticsearchSettings:ApiKey").Value??"")
                                    .WriteTo.Console()                                                                      
                                    .CreateLogger(); 
    }
}
