using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionTemplate;
using Prescription.Dtos.RequestDto.PrescriptionTemplateDto;
using Prescription.Dtos.RequestDto.ScannedPrescription;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionTemplateService
    {
        private readonly IPrescriptionTemplateQueryRepository _prescriptionTemplateQueryRepository;
        private readonly IPrescriptionTemplateCommandRepository _prescriptionTemplateCommandRepository;
        private readonly MapperService _mapperService;
        public PrescriptionTemplateService(IPrescriptionTemplateCommandRepository prescriptionTemplateCommandRepository, 
            IPrescriptionTemplateQueryRepository prescriptionTemplateQueryRepository,
            MapperService mapperService)
        {
            _prescriptionTemplateCommandRepository = prescriptionTemplateCommandRepository;
            _prescriptionTemplateQueryRepository = prescriptionTemplateQueryRepository;
            _mapperService = mapperService;
        }


        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>>();
            try
            {
                var prescriptionTemplates = await _prescriptionTemplateQueryRepository.GetAll();
                if (prescriptionTemplates == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionTemplates.Result, prescriptionTemplates.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionTemplates.Result, prescriptionTemplates.Message, StatusResponseMessage.success, prescriptionTemplates.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the scanned prescriptions.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }
        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>>> GetPrescriptionTemplatesByDoctorId(int doctorId,string Name)
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>>();
            try
            {
                var prescriptionTemplates = await _prescriptionTemplateQueryRepository.GetPrescriptionTemplatesByDoctorId(doctorId, Name);

                if (prescriptionTemplates == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionTemplates.Result, prescriptionTemplates.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionTemplates.Result, prescriptionTemplates.Message, StatusResponseMessage.success, prescriptionTemplates.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the scanned prescriptions.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>();
            try
            {
                var prescriptionTemplate = await _prescriptionTemplateQueryRepository.GetById(id);
                if (prescriptionTemplate == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionTemplate.Result, prescriptionTemplate.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionTemplate.Result, prescriptionTemplate.Message, StatusResponseMessage.success, prescriptionTemplate.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the scanned prescription.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionTemplateInsertRequestDto scannedPrescription)
        {
            var response = new Response<int>();
            try
            {
                var prescriptionTemplateEntity = await _mapperService.MapSingle<PrescriptionTemplateInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>(scannedPrescription);
                var insertResponse = await _prescriptionTemplateCommandRepository.Insert(prescriptionTemplateEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the scanned prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the scanned prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(PrescriptionTemplateUpdateRequestDto scannedPrescription)
        {
            var response = new Response<int>();
            try
            {
                var prescriptionTemplateEntity = await _mapperService.MapSingle<PrescriptionTemplateUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>(scannedPrescription);
                prescriptionTemplateEntity.UpdatedAt = DateTime.Now;
                prescriptionTemplateEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionTemplateCommandRepository.Update(prescriptionTemplateEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the scanned prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the scanned prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionTemplateCommandRepository.Delete(id);
        }
    }
}
