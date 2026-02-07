# Newly Created APIs - UI Usage Documentation

This document provides a comprehensive mapping of all newly created APIs to their UI usage, including which Angular services and components consume them.

---

## 📋 Table of Contents

1. [Speciality APIs](#1-speciality-apis)
2. [Specialization APIs](#2-specialization-apis)
3. [Documents Attachment APIs](#3-documents-attachment-apis)
4. [Notification APIs](#4-notification-apis)
5. [Summary](#summary)

---

## 1. Speciality APIs

### API Endpoints

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| POST | `/api/app/speciality` | Create speciality | ✅ Implemented |
| GET | `/api/app/speciality/{id}` | Get speciality by ID | ✅ Implemented |
| GET | `/api/app/speciality` | Get all specialities | ✅ Implemented |
| PUT | `/api/app/speciality` | Update speciality | ✅ Implemented |
| DELETE | `/api/app/speciality/{id}` | Delete speciality | ✅ Implemented |

**Total Endpoints**: 5

### UI Service

**File**: `src/app/api/services/speciality.service.ts`

```typescript
@Injectable({ providedIn: 'root' })
export class SpecialityService {
  create(input: SpecialityInputDto): Observable<SpecialityDto>
  get(id: number): Observable<SpecialityDto>
  getList(): Observable<SpecialityDto[]>
  update(input: SpecialityInputDto): Observable<SpecialityDto>
  delete(id: number): Observable<void>
}
```

### UI Components Using SpecialityService

#### 1. **SupperAdminSpecializationsComponent**
- **File**: `src/app/features-modules/supper-admin/specializations/supper-admin-specializations.component.ts`
- **Usage**:
  - `getList()` - Loads all specialities for dropdown selection
  - `create()` - Creates new speciality
  - `update()` - Updates existing speciality
  - `delete()` - Deletes speciality
- **Use Case**: Super Admin manages specialities (e.g., Cardiology, Neurology) in the system
- **Features**:
  - Create/Edit/Delete specialities
  - Display list of specialities
  - Form validation
  - Success/Error feedback

### API Request/Response Examples

#### Create Speciality
```http
POST /api/app/speciality
Content-Type: application/json

{
  "specialityName": "Cardiology",
  "description": "Heart and cardiovascular system",
  "tenantID": 1
}
```

#### Get All Specialities
```http
GET /api/app/speciality
```

#### Update Speciality
```http
PUT /api/app/speciality
Content-Type: application/json

{
  "specialityID": 1,
  "specialityName": "Cardiology Updated",
  "description": "Updated description",
  "tenantID": 1
}
```

#### Delete Speciality
```http
DELETE /api/app/speciality/1
```

---

## 2. Specialization APIs

### API Endpoints

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| POST | `/api/app/specialization` | Create specialization | ✅ Implemented |
| GET | `/api/app/specialization/{id}` | Get specialization by ID | ✅ Implemented |
| GET | `/api/app/specialization/by-speciality-id/{specialityId}` | Get by speciality ID | ✅ Implemented |
| GET | `/api/app/specialization` | Get all specializations | ✅ Implemented |
| GET | `/api/app/specialization/by-specialty-id/{specialityId}` | Get by speciality ID (alternative) | ✅ Implemented |
| GET | `/api/app/specialization/filtering` | Get filtered specializations | ✅ Implemented |
| PUT | `/api/app/specialization` | Update specialization | ✅ Implemented |
| DELETE | `/api/app/specialization/{id}` | Delete specialization | ✅ Implemented |

**Total Endpoints**: 8

### UI Service

**File**: `src/app/api/services/specialization.service.ts`

```typescript
@Injectable({ providedIn: 'root' })
export class SpecializationService {
  create(input: SpecializationInputDto): Observable<SpecializationDto>
  get(id: number): Observable<SpecializationDto>
  getBySpecialityId(specialityId: number): Observable<SpecializationDto>
  getList(): Observable<SpecializationDto[]>
  getListBySpecialtyId(specialityId: number): Observable<SpecializationDto[]>
  getListFiltering(): Observable<SpecializationDto[]>
  update(input: SpecializationInputDto): Observable<SpecializationDto>
  delete(id: number): Observable<void>
}
```

### UI Components Using SpecializationService

#### 1. **SupperAdminSpecializationsComponent**
- **File**: `src/app/features-modules/supper-admin/specializations/supper-admin-specializations.component.ts`
- **Usage**:
  - `getList()` - Loads all specializations for display
  - `create()` - Creates new specialization
  - `update()` - Updates existing specialization
  - `delete()` - Deletes specialization
- **Use Case**: Super Admin manages specializations (sub-categories of specialities) in the system
- **Features**:
  - Create/Edit/Delete specializations
  - Link specializations to specialities
  - Display list of specializations
  - Form validation
  - Success/Error feedback

#### 2. **SpecializationDetailsComponent** (Doctor Profile)
- **File**: `src/app/features-modules/doctor/profile-settings/components/specializations/specialization-details.component.ts`
- **Usage**:
  - `getBySpecialityId()` or `getListBySpecialtyId()` - Loads specializations for a specific speciality
  - Used when doctor selects a speciality, shows related specializations
- **Use Case**: Doctor profile settings - selecting specializations based on chosen speciality
- **Features**:
  - Dynamic loading of specializations based on speciality selection
  - Doctor specialization management

### API Request/Response Examples

#### Create Specialization
```http
POST /api/app/specialization
Content-Type: application/json

{
  "specializationName": "Interventional Cardiology",
  "specialityID": 1,
  "description": "Cardiac catheterization and interventions",
  "tenantID": 1
}
```

#### Get Specializations by Speciality ID
```http
GET /api/app/specialization/by-speciality-id/1
```

#### Get All Specializations
```http
GET /api/app/specialization
```

#### Get Filtered Specializations
```http
GET /api/app/specialization/filtering
```

---

## 3. Documents Attachment APIs

### API Endpoints

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| POST | `/api/app/documents-attachment` | Create document attachment | ✅ Implemented |
| DELETE | `/api/app/documents-attachment/{id}` | Delete attachment | ✅ Implemented |
| GET | `/api/app/documents-attachment/{id}` | Get attachment by ID | ✅ Implemented |
| GET | `/api/app/documents-attachment/attachment-info/{entityId}?entityType={type}&attachmentType={type}&relatedEntityid={id}` | Get attachment info | ✅ Implemented |
| GET | `/api/app/documents-attachment/document-info/{entityId}?entityType={type}&attachmentType={type}` | Get document info | ✅ Implemented |
| GET | `/api/app/documents-attachment?sorting={sort}&skipCount={skip}&maxResultCount={max}` | Get paginated attachments | ✅ Implemented |
| PUT | `/api/app/documents-attachment/{id}` | Update attachment | ✅ Implemented |

**Total Endpoints**: 7

### UI Service

**File**: `src/app/api/services/documents-attachment.service.ts`

```typescript
@Injectable({ providedIn: 'root' })
export class DocumentsAttachmentService {
  create(input: DocumentsAttachmentDto): Observable<DocumentsAttachmentDto>
  delete(id: number): Observable<void>
  get(id: number): Observable<DocumentsAttachmentDto>
  getAttachmentInfoByEntityTypeAndEntityIdAndAttachmentType(
    entityType: string,
    entityId: number,
    attachmentType: string,
    relatedEntityid?: number
  ): Observable<DocumentsAttachmentDto[]>
  getDocumentInfoByEntityTypeAndEntityIdAndAttachmentType(
    entityType: string,
    entityId: number,
    attachmentType: string
  ): Observable<DocumentsAttachmentDto>
  getList(input: PagedAndSortedResultRequestDto): Observable<PagedResultDto<DocumentsAttachmentDto>>
  update(id: number, input: DocumentsAttachmentDto): Observable<DocumentsAttachmentDto>
}
```

### UI Components Using DocumentsAttachmentService

#### 1. **DocumentsComponent** (Doctor Profile)
- **File**: `src/app/features-modules/doctor/profile-settings/components/documents/documents.component.ts`
- **Usage**:
  - `getAttachmentInfoByEntityTypeAndEntityIdAndAttachmentType()` - Loads doctor's documents
  - `create()` - Uploads new documents
  - `delete()` - Removes documents
  - `update()` - Updates document metadata
- **Use Case**: Doctor profile settings - managing profile documents (certificates, licenses, etc.)
- **Features**:
  - Document upload
  - Document list display
  - Document deletion
  - Document metadata editing

#### 2. **PreviousDocumentsDialogComponent** (Prescription)
- **File**: `src/app/features-modules/doctor/prescribe/components/others/previous-documents-dialog/previous-documents-dialog.component.ts`
- **Usage**:
  - `getDocumentInfoByEntityTypeAndEntityIdAndAttachmentType()` - Loads patient's previous documents
  - `getAttachmentInfoByEntityTypeAndEntityIdAndAttachmentType()` - Gets attachment list
- **Use Case**: During prescription creation, doctor can view patient's previous medical documents
- **Features**:
  - View patient documents
  - Access previous test results, reports
  - Document preview

### API Request/Response Examples

#### Create Document Attachment
```http
POST /api/app/documents-attachment
Content-Type: application/json

{
  "fileName": "doctor_certificate.pdf",
  "originalFileName": "Medical_Certificate.pdf",
  "path": "/uploads/documents/doctor_certificate.pdf",
  "entityType": "Doctor",
  "entityId": 123,
  "attachmentType": "Certificate",
  "relatedEntityid": null,
  "tenantID": 1
}
```

#### Get Attachment Info
```http
GET /api/app/documents-attachment/attachment-info/123?entityType=Doctor&attachmentType=Certificate&relatedEntityid=null
```

#### Get Document Info
```http
GET /api/app/documents-attachment/document-info/123?entityType=Patient&attachmentType=MedicalReport
```

#### Get Paginated Attachments
```http
GET /api/app/documents-attachment?sorting=CreatedAt DESC&skipCount=0&maxResultCount=10
```

#### Update Document Attachment
```http
PUT /api/app/documents-attachment/1
Content-Type: application/json

{
  "documentsAttachmentID": 1,
  "fileName": "updated_certificate.pdf",
  "originalFileName": "Updated_Certificate.pdf",
  "path": "/uploads/documents/updated_certificate.pdf",
  "entityType": "Doctor",
  "entityId": 123,
  "attachmentType": "Certificate",
  "relatedEntityid": null,
  "tenantID": 1
}
```

---

## 4. Notification APIs

### API Endpoints

| Method | Endpoint | Description | Status |
|--------|----------|-------------|--------|
| GET | `/api/app/notification/by-user-id/{userId}?role={role}` | Get notifications by user ID | ✅ Implemented |

**Total Endpoints**: 1

### UI Service

**File**: `src/app/api/services/notification.service.ts`

```typescript
@Injectable({ providedIn: 'root' })
export class NotificationService {
  getListByUserId(
    userId: number,
    role: string
  ): Observable<NotificationDto[]>
}
```

### UI Components Using NotificationService

#### 1. **Notification Components** (Various)
- **Usage**:
  - `getListByUserId()` - Loads notifications for a specific user
- **Use Case**: Display notifications to users (doctors, patients, admins) based on their role
- **Features**:
  - Role-based notification filtering
  - User-specific notifications
  - Real-time notification updates

### API Request/Response Examples

#### Get Notifications by User ID
```http
GET /api/app/notification/by-user-id/123?role=Doctor
```

**Response**:
```json
[
  {
    "notificationID": 1,
    "userId": 123,
    "title": "New Appointment",
    "message": "You have a new appointment scheduled",
    "isRead": false,
    "createdAt": "2025-02-15T10:30:00Z",
    "notificationType": "Appointment"
  }
]
```

---

## Summary

### Total Newly Created APIs: 21 Endpoints

| Module | Endpoints | UI Services | UI Components |
|--------|-----------|-------------|---------------|
| **Speciality** | 5 | 1 | 1 |
| **Specialization** | 8 | 1 | 2 |
| **Documents Attachment** | 7 | 1 | 2 |
| **Notification** | 1 | 1 | Multiple |

### API Base URL

All APIs use the base URL pattern:
```
{apiUrl}/api/app/{module}
```

Where `{apiUrl}` is configured in the Angular environment file.

### Authentication

All endpoints require authentication via JWT token:
- Header: `Authorization: Bearer {token}`
- Most endpoints also require specific permission policies

### Common Response Format

All APIs return responses in the following format:

```typescript
interface ApiResponse<T> {
  results: T;
  message: string;
  status: string;
  statusCode: number;
  isSuccess: boolean;
}
```

### Error Handling

All APIs follow consistent error handling:
- **400 Bad Request**: Invalid input or validation errors
- **401 Unauthorized**: Missing or invalid authentication token
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server-side errors

### UI Integration Points

1. **Super Admin Dashboard**: Speciality and Specialization management
2. **Doctor Profile Settings**: Specialization selection, Document management
3. **Prescription Module**: Previous documents viewing
4. **Notification System**: User notifications display

---

## Quick Reference

### Speciality APIs
- **Service**: `SpecialityService`
- **Main Component**: `SupperAdminSpecializationsComponent`
- **Use Case**: Master data management for medical specialities

### Specialization APIs
- **Service**: `SpecializationService`
- **Main Components**: 
  - `SupperAdminSpecializationsComponent` (Admin)
  - `SpecializationDetailsComponent` (Doctor Profile)
- **Use Case**: Master data management and doctor profile settings

### Documents Attachment APIs
- **Service**: `DocumentsAttachmentService`
- **Main Components**:
  - `DocumentsComponent` (Doctor Profile)
  - `PreviousDocumentsDialogComponent` (Prescription)
- **Use Case**: Document upload, management, and viewing

### Notification APIs
- **Service**: `NotificationService`
- **Main Components**: Various notification components
- **Use Case**: User notification system

---

**Last Updated**: 2025-02
**Status**: ✅ All APIs Documented and Mapped to UI Usage

