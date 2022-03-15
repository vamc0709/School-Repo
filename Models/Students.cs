using Task1.DTO;

namespace Task1.Models;


public record Student
{

    public long StudentId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTimeOffset DateOfBirth { get; set; }

    public long ClassRoomId { get; set; }

    public string Email { get; set; }

    public Gender Gender { get; set; }

    public studentDto asDto
    {
        get
        {
            return new studentDto
            {
                StudentId = StudentId,
                FirstName = FirstName,
                LastName = LastName,
                ClassRoomId = ClassRoomId,
                Email = Email,
                Gender = Gender.ToString().ToLower(),
            };
        }
    }
}


