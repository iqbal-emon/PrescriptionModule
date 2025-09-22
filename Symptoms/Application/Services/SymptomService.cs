
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using Symptoms.Dtos.RequestDto.SymptomDto;
using Symptoms.Dtos.RequestDto.SymtomDto;
using symtoms.Domain.Repositories.Systom;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace DoctorPrescription.Application.Services
{
    public class SymptomService
    {
        private readonly ISymptomsQueryRepository _symptomQueryRepository;
        private readonly ISymptomsCommandRepository _symptomCommandRepository;
        private readonly MapperService _mapperService;

        public SymptomService(ISymptomsQueryRepository symptomQueryRepository, ISymptomsCommandRepository symptomCommandRepository,
            MapperService mapperService)
        {
            _symptomQueryRepository = symptomQueryRepository;
            _symptomCommandRepository = symptomCommandRepository;
            _mapperService = mapperService;
        }

        // Get all symptoms
        public async Task<Response<List<Entities.EntityClass.Symptom>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Symptom>>();

            try
            {
                var symptoms = await _symptomQueryRepository.GetAll();

                if (symptoms == null)
                {
                    ResponseHelper.SetFailedResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, symptoms.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the symptoms.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Symptom>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.Symptom>>();

            try
            {
                var symptoms = await _symptomQueryRepository.GetBookMarks(doctorId);

                if (symptoms == null)
                {
                    ResponseHelper.SetFailedResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, symptoms.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the symptoms.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
        

        // Get symptom by ID
        public async Task<Response<Entities.EntityClass.Symptom>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Symptom>();

            try
            {
                var symptom = await _symptomQueryRepository.GetById(id);

                if (symptom == null)
                {
                    ResponseHelper.SetFailedResponse(response, symptom.Result, symptom.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, symptom.Result, symptom.Message, StatusResponseMessage.success, symptom.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the symptom.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new symptom
        public async Task<Response<int>> Insert(SymptomsInsertRequestDto symptom)
        {
            var response = new Response<int>();

            try
            {
                var symptomEntity = await _mapperService.MapSingle<SymptomsInsertRequestDto, Entities.EntityClass.Symptom>(symptom);

                var insertResponse = await _symptomCommandRepository.Insert(symptomEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Update an existing symptom
        public async Task<Response<int>> Update(SymptomsUpdateRequestDto symptom)
        {
            var response = new Response<int>();

            try
            {
                var symptomEntity = await _mapperService.MapSingle<SymptomsUpdateRequestDto, Entities.EntityClass.Symptom>(symptom);
                symptomEntity.UpdatedAt = DateTime.Now;
                symptomEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _symptomCommandRepository.Update(symptomEntity);
                response = updatedResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Delete a symptom
        public Task<Response<bool>> Delete(int id)
        {
            return _symptomCommandRepository.Delete(id);
        }
        public Task<Response<List<Entities.EntityClass.Symptom>>> GetAllSymptomByName(string SymtomName)
        {
            return _symptomQueryRepository.GetAllSymptomByName(SymtomName);
        }
    }
}
