﻿// <auto-generated />
using System;
using GradeTrackerWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GradeTrackerWebAPI.Migrations
{
    [DbContext(typeof(GradeTrackerContext))]
    [Migration("20250327162736_MadeTeacherDependOnSubject")]
    partial class MadeTeacherDependOnSubject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassEntitySubjectEntity", b =>
                {
                    b.Property<int>("ClassesId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("ClassesId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("ClassEntitySubjectEntity");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.AssignmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<bool>("IsGraded")
                        .HasColumnType("bit");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.ClassEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.SubjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.StudentEntity", b =>
                {
                    b.HasBaseType("GradeTrackerWebAPI.Models.UserEntity");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.HasIndex("ClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.TeacherEntity", b =>
                {
                    b.HasBaseType("GradeTrackerWebAPI.Models.UserEntity");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SubjectId")
                        .IsUnique()
                        .HasFilter("[SubjectId] IS NOT NULL");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ClassEntitySubjectEntity", b =>
                {
                    b.HasOne("GradeTrackerWebAPI.Models.ClassEntity", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GradeTrackerWebAPI.Models.SubjectEntity", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.AssignmentEntity", b =>
                {
                    b.HasOne("GradeTrackerWebAPI.Models.SubjectEntity", "Subject")
                        .WithMany("Assignments")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.StudentEntity", b =>
                {
                    b.HasOne("GradeTrackerWebAPI.Models.ClassEntity", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GradeTrackerWebAPI.Models.UserEntity", null)
                        .WithOne()
                        .HasForeignKey("GradeTrackerWebAPI.Models.StudentEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.TeacherEntity", b =>
                {
                    b.HasOne("GradeTrackerWebAPI.Models.UserEntity", null)
                        .WithOne()
                        .HasForeignKey("GradeTrackerWebAPI.Models.TeacherEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GradeTrackerWebAPI.Models.SubjectEntity", "Subject")
                        .WithOne("Teacher")
                        .HasForeignKey("GradeTrackerWebAPI.Models.TeacherEntity", "SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.ClassEntity", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("GradeTrackerWebAPI.Models.SubjectEntity", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Teacher")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
