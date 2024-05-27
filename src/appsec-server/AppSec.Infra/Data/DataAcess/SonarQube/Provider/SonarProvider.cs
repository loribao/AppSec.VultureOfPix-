using AppSec.Domain.Entities;
using AppSec.Infra.Data.DataAcess.JsonConvert;
using AppSec.Infra.Data.DataAcess.SonarQube.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.DataAcess.SonarQube.Provider
{
    public static class SonarProvider
    {
        public static async Task<T> GetMeasuresComponentPerPeriod<T>(string projectName, IConfiguration configuration) {

            var url = configuration.GetSection("sonar:url").Value??"localhost:9000";
            var token = configuration.GetSection("sonar:token").Value ?? "";
            var requestUri = $"/api/measures/component?additionalFields=period,metrics&component={projectName}&metricKeys=alert_status,quality_gate_details,new_violations,accepted_issues,new_accepted_issues,high_impact_accepted_issues,maintainability_issues,reliability_issues,security_issues,bugs,new_bugs,reliability_rating,new_reliability_rating,vulnerabilities,new_vulnerabilities,security_rating,new_security_rating,security_hotspots,new_security_hotspots,security_hotspots_reviewed,new_security_hotspots_reviewed,security_review_rating,new_security_review_rating,code_smells,new_code_smells,sqale_rating,new_maintainability_rating,sqale_index,new_technical_debt,coverage,new_coverage,lines_to_cover,new_lines_to_cover,tests,duplicated_lines_density,new_duplicated_lines_density,duplicated_blocks,ncloc,ncloc_language_distribution,projects,lines,new_lines";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());
            try
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(url);
                    http.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{token}")));

                    var resp = await http.GetAsync(requestUri);
                    if (resp.IsSuccessStatusCode)
                    {
                        return await resp.Content.ReadFromJsonAsync<T>(options);
                    }
                    else
                    {
                        throw new Exception($"{resp.RequestMessage}");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"msg: {e.Message}, trace:{e.StackTrace}");
            }            
        }
    }
}

