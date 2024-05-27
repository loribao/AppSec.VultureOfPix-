using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.DataAcess.OwaspZap.Model;
using AppSec.Infra.Data.DataAcess.OwaspZap.Provider;
using AppSec.Infra.Data.DataAcess.SonarQube.Interfaces;
using AppSec.Infra.Data.DataAcess.SonarQube.Model;
using AppSec.Infra.Data.DataAcess.SonarQube.Provider;
using Docker.DotNet.Models;
using HotChocolate.Authorization;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AppSec.Infra.Web.Graphql;

[Authorize]
public class QueryRoot
{
    [Authorize("admin")]
    [UseFiltering]
    public IQueryable<ProjectEntity> GetProjects([Service] IMongoDatabase db) => db.GetCollection<ProjectEntity>("projects").AsQueryable();

    [Authorize("admin")]
    public async Task<MeasuresComponentPeriod> GetOverallCode(string ProjectName, [Service] IConfiguration configuration)
    {
        var ret = await SonarProvider.GetMeasuresComponentPerPeriod<MeasuresComponentPeriod>(ProjectName, configuration);
        return ret;
    }

    [Authorize("admin")]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IQueryable<Reports<List<DiffRepositoryDTO>>> GetReportGit([Service] IMongoDatabase db) => db.GetCollection<Reports<List<DiffRepositoryDTO>>>("ReportGit").AsQueryable();

    [Authorize("admin")]
    [UsePaging]
    [UseProjection]
    [UseSorting]
    [UseFiltering]   
    public IExecutable<Reports<List<AppSec.Domain.DTOs.Component>>> GetReportSast([Service] IMongoDatabase db) => db.GetCollection<Reports<List<AppSec.Domain.DTOs.Component>>>("ReportSast").AsExecutable();

    [Authorize("admin")]
    [UsePaging]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<Reports<List<AppSec.Domain.DTOs.Site>>> GetReportDast([Service] IMongoDatabase db) => db.GetCollection<Reports<List<AppSec.Domain.DTOs.Site>>>("ReportDast").AsExecutable();

    [AllowAnonymous]
    public IEnumerable<UIDTO> GetUrls([Service] IConfiguration configuration)
    {
        var ret = new List<UIDTO>();
        var sonar = configuration.GetSection("sonar:UI").Get<UIDTO>();
        if (sonar.Show)
        {            
            if (!string.IsNullOrEmpty(sonar.URL))
            {
                ret.Add(sonar);
            }
        }
        var zap = configuration.GetSection("zap:UI").Get<UIDTO>();
        if (zap.Show)
        {   
            if (!string.IsNullOrEmpty(zap.URL))
            {
                ret.Add(zap);
            }
        }
        var kibana = configuration.GetSection("kibana:UI").Get<UIDTO>();
        if (kibana.Show)
        {            
            if (!string.IsNullOrEmpty(kibana.URL))
            {
                ret.Add(kibana);
            }
        }
        var server = configuration.GetSection("server:UI").Get<UIDTO>();
        if (server.Show)
        {            
            if (!string.IsNullOrEmpty(server.URL))
            {
                ret.Add(server);
            }
        }
        return ret;
    }


    [AllowAnonymous]
    public string Ping() => "Pong";
}
