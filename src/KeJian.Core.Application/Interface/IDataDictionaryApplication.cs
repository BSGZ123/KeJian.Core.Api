using System.Collections.Generic;
using System.Threading.Tasks;
using KeJian.Core.Domain.Models;

namespace GuiJun.Core.Application.Interface
{
    public interface IDataDictionaryApplication : IBaseApplication<DataDictionary>
    {
        Task<List<DataDictionary>> GetAsync(List<string> keys);
    }
}