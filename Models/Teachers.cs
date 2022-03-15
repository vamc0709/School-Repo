using Task1.DTOS;

namespace Task1.Modelss;



public record Teacher
{
    public long TeacherId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Qualification { get; set; }

    public long Mobile { get; set; }

    public string Email { get; set; }

    public long SubjectId { get; set; }
    public string SubjectName { get; set; }

    public Gender Gender { get; set; }

    public DateTimeOffset DateOfBirth { get; set; }

    public teacherDto asDto
    {
        get
        {
            return new teacherDto
            {
                TeacherId = TeacherId,
                FirstName = FirstName,
                LastName = LastName,
                SubjectId = SubjectId,
                Email = Email,
                Quaification = Qualification,
                DateOfBirth = DateOfBirth,
                Mobile = Mobile,
                Gender = Gender.ToString().ToLower(),
                SubjectName = SubjectName,
            };
        }
    }

}

