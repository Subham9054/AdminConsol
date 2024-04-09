using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGen.Model.ProjectMaster;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeGen.Model.Set_Permission
{
  public class Set_Permission
    {
        
        public string Action { get; set; }
        
        public string ActionCode { get; set; }
        
        public int FunctionId { get; set; }
        
        public string GlinkId { get; set; }
        
        public string GLinkName { get; set; }
        
        public string GroupDeptId { get; set; }
        
        public string GroupUserId { get; set; }
        
        public int ID { get; set; }
        
        public string PermissionStatus { get; set; }
        
        public string PermissionType { get; set; }
        
        public string PlinkAdd { get; set; }
        
        public int PlinkId { get; set; }
        
        public string PlinkManage { get; set; }
        
        public string PLinkName { get; set; }
        
        public string PlinkView { get; set; }
        
        public string PlnkId { get; set; }
        
        public int UpdatedBy { get; set; }
        
        public int UserID { get; set; }
        
        public string UserName { get; set; }
    }
    public class UserDetails
    {

        public int UserID { get; set; }
        public string UserName { get; set; }

    }
    public class RoleDetails
    {

        public int Roleid { get; set; }
        public string RoleName { get; set; }

    }
    public class FunctionDetails
    {

        public int Functionid { get; set; }
        public string FunctionName { get; set; }

    }
    public class DesignationDetails
    {
        public int INTDESIGID { get; set; }
        public string NVCHDESIGNAME { get; set; }
    }
    public class SetPermissionDetails
    {

        public int GLOBALID { get; set; }
        public string GLOBALNAME { get; set; }
        public int PRIMARYID { get; set; }
        public string PRIMARYNAME { get; set; }
        public string TINACCESS { get; set; }
        public string PRIMARYLINKCOUNT { get; set; }

        public string PRIMARYLINKPERMISSIONCOUNT { get; set; }
        public string VCHACTION1 { get; set; }

        //Added by Subham For Set Permission
        public string BUTTONNAME { get; set; }
        public string TABNAME { get; set; }
        public string NVCHPLINKNAME { get; set; }
        public int BUTTONID { get; set; } //ADDDED BY SUBHAM
        public int TABID { get; set; } //ADDDED BY SUBHAM
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserIds { get; set; }
        public string RoleIds { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
       public List<SelectListItem> Userlist { get; set; }
        public List<SelectListItem> Rolelist { get; set; }

    }
    public class AddupdateSetPermissionDesignation
    {

        public int INTACCESSID { get; set; }
        public int INTDESINATIONID { get; set; }
        public int INTPLINKID { get; set; }
        public string VCHACTION1 { get; set; }
        public string VCHACTION2 { get; set; }
        public string VCHACTION3 { get; set; }
        public int TINACCESS { get; set; }
        public int INTCREATEDBY { get; set; }
        public int INTUPDATEDBY { get; set; }


    }
    public class SetPermissionModel
    {
        public List<ViewPoject> ProjectList { get; set; }
        public List<SetPermissionDetails> GlobalPrimaryList { get; set; }
        public List<SetPermissionDetails> GlobalPrimaryListUser { get; set; }
        public List<UserDetails> UserList { get; set; }
        public List<RoleDetails> RoleList { get; set; }
        public List<FunctionDetails> FunctionList { get; set; }
        public List<DesignationDetails> DesignationList { get; set; }
        public AddupdateSetPermissionDesignation command { get; set; }
        public string setpermissionList { get; set; }
        public string setpermissionListUser { get; set; }
        public  int DesignId { get; set; }
        public int UserId { get; set; }
        public int Roleid { get; set; }
        public int INTPROJECTLINKID { get; set; }
    }
    //public class CodeGenLogin
    //{
    //    public int INTUSERID { get; set; }
    //    public string? VCHUSERNAME { get; set; }
    //    public string? VCHPASSWORD { get; set; }
    //    public string? VCHFULLNAME { get; set; }
    //    public string? ROLENAME { get; set; }
    //    public int? ROLEID { get; set; }
    //    public int? INTDESIGNATIONID { get; set; }
    //}

    #region Added By Subham For Set Permission User
    public class UserSet
    {
        public int INTPROJECTLINKID { get; set; }
        public int INTGLOBALLINKID { get; set; }
        public int INTPRIMARYTLINKID { get; set; }
        public int INTBUTTONLINKID { get; set; }
        public string TabName { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserIds { get; set; }
        public string RoleIds { get; set; }
        public List<UserSet> UserSets { get; set; }
        public int TABID { get; set; } //Added by Subham
    }
    #endregion
    #region Added By Subham For Set Permission Role
    public class RoleSet
    {
        public int RoleId{ get; set; }
        public string FunctionIds { get; set; }
        public int functionid { get; set; }
        public List<RoleSet> RoleSets { get; set; }
    }
    #endregion
}
