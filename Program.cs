using Task1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddTransient<IStudentRepository,StudentRepository>();
builder.Services.AddTransient<ITeacherRepository,TeacherRepository>();
builder.Services.AddTransient<ISubjectRepository,SubjectRepository>();
builder.Services.AddTransient<IClassRepository,ClassRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
