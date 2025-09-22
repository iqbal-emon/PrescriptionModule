
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Diseases.Dtos.RequestDto.DiseasesDto;
using Diseases.Domain.Repositories.Diseases;

namespace DoctorPrescription.Application.Services
{
    public class DiseasesService
    {
        private readonly IDiseasesQueryRepository _diseaseQueryRepository;
        private readonly IDiseasesCommandRepository _diseaseCommandRepository;
        private readonly MapperService _mapperService;

        public DiseasesService(
            IDiseasesQueryRepository diseaseQueryRepository,
            IDiseasesCommandRepository diseaseCommandRepository,
            MapperService mapperService)
        {
            _diseaseQueryRepository = diseaseQueryRepository;
            _diseaseCommandRepository = diseaseCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Disease>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Disease>>();

            try
            {
                var diseases = await _diseaseQueryRepository.GetAll();

                if (diseases == null)
                {
                    ResponseHelper.SetFailedResponse(response, diseases.Result, diseases.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diseases.Result, diseases.Message, StatusResponseMessage.success, diseases.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the diseases.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Disease>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Disease>();

            try
            {
                var disease = await _diseaseQueryRepository.GetById(id);

                if (disease == null)
                {
                    ResponseHelper.SetFailedResponse(response, disease.Result, disease.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, disease.Result, disease.Message, StatusResponseMessage.success, disease.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the disease.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(DiseasesInsertRequestDto disease)
        {
            var response = new Response<int>();

            try
            {
                var diseaseEntity = await _mapperService.MapSingle<DiseasesInsertRequestDto, Entities.EntityClass.Disease>(disease);

                var insertResponse = await _diseaseCommandRepository.Insert(diseaseEntity);

                response = insertResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the disease.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the disease.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(DiseasesUpdateRequestDto disease)
        {
            var response = new Response<int>();

            try
            {
                var diseaseEntity = await _mapperService.MapSingle<DiseasesUpdateRequestDto, Entities.EntityClass.Disease>(disease);
                diseaseEntity.UpdatedAt = DateTime.Now;
                diseaseEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _diseaseCommandRepository.Update(diseaseEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the disease.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the disease.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _diseaseCommandRepository.Delete(id);
        }
    }
}
