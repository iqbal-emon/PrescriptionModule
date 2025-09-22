
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
using Pharmacies.Domain.Repositories.Pharmacies;
using Pharmacies.Dtos.RequestDto.PharmaciesDto;

namespace Pharmacies.Application.Services
{
    public class PharmacyService
    {
        private readonly IPharmaciesQueryRepository _pharmacyQueryRepository;
        private readonly IPharmaciesCommandRepository _pharmacyCommandRepository;
        private readonly MapperService _mapperService;

        public PharmacyService(
            IPharmaciesQueryRepository pharmacyQueryRepository,
            IPharmaciesCommandRepository pharmacyCommandRepository,
            MapperService mapperService)
        {
            _pharmacyQueryRepository = pharmacyQueryRepository;
            _pharmacyCommandRepository = pharmacyCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Pharmacy>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Pharmacy>>();

            try
            {
                var pharmacies = await _pharmacyQueryRepository.GetAll();

                if (pharmacies == null || !pharmacies.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, pharmacies.Result,pharmacies.Message, StatusResponseMessage.success, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, pharmacies.Result, pharmacies.Message, StatusResponseMessage.success, pharmacies.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving pharmacies.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Pharmacy>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Pharmacy>();

            try
            {
                var pharmacy = await _pharmacyQueryRepository.GetById(id);

                if (pharmacy == null)
                {
                    ResponseHelper.SetFailedResponse(response, pharmacy.Result,pharmacy.Message, StatusResponseMessage.success, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, pharmacy.Result, pharmacy.Message, StatusResponseMessage.success, pharmacy.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the pharmacy.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(PharmaciesInsertRequestDto pharmacy)
        {
            var response = new Response<int>();

            try
            {
                var pharmacyEntity = await _mapperService.MapSingle<PharmaciesInsertRequestDto, Entities.EntityClass.Pharmacy>(pharmacy);

                var insertResponse = await _pharmacyCommandRepository.Insert(pharmacyEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the pharmacy.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the pharmacy.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PharmaciesUpdateRequestDto pharmacy)
        {
            var response = new Response<int>();

            try
            {
                var pharmacyEntity = await _mapperService.MapSingle<PharmaciesUpdateRequestDto, Entities.EntityClass.Pharmacy>(pharmacy);
                pharmacyEntity.UpdatedAt = DateTime.UtcNow; // Use UTC time for consistency

                var updatedResponse = await _pharmacyCommandRepository.Update(pharmacyEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the pharmacy.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the pharmacy.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                var deleteResponse = await _pharmacyCommandRepository.Delete(id);

                ResponseHelper.SetSuccessResponse(response, deleteResponse.Result, deleteResponse.Message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while deleting the pharmacy.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while deleting the pharmacy.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}