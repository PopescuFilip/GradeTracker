using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Data
{
    public class GradeTrackerContext(DbContextOptions<GradeTrackerContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<AssignmentEntity> Assignments { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEntity>()
                .HasMany(s => s.Subjects)
                .WithMany(s => s.Students);

            modelBuilder.Entity<StudentEntity>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignmentEntity>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AssignmentEntity>()
                .HasMany(a => a.Grades)
                .WithOne(g => g.Assignment)
                .HasForeignKey(g => g.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubjectEntity>()
                .HasOne(s => s.Teacher)
                .WithOne(t => t.Subject)
                .HasForeignKey<TeacherEntity>(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeacherEntity>()
                .HasIndex(t => t.Id)
                .IsUnique();
        }
    }
}