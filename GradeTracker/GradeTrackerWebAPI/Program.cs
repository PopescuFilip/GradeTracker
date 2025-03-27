using GradeTrackerWebAPI.Data;
using GradeTrackerWebAPI.Models;
using GradeTrackerWebAPI.Services;
using GradeTrackerWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEntityService<TeacherEntity>, EntityService<TeacherEntity>>();
builder.Services.AddScoped<IEntityService<AssignmentEntity>, EntityService<AssignmentEntity>>();
builder.Services.AddScoped<IEntityService<StudentEntity>, StudentService>();
builder.Services.AddScoped<IEntityService<SubjectEntity>, SubjectService>();
builder.Services.AddScoped<IEntityService<GradeEntity>, GradeService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<GradeTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GradeTrackerDB")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await PopulateDb(scope);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task PopulateDb(IServiceScope scope)
{
    var subjectService = scope.ServiceProvider.GetRequiredService<IEntityService<SubjectEntity>>();
    var subjects = await subjectService.GetAll();
    if (subjects == null || subjects.Count == 0)
    {
        await subjectService.Create(new SubjectEntity() { Name = "Math" });
        await subjectService.Create(new SubjectEntity() { Name =  "English" });
    }

    var teacherService = scope.ServiceProvider.GetRequiredService<IEntityService<TeacherEntity>>();
    var teachers = await teacherService.GetAll();
    if (teachers == null || teachers.Count == 0)
    {
        var mathTeacher = new TeacherEntity()
        {
            FirstName = "MathTeacher",
            LastName = "Geraldson",
            Username = "teacher",
            Password = "pass",
            Subject = (await subjectService.GetAll()).First(s => s.Name == "Math")
        };
        var englishTeacher = new TeacherEntity()
        {
            FirstName = "EnglishTeacher",
            LastName = "Smith",
            Username = "smith",
            Password = "pass",
            Subject = (await subjectService.GetAll()).First(s => s.Name == "English")
        };
        await teacherService.Create(englishTeacher);
        await teacherService.Create(mathTeacher);
    }

    var studentService = scope.ServiceProvider.GetRequiredService<IEntityService<StudentEntity>>();
    var students = await studentService.GetAll();
    if (students == null || students.Count == 0)
    {
        var newStudent1 = new StudentEntity()
        {
            FirstName = "John",
            LastName = "Maical",
            Username = "student",
            Password = "pass",
            Subjects = await subjectService.GetAll()
        };
        var newStudent2 = new StudentEntity()
        {
            FirstName = "Alex",
            LastName = "Butcher",
            Username = "alex",
            Password = "pass",
            Subjects = await subjectService.GetAll()
        };
        var newStudent3 = new StudentEntity()
        {
            FirstName = "Chad",
            LastName = "Chad",
            Username = "chad",
            Password = "pass",
            Subjects = { (await subjectService.GetAll()).First() }
        };
        await studentService.Create(newStudent1);
        await studentService.Create(newStudent2);
        await studentService.Create(newStudent3);
    }
}