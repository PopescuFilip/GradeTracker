﻿using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeTrackerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController(IEntityService<AssignmentEntity> entityService) : BaseEntityController<AssignmentEntity>(entityService)
    {
    }
}