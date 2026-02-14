using DataAccess.DatabaseAccessLayer;
using Doctor.Domain.Repositories.DigitalSignature;
using Doctor.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DigitalSignature
{
    public class DigitalSignatureQueryRepository : IDigitalSignatureQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DigitalSignatureQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DigitalSignature>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DigitalSignature>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DigitalSignature, dynamic>("DigitalSignature_GetAll", new { });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DigitalSignatureResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DigitalSignature>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DigitalSignature>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DigitalSignature, dynamic>("DigitalSignature_GetById", new
                {
                    DigitalSignatureID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DigitalSignatureResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DigitalSignature>> GetByDoctorId(int doctorId)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DigitalSignature>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DigitalSignature, dynamic>("DigitalSignature_GetByDoctorId", new
                {
                    DoctorID = doctorId
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DigitalSignatureResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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

