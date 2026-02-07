# API Migration Plan - Mycompany to PrescriptionModule

This document maps all APIs from API_LIST.md and identifies what needs to be implemented in PrescriptionModule.

## Overview
- **Total APIs Required**: 170
- **Authentication API**: 4 endpoints
- **Main API**: 118 endpoints  
- **Prescription API**: 47 endpoints
- **External APIs**: 1 endpoint

---

## 1. AUTHENTICATION API
**Base URL**: `{authenticationApi}/api/app/auth`

### Required Endpoints:
1. ✅ `POST /api/app/auth/login-api` - Login with user credentials
2. ❌ `POST /api/app/auth/refresh-token` - Refresh access token
3. ❌ `POST /api/app/auth/verify-access-token` - Verify access token validity
4. ❌ `POST /api/app/auth/firebase/verify` - Get user by Firebase token

### Current Status:
- PrescriptionModule has basic AuthController at `/api/auth/login`
- Need to add: refresh-token, verify-access-token, firebase/verify
- Need to change route to `/api/app/auth/`

---

## 2. MAIN API
**Base URL**: `{apiUrl}/api/app`

### 2.1 User Account Management (16 endpoints)

#### UserAccountsService:
1. ❌ `POST /api/app/user-accounts/decode-jwt`
2. ❌ `POST /api/app/user-accounts/is-user-exists?userName={userName}`
3. ❌ `POST /api/app/user-accounts/login`
4. ❌ `POST /api/app/user-accounts/refresh-access-token`
5. ❌ `POST /api/app/user-accounts/reset-password`
6. ❌ `POST /api/app/user-accounts/reset-password_App`
7. ❌ `POST /api/app/user-accounts/signup-user?password={password}&role={role}`
8. ❌ `POST /api/app/user-accounts/user-data-remove?role={role}`

#### UserManageAccountsService:
9. ❌ `POST /api/app/user-manage-accounts/check-user-exist-by-user-name?mobileNo={mobileNo}`
10. ❌ `GET /api/app/user-manage-accounts/user-roles/{userId}`
11. ❌ `POST /api/app/user-manage-accounts/reset-password`
12. ❌ `POST /api/app/user-manage-accounts/save-otp-for-verify-user-later`
13. ❌ `POST /api/app/user-manage-accounts/send-otp`
14. ❌ `POST /api/app/user-manage-accounts/signup-user`
15. ❌ `POST /api/app/user-manage-accounts/user-password-changes-script`
16. ❌ `POST /api/app/user-manage-accounts/verify-otp`

**Status**: All missing - Need to create UserAccountsController and UserManageAccountsController

---

### 2.2 Doctor Profile (22 endpoints)

1. ❌ `POST /api/app/doctor-profile` - Create doctor profile
2. ❌ `GET /api/app/doctor-profile/{id}` - Get doctor by ID
3. ❌ `GET /api/app/doctor-profile/active-doctor-list` - Get all active doctors
4. ❌ `GET /api/app/doctor-profile/by-user-id/{userId}` - Get doctor by user ID
5. ❌ `GET /api/app/doctor-profile/by-user-name?userName={userName}` - Get doctor by username
6. ❌ `GET /api/app/doctor-profile/by-user-email?emailAddress={email}` - Get doctor by email
7. ❌ `GET /api/app/doctor-profile/currently-online-doctor-list` - Get currently online doctors
8. ❌ `GET /api/app/doctor-profile/{id}/doctor-by-profile-id` - Get doctor by profile ID
9. ❌ `GET /api/app/doctor-profile/{id}/doctor-details-by-admin` - Get doctor details (admin)
10. ❌ `GET /api/app/doctor-profile/doctor-list-filter` - Get filtered doctor list
11. ❌ `GET /api/app/doctor-profile/doctor-list-filter-by-admin` - Get filtered doctor list (admin)
12. ❌ `GET /api/app/doctor-profile/doctor-list-filter-mobile-app` - Get filtered doctor list (mobile)
13. ❌ `GET /api/app/doctor-profile/doctors-count-by-filters` - Get doctors count by filters
14. ❌ `GET /api/app/doctor-profile` - Get all doctors
15. ❌ `GET /api/app/doctor-profile/doctor-list-by-admin` - Get doctor list (admin)
16. ❌ `GET /api/app/doctor-profile/live-online-doctor-list` - Get live online doctor list
17. ❌ `PUT /api/app/doctor-profile` - Update doctor profile
18. ❌ `PUT /api/app/doctor-profile/active-status-by-admin/{id}?activeStatus={status}` - Update active status (admin)
19. ❌ `PUT /api/app/doctor-profile/doctor-profile` - Update doctor profile
20. ❌ `PUT /api/app/doctor-profile/doctors-online-status/{id}?onlineStatus={status}` - Update online status
21. ❌ `PUT /api/app/doctor-profile/expertise/{id}?expertise={expertise}` - Update expertise
22. ❌ `PUT /api/app/doctor-profile/profile-step/{profileId}?step={step}` - Update profile step

**Status**: PrescriptionModule has Doctor module but routes are different. Need to add/update endpoints.

---

### 2.3 Patient Profile (15 endpoints)

1. ❌ `POST /api/app/patient-profile` - Create patient profile
2. ❌ `GET /api/app/patient-profile/{id}` - Get patient by ID
3. ✅ `GET /api/app/patient-profile/by-phone-and-code?pCode={code}&pPhone={phone}` - **IMPLEMENTED** (as `/api/2025-02/get-patient-by-phone-and-code`)
4. ❌ `GET /api/app/patient-profile/by-user-id/{userId}` - Get patient by user ID
5. ✅ `GET /api/app/patient-profile/by-user-name?userName={userName}` - **IMPLEMENTED** (as `/api/2025-02/get-patient-by-user-name`)
6. ❌ `GET /api/app/patient-profile/doctor-list-by-creator-id-filter/{profileId}` - Get doctor list by creator (filtered)
7. ✅ `GET /api/app/patient-profile/doctor-list-filter` - **IMPLEMENTED** (as `/api/2025-02/get-patient-list-filter`)
8. ✅ `GET /api/app/patient-profile` - **IMPLEMENTED** (as `/api/2025-02/get-all-patients-list`)
9. ❌ `GET /api/app/patient-profile/patient-list-by-admin` - Get patient list (admin)
10. ❌ `GET /api/app/patient-profile/patient-list-by-agent-master/{masterId}` - Get patients by agent master
11. ❌ `GET /api/app/patient-profile/patient-list-by-agent-super-visor/{supervisorId}` - Get patients by agent supervisor
12. ✅ `GET /api/app/patient-profile/patient-list-by-search-user-profile-id/{profileId}?role={role}&name={name}` - **IMPLEMENTED**
13. ✅ `GET /api/app/patient-profile/patient-list-by-user-profile-id/{profileId}?role={role}` - **IMPLEMENTED**
14. ❌ `GET /api/app/patient-profile/patient-list-filter-by-admin/{userId}?role={role}` - Get filtered patient list (admin)
15. ❌ `PUT /api/app/patient-profile` - Update patient profile

**Status**: Some endpoints implemented but routes need to match `/api/app/patient-profile/`. Need to add missing ones.

---

### 2.4 Appointment (2 endpoints)

1. ❌ `GET /api/app/appointment/patient-list-by-doctor-id/{doctorId}` - Get patients by doctor ID
2. ❌ `GET /api/app/doctor-schedule-day-session/session-list` - Get appointment session list

**Status**: PrescriptionModule has Appointment module but routes may differ.

---

### 2.5 Doctor Schedule (6 endpoints)

1. ❌ `POST /api/app/doctor-schedule` - Create doctor schedule
2. ❌ `GET /api/app/doctor-schedule/{id}` - Get schedule by ID
3. ❌ `GET /api/app/doctor-schedule/by-doctor-id-list/{doctorId}` - Get schedules by doctor ID
4. ❌ `GET /api/app/doctor-schedule/details-schedule-list-by-doctor-chamber-id?doctorId={id}&chamberId={id}` - Get schedule details
5. ❌ `PUT /api/app/doctor-schedule` - Update schedule
6. ❌ `DELETE /api/app/doctor-schedule/{id}` - Delete schedule

**Status**: PrescriptionModule has DoctorSchedule module but routes may differ.

---

### 2.6 Doctor Chamber (5 endpoints)

1. ❌ `POST /api/app/doctor-chamber` - Create doctor chamber
2. ❌ `PUT /api/app/doctor-chamber` - Update doctor chamber
3. ❌ `DELETE /api/app/doctor-chamber/{id}` - Delete doctor chamber
4. ❌ `GET /api/app/doctor-chamber/{id}` - Get chamber by ID
5. ❌ `GET /api/app/doctor-chamber/doctor-chamber-list-by-doctor-id/{doctorProfileId}` - Get chambers by doctor ID

**Status**: PrescriptionModule has DoctorChamber module but routes may differ.

---

### 2.7 Doctor Degree (7 endpoints)

1. ❌ `POST /api/app/doctor-degree` - Create doctor degree
2. ❌ `DELETE /api/app/doctor-degree/{id}` - Delete doctor degree
3. ❌ `GET /api/app/doctor-degree/{id}` - Get degree by ID
4. ❌ `GET /api/app/doctor-degree/doctor-degree-list-by-doctor-id/{doctorId}` - Get degrees by doctor ID
5. ❌ `GET /api/app/doctor-degree` - Get all degrees
6. ❌ `GET /api/app/doctor-degree/by-doctor-id/{doctorId}` - Get degrees by doctor ID (alternative)
7. ❌ `PUT /api/app/doctor-degree` - Update doctor degree

**Status**: PrescriptionModule has DoctorDegree module but routes may differ.

---

### 2.8 Doctor Specialization (10 endpoints)

1. ❌ `POST /api/app/doctor-specialization` - Create doctor specialization
2. ❌ `DELETE /api/app/doctor-specialization/{id}` - Delete specialization
3. ❌ `GET /api/app/doctor-specialization/{id}` - Get specialization by ID
4. ❌ `GET /api/app/doctor-specialization/by-speciality-id/{specialityId}` - Get by speciality ID
5. ❌ `GET /api/app/doctor-specialization/doctor-specialization-list-by-doctor-id/{doctorId}` - Get specializations by doctor ID
6. ❌ `GET /api/app/doctor-specialization/doctor-specialization-list-by-doctor-id-speciality-id?doctorId={id}&specialityId={id}` - Get by doctor and speciality ID
7. ❌ `GET /api/app/doctor-specialization/doctor-specialization-list-by-speciality-id/{specialityId}` - Get by speciality ID
8. ❌ `GET /api/app/doctor-specialization` - Get all specializations
9. ❌ `GET /api/app/doctor-specialization/by-doctor-id-sp-id?doctorId={id}&specialityId={id}` - Get by doctor ID and speciality ID
10. ❌ `PUT /api/app/doctor-specialization` - Update specialization

**Status**: PrescriptionModule has DoctorSpecialization module but routes may differ.

---

### 2.9 Degree (Master Data) (4 endpoints)

1. ❌ `POST /api/app/degree` - Create degree
2. ❌ `GET /api/app/degree/{id}` - Get degree by ID
3. ❌ `GET /api/app/degree` - Get all degrees
4. ❌ `PUT /api/app/degree` - Update degree

**Status**: PrescriptionModule has Degree module but routes may differ.

---

### 2.10 Speciality (Master Data) (5 endpoints)

1. ❌ `POST /api/app/speciality` - Create speciality
2. ❌ `GET /api/app/speciality/{id}` - Get speciality by ID
3. ❌ `GET /api/app/speciality` - Get all specialities
4. ❌ `PUT /api/app/speciality` - Update speciality
5. ❌ `DELETE /api/app/speciality/{id}` - Delete speciality

**Status**: Need to check if Speciality module exists.

---

### 2.11 Specialization (Master Data) (8 endpoints)

1. ❌ `POST /api/app/specialization` - Create specialization
2. ❌ `GET /api/app/specialization/{id}` - Get specialization by ID
3. ❌ `GET /api/app/specialization/by-speciality-id/{specialityId}` - Get by speciality ID
4. ❌ `GET /api/app/specialization` - Get all specializations
5. ❌ `GET /api/app/specialization/by-specialty-id/{specialityId}` - Get by speciality ID (alternative)
6. ❌ `GET /api/app/specialization/filtering` - Get filtered specializations
7. ❌ `PUT /api/app/specialization` - Update specialization
8. ❌ `DELETE /api/app/specialization/{id}` - Delete specialization

**Status**: Need to check if Specialization module exists.

---

### 2.12 Prescription Master (10 endpoints)

1. ❌ `POST /api/app/prescription-master` - Create prescription master
2. ❌ `GET /api/app/prescription-master/{id}` - Get prescription by ID
3. ❌ `GET /api/app/prescription-master` - Get all prescriptions
4. ❌ `GET /api/app/prescription-master/patient-disease-list/{patientId}` - Get patient disease list
5. ❌ `GET /api/app/prescription-master/prescription-count` - Get prescription count
6. ❌ `GET /api/app/prescription-master/prescription-list-by-appointment-creator-id/{patientId}` - Get prescriptions by appointment creator
7. ❌ `GET /api/app/prescription-master/prescription-master-list-by-doctor-id/{doctorId}` - Get prescriptions by doctor ID
8. ❌ `GET /api/app/prescription-master/prescription-master-list-by-doctor-id-patient-id?doctorId={id}&patientId={id}` - Get prescriptions by doctor and patient ID
9. ❌ `GET /api/app/prescription-master/prescription-master-list-by-patient-id/{patientId}` - Get prescriptions by patient ID
10. ❌ `PUT /api/app/prescription-master` - Update prescription master

**Status**: PrescriptionModule has Prescription module but routes may differ.

---

### 2.13 Documents Attachment (7 endpoints)

1. ❌ `POST /api/app/documents-attachment` - Create document attachment
2. ❌ `DELETE /api/app/documents-attachment/{id}` - Delete attachment
3. ❌ `GET /api/app/documents-attachment/{id}` - Get attachment by ID
4. ❌ `GET /api/app/documents-attachment/attachment-info/{entityId}?entityType={type}&attachmentType={type}&relatedEntityid={id}` - Get attachment info
5. ❌ `GET /api/app/documents-attachment/document-info/{entityId}?entityType={type}&attachmentType={type}` - Get document info
6. ❌ `GET /api/app/documents-attachment?sorting={sort}&skipCount={skip}&maxResultCount={max}` - Get paginated attachments
7. ❌ `PUT /api/app/documents-attachment/{id}` - Update attachment

**Status**: Need to check if DocumentsAttachment module exists.

---

### 2.14 Notification (1 endpoint)

1. ❌ `GET /api/app/notification/by-user-id/{userId}?role={role}` - Get notifications by user ID

**Status**: PrescriptionModule has Notification module but route may differ.

---

## 3. PRESCRIPTION API
**Base URL**: `{prescriptionApi}/api/2025-02` (or `/api/2025-20` for some endpoints)

### 3.1 Location (2 endpoints)

1. ❌ `GET /api/2025-02/gets-all-division_list` - Get all divisions
2. ❌ `GET /api/2025-02/gets_district_by_division_id?divisonId={id}` - Get districts by division ID

**Status**: Need to check if Location module exists.

---

### 3.2 Prescription (10 endpoints)

1. ✅ `POST /api/2025-02/create-prescription` - **EXISTS** in PrescriptionModule
2. ✅ `POST /api/2025-02/create-prescription` - Save prescription as template - **EXISTS**
3. ✅ `GET /api/2025-02/get-template-prescription-by-id?templateId={id}` - **EXISTS**
4. ✅ `GET /api/2025-02/gets-all-prescription-template-by-doctor-id?DoctorId={id}` - **EXISTS**
5. ❌ `GET /api/2025-02/get-pdf-prescriptions-by-patient-doctor-id?patientId={id}&doctorId={id}` - Get PDF prescriptions
6. ❌ `GET /api/2025-02/get-pdf-prescriptions-by-doctor-prehand-id?doctorId={id}` - Get pre-hand prescriptions
7. ❌ `GET /api/2025-02/get-pdf-prescriptions-by-patient-doctor-id?patientId={id}` - Get PDF prescriptions by patient ID
8. ❌ `GET /api/2025-02/get-prescription-pdf-by-appointment-id?appointmentId={id}` - Get prescription PDF by appointment ID
9. ❌ `GET /api/app/prescription-master/{id}/prescription` - Get prescription (prescription API)
10. ❌ `GET /api/app/prescription-master/prescription-by-appointment-id/{appointmentId}` - Get prescription by appointment ID

**Status**: Some endpoints exist, need to add missing PDF-related endpoints.

---

### 3.3 Appointment (Prescription API) (3 endpoints)

1. ❌ `GET /api/2025-20/appointment/get-by-id?id={id}` - Get appointment by ID
2. ❌ `GET /api/2025-20/appointment/appointment-get-by-doctorId?doctorId={id}` - Get appointments by doctor ID
3. ❌ `POST /api/2025-20/appointment/create_appointment` - Create appointment

**Status**: PrescriptionModule has Appointment module but routes may differ.

---

### 3.4-3.12 Other Prescription APIs (35 endpoints)

Most of these exist in PrescriptionModule:
- Advice (4 endpoints)
- Diagnosis (4 endpoints)
- Follow-up (4 endpoints)
- Investigation (3 endpoints)
- Medicine/Medication (4 endpoints)
- Chief Complaints (4 endpoints)
- History (4 endpoints)
- Upload Image (1 endpoint)
- Admin Prescription Analytics (4 endpoints)

**Status**: Need to verify all routes match API_LIST.md

---

## Implementation Priority

### Phase 1: Critical APIs (Authentication & User Management)
1. Authentication API (4 endpoints)
2. User Account Management (16 endpoints)

### Phase 2: Core Profile APIs
3. Doctor Profile (22 endpoints)
4. Patient Profile (remaining endpoints)
5. Appointment (2 endpoints)

### Phase 3: Supporting APIs
6. Doctor Schedule, Chamber, Degree, Specialization
7. Master Data (Degree, Speciality, Specialization)
8. Prescription Master
9. Documents Attachment
10. Notification

### Phase 4: Prescription API Enhancements
11. Location APIs
12. PDF-related endpoints
13. Appointment (Prescription API)
14. Verify all Prescription API routes

---

## Notes

1. **Route Differences**: 
   - Mycompany uses `/api/app/` for Main API
   - PrescriptionModule uses `/api/2025-02/` for Prescription API
   - Need to create a unified routing strategy or adapter layer

2. **Module Structure**:
   - Mycompany uses ABP Framework (auto-generates REST from services)
   - PrescriptionModule uses explicit controllers
   - Need to create controllers matching API_LIST.md routes

3. **Agent Functionality**:
   - Some Patient endpoints reference Agent entities
   - May need to implement Agent module or adapt logic

4. **Database**:
   - Many endpoints will require new stored procedures
   - Need to create SQL scripts for all new endpoints

