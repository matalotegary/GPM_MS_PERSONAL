using common.library.Constant;
using domain.DomainModels.DefaultUser;
using infrastructure;

public class CodeDefaultUserDataSeed
{
    public void InsertData(PersonalInfoDbContext context)
    {
        // Ensure this method is correctly defined and does not throw exceptions
        context.CodeDefaultUser.RemoveRange(context.CodeDefaultUser);
        context.SaveChanges();

        InsertIdempotence(ref context, "ADMIN2", "ADMIN12345", 1, 1);
        context.SaveChanges();
    }

    private void InsertIdempotence(ref PersonalInfoDbContext context, string userName, string passWord, int userRoleId, int deptId)
    {
        var codeDefaultUserEntity = context.CodeDefaultUser.FirstOrDefault(o =>
            o.UserName == userName &&
            o.Password == passWord &&
            o.UserRoleId == userRoleId &&
            o.DepartmentId == deptId);

        if (codeDefaultUserEntity! != null!)
        {
            context.CodeDefaultUser.Remove(codeDefaultUserEntity);
        }
        context.CodeDefaultUser.Add(new CodeDefaultUserEntity(userName, passWord, userRoleId, deptId, UserTypeConstant.System, UserTypeConstant.System));
    }
}
