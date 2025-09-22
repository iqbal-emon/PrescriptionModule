
using DoctorDegree.Domain.Repositories.DoctorDegree;
using DoctorDegree.Dtos.RequestDto.DoctorDegreeDto;
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

namespace DoctorDegree.Application.Services
{
    public class DoctorDegreeService
    {
        private readonly IDoctorDegreeQueryRepository _degreeQueryRepository;
        private readonly IDoctorDegreeCommandRepository _degreeCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorDegreeService(
            IDoctorDegreeQueryRepository degreeQueryRepository,
            IDoctorDegreeCommandRepository degreeCommandRepository,
            MapperService mapperService)
        {
            _degreeQueryRepository = degreeQueryRepository;
            _degreeCommandRepository = degreeCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorDegree>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorDegree>>();

            try
            {
                var degrees = await _degreeQueryRepository.GetAll();

                if (degrees == null)
                {
                    ResponseHelper.SetFailedResponse(response, degrees.Result, degrees.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, degrees.Result, degrees.Message, StatusResponseMessage.success, degrees.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorDegrees.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorDegree>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorDegree>();

            try
            {
                var degree = await _degreeQueryRepository.GetById(id);

                if (degree == null)
                {
                    ResponseHelper.SetFailedResponse(response, degree.Result, degree.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, degree.Result, degree.Message, StatusResponseMessage.success, degree.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorDegree.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorDegreeInsertRequestDto degree)
        {
            var response = new Response<int>();

            try
            {
                var degreeEntity = await _mapperService.MapSingle<DoctorDegreeInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorDegree>(degree);
                degreeEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _degreeCommandRepository.Insert(degreeEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorDegree.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorDegree.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorDegreeUpdateRequestDto degree)
        {
            var response = new Response<int>();

            try
            {
                var degreeEntity = await _mapperService.MapSingle<DoctorDegreeUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorDegree>(degree);
                degreeEntity.UpdatedAt = DateTime.Now;
                degreeEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _degreeCommandRepository.Update(degreeEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorDegree.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorDegree.";
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
