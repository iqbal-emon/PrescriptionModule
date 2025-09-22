using Microsoft.AspNetCore.Http;
using PrescriptionPdf.Domain.Repositories;
using PrescriptionPdf.Domain.Repositories.PrescriptionPdf;
using PrescriptionPdf.Dtos.RequestDto;
using PrescriptionPdf.Dtos.RequestDto.PrescriptionPdfDto;
using PrescriptionPdf.Dtos.ResponseDto.PrescriptionPdfDto;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace PrescriptionPdf.Application.Services
{
    public class PrescriptionPdfService
    {
        private readonly IPrescriptionPdfQueryRepository _prescriptionPdfQueryRepository;
        private readonly IPrescriptionPdfCommandRepository _prescriptionPdfCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionPdfService(IPrescriptionPdfQueryRepository prescriptionPdfQueryRepository, IPrescriptionPdfCommandRepository prescriptionPdfCommandRepository,
            MapperService mapperService)
        {
            _prescriptionPdfQueryRepository = prescriptionPdfQueryRepository;
            _prescriptionPdfCommandRepository = prescriptionPdfCommandRepository;
            _mapperService = mapperService;
        }




        public async Task<Response<List<PrescriptionPdfPatientResponseDto>>> GetByPatientDoctorId(int patientId,int doctorId)
        {
            var response = new Response<List<PrescriptionPdfPatientResponseDto>>();

            try
            {
                var prescriptions = await _prescriptionPdfQueryRepository.GetByPatientDoctorId(patientId, doctorId);

                if (prescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, prescriptions.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescriptions.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PrescriptionPdfPatientResponseDto>>> GetPrehandByDoctorId(int doctorId)
        {
            var response = new Response<List<PrescriptionPdfPatientResponseDto>>();

            try
            {
                var prescriptions = await _prescriptionPdfQueryRepository.GetPrehandByDoctorId(doctorId);

                if (prescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptions.Result, prescriptions.Message, StatusResponseMessage.success, prescriptions.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescriptions.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }





        

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>();

            try
            {
                var prescription = await _prescriptionPdfQueryRepository.GetById(id);

                if (prescription == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescription.Result, prescription.Message, StatusResponseMessage.success, prescription.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionPdfInsertRequestDto prescription)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionEntity = await _mapperService.MapSingle<PrescriptionPdfInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>(prescription);
                var insertResponse = await _prescriptionPdfCommandRepository.Insert(prescriptionEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PrescriptionPdfUpdateRequestDto prescription)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionEntity = await _mapperService.MapSingle<PrescriptionPdfUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>(prescription);
                prescriptionEntity.UpdatedAt = DateTime.Now;
                var updateResponse = await _prescriptionPdfCommandRepository.Update(prescriptionEntity);
                response = updateResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the prescription.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionPdfCommandRepository.Delete(id);
        }
    }
}
