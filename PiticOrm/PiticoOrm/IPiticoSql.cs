namespace PiticoOrm
{
    public interface IPiticoSql
    {
        T Add<T>(T item) where T : class, new();
        void Delete<T>(T item);
        IEnumerable<T> MapRows<T>(string sql, DatabaseMapRowDelegate<T> action) where T : class, new();
        List<T> Query<T>(string sql) where T : class, new();
        T Update<T>(T item) where T : class, new();
    }
}