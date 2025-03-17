using System;
using System.Collections.Generic;
using System.Reflection;
using Dapper;

namespace SampleProject.Infrastructure.Database;

internal static class DatabaseHelper
{
    public static DynamicParameters GetDynamicParameters<T>(T model)
    {
        return ProcessProperties<T, DynamicParameters>(model, (parameters, property, value) =>
        {
            if (value is not null)
            {
                parameters.Add(property.Name, value);
            }
        });
    }
    
    public static List<string> GetFieldToUpdateList<T>(T model)
    {
        return ProcessProperties<T, List<string>>(model, (list, property, value) =>
        {
            if (value is not null)
            {
                list.Add($"\"{property.Name}\" = {DatabaseConstants.ParametersPrefix}{property.Name}");
            }
        });
    }
    
    private static TResult ProcessProperties<TModel, TResult>(TModel modelInstance, Action<TResult, PropertyInfo, object> action)
    {
        var result = Activator.CreateInstance<TResult>();
        var properties = typeof(TModel).GetProperties();

        foreach (var property in properties)
        {
            object value = property.GetValue(modelInstance, null);
            action(result, property, value);
        }

        return result;
    }
}