using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HooPulse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fantasy_teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fantasy_teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HomeClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    AwayClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduledAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    HomeScore = table.Column<int>(type: "integer", nullable: true),
                    AwayScore = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_clubs_AwayClubId",
                        column: x => x.AwayClubId,
                        principalTable: "clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_clubs_HomeClubId",
                        column: x => x.HomeClubId,
                        principalTable: "clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_players_clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "team_slots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FantasyTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_slots_fantasy_teams_FantasyTeamId",
                        column: x => x.FantasyTeamId,
                        principalTable: "fantasy_teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_slots_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clubs_ShortName",
                table: "clubs",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fantasy_teams_OwnerId",
                table: "fantasy_teams",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_AwayClubId",
                table: "matches",
                column: "AwayClubId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_HomeClubId",
                table: "matches",
                column: "HomeClubId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_ScheduledAt",
                table: "matches",
                column: "ScheduledAt");

            migrationBuilder.CreateIndex(
                name: "IX_matches_Status",
                table: "matches",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_players_ClubId",
                table: "players",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_players_FirstName_LastName",
                table: "players",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_team_slots_FantasyTeamId_PlayerId",
                table: "team_slots",
                columns: new[] { "FantasyTeamId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_slots_FantasyTeamId_Position",
                table: "team_slots",
                columns: new[] { "FantasyTeamId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_slots_PlayerId",
                table: "team_slots",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "team_slots");

            migrationBuilder.DropTable(
                name: "fantasy_teams");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "clubs");
        }
    }
}
