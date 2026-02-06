using DataAccess.DatabaseAccessLayer;
using Doctor.Domain.Repositories.DoctorExpertise;
using Doctor.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorExpertise
{
    public class DoctorExpertiseQueryRepository : IDoctorExpertiseQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorExpertiseQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorExpertise>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorExpertise>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorExpertise, dynamic>("DoctorExpertise_GetAll", new
                {
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorExpertiseResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorExpertise>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorExpertise>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorExpertise, dynamic>("DoctorExpertise_GetById", new
                {
                    DoctorExpertiseID = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorExpertiseResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorExpertise>>> GetByExpertiseId(int expertiseId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorExpertise>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorExpertise, dynamic>("DoctorExpertise_GetByExpertiseId", new
                {
                    DoctorExpertiseID = expertiseId
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorExpertiseResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }
    }
}

