using Microsoft.EntityFrameworkCore;
using score_management_be.Models;

namespace score_management_be.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Conduct> Conducts { get; set; }
        public DbSet<TeacherUser> TeacherUser { get; set; }
        public DbSet<StudentUser> StudentUser { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TeachingSubject> TeachingSubjects { get; set; }
        public DbSet<GradeSubject> GradeSubjects { get; set; }
        public DbSet<TeachingAssignment> TeachingAssignments { get; set; }
        public DbSet<HomeroomTeacher> HomeroomTeachers { get; set; }
        public DbSet<ConductAssessment> ConductAssessment { get; set; }
        public DbSet<LearningOutcomes> LearningOutcomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // STUDENT - USER
            modelBuilder.Entity<StudentUser>()
                .HasKey(x => new { x.StudentId, x.UserId });

            modelBuilder.Entity<StudentUser>()
                .HasOne(x => x.Student)
                .WithOne(x => x.StudentUser)
                .HasForeignKey<StudentUser>(x => x.StudentId);

            modelBuilder.Entity<StudentUser>()
                .HasOne(x => x.User)
                .WithOne(x => x.StudentUser)
                .HasForeignKey<StudentUser>(x => x.UserId);

            // TEACHER - USER (assuming similar model structure)
            modelBuilder.Entity<TeacherUser>()
                .HasKey(x => new { x.TeacherId, x.UserId });

            modelBuilder.Entity<TeacherUser>()
                .HasOne(x => x.Teacher)
                .WithOne(x => x.TeacherUser)
                .HasForeignKey<TeacherUser>(x => x.TeacherId);

            modelBuilder.Entity<TeacherUser>()
                .HasOne(x => x.User)
                .WithOne(x => x.TeacherUser)
                .HasForeignKey<TeacherUser>(x => x.UserId);

            // USER - ROLE
            modelBuilder.Entity<UserRole>()
                .HasKey(val => new { val.RoleId, val.UserId });
            modelBuilder.Entity<UserRole>()
                .HasOne(val => val.User)
                .WithMany(val => val.UserRoles)
                .HasForeignKey(val => val.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(val => val.Role)
                .WithMany(val => val.UserRoles)
                .HasForeignKey(val => val.RoleId);

            // SEMESTER - SCHOOLYEAR
            modelBuilder.Entity<SchoolYear>()
                .HasMany(val => val.Semesters)
                .WithOne(val => val.SchoolYear)
                .HasForeignKey(val => val.SchoolYearId);

            // CLASSROOM - GRADE
            modelBuilder.Entity<Classroom>()
                .HasOne(val => val.Grade)
                .WithMany(val => val.Classrooms)
                .HasForeignKey(val => val.GradeId);

            // TEACHER - SUBJECT
            modelBuilder.Entity<TeachingSubject>()
                .HasOne(val => val.Subject)
                .WithMany(val => val.TeachingSubjects)
                .HasForeignKey(val => val.SubjectId);
            modelBuilder.Entity<TeachingSubject>()
                .HasOne(val => val.Teacher)
                .WithMany(val => val.TeachingSubjects)
                .HasForeignKey(val => val.TeacherId);

            // GRADE - SUBJECT
            modelBuilder.Entity<GradeSubject>()
                .HasKey(val => new { val.SubjectId, val.GradeId });
            modelBuilder.Entity<GradeSubject>()
                .HasOne(val => val.Subject)
                .WithMany(val => val.GradeSubjects)
                .HasForeignKey(val => val.SubjectId);
            modelBuilder.Entity<GradeSubject>()
                .HasOne(val => val.Grade)
                .WithMany(val => val.GradeSubjects)
                .HasForeignKey(val => val.GradeId);

            // TEACHINGSUBJECT - SEMESTER
            modelBuilder.Entity<TeachingSubjectSemester>()
                .HasKey(val => new { val.TeachingSubjectId, val.SemesterId });
            modelBuilder.Entity<TeachingSubjectSemester>()
                .HasOne(val => val.TeachingSubject)
                .WithMany(val => val.TeachingSubjectSemesters)
                .HasForeignKey(val => val.TeachingSubjectId);
            modelBuilder.Entity<TeachingSubjectSemester>()
                .HasOne(val => val.Semester)
                .WithMany(val => val.TeachingSubjectSemesters)
                .HasForeignKey(val => val.SemesterId);

            // TEACHER - CLASSROOM - SEMESTER
            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(val => val.Teacher)
                .WithMany(val => val.TeachingAssignments)
                .HasForeignKey(val => val.TeacherId);
            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(val => val.Classroom)
                .WithMany(val => val.TeachingAssignments)
                .HasForeignKey(val => val.ClassroomId);
            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(val => val.Semester)
                .WithMany(val => val.TeachingAssignments)
                .HasForeignKey(val => val.SemesterId);

            // TEACHER - CLASSROOM - SEMESTER - STUDENT
            modelBuilder.Entity<HomeroomTeacher>()
                .HasOne(val => val.Teacher)
                .WithMany(val => val.HomeroomTeachers)
                .HasForeignKey(val => val.TeacherId);
            modelBuilder.Entity<HomeroomTeacher>()
                .HasOne(val => val.Classroom)
                .WithMany(val => val.HomeroomTeachers)
                .HasForeignKey(val => val.ClassroomId);
            modelBuilder.Entity<HomeroomTeacher>()
                .HasOne(val => val.Semester)
                .WithMany(val => val.HomeroomTeachers)
                .HasForeignKey(val => val.SemesterId);
            modelBuilder.Entity<HomeroomTeacher>()
                .HasOne(val => val.Student)
                .WithMany(val => val.HomeroomTeachers)
                .HasForeignKey(val => val.StudentId);

            // CONDUCT - HOMEROOMTEACHER
            modelBuilder.Entity<ConductAssessment>()
                .HasKey(val => new { val.ConductId, val.HomeroomTeacherId });
            modelBuilder.Entity<ConductAssessment>()
                .HasOne(val => val.Conduct)
                .WithMany(val => val.ConductAssessments)
                .HasForeignKey(val => val.ConductId);
            modelBuilder.Entity<ConductAssessment>()
                .HasOne(val => val.HomeroomTeacher)
                .WithMany(val => val.ConductAssessments)
                .HasForeignKey(val => val.HomeroomTeacherId);
        }
    }
}
