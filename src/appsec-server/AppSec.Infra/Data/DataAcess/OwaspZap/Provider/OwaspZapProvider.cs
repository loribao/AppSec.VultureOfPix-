using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;
using AppSec.Infra.Data.DataAcess.OwaspZap.Model;
using LibGit2Sharp;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.DataAcess.OwaspZap.Provider
{
    public static class OwaspZapProvider
    {
        public static async Task<ZapReportDto?> GetReport(IConfiguration configuration, CancellationToken cancellationToken=default)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    var opt = new JsonSerializerOptions
                    {
                        NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    };
                    var baseurl = configuration.GetSection("zap:url").Value ?? throw new ArgumentNullException("zap:url");
                    client.BaseAddress = new Uri(baseurl);
                    string apiUrl = $"/OTHER/core/other/jsonreport/?";
                    HttpResponseMessage response = await client.GetAsync(apiUrl, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(responseBody))
                        {
                            var content = JsonSerializer.Deserialize<ZapReportDto>(responseBody, opt);
                            if (content == null)
                            {
                                throw new Exception("report not found");
                            }
                            return content;
                        }
                        else
                        {
                            throw new Exception($"not content body");
                        }
                    }
                    else
                    {
                        throw new Exception($"not sucess request: status {response.StatusCode} msg:{response.RequestMessage?.ToString()}"); ;
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }        
        }
    }
}
