using DataAccess.DatabaseAccessLayer;
using PrescriptionPdf.Domain.Repositories.PrescriptionPdf;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using Entities.EntityClass.PrescriptionEntity;
using prescriptionPdf.Utility;
using Entities.EntityClass.PatientEntity;
using PrescriptionPdf.Dtos.ResponseDto.PrescriptionPdfDto;

namespace PrescriptionPdf.Infrastructure.RepositoriesImplement.PrescriptionPdf
{
    public class PrescriptionPdfQueryRepository : IPrescriptionPdfQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionPdfQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<PrescriptionPdfPatientResponseDto>>> GetByPatientDoctorId(int patientId,int doctorId)
        {
            var response = new Response<List<PrescriptionPdfPatientResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<PrescriptionPdfPatientResponseDto, dynamic>("PrescriptionPdf_GetByPatientDoctorId", new {
                    patientId = patientId,
                    doctorId = doctorId

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPdfResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<PrescriptionPdfPatientResponseDto>>> GetPrehandByDoctorId(int doctorId)
        {
            var response = new Response<List<PrescriptionPdfPatientResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<PrescriptionPdfPatientResponseDto, dynamic>("PrescriptionPdf_GetPrehandByDoctorId", new
                {
                    doctorId = doctorId

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPdfResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }


        

        public Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf, dynamic>("PrescriptionPdf_GetByAppointmentId", new
                {
                    appointmentId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionPdfResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
