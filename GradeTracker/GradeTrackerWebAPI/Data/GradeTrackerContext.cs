using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Data
{
    /// <summary>
    /// Database context for the Grade Tracker system.
    /// </summary>
    public class GradeTrackerContext(DbContextOptions<GradeTrackerContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the users in the system.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Gets or sets the students.
        /// </summary>
        public DbSet<StudentEntity> Students { get; set; }

        /// <summary>
        /// Gets or sets the assignments.
        /// </summary>
        public DbSet<AssignmentEntity> Assignments { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        public DbSet<SubjectEntity> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the teachers.
        /// </summary>
        public DbSet<TeacherEntity> Teachers { get; set; }

        /// <summary>
        /// Configures the entity relationships and constraints.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
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