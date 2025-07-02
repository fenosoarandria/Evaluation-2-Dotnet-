using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class Import
{
 private readonly ApplicationDbContext _dbContext;

    public Import(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ImportCsvToDatabase<T>(string table,IFormFile file, Func<CsvReader, T> mapFunc, CsvConfiguration? csvConfig = null) where T : class
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Le fichier est vide ou n'existe pas.");
        }

        if (csvConfig == null)
        {
            csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                BadDataFound = null,
                MissingFieldFound = null
            };
        }

        using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            csv.Read();
            csv.ReadHeader();

            var entities = new List<T>();

            while (csv.Read())
            {
                var entity = mapFunc(csv);
                entities.Add(entity);
            }

            // Bulk insert using raw SQL
            foreach (var entity in entities)
            {
                var sql = GetInsertSql<T>(table);
                var parameters = GetSqlParameters<T>(entity);
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
            }
        }
    }

    private string GetInsertSql<T>(string table)
    {
        var tableName = table; // Assume que le nom de la classe correspond au nom de la table
        var properties = typeof(T).GetProperties();
        var valueParams = string.Join(", ", properties.Select(p => $"@{p.Name.ToLower()}"));
        return $"INSERT INTO {tableName} VALUES ({valueParams})";
    }

    private NpgsqlParameter[] GetSqlParameters<T>(T entity)
    {
        var properties = typeof(T).GetProperties();
        var parameters = new List<NpgsqlParameter>();
        foreach (var property in properties)
        {
            parameters.Add(new NpgsqlParameter($"@{property.Name.ToLower()}", property.GetValue(entity) ?? DBNull.Value));
        }

        return parameters.ToArray();
    }

}
