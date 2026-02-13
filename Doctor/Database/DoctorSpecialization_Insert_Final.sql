USE [SoowGood_System_Dev]
GO
/****** Object:  StoredProcedure [dbo].[DoctorSpecialization_Insert]    Script Date: 2/13/2026 5:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[DoctorSpecialization_Insert]
    @DoctorID INT,
    @SpecialityID INT = NULL,
    @SpecializationID INT = NULL,
    @ServiceDetails NVARCHAR(500) = NULL,
    @DocumentName NVARCHAR(200) = NULL,
    @CreatedAt DATETIME = NULL,
    @UpdatedAt DATETIME = NULL,
    @IsDeleted BIT = NULL,
    @DoctorSpecializationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate that DoctorID exists in Doctor table
    -- Note: Change [dbo].[Doctor] to [dbo].[Doctors] if your table name is plural
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Doctor] WHERE [DoctorID] = @DoctorID AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL))
    BEGIN
        RAISERROR('The specified DoctorID (%d) does not exist in the Doctor table or has been deleted.', 16, 1, @DoctorID);
        SET @DoctorSpecializationID = 0;
        RETURN;
    END;
    
    -- Check for duplicate: DoctorID and SpecializationID combination already exists
    IF EXISTS (SELECT 1 FROM [dbo].[DoctorSpecialization] 
               WHERE [DoctorID] = @DoctorID 
               AND ([SpecializationID] = @SpecializationID OR ([SpecializationID] IS NULL AND @SpecializationID IS NULL))
               AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL))
    BEGIN
        RAISERROR('A specialization record already exists for DoctorID (%d) with SpecializationID (%s). Duplicate entries are not allowed.', 16, 1, @DoctorID, ISNULL(CAST(@SpecializationID AS NVARCHAR(10)), 'NULL'));
        SET @DoctorSpecializationID = 0;
        RETURN;
    END;
    
    INSERT INTO [dbo].[DoctorSpecialization]
    (
        [DoctorID],
        [SpecialityID],
        [SpecializationID],
        [ServiceDetails],
        [DocumentName],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorID,
        @SpecialityID,
        @SpecializationID,
        @ServiceDetails,
        @DocumentName,
        ISNULL(@CreatedAt, GETDATE()),
        ISNULL(@UpdatedAt, GETDATE()),
        ISNULL(@IsDeleted, 0)
    );
    SET @DoctorSpecializationID = SCOPE_IDENTITY();
END
GO

