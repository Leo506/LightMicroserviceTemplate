using Calabonga.OperationResults;

namespace LightMicroserviceModule.DbBase;

public interface IDbWorker<T>
{
    Task<IEnumerable<T>> GetAllRecords();

    Task<IEnumerable<T>> GetRecordsByFilter(Func<T, bool> predicate);

    Task<OperationResult<bool>> AddNewRecord(T record);

    Task<OperationResult<bool>> AddNewRecordsRange(IEnumerable<T> records);
}