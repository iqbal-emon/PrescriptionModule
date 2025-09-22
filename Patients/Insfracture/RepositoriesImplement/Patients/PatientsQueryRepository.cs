using DataAccess.DatabaseAccessLayer;
using PatienFolowUp.Domain.Repositories.Patients;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using PatienFolowUp.Utility;
using Entities.EntityClass.PatientEntity;

namespace PatienFolowUp.Insfracture.RepositoriesImplement.Patients
{
    public class PatientsQueryRepository : IPatientsQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PatientsQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Patient>>> GetAll()
        {
            var response = new Response<List<Patient>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Patient, dynamic>("Patients_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PatientsResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
        


              public async Task<Response<Patient>> GetByRoleAndReferenceId(int userId)
        {
            var response = new Response<Patient>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Patient, dynamic>("Patients_GetByUserId", new
                {
                    UserId=userId,
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PatientsResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }


        public async Task<Response<Patient>> GetById(int id)
        {
            var response = new Response<Patient>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Patient, dynamic>("Patients_GetById", new
                {
                    PatientID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PatientsResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
    }
}