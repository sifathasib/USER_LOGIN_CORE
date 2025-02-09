using Oracle.ManagedDataAccess.Client;
using System.Data;
using Microsoft.Extensions.Configuration;
namespace USER_LOGIN.Data
{
    public class DataAccess
    {
        private readonly string? _connectionString;
        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:OracleDbConnection"];
        }
        public  OracleConnection GetConnection()
        { 
            
            return new OracleConnection(_connectionString);
        }
        /// <summary>
        /// Executes the given SQL query with Oracle parameters and returns the result as a DataTable.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">A variable number of OracleParameter objects.</param>
        /// <returns>A DataTable containing the query results.</returns>
        public async Task<DataTable> ExecuteQueryAsync(string query, params OracleParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            // Create and open the Oracle connection.
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                // Create a command with the provided query.
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    // Add each provided parameter to the command.
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            // If the parameter value is null, use DBNull.Value.
                            parameter.Value = parameter.Value ?? DBNull.Value;
                            command.Parameters.Add(parameter);
                        }
                    }

                    // Execute the query and load the results into the DataTable.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }
    }
}