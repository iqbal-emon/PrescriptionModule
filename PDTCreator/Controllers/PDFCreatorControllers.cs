using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PDTCreator.Application;
using PDTCreator.Dtos.RequestDto;
using PDTCreator.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Permission;

namespace PDTCreator.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class PDFCreatorControllers : ControllerBase
    {
        private readonly PDFWHKService _pdfCreatorService;
        public PDFCreatorControllers(PDFWHKService pdfCreatorService)
        {
            _pdfCreatorService = pdfCreatorService;
        }
        [Authorize(Policy = PermissionConstants.EmailTemplateGetAll)]
        [HttpPost("create-pdf")]
        public async Task<ActionResult<ApiResponse<PdfCreatorResponseDto>>> CreatePdf(PdfGenerationRequestDto request)
        {
            var apiResponse = new ApiResponse<string>();
            try
            {
                var response = await _pdfCreatorService.CreatePdf(request);
                if (response.IsSuccess)
                {
                    if (response.Result != null)
                    {

                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Pdf created successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Pdf creation failed");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Pdf creation failed");
            }
            return Ok(apiResponse);
        }
    }
}
