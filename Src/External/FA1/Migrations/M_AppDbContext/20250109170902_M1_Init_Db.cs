using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FA1.Migrations.M_AppDbContext
{
    /// <inheritdoc />
    public partial class M1_Init_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "todo");

            migrationBuilder
                .AlterDatabase()
                .Annotation(
                    "Npgsql:CollationDefinition:case_insensitive",
                    "en-u-ks-primary,en-u-ks-primary,icu,False"
                );

            migrationBuilder.CreateTable(
                name: "role",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: "bigint", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "user",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: "bigint", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    UserName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedUserName = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    Email = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedEmail = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "role_claim",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_claim_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "todo",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "additional_user_info",
                schema: "todo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    first_name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    last_name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(65535)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_user_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_additional_user_info_user_Id",
                        column: x => x.Id,
                        principalSchema: "todo",
                        principalTable: "user",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_claim",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claim_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "todo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_login",
                schema: "todo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_login", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_user_login_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "todo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "todo",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_role_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "todo",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_user_role_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "todo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_token",
                schema: "todo",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_user_token",
                        x => new
                        {
                            x.UserId,
                            x.LoginProvider,
                            x.Name,
                        }
                    );
                    table.ForeignKey(
                        name: "FK_user_token_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "todo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "todo_task_list",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: "bigint", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    created_date = table.Column<DateTime>(
                        type: "TIMESTAMP WITH TIME ZONE",
                        nullable: false
                    ),
                    user_id = table.Column<long>(type: "BIGINT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_task_list", x => x.Id);
                    table.ForeignKey(
                        name: "FK_todo_task_list_additional_user_info_user_id",
                        column: x => x.user_id,
                        principalSchema: "todo",
                        principalTable: "additional_user_info",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "todo_task",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: "bigint", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    content = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    note = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    created_date = table.Column<DateTime>(
                        type: "TIMESTAMP WITH TIME ZONE",
                        nullable: false
                    ),
                    due_date = table.Column<DateTime>(
                        type: "TIMESTAMP WITH TIME ZONE",
                        nullable: false
                    ),
                    is_in_my_day = table.Column<bool>(type: "boolean", nullable: false),
                    is_important = table.Column<bool>(type: "boolean", nullable: false),
                    is_finished = table.Column<bool>(type: "boolean", nullable: false),
                    recurring_expression = table.Column<string>(
                        type: "VARCHAR(255)",
                        nullable: false
                    ),
                    task_list_id = table.Column<long>(type: "BIGINT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_todo_task_todo_task_list_task_list_id",
                        column: x => x.task_list_id,
                        principalSchema: "todo",
                        principalTable: "todo_task_list",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "todo_task_step",
                schema: "todo",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: "bigint", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    content = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    created_date = table.Column<DateTime>(
                        type: "TIMESTAMP WITH TIME ZONE",
                        nullable: false
                    ),
                    todo_task_id = table.Column<long>(type: "BIGINT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_task_step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_todo_task_step_todo_task_todo_task_id",
                        column: x => x.todo_task_id,
                        principalSchema: "todo",
                        principalTable: "todo_task",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "todo",
                table: "role",
                column: "NormalizedName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_role_claim_RoleId",
                schema: "todo",
                table: "role_claim",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_todo_task_task_list_id",
                schema: "todo",
                table: "todo_task",
                column: "task_list_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_todo_task_list_user_id",
                schema: "todo",
                table: "todo_task_list",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_todo_task_step_todo_task_id",
                schema: "todo",
                table: "todo_task_step",
                column: "todo_task_id"
            );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "todo",
                table: "user",
                column: "NormalizedEmail"
            );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "todo",
                table: "user",
                column: "NormalizedUserName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_claim_UserId",
                schema: "todo",
                table: "user_claim",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_login_UserId",
                schema: "todo",
                table: "user_login",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_user_role_RoleId",
                schema: "todo",
                table: "user_role",
                column: "RoleId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "role_claim", schema: "todo");

            migrationBuilder.DropTable(name: "todo_task_step", schema: "todo");

            migrationBuilder.DropTable(name: "user_claim", schema: "todo");

            migrationBuilder.DropTable(name: "user_login", schema: "todo");

            migrationBuilder.DropTable(name: "user_role", schema: "todo");

            migrationBuilder.DropTable(name: "user_token", schema: "todo");

            migrationBuilder.DropTable(name: "todo_task", schema: "todo");

            migrationBuilder.DropTable(name: "role", schema: "todo");

            migrationBuilder.DropTable(name: "todo_task_list", schema: "todo");

            migrationBuilder.DropTable(name: "additional_user_info", schema: "todo");

            migrationBuilder.DropTable(name: "user", schema: "todo");
        }
    }
}
