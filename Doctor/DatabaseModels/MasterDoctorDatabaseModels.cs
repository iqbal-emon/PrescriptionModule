namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for MasterDoctor_DeleteById stored procedure
    /// Parameters: @MasterDoctorID INT
    /// </summary>
    public class MasterDoctorDeleteModel
    {
        public int MasterDoctorID { get; set; }
    }

    /// <summary>
    /// Database model for MasterDoctor_Insert stored procedure
    /// Parameters: @DoctorID INT, @AgentMasterID INT = NULL, @MasterDoctorID INT OUTPUT
    /// </summary>
    public class MasterDoctorInsertModel
    {
        public int DoctorID { get; set; }
        public int? AgentMasterID { get; set; }
        public int MasterDoctorID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for MasterDoctor_Update stored procedure
    /// Parameters: @MasterDoctorID INT, @DoctorID INT, @AgentMasterID INT = NULL, @UpdatedId INT OUTPUT
    /// </summary>
    public class MasterDoctorUpdateModel
    {
        public int MasterDoctorID { get; set; }
        public int DoctorID { get; set; }
        public int? AgentMasterID { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

