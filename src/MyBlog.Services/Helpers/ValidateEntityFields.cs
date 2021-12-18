using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class ValidateEntityFields
{
    public static void EnsureFieldsIsValid<T>(this T entity)
    {
        Guard.Against.Null(entity, nameof(entity));

        Type entityType = entity.GetType();
        foreach (PropertyInfo property in entityType.GetProperties())
        {
            RequiredAttribute requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttribute != null)
                Guard.Against.NullOrWhiteSpace((string)property.GetValue(entity), property.Name, requiredAttribute.ErrorMessage);
        }
    }
}