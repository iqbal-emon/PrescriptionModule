using Microsoft.Extensions.Configuration;
using PDTCreator.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace PDTCreator.Application
{
    public class PDFWHKService
    {
        private readonly IConfiguration _configuration;
        public PDFWHKService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> ConvertHtmlFileToPdf(string htmlFilePath, string pdfFilePath, string wkhtmltopdfPath)
        {
            //string wkhtmltopdfPath = @"D:\CutOutWiz Projects\Other\BulkEmailSenderWinApp\BulkEmailSenderWinApp.Pdf\whkExecuteFile\wkhtmltopdf.exe"; // Path to the wkhtmltopdf executable

            Process process = new Process();
            process.StartInfo.FileName = wkhtmltopdfPath;
            process.StartInfo.Arguments = $"\"{htmlFilePath}\" \"{pdfFilePath}\""; // Enclose paths in quotes to handle spaces
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            string output = "";

            process.OutputDataReceived += (sender, e) => output += e.Data + Environment.NewLine;
            process.ErrorDataReceived += (sender, e) => output += e.Data + Environment.NewLine;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                return pdfFilePath;
            }
            else
            {
                Console.WriteLine("Conversion failed with exit code: " + process.ExitCode);
                Console.WriteLine("Output and error messages:");
                Console.WriteLine(output);
                return null;
            }
        }

        public async Task<string> ConvertHtmlToPdf(string htmlContent, string pdfFilePath, string wkhtmltopdfPath, string fileName = null)
        {
            try
            {


                string whk = _configuration.GetSection("AppSettings").GetSection("PDFCREATEDPATH").Value;
                string subdirectory = "KOW";

                // Construct the full directory path
                string directoryPath = Path.Combine(whk, subdirectory);

                // Ensure directory exists
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Construct the full file path
                string tempHtmlFilePath = Path.Combine(directoryPath, fileName);
                File.WriteAllText(tempHtmlFilePath, htmlContent);


                Process process = new Process();
            process.StartInfo.FileName = wkhtmltopdfPath;
            process.StartInfo.Arguments = $"\"{tempHtmlFilePath}\" \"{pdfFilePath}\""; // Enclose paths in quotes to handle spaces
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            string output = "";

            process.OutputDataReceived += (sender, e) => output += e.Data + Environment.NewLine;
            process.ErrorDataReceived += (sender, e) => output += e.Data + Environment.NewLine;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(); // Use async wait for exit

            if (process.ExitCode == 0)
            {
                // Delete temporary HTML file
                File.Delete(tempHtmlFilePath);
                return pdfFilePath;
            }
            else
            {
                Console.WriteLine("Conversion failed with exit code: " + process.ExitCode);
                Console.WriteLine("Output and error messages:");
                Console.WriteLine(output);
                // Delete temporary HTML file
                File.Delete(tempHtmlFilePath);
                return null;
            }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<string>> CreatePdf(PdfGenerationRequestDto request)
        {
            var response = new Response<string>();  
            if (string.IsNullOrEmpty(request.WkhtmltopdfPath))
            {
                throw new Exception("Path to wkhtmltopdf executable is required");
            }
            if (string.IsNullOrEmpty(request.PdfFilePath))
            {
                throw new Exception("Path to save PDF file is required");
            }
            if (string.IsNullOrEmpty(request.HtmlContent))
            {
                throw new Exception("HTML content is required");
            }
            if (string.IsNullOrEmpty(request.FileName))
            {
                throw new Exception("File name is required");
            }
            var result = await ConvertHtmlToPdf(request.HtmlContent, request.PdfFilePath, request.WkhtmltopdfPath, request.FileName);
            if (result != null)
            {
                response.IsSuccess = true;
                response.Result = result;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "PDF creation failed";
            }
            return response;
        }
    }
}
