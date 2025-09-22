using Microsoft.AspNetCore.Http;
using PrescriptionInvestigation.Domain.Repositories.PrescriptionInvestigation;
using PrescriptionInvestigation.Dtos.RequestDto.PrescriptionInvestigationDto;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace PrescriptionInvestigation.Application.services
{
    public class PrescriptionInvestigationService
    {
        private readonly IPrescriptionInvestigationQueryRepository _prescriptionInvestigationQueryRepository;
        private readonly IPrescriptionInvestigationCommandRepository _prescriptionInvestigationCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionInvestigationService(IPrescriptionInvestigationQueryRepository prescriptionInvestigationQueryRepository,
            IPrescriptionInvestigationCommandRepository prescriptionInvestigationCommandRepository, MapperService mapperService)
        {
            _prescriptionInvestigationQueryRepository = prescriptionInvestigationQueryRepository;
            _prescriptionInvestigationCommandRepository = prescriptionInvestigationCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>>();
            try
            {
                var prescriptionInvestigations = await _prescriptionInvestigationQueryRepository.GetAll();
                if (prescriptionInvestigations == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionInvestigations.Result, prescriptionInvestigations.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionInvestigations.Result, prescriptionInvestigations.Message, StatusResponseMessage.success, prescriptionInvestigations.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription investigations.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>();
            try
            {
                var prescriptionInvestigation = await _prescriptionInvestigationQueryRepository.GetById(id);
                if (prescriptionInvestigation == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionInvestigation.Result, prescriptionInvestigation.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionInvestigation.Result, prescriptionInvestigation.Message, StatusResponseMessage.success, prescriptionInvestigation.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription investigation.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionInvestigationInsertRequestDto prescriptionInvestigation)
        {
            var response = new Response<int>();
            try
            {
                var prescriptionInvestigationEntity = await _mapperService.MapSingle<PrescriptionInvestigationInsertRequestDto, Entities.EntityClass.PatientEntity.PrescriptionInvestigation>(prescriptionInvestigation);
                var insertResponse = await _prescriptionInvestigationCommandRepository.Insert(prescriptionInvestigationEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the prescription investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the prescription investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(PrescriptionInvestigationUpdateRequestDto prescriptionInvestigation)
        {
            var response = new Response<int>();
            try
            {
                var prescriptionInvestigationEntity = await _mapperService.MapSingle<PrescriptionInvestigationUpdateRequestDto, Entities.EntityClass.PatientEntity.PrescriptionInvestigation>(prescriptionInvestigation);
                prescriptionInvestigationEntity.UpdatedAt = DateTime.Now;
                prescriptionInvestigationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionInvestigationCommandRepository.Update(prescriptionInvestigationEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the prescription investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the prescription investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionInvestigationCommandRepository.Delete(id);
        }
    }
}
