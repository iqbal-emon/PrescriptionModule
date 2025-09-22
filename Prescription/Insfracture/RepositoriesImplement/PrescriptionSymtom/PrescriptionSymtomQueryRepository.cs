using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass.PrescriptionEntity;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionSymtom;
using Prescription.Dtos.ResponseDto.PrescriptionPatientHistory;
using Prescription.Dtos.ResponseDto.PrescriptionSymtomDto;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionSymptom
{
    public class PrescriptionSymptomQueryRepository : IPrescriptionSymtomQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionSymptomQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom, dynamic>("PrescriptionSymtom_GetAll", new { });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionSymtomResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom, dynamic>("PrescriptionSymtom_GetById", new
                {
                    PrescriptionSymtomID = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionSymtomResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
        public async Task<Response<List<PrescriptionSymtomResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionSymtomResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<PrescriptionSymtomResponseDto, dynamic>("PrescriptionSymptom_GetByPrescriptionId", new
                {
                    PrescriptionId = id
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionExaminationResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>> GetPrescriptionSymptomByName(string name, string uniCode)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom, dynamic>("PrescriptionSymptom_GetByName", new
                {
                    name = name,
                    uniCode = uniCode
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionSymtomResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
