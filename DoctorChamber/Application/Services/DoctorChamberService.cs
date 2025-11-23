using DoctorChamber.Domain.Repositories.DoctorChamber;
using DoctorChamber.Dtos.RequestDto.DoctorChamberDto;
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

namespace DoctorChamber.Application.Services
{
    public class DoctorChamberService
    {
        private readonly IDoctorChamberQueryRepository _degreeQueryRepository;
        private readonly IDoctorChamberCommandRepository _degreeCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorChamberService(
            IDoctorChamberQueryRepository degreeQueryRepository,
            IDoctorChamberCommandRepository degreeCommandRepository,
            MapperService mapperService)
        {
            _degreeQueryRepository = degreeQueryRepository;
            _degreeCommandRepository = degreeCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>();

            try
            {
                var chambers = await _degreeQueryRepository.GetAll();

                if (chambers == null)
                {
                    ResponseHelper.SetFailedResponse(response, chambers.Result, chambers.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, chambers.Result, chambers.Message, StatusResponseMessage.success, chambers.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorChambers.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorChamber>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorChamber>();

            try
            {
                var chamber = await _degreeQueryRepository.GetById(id);

                if (chamber == null)
                {
                    ResponseHelper.SetFailedResponse(response, chamber.Result, chamber.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, chamber.Result, chamber.Message, StatusResponseMessage.success, chamber.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorChamber.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorChamberInsertRequestDto chamber)
        {
            var response = new Response<int>();

            try
            {
                var chamberEntity = await _mapperService.MapSingle<DoctorChamberInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorChamber>(chamber);
                chamberEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _degreeCommandRepository.Insert(chamberEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorChamber.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorChamber.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorChamberUpdateRequestDto chamber)
        {
            var response = new Response<int>();

            try
            {
                var chamberEntity = await _mapperService.MapSingle<DoctorChamberUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorChamber>(chamber);
                chamberEntity.UpdatedAt = DateTime.Now;
                chamberEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _degreeCommandRepository.Update(chamberEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorChamber.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorChamber.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
       
        public Task<Response<bool>> Delete(int id)
        {
            return _degreeCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.CountryEntity.District>>> GetAllDistrict(int divisonId)
        {
            var response = new Response<List<Entities.CountryEntity.District>>();

            try
            {
                var result = await _degreeQueryRepository.GetAllDistrict(divisonId);

                if (result.Results == null || result.Results.Count == 0)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        result.Results,
                        "No districts found",
                        StatusResponseMessage.success,
                        StatusCodes.Status400BadRequest
                    );
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(
                        response,
                        result.Results, // removed .Result
                        "Districts retrieved successfully",
                        StatusResponseMessage.success,
                        StatusCodes.Status200OK
                    );
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving districts.";
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }

            return response;
        }

        public async Task<Response<List<Entities.CountryEntity.Division>>> GetAllDivision()
        {
            var response = new Response<List<Entities.CountryEntity.Division>>();

            try
            {
                var result = await _degreeQueryRepository.GetAllDivision();

                if (result.Results == null || result.Results.Count == 0)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        result.Results,
                        "No districts found",
                        StatusResponseMessage.success,
                        StatusCodes.Status400BadRequest
                    );
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(
                        response,
                        result.Results, // removed .Result
                        "Districts retrieved successfully",
                        StatusResponseMessage.success,
                        StatusCodes.Status200OK
                    );
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving districts.";
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }

            return response;
        }

    }
}
