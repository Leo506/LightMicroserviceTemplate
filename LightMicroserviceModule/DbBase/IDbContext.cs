namespace LightMicroserviceModule.DbBase;

public interface IDbContext<out T> : IEnumerable<T>
{
}