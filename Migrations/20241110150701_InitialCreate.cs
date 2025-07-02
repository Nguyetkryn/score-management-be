using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace score_management_be.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conducts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYears",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYears", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MotherFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionalLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seniority = table.Column<int>(type: "int", nullable: false),
                    IsHomeroomTeacher = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Birth = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfStudents = table.Column<int>(type: "int", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.id);
                    table.ForeignKey(
                        name: "FK_Semesters_SchoolYears_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYears",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeSubjects",
                columns: table => new
                {
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSubjects", x => new { x.SubjectId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_GradeSubjects_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingSubjects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingSubjects", x => x.id);
                    table.ForeignKey(
                        name: "FK_TeachingSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingSubjects_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentUser",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsers", x => new { x.StudentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_StudentUsers_Students_UserId",
                        column: x => x.UserId,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherUser",
                columns: table => new
                {
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherUsers", x => new { x.TeacherId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TeacherUsers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeroomTeachers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeroomTeachers", x => x.id);
                    table.ForeignKey(
                        name: "FK_HomeroomTeachers_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeroomTeachers_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeroomTeachers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeroomTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingAssignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingAssignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingAssignments_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachingSubjectSemester",
                columns: table => new
                {
                    TeachingSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingSubjectSemester", x => new { x.TeachingSubjectId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_TeachingSubjectSemester_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingSubjectSemester_TeachingSubjects_TeachingSubjectId",
                        column: x => x.TeachingSubjectId,
                        principalTable: "TeachingSubjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConductAssessment",
                columns: table => new
                {
                    HomeroomTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConductAssessment", x => new { x.ConductId, x.HomeroomTeacherId });
                    table.ForeignKey(
                        name: "FK_ConductAssessment_Conducts_ConductId",
                        column: x => x.ConductId,
                        principalTable: "Conducts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConductAssessment_HomeroomTeachers_HomeroomTeacherId",
                        column: x => x.HomeroomTeacherId,
                        principalTable: "HomeroomTeachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningOutcomes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OralTestScore = table.Column<double>(type: "float", nullable: false),
                    TestScore = table.Column<double>(type: "float", nullable: false),
                    MidtermScore = table.Column<double>(type: "float", nullable: false),
                    FinalScore = table.Column<double>(type: "float", nullable: false),
                    OverallScore = table.Column<double>(type: "float", nullable: false),
                    OutComeDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TeachingAssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningOutcomes", x => x.id);
                    table.ForeignKey(
                        name: "FK_LearningOutcomes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningOutcomes_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningOutcomes_TeachingAssignments_TeachingAssignmentId",
                        column: x => x.TeachingAssignmentId,
                        principalTable: "TeachingAssignments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_GradeId",
                table: "Classrooms",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConductAssessment_HomeroomTeacherId",
                table: "ConductAssessment",
                column: "HomeroomTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSubjects_GradeId",
                table: "GradeSubjects",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeroomTeachers_ClassroomId",
                table: "HomeroomTeachers",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeroomTeachers_SemesterId",
                table: "HomeroomTeachers",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeroomTeachers_StudentId",
                table: "HomeroomTeachers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeroomTeachers_TeacherId",
                table: "HomeroomTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningOutcomes_StudentId",
                table: "LearningOutcomes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningOutcomes_SubjectId",
                table: "LearningOutcomes",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningOutcomes_TeachingAssignmentId",
                table: "LearningOutcomes",
                column: "TeachingAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SchoolYearId",
                table: "Semesters",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_UserId",
                table: "StudentUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherUsers_UserId",
                table: "TeacherUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_ClassroomId",
                table: "TeachingAssignments",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_SemesterId",
                table: "TeachingAssignments",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssignments_TeacherId",
                table: "TeachingAssignments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSubjects_SubjectId",
                table: "TeachingSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSubjects_TeacherId",
                table: "TeachingSubjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSubjectSemester_SemesterId",
                table: "TeachingSubjectSemester",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConductAssessment");

            migrationBuilder.DropTable(
                name: "GradeSubjects");

            migrationBuilder.DropTable(
                name: "LearningOutcomes");

            migrationBuilder.DropTable(
                name: "StudentUser");

            migrationBuilder.DropTable(
                name: "TeacherUser");

            migrationBuilder.DropTable(
                name: "TeachingSubjectSemester");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Conducts");

            migrationBuilder.DropTable(
                name: "HomeroomTeachers");

            migrationBuilder.DropTable(
                name: "TeachingAssignments");

            migrationBuilder.DropTable(
                name: "TeachingSubjects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "SchoolYears");
        }
    }
}
