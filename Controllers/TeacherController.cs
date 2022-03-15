using Microsoft.AspNetCore.Mvc;
using Task1.Repositories;
using Task1.DTOS;
using Task1.Modelss;




namespace Task1.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherRepository _teacher;
    private readonly ILogger<TeacherController> _logger;

    private readonly IStudentRepository _student;

    public TeacherController(ILogger<TeacherController> logger,ITeacherRepository teacher,IStudentRepository student)
    {
        _logger = logger;

        _teacher = teacher;

        _student = student;

        
    }
    [HttpGet]

    public async Task<ActionResult<List<teacherDto>>> GetAllusers()
    {
        var teachersList = await _teacher.GetList();

        var dtoList = teachersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{teacher_id}")]

    public async Task<ActionResult<teacherDto>> GetUserById([FromRoute] long teacher_id)
    {
        var teacher = await _teacher.GetById(teacher_id);

        if (teacher is null)
            return NotFound("No user found with given teacher_id");

        
         var dto = teacher.asDto;

        dto.Student = await _student.GetList(teacher.TeacherId);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<teacherDto>> CreateUser([FromBody] UsersCreateDto Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.ToLower())))

            return BadRequest("Gender value is not recognized");

        var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;

        if (subtractDate.TotalDays / 365 < 25.0)

            return BadRequest("Teacher must be at least 25 years old");

        var toCreateUser = new Teacher
        {
            FirstName = Data.FirstName,
            LastName = Data.LastName,
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Email = Data.Email.Trim().ToLower(),
            Gender = Enum.Parse<Gender>(Data.Gender,true),
            SubjectId = Data.SubjectId,
            Qualification = Data.Quaification,
            Mobile = Data.Mobile
            

        };

        var createdUser = await _teacher.Create(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);
    }

    
    

    [HttpPut("{teacher_id}")]

    public async Task<ActionResult> UpdateUser([FromRoute] long teacher_id,
    [FromBody] UsersUpdateDto Data)
    {
        var existing = await _teacher.GetById(teacher_id);
        if (existing is null)
            return NotFound("No user found with given teacher id");

        var toUpdateUser = existing with
        {
            Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            LastName = Data.LastName?.Trim() ?? existing.LastName,
            
        };

        var didUpdate = await _teacher.Update(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }


    [HttpDelete("{teacher_id}")]

    public async Task<ActionResult> DeleteUser([FromRoute] long teacher_id)
    {
        var existing = await _teacher.GetById(teacher_id);

        if (existing is null)
            return NotFound("No user found with given teacher id");

        var didDelete = await _teacher.Delete(teacher_id);

        return NoContent();

    }


}
    