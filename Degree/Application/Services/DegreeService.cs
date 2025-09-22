using Degree.Domain.Repositories.Degree;
using Degree.Dtos.RequestDto.DegreeDto;
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

namespace Degree.Application.Services
{
    public class Degreervice
    {
        private readonly IDegreeQueryRepository _degreeQueryRepository;
        private readonly IDegreeCommandRepository _degreeCommandRepository;
        private readonly MapperService _mapperService;

        public Degreervice(
            IDegreeQueryRepository degreeQueryRepository,
            IDegreeCommandRepository degreeCommandRepository,
            MapperService mapperService)
        {
            _degreeQueryRepository = degreeQueryRepository;
            _degreeCommandRepository = degreeCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.Degree>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.Degree>>();

            try
            {
                var Degree = await _degreeQueryRepository.GetAll();

                if (Degree == null)
                {
                    ResponseHelper.SetFailedResponse(response, Degree.Result, Degree.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, Degree.Result, Degree.Message, StatusResponseMessage.success, Degree.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Degree.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.Degree>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.Degree>();

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
                response.Message = "A database error occurred while retrieving the degree.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DegreeInsertRequestDto degree)
        {
            var response = new Response<int>();

            try
            {
                var degreeEntity = await _mapperService.MapSingle<DegreeInsertRequestDto, Entities.EntityClass.DoctorEntity.Degree>(degree);
                degreeEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _degreeCommandRepository.Insert(degreeEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the degree.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the degree.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DegreeUpdateRequestDto degree)
        {
            var response = new Response<int>();

            try
            {
                var degreeEntity = await _mapperService.MapSingle<DegreeUpdateRequestDto, Entities.EntityClass.DoctorEntity.Degree>(degree);
                degreeEntity.UpdatedAt = DateTime.Now;
                degreeEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _degreeCommandRepository.Update(degreeEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the degree.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the degree.";
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