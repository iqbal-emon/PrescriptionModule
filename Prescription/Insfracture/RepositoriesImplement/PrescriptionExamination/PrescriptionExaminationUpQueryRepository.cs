using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionExamination;
using Prescription.Dtos.ResponseDto.PrescriptionExaminationDto;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionExamination
{
    public class PrescriptionExaminationQueryRepository : IPrescriptionExminationQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionExaminationQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination, dynamic>("PrescriptionExamination_GetAll", new { });
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

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination, dynamic>("PrescriptionExamination_GetById", new
                {
                    PrescriptionExaminationId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionExaminationResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>> GetPrescriptionExaminationByName(string name, string uniCode)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination, dynamic>("PrescriptionExamination_GetByName", new
                {
                    name = name,
                    uniCode = uniCode
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionExaminationResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
        public async Task<Response<List<PrescriptionExaminationResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionExaminationResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<PrescriptionExaminationResponseDto, dynamic>("PrescriptionExamination_GetByPrescriptionId", new
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
    }
}
