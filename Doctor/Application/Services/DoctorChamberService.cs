using Doctor.Domain.Repositories.DoctorChamber;
using Doctor.Dtos.RequestDto.DoctorChamberDto;
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

namespace Doctor.Application.Services
{
    public class DoctorChamberService
    {
        private readonly IDoctorChamberQueryRepository _chamberQueryRepository;
        private readonly IDoctorChamberCommandRepository _chamberCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorChamberService(
            IDoctorChamberQueryRepository chamberQueryRepository,
            IDoctorChamberCommandRepository chamberCommandRepository,
            MapperService mapperService)
        {
            _chamberQueryRepository = chamberQueryRepository;
            _chamberCommandRepository = chamberCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>();

            try
            {
                var chambers = await _chamberQueryRepository.GetAll();

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
                var chamber = await _chamberQueryRepository.GetById(id);

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
                var insertResponse = await _chamberCommandRepository.Insert(chamberEntity);

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
                var updatedResponse = await _chamberCommandRepository.Update(chamberEntity);

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
            return _chamberCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>();

            try
            {
                var chambers = await _chamberQueryRepository.GetByDoctorId(doctorId);

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
                response.Message = "A database error occurred while retrieving the DoctorChambers by doctor ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.CountryEntity.District>>> GetAllDistrict(int divisonId)
        {
            var response = new Response<List<Entities.CountryEntity.District>>();

            try
            {
                var result = await _chamberQueryRepository.GetAllDistrict(divisonId);

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
                        result.Results,
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
                var result = await _chamberQueryRepository.GetAllDivision();

                if (result.Results == null || result.Results.Count == 0)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        result.Results,
                        "No divisions found",
                        StatusResponseMessage.success,
                        StatusCodes.Status400BadRequest
                    );
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(
                        response,
                        result.Results,
                        "Divisions retrieved successfully",
                        StatusResponseMessage.success,
                        StatusCodes.Status200OK
                    );
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving divisions.";
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

