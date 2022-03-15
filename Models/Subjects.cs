using Task1.DTO;

namespace Task1.Models1;


public record Subject
{

    public long SubjectId { get; set; }

    public string SubjectName { get; set; }

    public subjectDto asDto
    {
        get
        {
            return new subjectDto
            {
                SubjectId = SubjectId,
                SubjectName = SubjectName,

            };
        }
    }



}