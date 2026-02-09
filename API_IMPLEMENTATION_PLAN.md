# API Implementation Plan

## Summary

Based on API_LIST.md analysis, the following actions are required:

### ✅ Already Implemented
- Most Prescription API endpoints (api/2025-02/)
- Authentication endpoints
- User account management endpoints
- Most entity CRUD operations

### ⚠️ Needs Route Updates
- Main API endpoints need to support `api/app/` routes in addition to `api/2025-02/`
- Solution: Add route aliases or create wrapper controllers

### ❌ Missing Controllers
1. **LocationController** - For division and district endpoints
2. **SpecializationController** - For specialization master data (different from DoctorSpecialization)

### 📋 Route Mapping Required

#### Main API Routes (api/app/*)
- `/api/app/doctor-profile/*` → Currently at `api/2025-02/`
- `/api/app/patient-profile/*` → Currently at `api/2025-02/`
- `/api/app/doctor-schedule/*` → Currently at `api/2025-02/`
- `/api/app/doctor-chamber/*` → Currently at `api/2025-02/`
- `/api/app/doctor-degree/*` → Currently at `api/2025-02/`
- `/api/app/doctor-specialization/*` → Currently at `api/2025-02/`
- `/api/app/degree/*` → Currently at `api/2025-02/`
- `/api/app/speciality/*` → Currently at `api/2025-02/speciality`
- `/api/app/specialization/*` → **MISSING**
- `/api/app/prescription-master/*` → Currently scattered across Prescription controllers
- `/api/app/documents-attachment/*` → Currently at `api/2025-02/documents-attachment`
- `/api/app/notification/*` → Currently at `api/2025-02/notification`
- `/api/app/appointment/*` → Currently at `api/2025-02/appointment`

#### Prescription API Routes (api/2025-02/*)
- `/api/2025-02/gets-all-division_list` → **MISSING**
- `/api/2025-02/gets_district_by_division_id` → **MISSING**

## Implementation Strategy

### Phase 1: Create Missing Controllers
1. Create LocationController for division/district endpoints
2. Create SpecializationController for specialization master data

### Phase 2: Add Route Aliases
Add `[Route("api/app/...")]` attributes to existing controllers to support both route patterns

### Phase 3: Verify Database Alignment
- Compare all entity properties with database table columns
- Verify stored procedure parameters match entity properties

### Phase 4: Testing
- Test all endpoints match API_LIST.md
- Verify response formats
- Check authentication/authorization

## Next Steps

1. ✅ Create LocationController
2. ✅ Create SpecializationController  
3. ⏳ Add route aliases to existing controllers
4. ⏳ Verify database-entity alignment
5. ⏳ Test all endpoints

