namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for CampaignDoctor_DeleteById stored procedure
    /// Parameters: @CampaignDoctorID INT
    /// </summary>
    public class CampaignDoctorDeleteModel
    {
        public int CampaignDoctorID { get; set; }
    }

    /// <summary>
    /// Database model for CampaignDoctor_Insert stored procedure
    /// Parameters: @DoctorID INT, @CampaignID INT = NULL, @CampaignDoctorID INT OUTPUT
    /// </summary>
    public class CampaignDoctorInsertModel
    {
        public int DoctorID { get; set; }
        public int? CampaignID { get; set; }
        public int CampaignDoctorID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for CampaignDoctor_Update stored procedure
    /// Parameters: @CampaignDoctorID INT, @DoctorID INT, @CampaignID INT = NULL, @UpdatedId INT OUTPUT
    /// </summary>
    public class CampaignDoctorUpdateModel
    {
        public int CampaignDoctorID { get; set; }
        public int DoctorID { get; set; }
        public int? CampaignID { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

