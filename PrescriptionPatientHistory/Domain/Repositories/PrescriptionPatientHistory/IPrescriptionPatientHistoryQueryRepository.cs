using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace PrescriptionPatientHistory.Domain.Repositories.PrescriptionPatientHistory
{
    public interface IPrescriptionPatientHistoryQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>
    {
        Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>> GetAllPrescriptionPatientHistoryByName(string prescriptionPatientHistory);
        Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAllPrescriptionPatientHistoryPrevious(int patientId);

        

    }
}
