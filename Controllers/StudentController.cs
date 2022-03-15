using Microsoft.AspNetCore.Mvc;
using Task1.Repositories;
using Task1.DTO;
using Task1.Models;


namespace Task1.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _student;
    private readonly ILogger<StudentController> _logger;

    private readonly ITeacherRepository _teacher;

    private readonly ISubjectRepository _subject;

    

    public StudentController(ILogger<StudentController> logger, IStudentRepository student, ITeacherRepository teacher , ISubjectRepository subject)
    {
        _logger = logger;

        _student = student;

        _teacher = teacher;

        _subject = subject;

        


    }
    [HttpGet]

    public async Task<ActionResult<List<studentDto>>> GetAllusers()
    {
        var usersList = await _student.GetList();

        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{student_id}")]

    public async Task<ActionResult<studentDto>> GetUserById([FromRoute] long student_id)
    {
        var user = await _student.GetById(student_id);

        if (user is null)
            return NotFound("No user found with given student_id");

        var dto = user.asDto;

        dto.Teacher = await _teacher.GetList(user.StudentId);
        dto.Subject = await _subject.GetListSubjects(user.StudentId);
        

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<studentDto>> CreateUser([FromBody] UserCreateDto Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.ToLower())))

            return BadRequest("Gender value is not recognized");

        var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;

        if (subtractDate.TotalDays / 365 < 18.0)

            return BadRequest("Student must be at least 18 years old");

        var toCreateUser = new Student
        {
            FirstName = Data.FirstName,
            LastName = Data.LastName,
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Email = Data.Email.Trim().ToLower(),
            Gender = Enum.Parse<Gender>(Data.Gender, true),
            ClassRoomId = Data.ClassRoomId,


        };

        var createdUser = await _student.Create(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);
    }

    [HttpPut("{student_id}")]

    public async Task<ActionResult> UpdateUser([FromRoute] long student_id,
    [FromBody] UserUpdateDto Data)
    {
        var existing = await _student.GetById(student_id);
        if (existing is null)
            return NotFound("No user found with given student id");

        var toUpdateUser = existing with
        {
            Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            LastName = Data.LastName?.Trim() ?? existing.LastName,
            DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _student.Update(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }


    [HttpDelete("{student_id}")]

    public async Task<ActionResult> DeleteUser([FromRoute] long student_id)
    {
        var existing = await _student.GetById(student_id);

        if (existing is null)
            return NotFound("No user found with given student id");

        var didDelete = await _student.Delete(student_id);

        return NoContent();

    }


}
