﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GradeTrackerWebAPI.Models
{
    [Table("Subjects")]
    public class SubjectEntity : Entity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<AssignmentEntity> Assignments { get; set; } = new List<AssignmentEntity>();

        [JsonIgnore]
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();

        [JsonIgnore]
        public virtual TeacherEntity Teacher { get; set; } = null!;
    }
}