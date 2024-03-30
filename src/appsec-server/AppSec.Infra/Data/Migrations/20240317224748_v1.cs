using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSec.Infra.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DastAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UrlDast = table.Column<string>(type: "TEXT", nullable: false),
                    UserDast = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DastAnalysis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Branch = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SastAnalisys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UrlBase = table.Column<string>(type: "TEXT", nullable: false),
                    User = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    Languages = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SastAnalisys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DastReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    programName = table.Column<string>(type: "TEXT", nullable: false),
                    version = table.Column<string>(type: "TEXT", nullable: false),
                    generated = table.Column<string>(type: "TEXT", nullable: false),
                    DastAnalysisEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DastReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DastReport_DastAnalysis_DastAnalysisEntityId",
                        column: x => x.DastAnalysisEntityId,
                        principalTable: "DastAnalysis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RepoCommits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sha = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Files = table.Column<string>(type: "TEXT", nullable: false),
                    RepoEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepoCommits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepoCommits_Repos_RepoEntityId",
                        column: x => x.RepoEntityId,
                        principalTable: "Repos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    RepositoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SastId = table.Column<int>(type: "INTEGER", nullable: true),
                    DastId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_DastAnalysis_DastId",
                        column: x => x.DastId,
                        principalTable: "DastAnalysis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Repos_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Repos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_SastAnalisys_SastId",
                        column: x => x.SastId,
                        principalTable: "SastAnalisys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SastMeasuresSearchHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SastAnalisysEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SastMeasuresSearchHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SastMeasuresSearchHistory_SastAnalisys_SastAnalisysEntityId",
                        column: x => x.SastAnalisysEntityId,
                        principalTable: "SastAnalisys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    host = table.Column<string>(type: "TEXT", nullable: false),
                    port = table.Column<string>(type: "TEXT", nullable: false),
                    ssl = table.Column<string>(type: "TEXT", nullable: false),
                    DastReportId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_DastReport_DastReportId",
                        column: x => x.DastReportId,
                        principalTable: "DastReport",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SastMeasuresSearchHistoryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SastMeasuresSearchHistoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SastMeasuresSearchHistoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SastMeasuresSearchHistoryItem_SastMeasuresSearchHistory_SastMeasuresSearchHistoryId",
                        column: x => x.SastMeasuresSearchHistoryId,
                        principalTable: "SastMeasuresSearchHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pluginid = table.Column<string>(type: "TEXT", nullable: false),
                    alertRef = table.Column<string>(type: "TEXT", nullable: false),
                    alert = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    riskcode = table.Column<string>(type: "TEXT", nullable: false),
                    confidence = table.Column<string>(type: "TEXT", nullable: false),
                    riskdesc = table.Column<string>(type: "TEXT", nullable: false),
                    desc = table.Column<string>(type: "TEXT", nullable: false),
                    count = table.Column<string>(type: "TEXT", nullable: false),
                    solution = table.Column<string>(type: "TEXT", nullable: false),
                    otherinfo = table.Column<string>(type: "TEXT", nullable: false),
                    reference = table.Column<string>(type: "TEXT", nullable: false),
                    cweid = table.Column<string>(type: "TEXT", nullable: false),
                    wascid = table.Column<string>(type: "TEXT", nullable: false),
                    sourceid = table.Column<string>(type: "TEXT", nullable: false),
                    SiteId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alert_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Instance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    uri = table.Column<string>(type: "TEXT", nullable: false),
                    method = table.Column<string>(type: "TEXT", nullable: false),
                    param = table.Column<string>(type: "TEXT", nullable: false),
                    attack = table.Column<string>(type: "TEXT", nullable: false),
                    evidence = table.Column<string>(type: "TEXT", nullable: false),
                    otherinfo = table.Column<string>(type: "TEXT", nullable: false),
                    AlertId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instance_Alert_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alert",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alert_SiteId",
                table: "Alert",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_DastReport_DastAnalysisEntityId",
                table: "DastReport",
                column: "DastAnalysisEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Instance_AlertId",
                table: "Instance",
                column: "AlertId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DastId",
                table: "Projects",
                column: "DastId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RepositoryId",
                table: "Projects",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SastId",
                table: "Projects",
                column: "SastId");

            migrationBuilder.CreateIndex(
                name: "IX_RepoCommits_RepoEntityId",
                table: "RepoCommits",
                column: "RepoEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SastMeasuresSearchHistory_SastAnalisysEntityId",
                table: "SastMeasuresSearchHistory",
                column: "SastAnalisysEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SastMeasuresSearchHistoryItem_SastMeasuresSearchHistoryId",
                table: "SastMeasuresSearchHistoryItem",
                column: "SastMeasuresSearchHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_DastReportId",
                table: "Site",
                column: "DastReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instance");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "RepoCommits");

            migrationBuilder.DropTable(
                name: "SastMeasuresSearchHistoryItem");

            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "Repos");

            migrationBuilder.DropTable(
                name: "SastMeasuresSearchHistory");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "SastAnalisys");

            migrationBuilder.DropTable(
                name: "DastReport");

            migrationBuilder.DropTable(
                name: "DastAnalysis");
        }
    }
}
