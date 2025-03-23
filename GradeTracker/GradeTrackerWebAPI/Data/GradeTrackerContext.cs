using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Data
{
    public class GradeTrackerContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<AssignmentEntity> Assignments { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }

        public GradeTrackerContext(DbContextOptions<GradeTrackerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Table-Per-Type (TPT) inheritance for Users
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<StudentEntity>().ToTable("Students");
            modelBuilder.Entity<TeacherEntity>().ToTable("Teachers");

            // Configure ClassStudent many-to-many join entity
            modelBuilder.Entity<StudentEntity>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete if needed

            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.HasKey(cs => new { cs.ClassId, cs.SubjectId });

                entity.HasOne(cs => cs.Class)
                    .WithMany(c => c.ClassSubjects)
                    .HasForeignKey(cs => cs.ClassId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure Assignment -> Subject relationship
            modelBuilder.Entity<AssignmentEntity>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Teacher <-> Subject one-to-one relationship
            modelBuilder.Entity<SubjectEntity>()
                .HasOne(s => s.Teacher)          // Subject has one Teacher
                .WithOne(t => t.Subject)         // Teacher has one Subject
                .HasForeignKey<SubjectEntity>(s => s.TeacherId) // FK in Subject
                .OnDelete(DeleteBehavior.Cascade); // Delete Subject if Teacher is deleted

            modelBuilder.Entity<SubjectEntity>()
                .HasIndex(s => s.TeacherId)
                .IsUnique(); // Each TeacherId can appear only once in Subjects

            modelBuilder.Entity<TeacherEntity>()
                .HasIndex(t => t.Id)
                .IsUnique(); // Not strictly necessary but clarifies the principal key
        }
    }
}
