using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
    public class AdminService
    {
        public IAdminRepository adminRepository;

        public AdminService(IAdminRepository repo)
        {
            adminRepository = repo;
        }

        public IEnumerable<AdminDTO> Search(int? adminId, string adminAccount)
        {
            var admins = adminRepository.Search(adminId, adminAccount);
            //var admins = adminRepository.Search(adminId, adminAccount).Where(x => x.Role.id == x.Department.id * 10 + 2);
            return admins;
        }

        public (bool, string) adminCreate(AdminDTO dto)
        {
            if (adminRepository.IsExisted(dto.adminAccount))
            {
                return (false, "帳號已存在");
            }
            adminRepository.adminCreate(dto);
            return (true, null);
        }

    }
}