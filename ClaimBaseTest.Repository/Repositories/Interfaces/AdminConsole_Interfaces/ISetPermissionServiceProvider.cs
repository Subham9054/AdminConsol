using CodeGen.Model.Set_Permission;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGen.Model.User;
namespace CodeGen.Repository.Repositories.Interfaces
{
    public interface ISetPermissionServiceProvider
    {
        Task<SetPermissionModel> GetAllPrimaryLinkByGlobalLink(SetPermissionModel objpermission);
        Task<SetPermissionModel> GetAllPrimaryLinkByGlobalLinkUserwise(SetPermissionModel objpermission);
        string SetPermissionLink_Designation(int designationId, int Plinkid, int Intaccess, int user, int projectId);
        string SetPermissionLink_User(int userId, int Plinkid, int Intaccess, int user, int projectId);
        string DeletePermissionLink_Designation(int DesignationId, int projectId);
        string DeletePermissionLink_User(int userId, int projectId);
        Task<SetPermissionModel> GetAllUser();
        Task<SetPermissionModel> GetAllRole();
        Task<SetPermissionModel> GetAllDesignation();
        string RemovePermissionList_User(int userId, int updatedby);
        IList<LinkAccess> GetLinkAccessByUserId(int UserId, int ProjectId, int desgid);

        #region Added By Subham For User Set Permission
        public Task<SetPermissionModel> GetPrimaryLinkByGlobalLink(int Globid);
        public Task<SetPermissionModel> GetButtonByPrimaryLink(int Primid);
        public Task<SetPermissionModel> GetAllDetails(int Projectid, int globalLinkId, int primaryLinkId, int buttonId);
        public Task<SetPermissionModel> GetAllUserID();
        Task<int> SaveUserdetail(UserSet userSet);
        public Task<int> RemoveUserdetail(UserSet userSet);
        public Task<int> UpdateUserdetail(UserSet userSet);
        public Task<int> UpdateRoledetail(UserSet userSet);
        #endregion

        //For View Component
        IList<ButtonTab1> GetButtonWithTabAccess(string plinkid, string btnid, int useridlClaims);
        public string GetButtondefaultTab(string plinkid);
        public string GetdefaultTab(string plinkValue, string btnid);
        public Task<SetPermissionModel> bindUserddl(int projid, int globid, int plid, int btnid,int tabid);
        public Task<SetPermissionModel> bindroleddl(int projid, int globid, int plid, int btnid,int tabid);
        public Task<SetPermissionModel> GetUserId(int Projectid, int globalLinkId, int primaryLinkId, int buttonId);
        public Task<int> DeleteUID(int userid);
        public Task<int> DeleteRID(int roleId);
        public Task<SetPermissionModel> bindrole();
        public Task<SetPermissionModel> bindFunction();
        public string SaveRolefunction(RoleSet roleSet);

    }
}
