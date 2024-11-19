using FMSDataServer.Api;
using FMSDataServer.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FMSDataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/fms/subject", async (FMSDataDbContext dbContext, Subject subject) =>
{
    dbContext.Subjects.Add(subject);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/Subject/{subject.Id}", subject);
});

app.MapPost("/fms/teacher", async (FMSDataDbContext dbContext, Teacher teacher) =>
{
    dbContext.Teachers.Add(teacher);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/teacher/{teacher.Id}", teacher);
});


app.MapPost("/fms/class", async (FMSDataDbContext dbContext, ModelClass modelClass) =>
{
    dbContext.Classes.Add(modelClass);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/class/{modelClass.Id}", modelClass);
});

app.MapPost("/fms/student", async (FMSDataDbContext dbContext, Student student) =>
{
    dbContext.Students.Add(student);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/student/{student.Id}", student);
});

app.MapPost("/fms/teachersubject", async (FMSDataDbContext dbContext, TeacherSubject teacherSubject) =>
{
    dbContext.TeacherSubjects.Add(teacherSubject);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/teachersubject/{teacherSubject.Id}", teacherSubject);
});

app.MapPost("/fms/lecture", async (FMSDataDbContext dbContext, Lecture lecture) =>
{
    dbContext.Lectures.Add(lecture);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/fms/lecture/{lecture.Id}", lecture);
});


app.MapGet("/fms/subject", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Subjects.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/teacher", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Teachers.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/class", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Classes.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/student", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Students.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/teachersubject", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.TeacherSubjects.AsNoTracking().ToListAsync());
});

app.MapGet("/fms/lecture", async (FMSDataDbContext dbContext) =>
{
    return Results.Ok(await dbContext.Lectures.AsNoTracking().ToListAsync());
});





app.Run();

