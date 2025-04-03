﻿using GradeTracker.Models;
namespace GradeTracker.Services.Interfaces
{
    public interface IGradeService
    {
        Task<List<GradeEntity>?> GetGradesForStudent(int studentId);
        Task<List<GradeEntity>> GetGradesForSubjectAndStudent(int subjectId, int studentId);
        Task<List<GradeEntity>?> GetGradesForAssignmentAndStudent(int studentId, int assignmentId);
        Task<bool> UpdateGrade(int gradeId, int newGrade);
        Task<bool> DeleteGrade(int gradeId);
    }
}