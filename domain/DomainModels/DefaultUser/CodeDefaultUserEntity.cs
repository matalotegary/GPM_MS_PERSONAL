using common.library.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.DomainModels.DefaultUser
{
    public class CodeDefaultUserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public int DepartmentId { get; set; }
        public CodeDefaultUserEntity() { }

        public CodeDefaultUserEntity(
            string userName,
            string passWord,
            int userRoleId,
            int deptId,
            string createdBy,
            string createdByType) : base (createdBy, createdBy)
        {
            UserName = userName;
            Password = passWord;
            UserRoleId = userRoleId;
            DepartmentId = deptId;
            CreatedBy = createdBy;
            CreatedByType = createdByType;              
        }

    }
}
