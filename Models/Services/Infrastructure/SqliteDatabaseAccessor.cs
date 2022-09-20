using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NETCorso.Models.Options;

namespace NETCorso.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        private readonly ILogger<SqliteDatabaseAccessor> logger;

        public SqliteDatabaseAccessor(ILogger<SqliteDatabaseAccessor>  logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions)
        {
            this.logger = logger;
            ConnectionStringsOptions = connectionStringsOptions;
        }

        public IOptionsMonitor<ConnectionStringsOptions> ConnectionStringsOptions { get; }

        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());
            

            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for(var i = 0; i < queryArguments.Length; i++){
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            string connectionString = ConnectionStringsOptions.CurrentValue.Default;
            using (var conn = new SqliteConnection(connectionString))
            { //apertura connessione
                await conn.OpenAsync(); //connessione già preparata da connection pool
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameters);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataSet = new DataSet();
                        dataSet.EnforceConstraints = false;

                        do{
                        var dataTable = new DataTable();
                        dataSet.Tables.Add(dataTable);
                        dataTable.Load(reader);
                        }while(!reader.IsClosed); //il datatable guarda se nel reader ci sono altri risultati da leggere. Se ci sono lascia il data reader aperto, altrimenti lo chiude

                        return dataSet;
                    }
                }
            }
        }
    }
}