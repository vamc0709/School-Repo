using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Task1.DTOS;

namespace Task1.DTO;

public record subjectDto
{
       [JsonPropertyName("subject_id")]
       public long SubjectId{get;set;}


        [JsonPropertyName("subject_name")]
        public string SubjectName{get;set;}

        [JsonPropertyName("who Teaches")]
        

        public List<teacherDto> Teachers{get;set;}

       

}

