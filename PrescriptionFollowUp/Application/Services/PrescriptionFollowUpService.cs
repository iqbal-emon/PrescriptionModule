using Microsoft.AspNetCore.Http;
using PrescriptionFollowUp.Domain.Repositories.PrescriptionFollowUp;
using PrescriptionFollowUp.Dtos.RequestDto.PrescriptionFollowUpDto;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace PrescriptionFollowUp.Application.Services
{
    public class PrescriptionFollowUpService
    {
        private readonly IPrescriptionFollowUpQueryRepository _prescriptionFollowUpQueryRepository;
        private readonly IPrescriptionFollowUpCommandRepository _prescriptionFollowUpCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionFollowUpService(
            IPrescriptionFollowUpQueryRepository prescriptionFollowUpQueryRepository,
            IPrescriptionFollowUpCommandRepository prescriptionFollowUpCommandRepository,
            MapperService mapperService)
        {
            _prescriptionFollowUpQueryRepository = prescriptionFollowUpQueryRepository;
            _prescriptionFollowUpCommandRepository = prescriptionFollowUpCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>>();
            try
            {
                var followUps = await _prescriptionFollowUpQueryRepository.GetAll();

                if (followUps == null)
                {
                    ResponseHelper.SetFailedResponse(response, followUps.Result, followUps.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, followUps.Result, followUps.Message, StatusResponseMessage.success, followUps.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving prescription follow-ups.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>();
            try
            {
                var followUp = await _prescriptionFollowUpQueryRepository.GetById(id);

                if (followUp == null)
                {
                    ResponseHelper.SetFailedResponse(response, followUp.Result, followUp.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, followUp.Result, followUp.Message, StatusResponseMessage.success, followUp.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription follow-up.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionFollowUpInsertRequestDto followUp)
        {
            var response = new Response<int>();
            try
            {
                var followUpEntity = await _mapperService.MapSingle<PrescriptionFollowUpInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>(followUp);
                var insertResponse = await _prescriptionFollowUpCommandRepository.Insert(followUpEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the prescription follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the prescription follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(PrescriptionFollowUpUpdateRequestDto followUp)
        {
            var response = new Response<int>();
            try
            {
                var followUpEntity = await _mapperService.MapSingle<PrescriptionFollowUpUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>(followUp);
                followUpEntity.UpdatedAt = DateTime.Now;
                followUpEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionFollowUpCommandRepository.Update(followUpEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the prescription follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the prescription follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionFollowUpCommandRepository.Delete(id);
        }
    }
}
