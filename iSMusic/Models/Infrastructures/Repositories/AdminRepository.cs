using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.Infrastructures.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private AppDbContext _db;
        public AdminRepository(AppDbContext db)
        {
            _db = db;
        }
        public IEnumerable<AdminDTO> Search(int? adminId, string adminAccount)
        {
            IEnumerable<Admin_Role_Metadata> query = _db.Admin_Role_Metadata;
            query = query.OrderBy(x => x.Admin.adminAccount);

            return query.Select(x => x.ToAdminDTO());
        }

        public bool IsExisted(string account)
        {
            Admin model;
            var admins = _db.Admins;
            model = admins.SingleOrDefault(x => x.adminAccount == account);

            return model != null;
        }

        public void adminCreate(AdminDTO dto)
        {
            _db.Admins.Add(dto.ToAdminEntity());
            _db.SaveChanges();
        }
    }
}