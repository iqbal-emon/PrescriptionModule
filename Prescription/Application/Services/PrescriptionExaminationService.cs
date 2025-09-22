using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionExamination;
using Prescription.Dtos.RequestDto.PrescriptionExaminationDto;
using Prescription.Dtos.ResponseDto.PrescriptionExaminationDto;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace PrescriptionExamination.Application.Services
{
    public class PrescriptionExaminationService
    {
        private readonly IPrescriptionExminationQueryRepository _prescriptionExaminationQueryRepository;
        private readonly IPrescriptionExminationCommandRepository _prescriptionExaminationCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionExaminationService(
            IPrescriptionExminationQueryRepository prescriptionExaminationQueryRepository,
            IPrescriptionExminationCommandRepository prescriptionExaminationCommandRepository,
            MapperService mapperService)
        {
            _prescriptionExaminationQueryRepository = prescriptionExaminationQueryRepository;
            _prescriptionExaminationCommandRepository = prescriptionExaminationCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>>();

            try
            {
                var prescriptionExaminations = await _prescriptionExaminationQueryRepository.GetAll();

                if (prescriptionExaminations == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionExaminations.Result, prescriptionExaminations.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionExaminations.Result, prescriptionExaminations.Message, StatusResponseMessage.success, prescriptionExaminations.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription examinations.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>();

            try
            {
                var prescriptionExamination = await _prescriptionExaminationQueryRepository.GetById(id);

                if (prescriptionExamination == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionExamination.Result, prescriptionExamination.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionExamination.Result, prescriptionExamination.Message, StatusResponseMessage.success, prescriptionExamination.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription examination.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PrescriptionExaminationResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionExaminationResponseDto>>();
            try
            {
                var prescriptionExaminations = await _prescriptionExaminationQueryRepository.GetByPrescriptionId(id);
                if (prescriptionExaminations == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionExaminations.Result, prescriptionExaminations.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionExaminations.Result, prescriptionExaminations.Message, StatusResponseMessage.success, prescriptionExaminations.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription examinations.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionExaminationInsertRequestDto prescriptionExamination)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionExaminationEntity = await _mapperService.MapSingle<PrescriptionExaminationInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>(prescriptionExamination);
                var insertResponse = await _prescriptionExaminationCommandRepository.Insert(prescriptionExaminationEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the prescription examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the prescription examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PrescriptionExaminationUpdateRequestDto prescriptionExamination)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionExaminationEntity = await _mapperService.MapSingle<PrescriptionExaminationUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>(prescriptionExamination);
                prescriptionExaminationEntity.UpdatedAt = DateTime.Now;
                prescriptionExaminationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionExaminationCommandRepository.Update(prescriptionExaminationEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the prescription examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the prescription examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionExaminationCommandRepository.Delete(id);
        }
    }
}
