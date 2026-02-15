using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using Utility;
using Utility.Response;

namespace DataAccess.DatabaseAccessLayer
{
    public class SqlDataAccessLayer : ISqlDataAccessLayer
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString = AppSettings.ConnectionStringForDapper;
        public SqlDataAccessLayer(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Received parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="paramiters"></param>
        /// <returns></returns>
        [Obsolete]
        public async Task<List<T>> LoadDataUsingProcedure<T, U>(string storedProcedure, U paramiters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var data = await connection.QueryAsync<T>(storedProcedure, paramiters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 120);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<T>();
            }

        }
        /// <summary>
        /// Received parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="paramiters"></param>
        /// <returns></returns>
        public async Task<T?> LoadSingleDataUsingProcedure<T, U>(string storedProcedure, U paramiters)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var data = await connection.QueryAsync<T>(storedProcedure, paramiters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 120);

                return data.First();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Console.WriteLine(ex.Message);

                return default;
            }

        }
        // DataAccess/DatabaseAccessLayer/SqlDataAccessLayer.cs
        // DataAccess/DatabaseAccessLayer/SqlDataAccessLayer.cs
        public async Task<(T1 FirstResult, List<T2> SecondResult)> LoadMultipleResultUsingProcedure<T1, T2, U>(
       string storedProcedure, U parameters)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            using var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            // প্রথম রেজাল্টটা যদি COUNT বা Single Row হয়:
            var firstResultList = await multi.ReadAsync<T1>();
            var firstResult = firstResultList.FirstOrDefault(); // নিরাপদভাবে নাও

            // দ্বিতীয় রেজাল্ট লিস্ট
            var secondResult = (await multi.ReadAsync<T2>()).ToList();

            return (firstResult, secondResult);
        }



        // DataAccess layer - নতুন method তৈরি করুন
        public async Task<(int TotalCount, List<T> Data)> LoadPagedDataUsingProcedure<T, U>(
            string storedProcedure, U parameters)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            using var multi = await connection.QueryMultipleAsync(
                storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            // প্রথম result set থেকে TotalCount read করুন
            var totalCountResult = await multi.ReadFirstOrDefaultAsync<dynamic>();
            int totalCount = totalCountResult?.TotalCount ?? 0;

            // দ্বিতীয় result set থেকে data read করুন
            var data = (await multi.ReadAsync<T>()).ToList();

            return (totalCount, data);
        }

        /// <summary>
        /// Here received model for parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<T>> LoadDataUsingProcedure<T>(string storedProcedure, T model)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                var data = await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 120);

                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<T>();
            }

        }
        /// <summary>
        /// Here received model for parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<T?> LoadSingleDataUsingProcedure<T>(string storedProcedure, T model)
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                var data = await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, commandTimeout: 120);

                return data.First();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Console.WriteLine(ex.Message);

                return default;
            }

        }
        /// <summary>
        /// Here received model for parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveDataUsingProcedure<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID
                //parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Data insert and return same model when data insert successfully
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
		public async Task<T> SaveDataUsingProcedureAndReturnModel<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter if you need to capture any value like an ID from the stored procedure
                // Example for Guid: (if there's an output ID from the DB after insertion)
                // parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // After successful insertion, return the model
                return model;
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Data insert and return data when data insert successfully 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns>TResult</returns>
        public async Task<TResult> SaveDataUsingProcedureAndReturnData<T, TResult>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model
                await AddParameters(parameters, model);

                // Call the stored procedure and capture the result (assuming the SP returns data after update)
                var result = await connection.QueryFirstOrDefaultAsync<TResult>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                return result; // Return the data received from the SP
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Here received model for parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> SaveDataUsingProcedureReturnId<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID
                parameters.Add("@id", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                Guid id = parameters.Get<Guid>("@id");
                return id;
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Data insert and return Guid or INT 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
		public async Task<(Guid? ReturnGuid, int? ReturnINT)> SaveDataUsingProcedureReturnIdTuple<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID (Guid or int)
                parameters.Add("@id", dbType: DbType.Object, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                var idValue = parameters.Get<object>("@id");

                // Return the result as either a Guid or an int
                if (idValue != null)
                {
                    if (Guid.TryParse(idValue.ToString(), out Guid guidId))
                    {
                        return (guidId, null);
                    }
                    else if (int.TryParse(idValue.ToString(), out int intId))
                    {
                        return (null, intId);
                    }
                    else
                    {
                        throw new NullReferenceException("Unexpected output parameter type.");
                    }
                }

                return (null, null);
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Data insert and return INT Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
		public async Task<int> SaveDataUsingProcedureReturnIntId<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID (int)
                parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value (ID)
                int id = parameters.Get<int>("@id");
                return id;
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Data insert and return Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
		public async Task<T> SaveDataUsingProcedureReturnObject<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID
                parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                return model;
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Here received model for parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateDataUsingProcedure<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID
                // parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Input);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Here received model for parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> UpdateDataUsingProcedureReturnId<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter to capture the ID
                parameters.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                Guid id = parameters.Get<Guid>("@Id");
                return id;
            }
            catch (SqlException sqlEx)
            {
                // Log or handle SQL-specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
        /// <summary>
        /// Execute Raw SQL query and received model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="model"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<Response<T>> ExecuteSqlQueryWithModel<T>(string sqlQuery, T model, string connectionId = "Default")
        {
            var response = new Response<T>();
            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                await connection.ExecuteAsync(sqlQuery, parameters);
                response.IsSuccess = true;
                response.StatusCode = 200;
            }
            catch (SqlException sqlEx)
            {
                response.IsSuccess = true;
                response.StatusCode = 200;
                response.Message = sqlEx.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = true;
                response.StatusCode = 200;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
        /// <summary>
        /// Execute Raw SQL query and received parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<Response<T>> ExecuteRawSqlQueryWithParameters<T>(string sqlQuery, T parameters, string connectionId = "Default")
        {
            var response = new Response<T>();
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

                await connection.ExecuteAsync(sqlQuery, parameters);
                response.IsSuccess = true;
                response.StatusCode = 200;
            }
            catch (SqlException sqlEx)
            {
                response.IsSuccess = true;
                response.StatusCode = 200;
                response.Message = sqlEx.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = true;
                response.StatusCode = 200;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
        /// <summary>
        /// Received dynamic parameter and converted in parameter value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="model"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private async Task AddParameters<T>(DynamicParameters parameters, T model, string prefix = "")
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                var propValue = prop.GetValue(model);
                var paramName = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}_{prop.Name}";

                if (propValue != null && prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    await AddParameters(parameters, propValue, paramName);
                }
                else
                {
                    parameters.Add(paramName, propValue);
                }
            }
        }
        /// <summary>
        /// Received dynamic parameter and converted in parameter value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="model"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        private async Task AddParameter<T>(DynamicParameters parameters, T model, string prefix = "")
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                var propValue = prop.GetValue(model);
                var paramName = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}_{prop.Name}";

                // Check if the property is a class, and not a string
                if (propValue != null && prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    // Recursively add parameters for nested properties
                    await AddParameter(parameters, propValue, paramName);
                }
                else
                {
                    // Get DbType based on property type
                    DbType dbType = await GetDbType(prop.PropertyType);

                    // Add the parameter with the specified type
                    parameters.Add(paramName, propValue, dbType);
                }
            }
        }
        /// <summary>
        /// Get parameter type from property type.
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
		private async Task<DbType> GetDbType(Type propertyType)
        {
            await Task.Yield();
            // Mapping .NET types to corresponding DbType
            if (propertyType == typeof(int) || propertyType == typeof(int?))
                return DbType.Int32;
            if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                return DbType.Guid;
            if (propertyType == typeof(string))
                return DbType.String;
            if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                return DbType.Boolean;
            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                return DbType.DateTime;
            if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                return DbType.Decimal;
            if (propertyType == typeof(double) || propertyType == typeof(double?))
                return DbType.Double;
            if (propertyType == typeof(float) || propertyType == typeof(float?))
                return DbType.Single;
            if (propertyType == typeof(byte[]))
                return DbType.Binary;

            // Default to Object for unsupported or unknown types
            return DbType.Object;
        }

        public async Task<int> SaveDataUsingProcedureReturnIdWithIntDataType<T>(string storedProcedure, T model)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);
                
                // Log parameters being sent (for debugging)
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine($"🔍 SaveDataUsingProcedureReturnIdWithIntDataType - {storedProcedure}");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine($"📋 Parameter Names ({parameters.ParameterNames.Count()}): {string.Join(", ", parameters.ParameterNames)}");
                foreach (var paramName in parameters.ParameterNames)
                {
                    var paramValue = parameters.Get<object>(paramName);
                    Console.WriteLine($"   @{paramName}: {paramValue} (Type: {paramValue?.GetType().Name ?? "null"})");
                }
                Console.WriteLine("═══════════════════════════════════════════════════════════");

                // Find the ID property that should be the OUTPUT parameter
                // Exclude properties that are clearly input parameters (like DoctorProfileId, SessionId, ScheduleId, etc.)
                // Only treat properties as OUTPUT if they represent the generated entity ID (like Id, AppointmentId, etc.)
                var excludedNames = new[] { "DoctorProfileId", "SessionId", "ScheduleId", "PatientId", "UserId", 
                                           "SpecialityId", "ChamberId", "TenantId", "ReferenceId", "ReferenceUserId" };
                
                var idProperty = typeof(T).GetProperties()
                    .FirstOrDefault(p => 
                        (p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) || 
                         p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase) ||
                         (p.Name.EndsWith("ID", StringComparison.OrdinalIgnoreCase) && 
                          !excludedNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase))) 
                        && (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?)));

                string outputParameterName = null;
                bool hasOutputParameter = false;

                if (idProperty != null)
                {
                    outputParameterName = idProperty.Name;
                    // Add as OUTPUT parameter with @ prefix for SQL Server
                    // Note: Adding with same name will overwrite if it was already added as input parameter
                    parameters.Add($"@{outputParameterName}", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    hasOutputParameter = true;
                }
                else
                {
                    // Fallback: try common OUTPUT parameter names
                    var commonNames = new[] { "id", "Id", "ID", "UserID" };
                    foreach (var name in commonNames)
                    {
                        if (parameters.ParameterNames.Contains(name))
                        {
                            outputParameterName = name;
                            // Add as OUTPUT parameter - will overwrite if already exists
                            parameters.Add($"@{name}", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            hasOutputParameter = true;
                            break;
                        }
                    }
                    // If no common name found, try adding "@id" as OUTPUT parameter
                    if (!hasOutputParameter)
                    {
                        outputParameterName = "id";
                        parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        hasOutputParameter = true;
                    }
                }

                // Try OUTPUT parameter approach first
                if (hasOutputParameter)
                {
                    try
                    {
                        // Execute the stored procedure
                        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                        // Retrieve the output parameter value
                        int id = parameters.Get<int>($"@{outputParameterName}");
                        // Return the value (even if 0, as it might be valid in some cases)
                        return id;
                    }
                    catch (SqlException sqlEx)
                    {
                        // If the stored procedure doesn't have the OUTPUT parameter, 
                        // SQL Server will throw an error. Fall back to ExecuteScalarAsync approach.
                        // Check if error is related to parameter (common error codes: 201, 8144, 8145)
                        if (sqlEx.Number == 201 || sqlEx.Number == 8144 || sqlEx.Number == 8145 || 
                            sqlEx.Message.Contains("parameter") || sqlEx.Message.Contains("Parameter"))
                        {
                            // Recreate parameters without OUTPUT parameter and try ExecuteScalarAsync instead
                            var parametersWithoutOutput = new DynamicParameters();
                            await AddParameters(parametersWithoutOutput, model);
                            var scalarResult = await connection.ExecuteScalarAsync<int>(storedProcedure, parametersWithoutOutput, commandType: CommandType.StoredProcedure);
                            return scalarResult;
                        }
                        // Re-throw if it's a different SQL error
                        throw;
                    }
                }

                // If no OUTPUT parameter was added, use QueryFirstOrDefaultAsync (for SELECT statements)
                // This handles cases like Appointment_Insert which uses SELECT @SerialNo AS SerialNo
                // or Doctor_Insert which uses SELECT SCOPE_IDENTITY() AS DoctorID
                
                // For stored procedures that return a single value via SELECT, use QueryFirstOrDefaultAsync
                // to handle both success (returns value) and error (returns error info) cases
                var resultSet = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    storedProcedure, 
                    parameters, 
                    commandType: CommandType.StoredProcedure
                );
                
                if (resultSet == null)
                {
                    Console.WriteLine("⚠️ Stored procedure returned null result");
                    return 0;
                }
                
                // Convert to dictionary for easier access
                var resultDict = (IDictionary<string, object>)resultSet;
                
                // Log all keys and values for debugging
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("🔍 Stored Procedure Result Analysis");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine($"📋 Result Keys: {string.Join(", ", resultDict.Keys)}");
                foreach (var kvp in resultDict)
                {
                    Console.WriteLine($"   {kvp.Key}: {kvp.Value} (Type: {kvp.Value?.GetType().Name ?? "null"})");
                }
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                
                // Check if result contains error information (from CATCH block)
                if (resultDict.ContainsKey("ErrorNumber") || resultDict.ContainsKey("ErrorMessage"))
                {
                    var errorNumber = resultDict.ContainsKey("ErrorNumber") ? resultDict["ErrorNumber"] : null;
                    var errorMessage = resultDict.ContainsKey("ErrorMessage") ? resultDict["ErrorMessage"] : "Unknown error";
                    
                    Console.WriteLine("❌ Stored procedure returned ERROR:");
                    Console.WriteLine($"   Error Number: {errorNumber}");
                    Console.WriteLine($"   Error Message: {errorMessage}");
                    Console.WriteLine("═══════════════════════════════════════════════════════════");
                    
                    throw new Exception($"Stored procedure error {errorNumber}: {errorMessage}");
                }
                
                // Try to get the value - check common return column names
                if (resultDict.ContainsKey("SerialNo"))
                {
                    var serialNo = Convert.ToInt32(resultDict["SerialNo"]);
                    Console.WriteLine($"✅ Successfully extracted SerialNo: {serialNo}");
                    return serialNo;
                }
                else if (resultDict.ContainsKey("SerialNO"))
                {
                    var serialNo = Convert.ToInt32(resultDict["SerialNO"]);
                    Console.WriteLine($"✅ Successfully extracted SerialNO: {serialNo}");
                    return serialNo;
                }
                else if (resultDict.ContainsKey("Id"))
                {
                    var id = Convert.ToInt32(resultDict["Id"]);
                    Console.WriteLine($"✅ Successfully extracted Id: {id}");
                    return id;
                }
                else if (resultDict.ContainsKey("ID"))
                {
                    var id = Convert.ToInt32(resultDict["ID"]);
                    Console.WriteLine($"✅ Successfully extracted ID: {id}");
                    return id;
                }
                else
                {
                    // Fallback: get first property value
                    var firstValue = resultDict.Values.FirstOrDefault();
                    Console.WriteLine($"⚠️ No recognized column found (expected SerialNo, Id, or ID)");
                    Console.WriteLine($"⚠️ First value in result: {firstValue}");
                    
                    if (firstValue != null && int.TryParse(firstValue.ToString(), out int intValue))
                    {
                        // Check if this looks like an error number
                        // SQL Server system errors: 1-49999
                        // User-defined errors: 50000+
                        // Common constraint errors: 515, 547, etc.
                        if (intValue >= 50000 || (intValue >= 1 && intValue <= 49999 && intValue != 0))
                        {
                            Console.WriteLine($"❌ Value {intValue} looks like a SQL error number!");
                            Console.WriteLine($"❌ This suggests the stored procedure failed but didn't return ErrorNumber/ErrorMessage keys");
                            Console.WriteLine($"❌ Result structure: {string.Join(", ", resultDict.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
                            
                            // Try to get error message from result if available
                            string errorMsg = "Unknown database error";
                            if (resultDict.ContainsKey("ErrorMessage"))
                            {
                                errorMsg = resultDict["ErrorMessage"]?.ToString() ?? errorMsg;
                            }
                            else if (resultDict.Values.Count > 1)
                            {
                                // Maybe error message is in second value
                                var secondValue = resultDict.Values.Skip(1).FirstOrDefault();
                                if (secondValue != null)
                                {
                                    errorMsg = secondValue.ToString();
                                }
                            }
                            
                            throw new Exception($"Stored procedure returned error number {intValue}. {errorMsg}");
                        }
                        
                        // If it's a small positive number, it might be a valid SerialNo
                        // But we should log a warning
                        if (intValue > 0 && intValue < 1000)
                        {
                            Console.WriteLine($"⚠️ Returning value {intValue} as SerialNo (but column name not recognized)");
                        }
                        
                        return intValue;
                    }
                    Console.WriteLine("❌ Could not extract integer value from result");
                    return 0;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Data insert and return INT Id with custom output parameter name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="model"></param>
        /// <param name="outputParameterName"></param>
        /// <returns></returns>
        public async Task<int> SaveDataUsingProcedureReturnIntIdWithCustomOutput<T>(string storedProcedure, T model, string outputParameterName = "@UserID")
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            try
            {
                var parameters = new DynamicParameters();

                // Add parameters for all properties of the model, including nested ones
                await AddParameters(parameters, model);

                // Add an output parameter with custom name to capture the ID (int)
                parameters.Add(outputParameterName, dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value (ID)
                int id = parameters.Get<int>(outputParameterName);
                return id;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

    }
}
