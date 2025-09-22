using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;


namespace symtoms.Domain.Repositories.Systom
{
    public interface ISymptomsQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Symptom>
    {
        Task<Response<List<Entities.EntityClass.Symptom>>> GetAllSymptomByName(string SymtomName);
        Task<Response<List<Entities.EntityClass.Symptom>>> GetBookMarks(int doctorId);

        
    }
}
