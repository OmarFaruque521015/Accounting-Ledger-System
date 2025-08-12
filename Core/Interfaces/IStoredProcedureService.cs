using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStoredProcedureService
    {
        Task<int> ExecuteNonQueryAsync(string procedureName, params object[] parameters);
        Task<T> ExecuteScalarAsync<T>(string procedureName, params object[] parameters);
        Task<List<T>> QueryAsync<T>(string procedureName, params object[] parameters) where T : class, new();
    }
}
