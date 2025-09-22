using DataAccess.DatabaseAccessLayer;
using PrescriptionPdf.Domain.Repositories.PrescriptionPdf;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using Entities.EntityClass.PrescriptionEntity;
using prescriptionPdf.Utility;

namespace PrescriptionPdf.Infrastructure.RepositoriesImplement.PrescriptionPdf
{
    public class PrescriptionPdfCommandRepository : IPrescriptionPdfCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionPdfCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf, dynamic>("PrescriptionPdf_DeleteById", new
                {
                    PrescriptionPdfID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionPdfResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.PrescriptionEntity.PrescriptionPdf entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>("PrescriptionPdf_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionPdfResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionPdfResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PrescriptionEntity.PrescriptionPdf entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>("PrescriptionPdf_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionPdfResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionPdfResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
    }
}
