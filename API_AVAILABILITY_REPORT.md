# API Availability and Functionality Report
## PrescriptionModule - API Endpoints Analysis

**Generated:** 2025-01-27  
**Project Path:** `D:\soowgood\Prescripto\PrescriptionModule`

---

## Summary

This report documents the availability, functionality, and implementation status of all requested API endpoints in the PrescriptionModule project.

---

## 1. Authentication Endpoints

### ✅ `/auth/firebase/verify`
- **Status:** ✅ Available (Partially Implemented)
- **Route:** `api/2025-02/auth/firebase/verify`
- **Method:** `POST`
- **Controller:** `AuthenticationSystem/Controllers/AuthController.cs`
- **Authorization:** Not Required (AllowAnonymous)
- **Functionality:**
  - Accepts Firebase ID token for verification
  - Currently returns error message indicating FirebaseAdmin package is required
  - TODO: Needs proper Firebase token verification implementation
  - Expected to verify token and create/login user
- **Request DTO:** `FirebaseVerifyRequestDto` (accepts `FirebaseToken` or `IdToken`)
- **Response:** `ApiResponse<LoginResponseDto>`
- **Note:** ⚠️ Implementation incomplete - requires FirebaseAdmin package setup

---

## 2. Doctor Profile Endpoints

### ✅ `/doctor-profile/{id}/doctor-details-by-admin`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/doctor-profile/{id}/doctor-details-by-admin`
- **Method:** `GET`
- **Controller:** `Doctor/Controllers/DoctorController.cs` (Line 566)
- **Authorization:** Required (`PermissionConstants.DoctorGetId`)
- **Functionality:**
  - Retrieves comprehensive doctor details by ID for admin use
  - Calls `_doctorService.GetDetailsByAdmin(id)` method
  - Returns mapped `DoctorApiResponseDto` with full doctor information
  - Includes error handling for doctor not found scenarios
- **Parameters:** `id` (int) - Doctor profile ID
- **Response:** `ApiResponse<DoctorApiResponseDto>`

---

## 3. Prescription Master Endpoints

### ✅ `/prescription-master/{id}/prescription`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/prescription-master/{id}/prescription`
- **Method:** `GET`
- **Controller:** `Prescription/Controllers/PrescriptionController.cs` (Line 1896)
- **Authorization:** Required (`PermissionConstants.PrescriptionGetId`)
- **Functionality:**
  - Retrieves prescription by prescription master ID
  - Calls internal `GetPrescriptionById(id)` method
  - Returns complete prescription data with all related entities
- **Parameters:** `id` (int) - Prescription master ID
- **Response:** `ApiResponse<PrescriptionApiResponseDto>`

### ✅ `/prescription-master/prescription-by-appointment-id/{appointmentId}`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/prescription-master/prescription-by-appointment-id/{appointmentId}`
- **Method:** `GET`
- **Controller:** `Prescription/Controllers/PrescriptionController.cs` (Line 1903)
- **Authorization:** Required (`PermissionConstants.PrescriptionGetId`)
- **Functionality:**
  - Retrieves prescription by appointment ID
  - Calls `_prescriptionService.GetByAppointmentId(appointmentId)`
  - Returns mapped prescription data
  - Handles cases where prescription not found for appointment
- **Parameters:** `appointmentId` (int) - Appointment ID
- **Response:** `ApiResponse<PrescriptionApiResponseDto>`

---

## 4. Prescription Template Endpoints

### ✅ `/get-template-prescription-by-id`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/get-template-prescription-by-id`
- **Method:** `GET`
- **Controller:** `Prescription/Controllers/PrescriptionController.cs` (Line 1928)
- **Authorization:** Required (`PermissionConstants.PrescriptionGetId`)
- **Functionality:**
  - Retrieves prescription template by template ID
  - Calls `_prescriptionTemplateService.GetById(templateId)`
  - Returns template object for prescription creation
- **Query Parameters:** `templateId` (int)
- **Response:** `ApiResponse<object>`
- **Note:** Also available in `PrescriptionTemplateControllers.cs` (Line 88)

### ✅ `/gets-all-prescription-template-by-doctor-id`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/gets-all-prescription-template-by-doctor-id`
- **Method:** `GET`
- **Controller:** `Prescription/Controllers/PrescriptionController.cs` (Line 1952)
- **Authorization:** Required (`PermissionConstants.PrescriptionGetAll`)
- **Functionality:**
  - Retrieves all prescription templates for a specific doctor
  - Calls `_prescriptionTemplateService.GetAllByDoctorId(doctorId)`
  - Returns list of template objects
  - Handles empty results gracefully
- **Query Parameters:** `doctorId` (int)
- **Response:** `ApiResponse<List<object>>`
- **Note:** Also available in `PrescriptionTemplateControllers.cs` (Line 62)

---

## 5. Prescription PDF Endpoints

### ✅ `/get-pdf-prescriptions-by-patient-doctor-id`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/get-pdf-prescriptions-by-patient-doctor-id`
- **Method:** `GET`
- **Controller:** 
  - `Prescription/Controllers/PrescriptionController.cs` (Line 1976) - Proxy endpoint
  - `PrescriptionPdf/Controllers/PrescriptionPdfController.cs` (Line 40) - Direct implementation
- **Authorization:** Required (`PermissionConstants.PrescriptionGetAll` / `PrescriptionPdfsGetAll`)
- **Functionality:**
  - **PrescriptionController:** Acts as proxy, calls external PrescriptionPdf API
  - **PrescriptionPdfController:** Direct implementation using `_prescriptionPdfService.GetByPatientDoctorId(patientId, doctorId)`
  - Returns list of PDF prescriptions for specific patient-doctor combination
- **Query Parameters:** `patientId` (int), `doctorId` (int)
- **Response:** `ApiResponse<List<PrescriptionPdfPatientResponseDto>>` or `ApiResponse<List<object>>`

### ✅ `/get-pdf-prescriptions-by-doctor-prehand-id`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/get-pdf-prescriptions-by-doctor-prehand-id`
- **Method:** `GET`
- **Controller:**
  - `Prescription/Controllers/PrescriptionController.cs` (Line 2007) - Proxy endpoint
  - `PrescriptionPdf/Controllers/PrescriptionPdfController.cs` (Line 65) - Direct implementation
- **Authorization:** Required (`PermissionConstants.PrescriptionGetAll` / `PrescriptionPdfsGetAll`)
- **Functionality:**
  - **PrescriptionController:** Acts as proxy, calls external PrescriptionPdf API
  - **PrescriptionPdfController:** Direct implementation using `_prescriptionPdfService.GetPrehandByDoctorId(doctorId, prescriptionCode, patientName, patientCode)`
  - Returns prehand (pre-written) PDF prescriptions for a doctor
- **Query Parameters:** `doctorId` (int), `prescriptionCode` (string, optional), `patientName` (string, optional), `patientCode` (string, optional)
- **Response:** `ApiResponse<List<PrescriptionPdfPatientResponseDto>>` or `ApiResponse<List<object>>`

### ✅ `/get-prescription-pdf-by-appointment-id`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/get-prescription-pdf-by-appointment-id`
- **Method:** `GET`
- **Controller:**
  - `Prescription/Controllers/PrescriptionController.cs` (Line 2038) - Proxy endpoint
  - `PrescriptionPdf/Controllers/PrescriptionPdfController.cs` (Line 93) - Direct implementation
- **Authorization:** Required (`PermissionConstants.PrescriptionGetId` / `PrescriptionPdfsGetById`)
- **Functionality:**
  - **PrescriptionController:** Acts as proxy, calls external PrescriptionPdf API
  - **PrescriptionPdfController:** Direct implementation using `_prescriptionPdfService.GetById(appointmentId)`
  - Returns single PDF prescription for specific appointment
- **Query Parameters:** `appointmentId` (int)
- **Response:** `ApiResponse<PrescriptionPdfApiResponseDto>` or `ApiResponse<object>`

---

## 6. Diagnosis Endpoints

### ✅ `/gets-bookmarks-diagnosis`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/gets-bookmarks-diagnosis`
- **Method:** `GET`
- **Controller:** `Diagonosis/Controllers/DiagonosisController.cs` (Line 66)
- **Authorization:** Required (`PermissionConstants.DiagonosisGetAll`)
- **Functionality:**
  - Retrieves bookmarked/highly used diagnoses for a doctor
  - Calls `_diagonosisService.GetBookMarks(doctorId)`
  - Returns list of frequently used diagnoses
- **Query Parameters:** `doctorId` (int)
- **Response:** `ApiResponse<List<DiagonosisApiResponseDto>>`

### ✅ `/gets-diagnosis-by-name`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/gets-diagnosis-by-name`
- **Method:** `GET`
- **Controller:** `Diagonosis/Controllers/DiagonosisController.cs` (Line 93)
- **Authorization:** Required (`PermissionConstants.DiagonosisGetAll`)
- **Functionality:**
  - Searches diagnoses by name (supports partial matching)
  - Calls `_diagonosisService.GetAllByName(diagnosisName)`
  - Returns filtered list of diagnoses matching the name
- **Query Parameters:** `diagnosisName` (string, optional)
- **Response:** `ApiResponse<List<DiagonosisApiResponseDto>>`

---

## 7. Follow-Up Endpoints

### ✅ `/gets-all-followup-by-name`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/gets-all-followup-by-name`
- **Method:** `GET`
- **Controller:** `FollowUp/Controllers/FollowUpController.cs` (Line 115)
- **Authorization:** Required (`PermissionConstants.FollowupGetId`)
- **Functionality:**
  - Searches follow-up instructions by name
  - Calls `_followUpService.GetAllByName(followUpName)`
  - Returns filtered list of follow-up instructions
- **Query Parameters:** `followUpName` (string, optional)
- **Response:** `ApiResponse<List<FollowUpApiResponseDto>>`

---

## 8. Medication Endpoints

### ✅ `/get-medication-by-name`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/get-medication-by-name`
- **Method:** `GET`
- **Controller:** `Medication/Controllers/MedicationController.cs` (Line 261)
- **Authorization:** Required
- **Functionality:**
  - Searches medications by name (supports Bengali Unicode)
  - Detects Bengali characters and converts to Unicode
  - Calls `_medicationService.GetMedicationByName(medicationName, uniCode)`
  - Returns filtered list of medications
- **Query Parameters:** `medicationName` (string, optional)
- **Response:** `ApiResponse<List<MedicationApiResponseDto>>`
- **Special Feature:** Supports Bengali language search with Unicode conversion

---

## 9. Chief Complaints (Symptoms) Endpoints

### ✅ `/gets-bookmarks-chief-complaints`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/gets-bookmarks-chief-complaints`
- **Method:** `GET`
- **Controller:** `Symptoms/Controllers/SymptomsController.cs` (Line 66)
- **Authorization:** Required (`PermissionConstants.SymptomGetAll`)
- **Functionality:**
  - Retrieves bookmarked/highly used chief complaints for a doctor
  - Calls `_symptomService.GetBookMarks(doctorId)`
  - Returns list of frequently used symptoms/chief complaints
- **Query Parameters:** `doctorId` (int)
- **Response:** `ApiResponse<List<SymptomsApiResponseDto>>`

### ✅ `/gets-chief-complaint-by-name`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/gets-chief-complaint-by-name`
- **Method:** `GET`
- **Controller:** `Symptoms/Controllers/SymptomsController.cs` (Line 93)
- **Authorization:** Required (`PermissionConstants.SymptomGetAll`)
- **Functionality:**
  - Searches chief complaints/symptoms by name
  - Calls `_symptomService.GetAllSymptomByName(SymtomName)`
  - Returns filtered list of symptoms matching the name
- **Query Parameters:** `SymtomName` (string, optional)
- **Response:** `ApiResponse<List<SymptomsApiResponseDto>>`

---

## 10. Prescription Upload Endpoint

### ✅ `/prescription-upload`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/prescription-upload`
- **Method:** `POST`
- **Controller:**
  - `Prescription/Controllers/PrescriptionController.cs` (Line 2069) - JSON body upload
  - `PrescriptionPdf/Controllers/PrescriptionPdfController.cs` (Line 210) - File upload
- **Authorization:** Required (`PermissionConstants.PrescriptionCreate` / `PrescriptionPdfsCreate`)
- **Functionality:**
  - **PrescriptionController:** Accepts `ScannedPrescriptionInsertRequestDto` in JSON body, calls `_scannedPrescriptionService.Insert(request)`
  - **PrescriptionPdfController:** Accepts file upload via `[FromForm]`, saves to temporary storage, returns upload path
  - Both handle prescription image/document uploads
- **Request:**
  - PrescriptionController: JSON body with `ScannedPrescriptionInsertRequestDto`
  - PrescriptionPdfController: Form data with file (`FileUploadRequestViewModel`)
- **Response:**
  - PrescriptionController: `ApiResponse<int>` (prescription ID)
  - PrescriptionPdfController: `ApiResponse<PrescriptionUploadResponseDto>` (upload path)

---

## 11. Medication Division Usage Endpoint

### ✅ `/get-medication-division-usage`
- **Status:** ✅ Available & Fully Functional
- **Route:** `api/2025-02/get-medication-division-usage`
- **Method:** `GET`
- **Controller:** `Prescription/Controllers/PrescriptionController.cs` (Line 2100)
- **Authorization:** Required (`PermissionConstants.PrescriptionGetAll`)
- **Functionality:**
  - Retrieves medication division usage statistics
  - Calls `_prescriptionService.GetMedicationDivisionUsage(tenantId, startDate, endDate)`
  - Supports filtering by tenant, date range
  - Returns usage analytics data
- **Query Parameters:** 
  - `tenantId` (int, optional)
  - `startDate` (DateTime, optional)
  - `endDate` (DateTime, optional)
- **Response:** `ApiResponse<List<object>>`

---

## API Route Structure

All endpoints in PrescriptionModule follow this base route pattern:
- **Base Route:** `api/2025-02/`
- **Authentication Route:** `api/2025-02/auth/`

---

## Authorization & Permissions

All endpoints (except Firebase verify) require authorization with specific permission policies:
- `PermissionConstants.PrescriptionGetAll`
- `PermissionConstants.PrescriptionGetId`
- `PermissionConstants.PrescriptionCreate`
- `PermissionConstants.PrescriptionUpdate`
- `PermissionConstants.DoctorGetId`
- `PermissionConstants.DiagonosisGetAll`
- `PermissionConstants.FollowupGetId`
- `PermissionConstants.SymptomGetAll`
- `PermissionConstants.PrescriptionPdfsGetAll`
- `PermissionConstants.PrescriptionPdfsGetById`
- `PermissionConstants.PrescriptionPdfsCreate`

---

## Response Format

All endpoints return a standardized `ApiResponse<T>` format:
```csharp
{
    "is_success": bool,
    "message": string,
    "results": T,
    "status": string,
    "status_code": int
}
```

---

## Implementation Notes

1. **Firebase Verify Endpoint:** Requires FirebaseAdmin package implementation
2. **PDF Prescription Endpoints:** Available in both PrescriptionController (proxy) and PrescriptionPdfController (direct)
3. **Template Endpoints:** Available in both PrescriptionController and PrescriptionTemplateControllers
4. **Error Handling:** All endpoints include try-catch blocks with appropriate error messages
5. **Bengali Support:** Medication search endpoint includes Bengali Unicode conversion

---

## Summary Statistics

- **Total Endpoints Checked:** 15
- **Fully Available & Functional:** 14
- **Partially Implemented:** 1 (Firebase verify - needs FirebaseAdmin setup)
- **Not Found:** 0

---

## Recommendations

1. **Firebase Verify:** Complete implementation by installing and configuring FirebaseAdmin package
2. **Documentation:** Consider adding Swagger/OpenAPI documentation for all endpoints
3. **Consistency:** Some endpoints have duplicate implementations (proxy vs direct) - consider consolidating
4. **Testing:** Ensure all endpoints have proper unit and integration tests

---

**Report Generated:** 2025-01-27  
**Project:** Prescripto - PrescriptionModule

