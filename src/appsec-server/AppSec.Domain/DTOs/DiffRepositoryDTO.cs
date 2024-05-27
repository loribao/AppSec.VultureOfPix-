using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AppSec.Domain.DTOs
{
    public class DiffRepositoryDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectId { get; set; }
        public DateTime DateAnalysis { get; set; }
        public string NameAuthor { get; set; }
        public string EmailAuthor { get; set; }
        public string DateAuthor { get; set; }
        public string MsgCommit { get; set; }
        public string OId { get; set; }
        public IList<ChangesRepository> FilesChanges { get; set; } = new List<ChangesRepository>();
        public IList<DiffRepository> diff { get; set; } = new List<DiffRepository>();
    }
    public class DiffRepository
    {
        public string diff { get; set; }
        public int LinesAdd { get; set; }
        public int LinesRemove { get; set; }
        public string Path { get; set; }
        public string Oid { get; set; }
        public string OldPath { get; set; }
        public string OldOid { get; set; }
    }
    public class ChangesRepository
    {
        public string Path { get; set; }
        public string Status { get; set; }
        public string OldOId { get; set; }
        public string OId { get; set; }
        public bool OldExists { get; set; }
        public string OldMode { get; set; }
        public string Mode { get; set; }
        public string OldFile { get; set; }
    }
}
