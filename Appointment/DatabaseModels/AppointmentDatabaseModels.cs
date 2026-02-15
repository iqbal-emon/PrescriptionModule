using System;

namespace Appointment.DatabaseModels
{
    /// <summary>
    /// Database model for Appointment_DeleteById stored procedure
    /// Parameters: @AppointmentId INT
    /// </summary>
    public class AppointmentDeleteModel
    {
        public int AppointmentId { get; set; }
    }

    /// <summary>
    /// Database model for Appointment_Update stored procedure
    /// Parameters: @Id INT, @SessionId INT = NULL, @ScheduleId INT = NULL, 
    /// @PatientId INT = NULL, @AppointmentDate DATETIME = NULL, @DoctorProfileId INT = NULL
    /// </summary>
    public class AppointmentUpdateModel
    {
        public int Id { get; set; }
        public int? SessionId { get; set; }
        public int? ScheduleId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? DoctorProfileId { get; set; }
    }
}

