using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace score_management_be.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipUserStudentTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xoá thủ công bằng SQL nếu tồn tại
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_StudentUser_UserId')
                DROP INDEX [IX_StudentUser_UserId] ON [StudentUser];
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_TeacherUser_UserId')
                DROP INDEX [IX_TeacherUser_UserId] ON [TeacherUser];
            ");

            // Tạo lại index như mong muốn
            migrationBuilder.CreateIndex(
                name: "IX_TeacherUser_TeacherId",
                table: "TeacherUser",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherUser_UserId",
                table: "TeacherUser",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUser_StudentId",
                table: "StudentUser",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUser_UserId",
                table: "StudentUser",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUser_Students_StudentId",
                table: "StudentUser",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUser_Students_StudentId",
                table: "StudentUser");

            migrationBuilder.DropIndex(
                name: "IX_TeacherUser_TeacherId",
                table: "TeacherUser");

            migrationBuilder.DropIndex(
                name: "IX_TeacherUser_UserId",
                table: "TeacherUser");

            migrationBuilder.DropIndex(
                name: "IX_StudentUser_StudentId",
                table: "StudentUser");

            migrationBuilder.DropIndex(
                name: "IX_StudentUser_UserId",
                table: "StudentUser");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherUser_UserId",
                table: "TeacherUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUser_UserId",
                table: "StudentUser",
                column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StudentUser_Students_UserId",
            //    table: "StudentUser",
            //    column: "UserId",
            //    principalTable: "Students",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
