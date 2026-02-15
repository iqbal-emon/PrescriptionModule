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
    /// Database model for Appointment_Insert stored procedure
    /// Parameters: @PatientName NVARCHAR(100), @PhoneNumber NVARCHAR(20), @Gender NVARCHAR(50),
    /// @BloodGroup NVARCHAR(10), @Age INT, @SessionId INT, @ScheduleId INT, @DoctorProfileId INT,
    /// @AppointmentDate DATETIME, @id INT OUTPUT
    /// </summary>
    public class AppointmentInsertModel
    {
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public int Age { get; set; }
        public int SessionId { get; set; }
        public int ScheduleId { get; set; }
        public int DoctorProfileId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int id { get; set; } // OUTPUT parameter
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

