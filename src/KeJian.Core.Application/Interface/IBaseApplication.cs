using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuiJun.Core.Application.Interface
{
    public interface IBaseApplication<TEntity>
    {
        Task<List<TEntity>> GetAsync();

        Task<TEntity> GetAsync(int id);

        Task<TEntity> CreateOrUpdateAsync(TEntity input);

        Task<bool> DeleteAsync(int id);
    }
}