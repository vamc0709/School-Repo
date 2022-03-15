using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Task1.DTOS;

namespace Task1.DTO;

public record classDto
{
       [JsonPropertyName("classroom_id")]
       public long SubjectId{get;set;}


        [JsonPropertyName("capacity")]
        public string Capacity{get;set;}

        [JsonPropertyName("Facilities")]
        

        public string Facilities{get;set;}

       

}