using System.Collections.Generic;
using System.Threading.Tasks;
using KeJian.Core.Domain.Models;

namespace GuiJun.Core.Application.Interface
{
    public interface IRecruitmentApplication : IBaseApplication<Recruitment>
    {
        Task<List<Recruitment>> GetByTypeAsync(int type);
    }
}