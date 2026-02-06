using Doctor.Domain.Repositories.DoctorScheduledDayOff;
using Doctor.Dtos.RequestDto.DoctorScheduledDayOffDto;
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
    public class DoctorScheduledDayOffService
    {
        private readonly IDoctorScheduledDayOffQueryRepository _dayOffQueryRepository;
        private readonly IDoctorScheduledDayOffCommandRepository _dayOffCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorScheduledDayOffService(
            IDoctorScheduledDayOffQueryRepository dayOffQueryRepository,
            IDoctorScheduledDayOffCommandRepository dayOffCommandRepository,
            MapperService mapperService)
        {
            _dayOffQueryRepository = dayOffQueryRepository;
            _dayOffCommandRepository = dayOffCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>>();

            try
            {
                var dayOffs = await _dayOffQueryRepository.GetAll();

                if (dayOffs == null)
                {
                    ResponseHelper.SetFailedResponse(response, dayOffs.Result, dayOffs.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, dayOffs.Result, dayOffs.Message, StatusResponseMessage.success, dayOffs.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorScheduledDayOffs.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>();

            try
            {
                var dayOff = await _dayOffQueryRepository.GetById(id);

                if (dayOff == null)
                {
                    ResponseHelper.SetFailedResponse(response, dayOff.Result, dayOff.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, dayOff.Result, dayOff.Message, StatusResponseMessage.success, dayOff.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorScheduledDayOff.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorScheduledDayOffInsertRequestDto dayOff)
        {
            var response = new Response<int>();

            try
            {
                var dayOffEntity = await _mapperService.MapSingle<DoctorScheduledDayOffInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>(dayOff);
                dayOffEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _dayOffCommandRepository.Insert(dayOffEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorScheduledDayOff.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorScheduledDayOff.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorScheduledDayOffUpdateRequestDto dayOff)
        {
            var response = new Response<int>();

            try
            {
                var dayOffEntity = await _mapperService.MapSingle<DoctorScheduledDayOffUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>(dayOff);
                dayOffEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _dayOffCommandRepository.Update(dayOffEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorScheduledDayOff.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorScheduledDayOff.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _dayOffCommandRepository.Delete(id);
        }
    }
}

