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
using Microsoft.AspNetCore.Mvc;
using PrescriptionDiagonosis.Dtos.RequestDto.PrescriptionDiagonosisDto;
using PrescriptionDiagonosis.Domain.Repositories.PrescriptionDiagonosis;

namespace Diagonosis.Application.Services
{
    public class PrescriptionDiagonsisService
    {
        private readonly IPrescriptionDiagonsisQueryRepository _prescriptionDiagonsisQueryRepository;
        private readonly IPrescriptionDiagonsisCommandRepository _prescriptionDiagonsisCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionDiagonsisService(IPrescriptionDiagonsisQueryRepository prescriptionDiagnosisQueryRepository, IPrescriptionDiagonsisCommandRepository prescriptionDiagnosisCommandRepository,
            MapperService mapperService)
        {
            _prescriptionDiagonsisQueryRepository = prescriptionDiagnosisQueryRepository;
            _prescriptionDiagonsisCommandRepository = prescriptionDiagnosisCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>>();

            try
            {
                var diagonosis = await _prescriptionDiagonsisQueryRepository.GetAll();

                if (diagonosis == null)
                {
                    ResponseHelper.SetFailedResponse(response, diagonosis.Result, diagonosis.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diagonosis.Result, diagonosis.Message, StatusResponseMessage.success, diagonosis.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Prescription Diagnosisyy.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>();

            try
            {
                var diagonosis = await _prescriptionDiagonsisQueryRepository.GetById(id);

                if (diagonosis == null)
                {
                    ResponseHelper.SetFailedResponse(response, diagonosis.Result, diagonosis.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diagonosis.Result, diagonosis.Message, StatusResponseMessage.success, diagonosis.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Prescription Diagnosis.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionDiagonsisInsertRequestDto diagonosis)
        {
            var response = new Response<int>();

            try
            {
                var diagonosisEntity = await _mapperService.MapSingle<PrescriptionDiagonsisInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>(diagonosis);

                var insertResponse = await _prescriptionDiagonsisCommandRepository.Insert(diagonosisEntity);
                response = insertResponse;




            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the Prescription Diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the Prescription Diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PrescriptionDiagonsisUpdateRequestDto diagonosis)
        {
            var response = new Response<int>();

            try
            {
                var diagonosisEntity = await _mapperService.MapSingle<PrescriptionDiagonsisUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>(diagonosis);
                diagonosisEntity.UpdatedAt = DateTime.Now;
                diagonosisEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionDiagonsisCommandRepository.Update(diagonosisEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the  Prescription Diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the diagonosis Diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionDiagonsisCommandRepository.Delete(id);
        }
    }
}
