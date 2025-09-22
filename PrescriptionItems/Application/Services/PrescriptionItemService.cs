
using Microsoft.AspNetCore.Http;
using PrescriptionItems.Domain.Repositories.PrescriptionItem;
using PrescriptionItems.Dtos.RequestDto.PrescriptionItem;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace DoctorPrescription.Application.Services
{
    public class PrescriptionItemService
    {
        private readonly IPrescriptionItemQueryRepository _prescriptionItemQueryRepository;
        private readonly IPrescriptionItemCommandRepository _prescriptionItemCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionItemService(IPrescriptionItemQueryRepository prescriptionItemQueryRepository,
            IPrescriptionItemCommandRepository prescriptionItemCommandRepository,
            MapperService mapperService)
        {
            _prescriptionItemQueryRepository = prescriptionItemQueryRepository;
            _prescriptionItemCommandRepository = prescriptionItemCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionItem>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionItem>>();
            try
            {
                var prescriptionItems = await _prescriptionItemQueryRepository.GetAll();

                if (prescriptionItems == null)
                {
                    ResponseHelper.SetFailedResponse(response,prescriptionItems.Result, prescriptionItems.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionItems.Result, prescriptionItems.Message, StatusResponseMessage.success, prescriptionItems.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving prescription items.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionItem>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionItem>();
            try
            {
                var prescriptionItem = await _prescriptionItemQueryRepository.GetById(id);

                if (prescriptionItem == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionItem.Result, prescriptionItem.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionItem.Result, prescriptionItem.Message, StatusResponseMessage.success, prescriptionItem.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription item.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionItemInsertRequestDto prescriptionItem)
        {
            var response = new Response<int>();
            try
            {
                var prescriptionItemEntity = await _mapperService.MapSingle<PrescriptionItemInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionItem>(prescriptionItem);
                var insertResponse = await _prescriptionItemCommandRepository.Insert(prescriptionItemEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the prescription item.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the prescription item.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(PrescriptionItemUpdateRequestDto prescriptionItem)
        {
            var response = new Response<int>();
            try
            {
                var prescriptionItemEntity = await _mapperService.MapSingle<PrescriptionItemUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionItem>(prescriptionItem);
                prescriptionItemEntity.UpdatedAt = DateTime.Now;
                prescriptionItemEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _prescriptionItemCommandRepository.Update(prescriptionItemEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the prescription item.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the prescription item.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionItemCommandRepository.Delete(id);
        }
    }
}
