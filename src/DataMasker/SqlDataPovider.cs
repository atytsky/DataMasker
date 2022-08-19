using System.Collections.Generic;
using System.Data.SqlClient;
using Bogus.DataSets;
using Dapper;
using DataMasker.Interfaces;
using DataMasker.Models;

namespace DataMasker;

public class SqlDataProvider : IDataProvider
{
    private readonly SqlConnection _connection;

    public SqlDataProvider(SqlConnection connection)
    {
        _connection = connection;
    }

    public bool CanProvide(DataType dataType)
    {
        return dataType == DataType.Sql;
    }

    public object GetValue(ColumnConfig columnConfig, IDictionary<string, object> obj, Name.Gender? gender)
    {
        var dynamicParameters = new DynamicParameters(obj);
        var newValue = _connection.ExecuteScalar(columnConfig.SqlValue.Query, dynamicParameters);
        if (newValue == null && columnConfig.SqlValue.ValueHandling == NotFoundValueHandling.KeepValue)
            newValue = obj[columnConfig.Name];


        return newValue;
    }
}