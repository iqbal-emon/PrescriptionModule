
using DoctorExpertise.Domain.Repositories.DoctorExpertise;
using DoctorExpertise.Dtos.RequestDto.DoctorExpertiseDto;
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

namespace DoctorExpertise.Application.Services
{
    public class DoctorExpertiseService
    {
        private readonly IDoctorExpertiseQueryRepository _degreeQueryRepository;
        private readonly IDoctorExpertiseCommandRepository _degreeCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorExpertiseService(
            IDoctorExpertiseQueryRepository degreeQueryRepository,
            IDoctorExpertiseCommandRepository degreeCommandRepository,
            MapperService mapperService)
        {
            _degreeQueryRepository = degreeQueryRepository;
            _degreeCommandRepository = degreeCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorExpertise>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorExpertise>>();

            try
            {
                var expertises = await _degreeQueryRepository.GetAll();

                if (expertises == null)
                {
                    ResponseHelper.SetFailedResponse(response, expertises.Result, expertises.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, expertises.Result, expertises.Message, StatusResponseMessage.success, expertises.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorExpertises.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorExpertise>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorExpertise>();

            try
            {
                var expertise = await _degreeQueryRepository.GetById(id);

                if (expertise == null)
                {
                    ResponseHelper.SetFailedResponse(response, expertise.Result, expertise.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, expertise.Result, expertise.Message, StatusResponseMessage.success, expertise.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorExpertise.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorExpertiseInsertRequestDto expertise)
        {
            var response = new Response<int>();

            try
            {
                var expertiseEntity = await _mapperService.MapSingle<DoctorExpertiseInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorExpertise>(expertise);
                expertiseEntity.CreatedAt = DateTime.Today;
                var insertResponse = await _degreeCommandRepository.Insert(expertiseEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorExpertise.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorExpertise.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorExpertiseUpdateRequestDto expertise)
        {
            var response = new Response<int>();

            try
            {
                var expertiseEntity = await _mapperService.MapSingle<DoctorExpertiseUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorExpertise>(expertise);
                expertiseEntity.UpdatedAt = DateTime.Now;
                expertiseEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _degreeCommandRepository.Update(expertiseEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorExpertise.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorExpertise.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _degreeCommandRepository.Delete(id);
        }
    }
}
