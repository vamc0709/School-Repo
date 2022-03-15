using Task1.DTO;

namespace Task1.Models1;


    public record Classroom
    {

        public long ClassroomId{get;set;}

        public string Capacity{get;set;}

        public string Facilities{get;set;}

         public  classDto asDto{
            get{
                return new classDto{
                    SubjectId = ClassroomId,
                    Capacity = Capacity,
                    Facilities = Facilities,
                    
                };
            }
        }

        

    }