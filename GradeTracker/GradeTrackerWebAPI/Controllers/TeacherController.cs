using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController(IEntityService<TeacherEntity> entityService) : BaseEntityController<TeacherEntity>(entityService)
{
}