using Task1.Models1;
using Dapper;
using System.Data;
using Task1.Utilities;
using Task1.DTO;


namespace Task1.Repositories;


public interface ISubjectRepository
{




    Task<Subject> GetById(long StudentId);

    Task<List<Subject>> GetList();

    Task<List<subjectDto>> GetListSubjects(long Id);

}


public class SubjectRepository : BaseRepository, ISubjectRepository
{

    public SubjectRepository(IConfiguration config) : base(config)

    {

    }


    public async Task<Subject> GetById(long SubjectId)
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}""
        WHERE subject_id = @SubjectId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Subject>(query,
            new
            {
                SubjectId = SubjectId
            });
    }

    public async Task<List<Subject>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}""";
        List<Subject> res;

        using (var con = NewConnection)
        {
            res = (await con.QueryAsync<Subject>(query)).AsList();
        }

        return res;
    }



    public async Task<List<subjectDto>> GetListSubjects(long Id)
    {
        var query = $@"SELECT s.* FROM {TableNames.student_subject} ss LEFT JOIN {TableNames.subject} s ON s.subject_id = ss.subject_id
        
        WHERE ss.student_id = @Id";

        using (var con = NewConnection)

        {
            return (await con.QueryAsync<subjectDto>(query, new { Id })).AsList();
        }
    }
}
