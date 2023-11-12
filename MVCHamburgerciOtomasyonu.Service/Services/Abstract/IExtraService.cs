using MVCHamburgerciOtomasyonu.Service.DTOs.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
    public interface IExtraService
    {
      Task<List<ExtraDto>> GetAllExtrasNonDeletedAsync();
        Task<List<ExtraDto>> GetAllExtrasWithImageNonDeletedAsync();
        Task<List<ExtraDto>> GetAllExtrasDeletedAsync();
        Task<List<ExtraDto>> GetAllExtrasWithImageDeletedAsync();
        Task<ExtraDto> GetExtraWithUserNonDeletedAsync(Guid menuID);
        Task CreateExtraAsync(ExtraAddDto extraAddDto);
        Task<string> UpdateExtraAsync(ExtraUpdateDto extraUpdateDto);
        Task<string> SafeDeleteExtraAsync(Guid ExtraID);
        Task<string> UndoDeleteExtraAsync(Guid ExtraID);
    }
}
