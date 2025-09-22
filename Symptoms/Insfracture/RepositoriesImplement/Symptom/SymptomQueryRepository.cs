using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Symptoms.Utility;
using symtoms.Domain.Repositories.Systom;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Symptoms.Infrastructure.RepositoriesImplement.Symptom
{
    public class SymptomQueryRepository : ISymptomsQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public SymptomQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all symptoms
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        // Get all symptoms from the database
        public async Task<Response<List<Entities.EntityClass.Symptom>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Symptom>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Symptom, dynamic>("Symptom_GetAll", new { });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, SymptomResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Symptom>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.Symptom>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Symptom, dynamic>("GetBookMarksSymtomByDoctorId", new {
                    DoctorReferenceID=doctorId
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, SymptomResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }


        

        public async Task<Response<List<Entities.EntityClass.Symptom>>> GetAllSymptomByName(string SymtomName)
        {
            var response = new Response<List<Entities.EntityClass.Symptom>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Symptom, dynamic>("Symptom_GetSymptomsByName", new 
                {
                    SymptomName = SymtomName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, SymptomResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Get a symptom by ID
        public async Task<Response<Entities.EntityClass.Symptom>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Symptom>();

            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Symptom, dynamic>("Symptom_GetById", new
                {
                    SymptomId = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, SymptomResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
