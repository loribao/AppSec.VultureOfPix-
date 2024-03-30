namespace AppSec.Infra.Data.Services.Commands;

public record SastStartCommand(int ProjectId, string Sonartoken);
