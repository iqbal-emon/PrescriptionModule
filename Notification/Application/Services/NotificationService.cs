using Notification.Domain.Repositories.Notification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Notification.Application.Services
{
    public class NotificationService
    {
        private readonly INotificationQueryRepository _notificationQueryRepository;
        private readonly INotificationCommandRepository _notificationCommandRepository;

        public NotificationService(
            INotificationQueryRepository notificationQueryRepository,
            INotificationCommandRepository notificationCommandRepository)
        {
            _notificationQueryRepository = notificationQueryRepository;
            _notificationCommandRepository = notificationCommandRepository;
        }

        public async Task<Response<List<Entities.EntityClass.Notification>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Notification>>();

            try
            {
                var notifications = await _notificationQueryRepository.GetAll();

                if (notifications == null)
                {
                    ResponseHelper.SetFailedResponse(response, notifications.Result, notifications.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, notifications.Result, notifications.Message, StatusResponseMessage.success, notifications.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Notifications.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Notification>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Notification>();

            try
            {
                var notification = await _notificationQueryRepository.GetById(id);

                if (notification == null)
                {
                    ResponseHelper.SetFailedResponse(response, notification.Result, notification.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, notification.Result, notification.Message, StatusResponseMessage.success, notification.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Notification.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Notification>>> GetByUserId(int userId, string role = "")
        {
            var response = new Response<List<Entities.EntityClass.Notification>>();

            try
            {
                var notifications = await _notificationQueryRepository.GetByUserId(userId, role);

                if (notifications == null)
                {
                    ResponseHelper.SetFailedResponse(response, notifications.Result, notifications.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, notifications.Result, notifications.Message, StatusResponseMessage.success, notifications.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Notifications by user ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}

