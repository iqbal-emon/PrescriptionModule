using Doctor.Domain.Repositories.DoctorFeesSetup;
using Doctor.Dtos.RequestDto.DoctorFeesSetupDto;
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
    public class DoctorFeesSetupService
    {
        private readonly IDoctorFeesSetupQueryRepository _feesSetupQueryRepository;
        private readonly IDoctorFeesSetupCommandRepository _feesSetupCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorFeesSetupService(
            IDoctorFeesSetupQueryRepository feesSetupQueryRepository,
            IDoctorFeesSetupCommandRepository feesSetupCommandRepository,
            MapperService mapperService)
        {
            _feesSetupQueryRepository = feesSetupQueryRepository;
            _feesSetupCommandRepository = feesSetupCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>>();

            try
            {
                var feesSetups = await _feesSetupQueryRepository.GetAll();

                if (feesSetups == null)
                {
                    ResponseHelper.SetFailedResponse(response, feesSetups.Result, feesSetups.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, feesSetups.Result, feesSetups.Message, StatusResponseMessage.success, feesSetups.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorFeesSetups.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>();

            try
            {
                var feesSetup = await _feesSetupQueryRepository.GetById(id);

                if (feesSetup == null)
                {
                    ResponseHelper.SetFailedResponse(response, feesSetup.Result, feesSetup.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, feesSetup.Result, feesSetup.Message, StatusResponseMessage.success, feesSetup.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorFeesSetup.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorFeesSetupInsertRequestDto feesSetup)
        {
            var response = new Response<int>();

            try
            {
                var feesSetupEntity = await _mapperService.MapSingle<DoctorFeesSetupInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorFeesSetup>(feesSetup);
                feesSetupEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _feesSetupCommandRepository.Insert(feesSetupEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorFeesSetup.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorFeesSetup.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorFeesSetupUpdateRequestDto feesSetup)
        {
            var response = new Response<int>();

            try
            {
                var feesSetupEntity = await _mapperService.MapSingle<DoctorFeesSetupUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorFeesSetup>(feesSetup);
                feesSetupEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _feesSetupCommandRepository.Update(feesSetupEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorFeesSetup.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorFeesSetup.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _feesSetupCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>>();

            try
            {
                var feesSetups = await _feesSetupQueryRepository.GetByDoctorId(doctorId);

                if (feesSetups == null)
                {
                    ResponseHelper.SetFailedResponse(response, feesSetups.Result, feesSetups.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, feesSetups.Result, feesSetups.Message, StatusResponseMessage.success, feesSetups.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorFeesSetups by doctor ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}

