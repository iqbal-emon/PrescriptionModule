using Speciality.Domain.Repositories.Speciality;
using Speciality.Dtos.RequestDto.SpecialityDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Speciality.Application.Services
{
    public class SpecialityService
    {
        private readonly ISpecialityQueryRepository _specialityQueryRepository;
        private readonly ISpecialityCommandRepository _specialityCommandRepository;
        private readonly MapperService _mapperService;

        public SpecialityService(
            ISpecialityQueryRepository specialityQueryRepository,
            ISpecialityCommandRepository specialityCommandRepository,
            MapperService mapperService)
        {
            _specialityQueryRepository = specialityQueryRepository;
            _specialityCommandRepository = specialityCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Speciality>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Speciality>>();

            try
            {
                var speciality = await _specialityQueryRepository.GetAll();

                if (speciality == null)
                {
                    ResponseHelper.SetFailedResponse(response, speciality.Result, speciality.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, speciality.Result, speciality.Message, StatusResponseMessage.success, speciality.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Speciality.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Speciality>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Speciality>();

            try
            {
                var speciality = await _specialityQueryRepository.GetById(id);

                if (speciality == null)
                {
                    ResponseHelper.SetFailedResponse(response, speciality.Result, speciality.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, speciality.Result, speciality.Message, StatusResponseMessage.success, speciality.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Speciality.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(SpecialityInsertRequestDto speciality)
        {
            var response = new Response<int>();

            try
            {
                var specialityEntity = await _mapperService.MapSingle<SpecialityInsertRequestDto, Entities.EntityClass.Speciality>(speciality);
                specialityEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _specialityCommandRepository.Insert(specialityEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the Speciality.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the Speciality.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(SpecialityUpdateRequestDto speciality)
        {
            var response = new Response<int>();

            try
            {
                var specialityEntity = await _mapperService.MapSingle<SpecialityUpdateRequestDto, Entities.EntityClass.Speciality>(speciality);
                specialityEntity.UpdatedAt = DateTime.Now;
                specialityEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _specialityCommandRepository.Update(specialityEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the Speciality.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the Speciality.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _specialityCommandRepository.Delete(id);
        }
    }
}

