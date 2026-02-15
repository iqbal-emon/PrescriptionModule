using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.MasterDoctor;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.MasterDoctor
{
    public class MasterDoctorCommandRepository : IMasterDoctorCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public MasterDoctorCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new MasterDoctorDeleteModel
                {
                    MasterDoctorID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.MasterDoctor, MasterDoctorDeleteModel>("MasterDoctor_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, MasterDoctorResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.MasterDoctor entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new MasterDoctorInsertModel
                {
                    DoctorID = entity.DoctorID,
                    AgentMasterID = entity.AgentMasterID
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("MasterDoctor_Insert", insertModel, "@MasterDoctorID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, MasterDoctorResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, MasterDoctorResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.MasterDoctor entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new MasterDoctorUpdateModel
                {
                    MasterDoctorID = entity.MasterDoctorID,
                    DoctorID = entity.DoctorID,
                    AgentMasterID = entity.AgentMasterID
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("MasterDoctor_Update", updateModel, "@UpdatedId");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, MasterDoctorResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, MasterDoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

