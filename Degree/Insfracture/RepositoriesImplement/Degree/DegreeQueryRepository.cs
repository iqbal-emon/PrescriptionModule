using DataAccess.DatabaseAccessLayer;
using Degree.Domain.Repositories.Degree;
using Degree.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Degree.Insfracture.RepositoriesImplement.Degree
{
    public class DegreeQueryRepository : IDegreeQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DegreeQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Degrees
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.Degree>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.Degree>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.Degree, dynamic>("Degree_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DegreeResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Degree by ID
        public async Task<Response<Entities.EntityClass.DoctorEntity.Degree>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.Degree>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.Degree, dynamic>("Degree_GetById", new
                {
                    DegreeID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DegreeResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.Degree>>> GetAllDegreeName(string degreeName)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.Degree>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.Degree, dynamic>("Degree_GetDegreesByName", new
                {
                    DegreeName = degreeName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DegreeResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
