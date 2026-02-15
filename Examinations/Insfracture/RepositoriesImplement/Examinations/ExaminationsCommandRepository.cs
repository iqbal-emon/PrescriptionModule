using DataAccess.DatabaseAccessLayer;
using Examinations.DatabaseModels;
using Examinations.Domain.Repositories.Examinations;
using Examinations.Utility;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Examinations.Insfracture.RepositoriesImplement.Examinations
{
    public class ExaminationsCommandRepository : IExaminationsCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public ExaminationsCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new ExaminationsDeleteModel
                {
                    ExaminationID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<ExaminationsDeleteModel, ExaminationsDeleteModel>(
                    "Examinations_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, ExaminationsResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Examination entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new ExaminationsInsertModel
                {
                    ExaminationID = 0, // SP accepts but doesn't use (auto-generated)
                    TenantID = entity.TenantID,
                    PatientID = entity.PatientID,
                    DoctorID = entity.DoctorID,
                    ExaminationDate = entity.ExaminationDate,
                    Findings = entity.Findings,
                    Notes = entity.Notes,
                    BloodPressure = entity.BloodPressure,
                    Pulse = entity.Pulse,
                    Temperature = entity.Temperature,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<ExaminationsInsertModel>(
                    "Examinations_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, ExaminationsResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, ExaminationsResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Examination entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new ExaminationsUpdateModel
                {
                    ExaminationId = entity.ExaminationID,
                    TenantId = entity.TenantID > 0 ? entity.TenantID : null,
                    PatientId = entity.PatientID > 0 ? entity.PatientID : null,
                    DoctorId = entity.DoctorID > 0 ? entity.DoctorID : null,
                    ExaminationDate = entity.ExaminationDate,
                    Findings = entity.Findings,
                    Notes = entity.Notes,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<ExaminationsUpdateModel>(
                    "Examinations_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, ExaminationsResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, ExaminationsResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
