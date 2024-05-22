using AppSec.Domain.DTOs;

namespace AppSec.Domain.Commands.StartRepositoryAnalysisCommand
{
    public class StartRepositoryAnalysisResponse
    {
        public string ProjectName { get; set; }
        public string ProjectId { get; set; }
        public DateTime RrunDate { get; set; } = DateTime.Now;
        public IEnumerable<DiffRepositoryDTO> diff { get; set; }
    }
}
