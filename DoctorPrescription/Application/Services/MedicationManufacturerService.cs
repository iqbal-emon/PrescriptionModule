using Medication.Domain.Repositories.MedicationManufacturer;
using Medication.Dtos.RequestDto.MedicationManufacturerDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Medication.Application.Services
{

    public class MedicationManufacturerService
        {
            private readonly IMedicationManufacturerQueryRepository _medicationManufacturerQueryRepository;
            private readonly IMedicationManufacturerCommandRepository _medicationManufacturerCommandRepository;
            private readonly MapperService _mapperService;

            public MedicationManufacturerService(IMedicationManufacturerQueryRepository medicationManufacturerQueryRepository, IMedicationManufacturerCommandRepository medicationManufacturerCommandRepository,
                MapperService mapperService)
            {
                _medicationManufacturerQueryRepository = medicationManufacturerQueryRepository;
                _medicationManufacturerCommandRepository = medicationManufacturerCommandRepository;
                _mapperService = mapperService;
            }

            public async Task<Response<List<Entities.EntityClass.MedicineEntity.MedicationManufacturer>>> GetAll()
            {
                var response = new Response<List<Entities.EntityClass.MedicineEntity.MedicationManufacturer>>();

                try
                {
                    var manufacturers = await _medicationManufacturerQueryRepository.GetAll();

                    if (manufacturers == null)
                    {
                        ResponseHelper.SetFailedResponse(response, manufacturers.Result, manufacturers.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                    else
                    {
                        ResponseHelper.SetSuccessResponse(response, manufacturers.Result, manufacturers.Message, StatusResponseMessage.success, manufacturers.StatusCode);
                    }
                }
                catch (SqlException sqlEx)
                {
                    response.Message = "A database error occurred while retrieving the medication manufacturers.";
                    ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }
                catch (Exception ex)
                {
                    response.Message = "An unexpected error occurred.";
                    ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }

                return response;
            }

            public async Task<Response<Entities.EntityClass.MedicineEntity.MedicationManufacturer>> GetById(int id)
            {
                var response = new Response<Entities.EntityClass.MedicineEntity.MedicationManufacturer>();

                try
                {
                    var manufacturer = await _medicationManufacturerQueryRepository.GetById(id);

                    if (manufacturer == null)
                    {
                        ResponseHelper.SetFailedResponse(response, manufacturer.Result, manufacturer.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                    }
                    else
                    {
                        ResponseHelper.SetSuccessResponse(response, manufacturer.Result, manufacturer.Message, StatusResponseMessage.success, manufacturer.StatusCode);
                    }
                }
                catch (SqlException sqlEx)
                {
                    response.Message = "A database error occurred while retrieving the medication manufacturer.";
                    ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }
                catch (Exception ex)
                {
                    response.Message = "An unexpected error occurred.";
                    ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }

                return response;
            }

            public async Task<Response<int>> Insert(MedicationManufacturerInsertRequestDto manufacturer)
            {
                var response = new Response<int>();

                try
                {
                    var manufacturerEntity = await _mapperService.MapSingle<MedicationManufacturerInsertRequestDto, Entities.EntityClass.MedicineEntity.MedicationManufacturer>(manufacturer);

                    var insertResponse = await _medicationManufacturerCommandRepository.Insert(manufacturerEntity);
                    response = insertResponse;
                }
                catch (SqlException sqlEx)
                {
                    response.Message = "A database error occurred while inserting the medication manufacturer.";
                    ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }
                catch (Exception ex)
                {
                    response.Message = "An unexpected error occurred while inserting the medication manufacturer.";
                    ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }

                return response;
            }

            public async Task<Response<int>> Update(MedicationManufacturerUpdateRequestDto manufacturer)
            {
                var response = new Response<int>();

                try
                {
                    var manufacturerEntity = await _mapperService.MapSingle<MedicationManufacturerUpdateRequestDto, Entities.EntityClass.MedicineEntity.MedicationManufacturer>(manufacturer);
                    manufacturerEntity.UpdatedAt = DateTime.Now;
                    manufacturerEntity.CreatedAt = DateTime.Now;
                    var updatedResponse = await _medicationManufacturerCommandRepository.Update(manufacturerEntity);
                    response = updatedResponse;
                }
                catch (SqlException sqlEx)
                {
                    response.Message = "A database error occurred while updating the medication manufacturer.";
                    ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }
                catch (Exception ex)
                {
                    response.Message = "An unexpected error occurred while updating the medication manufacturer.";
                    ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
                }

                return response;
            }

            public Task<Response<bool>> Delete(int id)
            {
                return _medicationManufacturerCommandRepository.Delete(id);
            }
        }
    }


