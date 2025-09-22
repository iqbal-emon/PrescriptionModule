using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Utility;
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
using ExpertiseCategory.Domain.Repositories.ExpertiseCategory;

namespace DoctorChamber.Insfracture.RepositoriesImplement.DoctorChamber
{
    public class ExpertiseCategoryQueryRepository : IExpertiseCategoryQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public ExpertiseCategoryQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Expertise Categories
        public async Task<Response<List<Entities.EntityClass.DoctorEntity.ExpertiseCategory>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.ExpertiseCategory>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.ExpertiseCategory, dynamic>("ExpertiseCategory_GetAll", new { });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ExpertiseCategoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Expertise Category by ID
        public async Task<Response<Entities.EntityClass.DoctorEntity.ExpertiseCategory>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.ExpertiseCategory>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.ExpertiseCategory, dynamic>("ExpertiseCategory_GetById", new
                {
                    CategoryId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ExpertiseCategoryResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Expertise Categories by Name
        public async Task<Response<List<Entities.EntityClass.DoctorEntity.ExpertiseCategory>>> GetAllByName(string categoryName)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.ExpertiseCategory>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.ExpertiseCategory, dynamic>("ExpertiseCategory_GetByName", new
                {
                    CategoryName = categoryName
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ExpertiseCategoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
