using GradeTrackerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeTrackerWebAPI.Data
{
    public class GradeTrackerContext(DbContextOptions<GradeTrackerContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<AssignmentEntity> Assignments { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEntity>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassEntity>()
                .HasMany(c => c.Students)
                .WithOne(cl => cl.Class)
                .HasForeignKey(cl => cl.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassEntity>()
                .HasMany(c => c.Subjects)
                .WithMany(s => s.Classes);

            modelBuilder.Entity<AssignmentEntity>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.SubjectId)
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