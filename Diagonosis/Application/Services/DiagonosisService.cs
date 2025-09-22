
using Diagonosis.Domain.Repositories.Diagonosis;
using Diagonosis.Dtos.RequestDto.DiagonosisDto;
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

namespace Diagonosis.Application.Services
{
    public class DiagonosisService
    {
        private readonly IDiagonosisQueryRepository _diagonosisQueryRepository;
        private readonly IDiagonosisCommandRepository _diagonosisCommandRepository;
        private readonly MapperService _mapperService;

        public DiagonosisService(
            IDiagonosisQueryRepository diagonosisQueryRepository,
            IDiagonosisCommandRepository diagonosisCommandRepository,
            MapperService mapperService)
        {
            _diagonosisQueryRepository = diagonosisQueryRepository;
            _diagonosisCommandRepository = diagonosisCommandRepository;
            _mapperService = mapperService;
        }

        // Get all  diagnoses
        public async Task<Response<List<Entities.EntityClass.Diagonosis>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Diagonosis>>();

            try
            {
                var diagnoses = await _diagonosisQueryRepository.GetAll();

                if (diagnoses == null)
                {
                    ResponseHelper.SetFailedResponse(response, diagnoses.Result, diagnoses.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diagnoses.Result, diagnoses.Message, StatusResponseMessage.success, diagnoses.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the  diagnoses.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        //Get Bookmarks
        public async Task<Response<List<Entities.EntityClass.Diagonosis>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.Diagonosis>>();

            try
            {
                var diagnoses = await _diagonosisQueryRepository.GetBookMarks(doctorId);

                if (diagnoses == null)
                {
                    ResponseHelper.SetFailedResponse(response, diagnoses.Result, diagnoses.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diagnoses.Result, diagnoses.Message, StatusResponseMessage.success, diagnoses.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the  diagnoses.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }


        

        // Get prescription diagnosis by ID
        public async Task<Response<Entities.EntityClass.Diagonosis>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Diagonosis>();

            try
            {
                var diagnosis = await _diagonosisQueryRepository.GetById(id);

                if (diagnosis == null)
                {
                    ResponseHelper.SetFailedResponse(response, diagnosis.Result, diagnosis.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, diagnosis.Result, diagnosis.Message, StatusResponseMessage.success, diagnosis.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the  diagnosis.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new prescription diagnosis
        public async Task<Response<int>> Insert(DiagnonosisInsertRequestDto diagnosis)
        {
            var response = new Response<int>();

            try
            {
                var diagnosisEntity = await _mapperService.MapSingle<DiagnonosisInsertRequestDto, Entities.EntityClass.Diagonosis>(diagnosis);

                var insertResponse = await _diagonosisCommandRepository.Insert(diagnosisEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the prescription diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the prescription diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Update an existing prescription diagnosis
        public async Task<Response<int>> Update(DiagnonosisUpdateRequestDto diagnosis)
        {
            var response = new Response<int>();

            try
            {
                var diagnosisEntity = await _mapperService.MapSingle<DiagnonosisUpdateRequestDto, Entities.EntityClass.Diagonosis>(diagnosis);
                diagnosisEntity.UpdatedAt = DateTime.Now;
                diagnosisEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _diagonosisCommandRepository.Update(diagnosisEntity);
                response = updatedResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the  diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the  diagnosis.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Delete a  diagnosis
        public Task<Response<bool>> Delete(int id)
        {
            return _diagonosisCommandRepository.Delete(id);
        }
        public Task<Response<List<Entities.EntityClass.Diagonosis>>> GetAllByName(string diagnoisis)
        {
            return _diagonosisQueryRepository.GetAllDiagononosisName(diagnoisis);
        }


    }
}
