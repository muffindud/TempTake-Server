using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TempTake_Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(32)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manager",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mac = table.Column<string>(type: "varchar(12)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manager", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    telegram_user_id = table.Column<string>(type: "varchar(32)", nullable: false),
                    telegram_username = table.Column<string>(type: "varchar(32)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "worker",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mac = table.Column<string>(type: "varchar(12)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group_manager",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    manager_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_manager", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_manager_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_manager_manager_manager_id",
                        column: x => x.manager_id,
                        principalTable: "manager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false),
                    is_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_user_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "manager_worker",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    manager_id = table.Column<int>(type: "integer", nullable: false),
                    worker_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manager_worker", x => x.id);
                    table.ForeignKey(
                        name: "FK_manager_worker_manager_manager_id",
                        column: x => x.manager_id,
                        principalTable: "manager",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_manager_worker_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entry",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    temperature_c = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    humidity_perc = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    pressure_mmhg = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    ppm = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    manager_worker_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry", x => x.id);
                    table.ForeignKey(
                        name: "FK_entry_manager_worker_manager_worker_id",
                        column: x => x.manager_worker_id,
                        principalTable: "manager_worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entry_manager_worker_id",
                table: "entry",
                column: "manager_worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_manager_group_id",
                table: "group_manager",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_manager_manager_id",
                table: "group_manager",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_user_group_id",
                table: "group_user",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_user_user_id",
                table: "group_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_manager_worker_manager_id",
                table: "manager_worker",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_manager_worker_worker_id",
                table: "manager_worker",
                column: "worker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entry");

            migrationBuilder.DropTable(
                name: "group_manager");

            migrationBuilder.DropTable(
                name: "group_user");

            migrationBuilder.DropTable(
                name: "manager_worker");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "manager");

            migrationBuilder.DropTable(
                name: "worker");
        }
    }
}
