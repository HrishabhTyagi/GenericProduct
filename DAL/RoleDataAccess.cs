using SqlServerEntity.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class RoleDataAccess : BaseDataAccess
    {
        /// <summary>
        /// List of Role
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoleList()
        {
            return _context.Roles.ToList();
        }

        /// <summary>
        /// Get specific role by id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetRole(int roleId)
        {
            return _context.Roles.FirstOrDefault(a =>a.RoleId == roleId && a.IsActive == true);
        }

        /// <summary>
        /// Insert new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Role PostRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int PutRole(Role role)
        {
            role.ModifiedOn = DateTime.Now;
            _context.Entry(role).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        /// <summary>
        /// Soft delete of role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DeleteRole(int roleId)
        {
            Role role = _context.Roles.Find(roleId);

            if (role == null)
                return 0;

            if (!role.IsDeleted)
                role.IsDeleted = !role.IsDeleted;

            _context.Entry(role).State = EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
