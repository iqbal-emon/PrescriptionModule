using DataAccess.DatabaseAccessLayer;
using Diagononosis.Utility;
using Diagonosis.Domain.Repositories.Diagonosis;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Diagonosis.Insfracture.RepositoriesImplement.Diagnonosis
{

    public class DiagononosisQueryRepository : IDiagonosisQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DiagononosisQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Prescription Diagnoses
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.Diagonosis>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Diagonosis>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Diagonosis, dynamic>("Diagnosis_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DiagononosisResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Diagonosis>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.Diagonosis>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Diagonosis, dynamic>("GetBookMarksDiagnosisByDoctorId", new
                {
                    DoctorReferenceID = doctorId
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DiagononosisResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }



        // Get Prescription Diagnosis by ID
        public async Task<Response<Entities.EntityClass.Diagonosis>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Diagonosis>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Diagonosis, dynamic>("Diagnosis_GetById", new
                {
                    DiagnosisId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DiagononosisResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Diagonosis>>> GetAllDiagononosisName(string diagonosisName)
        {
            var response = new Response<List<Entities.EntityClass.Diagonosis>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Diagonosis, dynamic>("Diagonosis_GetDiagnosesByName", new
                {
                    DiagnosisName = diagonosisName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DiagononosisResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
