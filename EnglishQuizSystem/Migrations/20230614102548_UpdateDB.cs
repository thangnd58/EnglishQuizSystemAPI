using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishQuizSystem.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    code_active = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quiz_id = table.Column<int>(type: "int", nullable: true),
                    text = table.Column<string>(type: "ntext", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: true),
                    type = table.Column<bool>(type: "bit", nullable: true),
                    difficulty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.id);
                    table.ForeignKey(
                        name: "FK__question__quiz_i__5070F446",
                        column: x => x.quiz_id,
                        principalTable: "quiz",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK__user__role_id__4BAC3F29",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: true),
                    text = table.Column<string>(type: "ntext", nullable: true),
                    is_correct = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK__answer__question__534D60F1",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_quiz",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    quiz_id = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_topic_pk", x => new { x.user_id, x.quiz_id });
                    table.ForeignKey(
                        name: "FK__user_quiz__quiz___5BE2A6F2",
                        column: x => x.quiz_id,
                        principalTable: "quiz",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__user_quiz__user___5AEE82B9",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_answer",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    answer_id = table.Column<int>(type: "int", nullable: false),
                    quiz_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_answer_pk", x => new { x.user_id, x.question_id, x.answer_id, x.quiz_id });
                    table.ForeignKey(
                        name: "FK__user_answ__answe__5812160E",
                        column: x => x.answer_id,
                        principalTable: "answer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__user_answ__quest__571DF1D5",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__user_answ__user___5629CD9C",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_answer_quiz_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answer_question_id",
                table: "answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_quiz_id",
                table: "question",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "UQ__user__7C9273C40FA9F6A6",
                table: "user",
                column: "user_name",
                unique: true,
                filter: "[user_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_answer_id",
                table: "user_answer",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_question_id",
                table: "user_answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_quiz_id",
                table: "user_answer",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_quiz_quiz_id",
                table: "user_quiz",
                column: "quiz_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_answer");

            migrationBuilder.DropTable(
                name: "user_quiz");

            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "quiz");
        }
    }
}
