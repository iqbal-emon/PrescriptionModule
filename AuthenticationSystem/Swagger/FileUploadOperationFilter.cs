using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AuthenticationSystem.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if any parameter has [FromForm] attribute and contains IFormFile
            var formParameters = context.ApiDescription.ActionDescriptor.Parameters
                .Where(p => p.BindingInfo?.BindingSource?.DisplayName == "Form" ||
                           p.BindingInfo?.BindingSource?.Id == "Form")
                .ToList();

            if (!formParameters.Any())
                return;

            // Check if any form parameter contains IFormFile
            var hasFileUpload = formParameters.Any(p =>
            {
                var paramType = p.ParameterType;
                if (paramType == typeof(IFormFile))
                    return true;
                
                // Check for nullable IFormFile
                var nullableUnderlyingType = Nullable.GetUnderlyingType(paramType);
                if (nullableUnderlyingType == typeof(IFormFile))
                    return true;
                
                // Check properties for IFormFile
                return paramType.GetProperties().Any(prop => 
                {
                    if (prop.PropertyType == typeof(IFormFile))
                        return true;
                    
                    var propNullableType = Nullable.GetUnderlyingType(prop.PropertyType);
                    return propNullableType == typeof(IFormFile);
                });
            });

            if (!hasFileUpload)
                return;

            // Clear existing parameters
            operation.Parameters?.Clear();

            // Get the DTO type from form parameters
            var dtoType = formParameters.FirstOrDefault()?.ParameterType;
            if (dtoType == null)
                return;

            // Create request body with multipart/form-data
            var properties = new Dictionary<string, OpenApiSchema>();
            var dtoProperties = dtoType.GetProperties();

            foreach (var property in dtoProperties)
            {
                var propType = property.PropertyType;
                var nullableUnderlyingType = Nullable.GetUnderlyingType(propType);
                
                // Check if property is IFormFile (nullable or non-nullable)
                if (propType == typeof(IFormFile) || nullableUnderlyingType == typeof(IFormFile))
                {
                    properties[property.Name] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary",
                        Description = "File to upload"
                    };
                }
                else
                {
                    properties[property.Name] = GetSchemaForType(property.PropertyType);
                }
            }

            operation.RequestBody = new OpenApiRequestBody
            {
                Required = true,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = properties
                        }
                    }
                }
            };
        }

        private OpenApiSchema GetSchemaForType(Type type)
        {
            var schema = new OpenApiSchema();

            // Handle nullable types
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType == typeof(int))
            {
                schema.Type = "integer";
                schema.Format = "int32";
            }
            else if (underlyingType == typeof(long))
            {
                schema.Type = "integer";
                schema.Format = "int64";
            }
            else if (underlyingType == typeof(string))
            {
                schema.Type = "string";
            }
            else if (underlyingType == typeof(bool))
            {
                schema.Type = "boolean";
            }
            else if (underlyingType == typeof(DateTime))
            {
                schema.Type = "string";
                schema.Format = "date-time";
            }
            else if (underlyingType == typeof(decimal) || underlyingType == typeof(double) || underlyingType == typeof(float))
            {
                schema.Type = "number";
            }
            else
            {
                schema.Type = "string";
            }

            return schema;
        }
    }
}

