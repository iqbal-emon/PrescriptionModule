using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using PrescriptionPatientHistory.Domain.Repositories.PrescriptionPatientHistory;
using PrescriptionPatientHistory.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace PrescriptionPatientHistory.Insfracture.RepositoriesImplement.PrescriptionPatientHistory
{
    public class PrescriptionPatientHistoryQueryRepository : IPrescriptionPatientHistoryQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionPatientHistoryQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all prescription patient histories from the database
        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, dynamic>("PrescriptionPatientHistory_GetAll", new { });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        // Get all prescription patient histories by name
        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>> GetAllPrescriptionPatientHistoryByName(string prescriptionPatientHistoryName)
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, dynamic>("PrescriptionPatientHistory_GetByName", new
                {
                    PrescriptionPatientHistoryName = prescriptionPatientHistoryName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Get a prescription patient history by ID
        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>();

            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, dynamic>("PrescriptionPatientHistory_GetById", new
                {
                    PrescriptionPatientHistoryID = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }


  



        public async Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAllPrescriptionPatientHistoryPrevious(int id)
        {
            var response = new Response<List<Entities.EntityClass.CommonHistory>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonHistory, dynamic>("PrescriptionPatientHistory_Previous", new
                {
                    PatientId = id
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

       
    }
}
