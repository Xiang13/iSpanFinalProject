using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
    public static partial class AdminExts
    {
        //public static AdminDTO ToAdminDTO(this Admin source)
        //{
        //    return new AdminDTO
        //    {
        //        id = source.id,
        //        adminAccount = source.adminAccount,
        //        adminEncryptedPassword = source.adminEncryptedPassword,
        //        departmentId = source.departmentId,
        //        Department = source.Department,
        //        Admin_Role_Metadata = source.Admin_Role_Metadata.Where(x => x.adminId == source.id).Select(x => x.roleId).ToList(),
        //    };
        //}
        public static AdminDTO ToAdminDTO(this Admin_Role_Metadata source)
        {
            return new AdminDTO
            {
                id = source.id,
                adminAccount = source.Admin.adminAccount,                
                //departmentId = source.Admin.departmentId,
                Department = source.Admin.Department,
                Role = source.Role
            };
        }
        
        public static AdminIndexVM ToAdminIndexVM(this AdminDTO source)
        {
            return new AdminIndexVM
            {
                id = source.id,
                adminAccount = source.adminAccount,                
                departmentName = source.Department.departmentName,
                roleName = source.Role.roleName
            };
        }
        
        public static Admin ToAdminEntity(this AdminDTO source)
        {
            return new Admin
            {                
                adminAccount = source.adminAccount,
                departmentId = source.departmentId,
                adminEncryptedPassword = source.adminEncryptedPassword,
                //departmentId = source.Department.id
            };
        }

        public static AdminDTO ToAdminCreateDTO(this AdminCreateVM source)
        {
            return new AdminDTO
            {
                adminAccount = source.adminAccount,
                Password = source.Password,
                departmentId = source.departmentId
            };
        }
    }
}