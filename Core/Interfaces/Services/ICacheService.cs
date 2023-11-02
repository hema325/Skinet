namespace Core.Interfaces.Services
{
    public interface ICacheService
    {
        Task DeleteAsync(string key);
        Task<TValue?> GetAsync<TValue>(string key);
        Task SetAsync<TValue>(string key, TValue value, TimeSpan? expiry = null, bool isCamelCase = false);
    }
}
