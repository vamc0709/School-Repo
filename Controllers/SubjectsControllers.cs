using Microsoft.AspNetCore.Mvc;
using Task1.Repositories;

using Task1.DTO;


namespace Task1.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectController : ControllerBase
{

    private readonly ISubjectRepository _subject;
    private readonly ILogger<SubjectController> _logger;

    private readonly  ITeacherRepository _teacher;

    public SubjectController(ILogger<SubjectController> logger, ISubjectRepository subject,ITeacherRepository teacher)
    {
        _logger = logger;

        _subject = subject;

        _teacher = teacher;
    }

    [HttpGet]

    public async Task<ActionResult<List<subjectDto>>> GetAllusers()
    {
        var usersList = await _subject.GetList();

        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{subject_id}")]

    public async Task<ActionResult<subjectDto>> GetUserById([FromRoute] long subject_id)
    {
        var user = await _subject.GetById(subject_id);

        var dto = user.asDto;

        dto.Teachers = await _teacher.GetList(subject_id);
        return Ok(dto);
    

    }



}