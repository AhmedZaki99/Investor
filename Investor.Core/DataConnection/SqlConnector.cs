using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Investor.Core
{
    public abstract class SqlConnector : IAsyncDisposable
    {

        #region Private Members

        private readonly SqlConnection connection;
        private readonly SqlCommand command;

        #endregion

        #region Protected Members

        protected abstract string CnnString { get; } 

        #endregion


        #region Constructor

        public SqlConnector()
        {
            connection = new SqlConnection(CnnString);
            command = new SqlCommand()
            {
                Connection = connection
            };
        }

        #endregion


        #region Connection

        public async Task<bool> OpenSqlConnectionAsync()
        {
            await connection.OpenAsync();

            return connection.State == System.Data.ConnectionState.Open;
        }

        public async ValueTask DisposeAsync()
        {
            await connection.DisposeAsync();
            await command.DisposeAsync();
        }

        #endregion

        #region Connection Test

        /// <summary>
        /// Explicitly testing the database connection.
        /// </summary>
        public async Task TestConnectionAsync(string table, string column = null)
        {
            Console.WriteLine();
            Console.WriteLine(column ?? "Data");
            Console.WriteLine("-----------------------------");


            command.CommandText =
                $@"
                    SELECT TOP 300 {column ?? "*"} FROM {table}
                ";


            await using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }

            Console.WriteLine("--------------------------------------------");
        }

        #endregion


        #region Database Checks

        /// <summary>
        /// Checks if a table in the database exists.
        /// </summary>
        /// <param name="schema">The schema which the table belongs to.</param>
        /// <param name="table">The table name.</param>
        /// <returns>True if the table exists, otherwise false.</returns>
        public bool TableExists(string schema, string table)
        {
            command.CommandText =
                $@"
                    IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}'))
                    SELECT 1 ELSE SELECT 0
                ";

            return (int)command.ExecuteScalar() == 1;
        }

        #endregion


        #region Helper Methods

        public static string SqlDateTimeFormat(DateTime time)
        {
            return $"'{time.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
        }

        #endregion

    }
}
