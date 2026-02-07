using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.Prescription;
using Prescription.Dtos.RequestDto.PrescriptionDto;
using Prescription.Dtos.ResponseDto.PrescriptionDto;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionService
    {
        private readonly IPrescriptionQueryRepository _prescriptionQueryRepository;
        private readonly IPrescriptionCommandRepository _prescriptionCommandRepository;
        private readonly MapperService _mapperService;
        public PrescriptionService(IPrescriptionQueryRepository prescriptionQueryRepository, IPrescriptionCommandRepository prescriptionCommandRepository,
            MapperService mapperService)
        {
            _prescriptionQueryRepository = prescriptionQueryRepository;
            _prescriptionCommandRepository = prescriptionCommandRepository;
            _mapperService = mapperService;
        }
        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>();

            try
            {
                var prescription = await _prescriptionQueryRepository.GetAll();

                if (prescription == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, prescription.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
        public async Task<Response<Entities.EntityClass.PrescriptionEntity.Prescription>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.Prescription>();

            try
            {
                var prescription = await _prescriptionQueryRepository.GetById(id);

                if (prescription == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, prescription.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }


        public async Task<Response<PrescriptionAnalyticsDto>> GetAnalytics()
        {
            var response = new Response<PrescriptionAnalyticsDto>();

            try
            {
                var prescription = await _prescriptionQueryRepository.GetAnalytics();

                if (prescription == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, prescription.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }



        public async Task<Response<int>> Insert(PrescriptionInsertRequestDto prescription)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionEntity = await _mapperService.MapSingle<PrescriptionInsertRequestDto, Entities.EntityClass.PrescriptionEntity.Prescription>(prescription);

                var insertResponse = await _prescriptionCommandRepository.Insert(prescriptionEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PrescriptionUpdateRequestDto prescription)
        {

            var response = new Response<int>();

            try
            {
                var precriptionEntity = await _mapperService.MapSingle<PrescriptionUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.Prescription>(prescription);
                precriptionEntity.UpdatedAt = DateTime.Now;
                precriptionEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionCommandRepository.Update(precriptionEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                response.Message = "A database error occurred while updating the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }

            return response;

        }
        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByPatientId(int patientId)
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>();

            try
            {
                var prescriptions = await _prescriptionQueryRepository.GetByPatientId(patientId);

                if (prescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, prescriptions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescriptions by patient ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>();

            try
            {
                var prescriptions = await _prescriptionQueryRepository.GetByDoctorId(doctorId);

                if (prescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, prescriptions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescriptions by doctor ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByDoctorIdAndPatientId(int doctorId, int patientId)
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>();

            try
            {
                var prescriptions = await _prescriptionQueryRepository.GetByDoctorIdAndPatientId(doctorId, patientId);

                if (prescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, prescriptions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescriptions by doctor and patient ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByAppointmentCreatorId(int patientId)
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>();

            try
            {
                var prescriptions = await _prescriptionQueryRepository.GetByAppointmentCreatorId(patientId);

                if (prescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, prescriptions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescriptions by appointment creator ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<object>>> GetPatientDiseaseList(int patientId)
        {
            var response = new Response<List<object>>();

            try
            {
                var diseaseList = await _prescriptionQueryRepository.GetPatientDiseaseList(patientId);

                if (diseaseList == null)
                {
                    ResponseHelper.SetFailedResponse(response, diseaseList.Result, diseaseList.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diseaseList.Result, diseaseList.Message, StatusResponseMessage.success, diseaseList.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patient disease list.";
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
