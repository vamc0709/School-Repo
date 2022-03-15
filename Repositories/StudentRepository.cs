using Task1.Models;
using Dapper;
using System.Data;
using Task1.Utilities;
using Task1.DTO;

namespace Task1.Repositories;


public interface IStudentRepository
{
    Task<Student> Create(Student Item);

    Task<bool> Update(Student Item);

    Task<bool> Delete(long EmployeeNumber);

    Task<Student> GetById(long EmployeeNumber);

    Task<List<Student>> GetList();

    Task<List<studentDto>> GetList(long Id);

}


public class StudentRepository : BaseRepository,IStudentRepository
{

    public StudentRepository(IConfiguration config) : base(config)

    {

    }

    public async Task<Student> Create(Student Item)
    {
        var query = $@"INSERT INTO ""{TableNames.students}"" (first_name, last_name, date_of_birth, email, gender,classroom_id) 
       VALUES (@FirstName, @LastName, @DateOfBirth,@Email, @Gender,@ClassroomId)
       RETURNING *";


        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Student>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long StudentId)
    {
        var query = $@"DELETE FROM ""{TableNames.students}"" WHERE student_id = @StudentId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { StudentId });
            return res > 0;
        }
    }

    public async Task<Student> GetById(long StudentId)
    {
        var query = $@"SELECT * FROM ""{TableNames.students}""
        WHERE student_id = @StudentId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Student>(query,
            new
            {
                StudentId = StudentId
            });
    }

    public async Task<List<Student>> GetList()
    {
        var query = $@"SELECT*FROM ""{TableNames.students}""";
        List<Student> res;

        using (var con = NewConnection)
        {
            res = (await con.QueryAsync<Student>(query)).AsList();
        }

        return res;
    }

    public async Task<List<studentDto>> GetList(long Id)
    {
        var query = $@"SELECT t.* FROM {TableNames.student_teacher} st LEFT JOIN {TableNames.students} t ON
       
       t.student_id = st.student_id WHERE st.teacher_id = @Id";

       using (var con = NewConnection)
       {

           return(await con.QueryAsync<studentDto>(query ,new {Id})).AsList();
       }
    }

    public async Task<bool> Update(Student Item)
    {
        var query = $@"UPDATE ""{TableNames.students}"" SET first_name = @FirstName,last_name = @LastName,date_of_birth = @DateOfBirth,email = @Email,gender = @Gender,classroom_id=@ClassroomId WHERE student_id = @StudentId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);

            return rowCount == 1;
        }
    }
}






