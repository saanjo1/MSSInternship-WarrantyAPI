using WarrantyAPI.Models;

namespace WarrantyAPI.Contracts
{
    public interface IRepository<T>
    {
        Task<string> Create(T warranty);
        Task<T> Read(string assetId);
        Task<IEnumerable<T>> Read();
        Task<bool> Update(T warranty);
        Task<bool> Delete(string warrantyId, string warrantyName);
    }
}

