using Doctor.Domain.Repositories.DigitalSignature;
using Doctor.Dtos.RequestDto.DigitalSignatureDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Doctor.Application.Services
{
    public class DigitalSignatureService
    {
        private readonly IDigitalSignatureQueryRepository _signatureQueryRepository;
        private readonly IDigitalSignatureCommandRepository _signatureCommandRepository;
        private readonly MapperService _mapperService;

        public DigitalSignatureService(
            IDigitalSignatureQueryRepository signatureQueryRepository,
            IDigitalSignatureCommandRepository signatureCommandRepository,
            MapperService mapperService)
        {
            _signatureQueryRepository = signatureQueryRepository;
            _signatureCommandRepository = signatureCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DigitalSignature>> GetByDoctorId(int doctorId)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DigitalSignature>();

            try
            {
                var signature = await _signatureQueryRepository.GetByDoctorId(doctorId);

                if (signature == null || !signature.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, signature?.Result, signature?.Message ?? "Digital signature not found", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, signature.Result, signature.Message, StatusResponseMessage.success, signature.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Digital Signature.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DigitalSignatureInsertRequestDto signature)
        {
            var response = new Response<int>();

            try
            {
                var signatureEntity = await _mapperService.MapSingle<DigitalSignatureInsertRequestDto, Entities.EntityClass.DoctorEntity.DigitalSignature>(signature);
                signatureEntity.CreatedAt = DateTime.Now;
                signatureEntity.IsActive = true;
                signatureEntity.IsDeleted = false;
                var insertResponse = await _signatureCommandRepository.Insert(signatureEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the Digital Signature.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the Digital Signature.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DigitalSignatureUpdateRequestDto signature)
        {
            var response = new Response<int>();

            try
            {
                var signatureEntity = await _mapperService.MapSingle<DigitalSignatureUpdateRequestDto, Entities.EntityClass.DoctorEntity.DigitalSignature>(signature);
                signatureEntity.UpdatedAt = DateTime.Now;
                signatureEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _signatureCommandRepository.Update(signatureEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the Digital Signature.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the Digital Signature.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DigitalSignature>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DigitalSignature>();

            try
            {
                var signature = await _signatureQueryRepository.GetById(id);

                if (signature == null || !signature.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, signature?.Result, signature?.Message ?? "Digital signature not found", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, signature.Result, signature.Message, StatusResponseMessage.success, signature.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Digital Signature.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _signatureCommandRepository.Delete(id);
        }
    }
}

