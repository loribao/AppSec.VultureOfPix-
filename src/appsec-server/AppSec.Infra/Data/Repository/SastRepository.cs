using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IDrivers;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Context;
using AppSec.Infra.DTos;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;
using System.Text.Json;
namespace AppSec.Infra.Data.Repository;

public class SastRepository : ISastRepository
{
    private readonly ILogger<SastRepository> _logger;
    private readonly ILanguageDriverSast _languageDriverSast;
    private readonly ContextAppSec _context;
    public SastRepository(ContextAppSec context, ILogger<SastRepository> logger, ILanguageDriverSast languageDriverSast)
    {
        _logger = logger;
        _languageDriverSast = languageDriverSast;
        _context = context;
    }
    public async Task<string> CreateIntegrationProject(SastAnalisysEntity project)
    {

        var baseAddress = new Uri(project.UrlBase);
        var project_name = project.Name;
        var project_key = project.Name;
        var token = project.Token;
        var token_name = project.Name;
        var user = project.User;
        var password = project.Password;
        using (var httpClient = new HttpClient { BaseAddress = baseAddress })
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user}:{password}")));

            var response = await httpClient.PostAsync($"api/projects/create?name={project_name}&project={project_name}", null);

            if (response.IsSuccessStatusCode)
            {

                Console.WriteLine("Projeto criado com sucesso");
            }
            else
            {
                Console.WriteLine($"Falha ao criar o projeto: ${response.StatusCode}");
            }
        }
        HttpResponseMessage resp;
        using (var httpClient = new HttpClient { BaseAddress = baseAddress })
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user}:{password}")));

            var response = await httpClient.PostAsync($"api/user_tokens/generate?name={token_name}&projectKey={project_key}", null);

            if (response.IsSuccessStatusCode)
            {
                resp = response;
                token = await resp.Content.ReadAsStringAsync();
                Console.WriteLine($"Token criado com sucesso: {await resp.Content.ReadAsStringAsync()}");
            }
            else
            {
                Console.WriteLine($"Falha ao criar token: {response.ToString()}");
            }
        }
        return token;
    }
    public async Task RunAnalysis(ProjectEntity project)
    {
        var baseAddress = project?.Sast?.UrlBase ?? "http://localhost:9000";
        var project_key = project?.Sast?.Name ?? "";
        var token = project?.Sast?.Token ?? "";
        var user = project?.Sast?.User ?? "";
        var password = project?.Sast?.Password ?? "";
        var language = project?.Sast?.Languages ?? Domain.Enums.Languages.CSharp;
        var path = project?.Path ?? "";
        _languageDriverSast.RunAnalysis(language, path, token, baseAddress, project_key, user, password);
    }

    public async Task RunAnalysis(int id, string token)
    {
        var project = _context.Projects.Where(x => x.Id == id).Select(x => new ProjectEntity()
        {
            Sast = new SastAnalisysEntity()
            {
                UrlBase = x.Sast.UrlBase,
                Name = x.Sast.Name,
                Token = x.Sast.Token,
                User = x.Sast.User,
                Password = x.Sast.Password,
                Languages = x.Sast.Languages
            },
            Path = x.Path,
            Name = x.Name,
            Id = x.Id,
            Description = x.Description,
            CreatedAt = x.CreatedAt,
            Dast = x.Dast,
            Repository = x.Repository
        }).FirstOrDefault();
        if (project is null)
        {
            _logger.LogError("Project not found");
            throw new NotFoundException("Project not found");
        }
        try
        {
            project.Sast.Token = token;
            await RunAnalysis(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task SyncAnalysis(int id)
    {
        try
        {
            var _project = _context.Projects.Where(x => x.Id == id).Select(x => x.Sast).FirstOrDefault();
            var baseAddress = new Uri(_project.UrlBase); // Substitua pelo endereço base da sua API
            var user = _project.User; // Substitua pelo seu usuário
            var password = _project.Password; // Substitua pela sua senha
            var project_key = _project.Name;
            var metrics = "ncloc,coverage,new_violations,vulnerabilities,bugs,code_smells,security_hotspots,security_review_rating,alert_status,quality_gate_details";
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user}:{password}")));

                var response = await httpClient.GetAsync($"api/measures/search_history?component={project_key}&metrics={metrics}");

                if (response.IsSuccessStatusCode)
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    var content = JsonSerializer.Deserialize<DtoMeasuresSearchHistory>(contentString);
                    if (content is not null)
                    {
                        var measures = new List<SastMeasuresSearchHistory>();
                        content.measures.ForEach(x =>
                        {
                            measures.Add(new SastMeasuresSearchHistory()
                            {
                                Name = x.metric,
                                History = x.history.Select(y => new SastMeasuresSearchHistoryItem()
                                {
                                    Date = y.date?.ToString() ?? "",
                                    Value = y.value ?? ""
                                }).ToList()
                            });
                        });
                        _project.Measures = measures;
                        _context.Update(_project);

                    }
                }
                else
                {
                    Console.WriteLine($"Falha ao fazer a solicitação: {response.StatusCode}");
                }
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

    }
}
