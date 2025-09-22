using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace DataAccess.DatabaseAccessLayer
{
    public interface ISqlDataAccessLayer
    {
        Task<List<T>> LoadDataUsingProcedure<T, U>(string storedProcedure, U paramiters);
        Task<T?> LoadSingleDataUsingProcedure<T, U>(string storedProcedure, U paramiters);
        Task<List<T>> LoadDataUsingProcedure<T>(string storedProcedure, T model);
        Task<T?> LoadSingleDataUsingProcedure<T>(string storedProcedure, T model);
        Task SaveDataUsingProcedure<T>(string storedProcedure, T model);
        Task<T> SaveDataUsingProcedureAndReturnModel<T>(string storedProcedure, T model);
        Task<TResult> SaveDataUsingProcedureAndReturnData<T, TResult>(string storedProcedure, T model);
        Task<Guid> SaveDataUsingProcedureReturnId<T>(string storedProcedure, T model);
        Task<(Guid? ReturnGuid, int? ReturnINT)> SaveDataUsingProcedureReturnIdTuple<T>(string storedProcedure, T model);
        Task<int> SaveDataUsingProcedureReturnIntId<T>(string storedProcedure, T model);
        Task<T> SaveDataUsingProcedureReturnObject<T>(string storedProcedure, T model);
        Task UpdateDataUsingProcedure<T>(string storedProcedure, T model);
        Task<Guid> UpdateDataUsingProcedureReturnId<T>(string storedProcedure, T model);
        Task<Response<T>> ExecuteSqlQueryWithModel<T>(string sqlQuery, T model, string connectionId = "Default");
        Task<Response<T>> ExecuteRawSqlQueryWithParameters<T>(string sqlQuery, T parameters, string connectionId = "Default");
        Task<int> SaveDataUsingProcedureReturnIdWithIntDataType<T>(string storedProcedure, T model);
    }
}
