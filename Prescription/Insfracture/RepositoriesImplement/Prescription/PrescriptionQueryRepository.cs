using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.Prescription;
using Prescription.Dtos.ResponseDto.PrescriptionDto;
using Prescription.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.Prescription
{
    internal class PrescriptionQueryRepository: IPrescriptionQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PrescriptionQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.Prescription, dynamic>("Prescription_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<PrescriptionAnalyticsDto>> GetAnalytics()
        {
            var response = new Response<PrescriptionAnalyticsDto>();

            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<PrescriptionAnalyticsDto, dynamic>(
                    "Prescription_GetAnaytics",
                    new { }
                );

                //var analytics = result.FirstOrDefault();

                response.Result = result;
                response.IsSuccess = true;

                ResponseHelper.SetSuccessResponse(
                    response,
                    result,
                    PrescriptionResponseMessage.common_get_all_success,
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }

            return response;
        }


        public async Task<Response<Entities.EntityClass.PrescriptionEntity.Prescription>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.Prescription>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.Prescription, dynamic>("Prescription_GetById", new
                {
                    PrescriptionId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
