namespace AppSec.Domain.Commands.StartDastCommand;

public record StartDastRequest(string Id, string urlTarget, string dastApiKey, string dastUrlApi);
