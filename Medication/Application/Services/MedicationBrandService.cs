using Medication.Domain.Repositories.MedicationBrand;
using Medication.Dtos.RequestDto.MedicationBrandDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Medication.Application.Services
{
    public class MedicationBrandService
    {
        private readonly IMedicationBrandQueryRepository _medicationBrandQueryRepository;
        private readonly IMedicationBrandCommandRepository _medicationBrandCommandRepository;
        private readonly MapperService _mapperService;

        public MedicationBrandService(IMedicationBrandQueryRepository medicationBrandQueryRepository, IMedicationBrandCommandRepository medicationBrandCommandRepository,
            MapperService mapperService)
        {
            _medicationBrandQueryRepository = medicationBrandQueryRepository;
            _medicationBrandCommandRepository = medicationBrandCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.MedicationBrand>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.MedicationBrand>>();

            try
            {
                var medicationBrands = await _medicationBrandQueryRepository.GetAll();

                if (medicationBrands.Result == null)
                {
                    ResponseHelper.SetFailedResponse(response, null, medicationBrands.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, medicationBrands.Result, medicationBrands.Message, StatusResponseMessage.success, medicationBrands.StatusCode);
                }
            }
            catch (SqlException)
            {
                ResponseHelper.SetFailedResponse(response, null, "A database error occurred while retrieving the medication brands.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                ResponseHelper.SetFailedResponse(response, null, "An unexpected error occurred.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.MedicineEntity.MedicationBrand>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.MedicineEntity.MedicationBrand>();

            try
            {
                var medicationBrand = await _medicationBrandQueryRepository.GetById(id);

                if (medicationBrand.Result == null)
                {
                    ResponseHelper.SetFailedResponse(response, null, medicationBrand.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, medicationBrand.Result, medicationBrand.Message, StatusResponseMessage.success, medicationBrand.StatusCode);
                }
            }
            catch (SqlException)
            {
                ResponseHelper.SetFailedResponse(response, null, "A database error occurred while retrieving the medication brand.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                ResponseHelper.SetFailedResponse(response, null, "An unexpected error occurred.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(MedicationBrandInsertRequestDto medicationBrand)
        {
            var response = new Response<int>();

            try
            {
                var medicationBrandEntity = await _mapperService.MapSingle<MedicationBrandInsertRequestDto, Entities.EntityClass.MedicineEntity.MedicationBrand>(medicationBrand);

                var insertResponse = await _medicationBrandCommandRepository.Insert(medicationBrandEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                ResponseHelper.SetFailedResponse(response, 0, "A database error occurred while inserting the medication brand.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                ResponseHelper.SetFailedResponse(response, 0, "An unexpected error occurred while inserting the medication brand.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(MedicationBrandUpdateRequestDto medicationBrand)
        {
            var response = new Response<int>();

            try
            {
                var medicationBrandEntity = await _mapperService.MapSingle<MedicationBrandUpdateRequestDto, Entities.EntityClass.MedicineEntity.MedicationBrand>(medicationBrand);
                medicationBrandEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _medicationBrandCommandRepository.Update(medicationBrandEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                ResponseHelper.SetFailedResponse(response, 0, "A database error occurred while updating the medication brand.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                ResponseHelper.SetFailedResponse(response, 0, "An unexpected error occurred while updating the medication brand.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _medicationBrandCommandRepository.Delete(id);
        }
    }
}