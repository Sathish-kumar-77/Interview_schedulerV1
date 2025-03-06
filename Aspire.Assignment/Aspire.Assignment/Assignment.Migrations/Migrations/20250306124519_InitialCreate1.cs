using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PanelMembers",
                columns: table => new
                {
                    PanelMemberId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanelMembers", x => x.PanelMemberId);
                    table.ForeignKey(
                        name: "FK_PanelMembers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportingManagers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportingManagers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ReportingManagers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAAdmins",
                columns: table => new
                {
                    TAAdminId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAAdmins", x => x.TAAdminId);
                    table.ForeignKey(
                        name: "FK_TAAdmins_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TARecruiters",
                columns: table => new
                {
                    TARecruiterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TARecruiters", x => x.TARecruiterId);
                    table.ForeignKey(
                        name: "FK_TARecruiters_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PanelCoordinators",
                columns: table => new
                {
                    PanelCoordinatorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PanelMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    AllocationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AllocationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanelCoordinators", x => x.PanelCoordinatorId);
                    table.ForeignKey(
                        name: "FK_PanelCoordinators_PanelMembers_PanelMemberId",
                        column: x => x.PanelMemberId,
                        principalTable: "PanelMembers",
                        principalColumn: "PanelMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PanelCoordinators_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    ReportingManagerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_Candidates_ReportingManagers_ReportingManagerId",
                        column: x => x.ReportingManagerId,
                        principalTable: "ReportingManagers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PanelMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    AvailableStartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AvailableEndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsBooked = table.Column<bool>(type: "INTEGER", nullable: false),
                    BookedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    CandidateId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportingManagerId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsManagerAttending = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_Slots_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_PanelMembers_PanelMemberId",
                        column: x => x.PanelMemberId,
                        principalTable: "PanelMembers",
                        principalColumn: "PanelMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_ReportingManagers_ReportingManagerId",
                        column: x => x.ReportingManagerId,
                        principalTable: "ReportingManagers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_TARecruiters_BookedBy",
                        column: x => x.BookedBy,
                        principalTable: "TARecruiters",
                        principalColumn: "TARecruiterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ReportingManagerId",
                table: "Candidates",
                column: "ReportingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PanelCoordinators_PanelMemberId",
                table: "PanelCoordinators",
                column: "PanelMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PanelCoordinators_UserId",
                table: "PanelCoordinators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PanelMembers_UserId",
                table: "PanelMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_BookedBy",
                table: "Slots",
                column: "BookedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_CandidateId",
                table: "Slots",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_PanelMemberId",
                table: "Slots",
                column: "PanelMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ReportingManagerId",
                table: "Slots",
                column: "ReportingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TAAdmins_UserId",
                table: "TAAdmins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TARecruiters_UserId",
                table: "TARecruiters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PanelCoordinators");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "TAAdmins");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "PanelMembers");

            migrationBuilder.DropTable(
                name: "TARecruiters");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ReportingManagers");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
