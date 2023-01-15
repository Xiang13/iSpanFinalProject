using iSMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<AdminDTO> Search(int? activityId, string activityName);

        bool IsExisted(string account);

        void adminCreate(AdminDTO dto);
    }
}
