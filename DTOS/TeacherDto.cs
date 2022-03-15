using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Task1.DTO;

namespace Task1.DTOS;

public record teacherDto
{
    [JsonPropertyName("teacher_id")]
    public long TeacherId { get; set; }


    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }


    [JsonPropertyName("last_name")]
    public string LastName { get; set; }


    [JsonPropertyName("email")]

    public string Email { get; set; }

    [JsonPropertyName("gender")]

    public string Gender { get; set; }

    [JsonPropertyName("subject_id")]

    public long SubjectId { get; set; }

    [JsonPropertyName("subject_name")]

    public string SubjectName { get; set; }


    [JsonPropertyName("mobile")]

    public long Mobile { get; set; }

    [JsonPropertyName("qualification")]

    public string Quaification { get; set; }

    [JsonPropertyName("date_of_bith")]

    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("students_assigned")]

    public List<studentDto> Student { get; set; }
}

public record UsersCreateDto
{


    [JsonPropertyName("first_name")]
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    [MaxLength(255)]
    public string Email { get; set; }


    [JsonPropertyName("gender")]
    [Required]
    [MaxLength(6)]

    public string Gender { get; set; }

    [JsonPropertyName("qualification")]
    [Required]
    public string Quaification { get; set; }

    [JsonPropertyName("subject_id")]
    [Required]

    public long SubjectId { get; set; }





    [JsonPropertyName("mobile")]

    public long Mobile { get; set; }

    [JsonPropertyName("date_of_birth")]

    public DateTimeOffset DateOfBirth { get; set; }
}

public record UsersUpdateDto
{

    [JsonPropertyName("last_name")]
    [MaxLength(50)]
    public string LastName { get; set; }



    [JsonPropertyName("email")]
    [MaxLength(255)]
    public string Email { get; set; }

}