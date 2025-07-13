using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiRagProxy.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProviderConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderType = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthenticationType = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseUrl = table.Column<string>(type: "TEXT", nullable: false),
                    EncryptedSecret = table.Column<string>(type: "TEXT", nullable: true),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModelConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModelName = table.Column<string>(type: "TEXT", nullable: false),
                    DeploymentName = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelConfigurations_ProviderConfigurations_ProviderConfigurationId",
                        column: x => x.ProviderConfigurationId,
                        principalTable: "ProviderConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelConfigurations_ProviderConfigurationId",
                table: "ModelConfigurations",
                column: "ProviderConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelConfigurations");

            migrationBuilder.DropTable(
                name: "ProviderConfigurations");
        }
    }
}
