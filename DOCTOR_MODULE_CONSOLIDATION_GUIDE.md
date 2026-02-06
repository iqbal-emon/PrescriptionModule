# Doctor Module Consolidation Guide

## Overview
This guide documents the consolidation of all doctor-related separate plugins into the main Doctor project.

## Modules to Consolidate
1. **DoctorChamber** → `Doctor/Chamber/`
2. **DoctorDegree** → `Doctor/Degree/`
3. **DoctorExpertise** → `Doctor/Expertise/`
4. **DoctorSchedule** → `Doctor/Schedule/`

## Namespace Changes
All namespaces should be updated from:
- `DoctorChamber.*` → `Doctor.Chamber.*` or `Doctor.*` (depending on structure)
- `DoctorDegree.*` → `Doctor.Degree.*` or `Doctor.*`
- `DoctorExpertise.*` → `Doctor.Expertise.*` or `Doctor.*`
- `DoctorSchedule.*` → `Doctor.Schedule.*` or `Doctor.*`

## File Structure in Doctor Project

```
Doctor/
├── Controllers/
│   ├── DoctorController.cs (existing)
│   ├── DoctorChamberController.cs (moved)
│   ├── DoctorDegreeController.cs (moved)
│   ├── DoctorExpertiseController.cs (moved)
│   └── DoctorScheduleController.cs (moved)
├── Application/
│   └── Services/
│       ├── DoctorService.cs (existing)
│       ├── DoctorChamberService.cs (moved)
│       ├── DoctorDegreeService.cs (moved)
│       ├── DoctorExpertiseService.cs (moved)
│       └── DoctorScheduleService.cs (moved)
├── Domain/
│   └── Repositories/
│       ├── Doctor/ (existing)
│       ├── DoctorChamber/ (moved)
│       ├── DoctorDegree/ (moved)
│       ├── DoctorExpertise/ (moved)
│       └── DoctorSchedule/ (moved)
├── Insfracture/
│   └── RepositoriesImplement/
│       ├── Doctor/ (existing)
│       ├── DoctorChamber/ (moved)
│       ├── DoctorDegree/ (moved)
│       ├── DoctorExpertise/ (moved)
│       └── DoctorSchedule/ (moved)
├── Dtos/
│   ├── RequestDto/
│   │   ├── DoctorDto/ (existing)
│   │   ├── DoctorChamberDto/ (moved)
│   │   ├── DoctorDegreeDto/ (moved)
│   │   ├── DoctorExpertiseDto/ (moved)
│   │   └── DoctorScheduleDto/ (moved)
│   └── ResponseDto/
│       ├── DoctorDto/ (existing)
│       ├── DoctorChamberDto/ (moved)
│       ├── DoctorDegreeDto/ (moved)
│       ├── DoctorExpertiseDto/ (moved)
│       └── DoctorScheduleDto/ (moved)
├── Utility/
│   ├── DoctorApiConstantsResponseMessage.cs (existing)
│   ├── DoctorResponseMessage.cs (existing)
│   ├── DoctorChamberApiConstantsResponseMessage.cs (moved)
│   ├── DoctorChamberResponseMessage.cs (moved)
│   ├── DoctorDegreeApiConstantsResponseMessage.cs (moved)
│   ├── DoctorDegreeResponseMessage.cs (moved)
│   ├── DoctorExpertiseApiConstantsResponseMessage.cs (moved)
│   ├── DoctorExpertiseResponseMessage.cs (moved)
│   ├── DoctorScheduleApiConstantsResponseMessage.cs (moved)
│   └── DoctorScheduleResponseMessage.cs (moved)
└── RegisterService.cs (updated to include all services)
```

## Files to Move

### DoctorChamber Module
1. Controllers/DoctorChamberController.cs ✅
2. Application/Services/DoctorChamberService.cs ✅
3. Domain/Repositories/DoctorChamber/IDoctorChamberQueryRepository.cs
4. Domain/Repositories/DoctorChamber/IDoctorChamberCommandRepository.cs
5. Insfracture/RepositoriesImplement/DoctorChamber/DoctorChamberQueryRepository.cs
6. Insfracture/RepositoriesImplement/DoctorChamber/DoctorChamberCommandRepository.cs
7. Dtos/RequestDto/DoctorChamberDto/DoctorChamberInsertRequestDto.cs
8. Dtos/RequestDto/DoctorChamberDto/DoctorChamberUpdateRequestDto.cs
9. Dtos/ResponseDto/DoctorChamberDto/DoctorChamberApiResponseDto.cs
10. Dtos/ResponseDto/DoctorChamberDto/DistrictApiResponseDto.cs
11. Dtos/ResponseDto/DoctorChamberDto/DivisionApiResponseDto.cs
12. Dtos/ResponseDto/DoctorChamberDto/DivisionApiResponse.cs
13. Utility/DoctorChamberResponseMessage.cs
14. Utility/DoctorChamberApiConstantsResponseMessage.cs

### DoctorDegree Module
1. Controllers/DoctorDegreeController.cs
2. Application/Services/DoctorDegreeService.cs
3. Domain/Repositories/DoctorDegree/IDoctorDegreeQueryRepository.cs
4. Domain/Repositories/DoctorDegree/IDoctorDegreeCommandRepository.cs
5. Insfracture/RepositoriesImplement/DoctorDegree/DoctorDegreeQueryRepository.cs
6. Insfracture/RepositoriesImplement/DoctorDegree/DoctorDegreeCommandRepository.cs
7. Dtos/RequestDto/DoctorDegreeDto/DoctorDegreeInsertRequestDto.cs
8. Dtos/RequestDto/DoctorDegreeDto/DoctorDegreeUpdateRequestDto.cs
9. Dtos/ResponseDto/DoctorDegreeDto/DoctorDegreeApiResponseDto.cs
10. Utility/DoctorDegreeResponseMessage.cs
11. Utility/DoctorDegreeApiConstantsResponseMessage.cs

### DoctorExpertise Module
1. Controllers/DoctorExpertiseController.cs
2. Application/Services/DoctorExpertiseService.cs
3. Domain/Repositories/DoctorExpertise/IDoctorExpertiseQueryRepository.cs
4. Domain/Repositories/DoctorExpertise/IDoctorExpertiseCommandRepository.cs
5. Insfracture/RepositoriesImplement/DoctorExpertise/DoctorExpertiseQueryRepository.cs
6. Insfracture/RepositoriesImplement/DoctorExpertise/DoctorExpertiseCommandRepository.cs
7. Dtos/RequestDto/DoctorExpertiseDto/DoctorExpertiseInsertRequestDto.cs
8. Dtos/RequestDto/DoctorExpertiseDto/DoctorExpertiseUpdateRequestDto.cs
9. Dtos/ResponseDto/DoctorExpertiseDto/DoctorExpertiseApiResponseDto.cs
10. Utility/DoctorExpertiseResponseMessage.cs
11. Utility/DoctorExpertiseApiConstantsResponseMessage.cs

### DoctorSchedule Module
1. Controllers/DoctorScheduleController.cs
2. Application/Services/DoctorScheduleService.cs
3. Domain/Repositories/DoctorSchedule/IDoctorScheduleQueryRepository.cs
4. Domain/Repositories/DoctorSchedule/IDoctorScheduleCommandRepository.cs
5. Insfracture/RepositoriesImplement/DoctorSchedule/DoctorScheduleQueryRepository.cs
6. Insfracture/RepositoriesImplement/DoctorSchedule/DoctorScheduleCommandRepository.cs
7. Dtos/RequestDto/DoctorScheduleDto/DoctorScheduleInsertRequestDto.cs
8. Dtos/RequestDto/DoctorScheduleDto/DoctorScheduleUpdateRequestDto.cs
9. Dtos/ResponseDto/DoctorScheduleDto/DoctorScheduleApiResponseDto.cs
10. Utility/DoctorScheduleResponseMessage.cs
11. Utility/DoctorScheduleApiConstantsResponseMessage.cs

## Steps Completed
- ✅ Updated RegisterService.cs to include all services
- ✅ Created DoctorChamberController.cs in Doctor/Controllers/
- ✅ Created DoctorChamberService.cs in Doctor/Application/Services/

## Next Steps
1. Move all remaining files from each module
2. Update all namespaces
3. Update all using statements
4. Test compilation
5. Remove old module folders (optional, after verification)

## Notes
- All files should maintain their original functionality
- Only namespaces and using statements need to be updated
- The RegisterService has been updated to register all services
- Controllers, Services, Repositories, DTOs, and Utilities all need to be moved

