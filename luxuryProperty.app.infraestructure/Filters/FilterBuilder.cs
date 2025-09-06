using MongoDB.Driver;

public class FilterBuilder<T>
{
    private readonly List<FilterDefinition<T>> _filters;

    public FilterBuilder()
    {
        _filters = new List<FilterDefinition<T>>();

        // Siempre filtrar por entidades no eliminadas si existe la propiedad "Deleted"
        if (typeof(T).GetProperty("Deleted") != null)
        {
            _filters.Add(Builders<T>.Filter.Eq("Deleted", false));
        }
    }

    public FilterBuilder<T> WithRegex(string field, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _filters.Add(Builders<T>.Filter.Regex(
                field,
                new MongoDB.Bson.BsonRegularExpression(value, "i")
            ));
        }
        Console.WriteLine($"filter{field}: {value}");
        Console.WriteLine(this);

        return this;
    }

    public FilterBuilder<T> WithRange<TField>(string field, TField? min, TField? max) where TField : struct
    {
        if (min.HasValue)
        {
            _filters.Add(Builders<T>.Filter.Gte(field, min.Value));
        }
        if (max.HasValue)
        {
            _filters.Add(Builders<T>.Filter.Lte(field, max.Value));
        }
        return this;
    }

    public FilterDefinition<T> Build()
    {
        if (_filters.Count == 0)
            return Builders<T>.Filter.Empty;

        return Builders<T>.Filter.And(_filters);
    }
}
