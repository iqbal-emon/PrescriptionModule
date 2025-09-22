using Microsoft.AspNetCore.Http;
using ScannedPrescription.Domain.Repositories.ScannedPrescription;
using ScannedPrescription.Dtos.RequestDto.ScannedPrescription;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace DoctorPrescription.Application.Services
{
    public class ScannedPrescriptionService
    {
        private readonly IScannedPrescrptionQueryRepository _scannedPrescriptionQueryRepository;
        private readonly IScannedPrescrptionCommandRepository _scannedPrescriptionCommandRepository;
        private readonly MapperService _mapperService;

        public ScannedPrescriptionService(IScannedPrescrptionQueryRepository scannedPrescriptionQueryRepository,
            IScannedPrescrptionCommandRepository scannedPrescriptionCommandRepository, MapperService mapperService)
        {
            _scannedPrescriptionQueryRepository = scannedPrescriptionQueryRepository;
            _scannedPrescriptionCommandRepository = scannedPrescriptionCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>>();
            try
            {
                var scannedPrescriptions = await _scannedPrescriptionQueryRepository.GetAll();
                if (scannedPrescriptions == null)
                {
                    ResponseHelper.SetFailedResponse(response, scannedPrescriptions.Result, scannedPrescriptions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, scannedPrescriptions.Result, scannedPrescriptions.Message, StatusResponseMessage.success, scannedPrescriptions.StatusCode);
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

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>();
            try
            {
                var scannedPrescription = await _scannedPrescriptionQueryRepository.GetById(id);
                if (scannedPrescription == null)
                {
                    ResponseHelper.SetFailedResponse(response, scannedPrescription.Result, scannedPrescription.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, scannedPrescription.Result, scannedPrescription.Message, StatusResponseMessage.success, scannedPrescription.StatusCode);
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

        public async Task<Response<int>> Insert(ScannedPrescriptionInsertRequestDto scannedPrescription)
        {
            var response = new Response<int>();
            try
            {
                var scannedPrescriptionEntity = await _mapperService.MapSingle<ScannedPrescriptionInsertRequestDto, Entities.EntityClass.PrescriptionEntity.ScannedPrescription>(scannedPrescription);
                var insertResponse = await _scannedPrescriptionCommandRepository.Insert(scannedPrescriptionEntity);
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

        public async Task<Response<int>> Update(ScannedPrescriptionUpdateRequestDto scannedPrescription)
        {
            var response = new Response<int>();
            try
            {
                var scannedPrescriptionEntity = await _mapperService.MapSingle<ScannedPrescriptionUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.ScannedPrescription>(scannedPrescription);
                scannedPrescriptionEntity.UpdatedAt = DateTime.Now;
                scannedPrescriptionEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _scannedPrescriptionCommandRepository.Update(scannedPrescriptionEntity);
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
            return _scannedPrescriptionCommandRepository.Delete(id);
        }
    }
}
