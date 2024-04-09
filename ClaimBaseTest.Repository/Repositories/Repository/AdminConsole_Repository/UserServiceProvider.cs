using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Model.SuccessMessage;
using CodeGen.Model.CommonFunction;
using CodeGen.Model.User;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;
//using CodeGen.Model.Set_Permission;
using CodeGen.Model.LoginModel;


namespace CodeGen.Repository.Repository
{
    public class UserServiceProvider: WizTestRepositoryBase,IUserServiceProvider
    {
	  #region Variable Declaration
        //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[DataBaseHelper.ConnectionString].ConnectionString);
        object param = new object();
       // int intOutput = 0;

        #endregion

      
        public UserServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
       

          public int ActivateUser(User objUser)
        {
            throw new NotImplementedException();
        }

        public int ChangePasswod(User user)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", user.ActionCode);
                aParam.Add("iv_intUserId", Convert.ToInt32(user.UserID));
                aParam.Add("v_intCreatedBy", user.CreatedBy);
                aParam.Add("v_vchPassword", user.UserPasswaord);
                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<SuccessMessage>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.AsList()[0].successid;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public int CreateUser(User objUser)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", objUser.ActionCode);
                aParam.Add("v_vchUserName", objUser.UserName);
                aParam.Add("v_vchPassword", objUser.UserPasswaord);
                aParam.Add("v_vchFullName", objUser.FullName);
                aParam.Add("v_intLevelDetailId", objUser.LeveleID);
                aParam.Add("v_intLocation", objUser.intLocation);
                aParam.Add("v_intDesigId", objUser.DesignationID);
                aParam.Add("v_vchGender", objUser.Gender);
                aParam.Add("v_dtmDoj", objUser.DateOfJoing);
                aParam.Add("v_dtmDob", objUser.DateOfBirth);
                aParam.Add("v_vchEmail", objUser.email);
                aParam.Add("v_vchMobTel", objUser.Mobile);
                aParam.Add("v_vchUserImage", objUser.UserImage);
                aParam.Add("v_intCreatedBy", objUser.CreatedBy);
                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<SuccessMessage>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.AsList()[0].successid;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public int DeActivateUser(User objUser)
        {
            throw new NotImplementedException();
        }

        public int DeleteUser(User objUser)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", objUser.ActionCode);
                aParam.Add("iv_intUserId", objUser.intuserid);
                aParam.Add("v_intCreatedBy", objUser.CreatedBy);
                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<SuccessMessage>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.AsList()[0].successid;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public int EditUser(User objUser)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", objUser.ActionCode);
                aParam.Add("v_vchUserName", objUser.UserName);
                aParam.Add("iv_intUserId", objUser.intuserid);
                aParam.Add("v_vchFullName", objUser.FullName);
                aParam.Add("v_intLevelDetailId", objUser.LeveleID);
                aParam.Add("v_intLocation", objUser.intLocation);
                aParam.Add("v_intDesigId", objUser.DesignationID);
                aParam.Add("v_vchGender", objUser.Gender);
                aParam.Add("v_dtmDoj", objUser.DateOfJoing);
                aParam.Add("v_dtmDob", objUser.DateOfBirth);
                aParam.Add("v_vchEmail", objUser.email);
                aParam.Add("v_vchMobTel", objUser.Mobile);
                aParam.Add("v_vchUserImage", objUser.UserImage);
                aParam.Add("v_intCreatedBy", objUser.CreatedBy);
                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<SuccessMessage>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.AsList()[0].successid;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public IList<User> FillChildSO(int RptID)
        {
            throw new NotImplementedException();
        }

        public IList<User> FillParentSO(int Location)
        {
            throw new NotImplementedException();
        }

        public IList<User> FillPermissionReport(int Location, int Access, int Plink)
        {
            throw new NotImplementedException();
        }

        public IList<User> FillUserRA()
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAllUsers(User objUser)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("p_action", objUser.ActionCode);
                aParam.Add("p_intdesignationid", objUser.intDesignationID);
                aParam.Add("p_intlocationid", objUser.intLocation);
                aParam.Add("p_vchmobtel", objUser.vchmobtel);
                aParam.Add("p_vchusername", objUser.vchusername);
                //aParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_VIEW";
                var result = Connection.Query<User>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public IList<User> GetDesigInfo(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<Designation> GetDesignations()
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("p_chrActionCode", "D");
                p.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_LEVELDETAILS_VIEW";
                var result = Connection.Query<Designation>(query, p, commandType: CommandType.StoredProcedure);
                return result.AsList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<User> GetDetails(User objUser)
        {
            throw new NotImplementedException();
        }

        public string GetFullNameFromId(int intUserId)
        {
            throw new NotImplementedException();
        }

        public IList<LevelDetail> GetLevelDetails(int hirachyId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("p_chrActionCode", "V");
                p.Add("p_inthirarchyid", hirachyId);
                p.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_LEVELDETAILS_VIEW";
                var result = Connection.Query<LevelDetail>(query, p, commandType: CommandType.StoredProcedure);
                return result.AsList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<LevelDetail> GetLevelDetailsByParentId(int ParentId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("p_chrActionCode", "P");
                p.Add("p_intparentLevelDtlsId", ParentId);
                p.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_LEVELDETAILS_VIEW";
                var result = Connection.Query<LevelDetail>(query, p, commandType: CommandType.StoredProcedure);
                return result.AsList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetLevelDTlID(string LvlDtlId)
        {
            throw new NotImplementedException();
        }

        public IList<LinkAccess> GetLinkAccessByUserId(int UserId)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("p_action", "VU");
                aParam.Add("p_intintuserid", UserId);
                aParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_VIEW";
                var result = Connection.Query<LinkAccess>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public IList<Location> GetLocation()
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("p_chrActionCode", "L");
                p.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_LEVELDETAILS_VIEW";
                var result = Connection.Query<Location>(query, p, commandType: CommandType.StoredProcedure);
                return result.AsList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetPosition(string LevelId)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetPosition2(string Id)
        {
            throw new NotImplementedException();
        }

        public int GetSubAdminPrev(string userid)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetTGradeDetail(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetUserById(int UserId, string ActionCode)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("p_action", ActionCode);
                aParam.Add("p_intintuserid", UserId);
                //aParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_VIEW";
                var result = Connection.Query<User>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }

        public User GetUserInfo(User user)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", user.ActionCode);
                aParam.Add("v_vchUserName", user.UserName);
                aParam.Add("v_vchPassword", user.UserPasswaord);
                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<User>(query, aParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }
        public User LDAPGetUserInfo(User user)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", user.ActionCode);
                aParam.Add("v_vchUserName",CommonFunction.DecryptData(user.UserName));

                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<User>(query, aParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }
        public string Message(string strActionCode, string strDuplicVal, string strUserId)
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown1()
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown3(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown4(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown5(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown6()
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown7()
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown8()
        {
            throw new NotImplementedException();
        }

        public IList<User> PopUpDropDown9(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> UserGetHierarchyID(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> UserGetLevel(string Id)
        {
            throw new NotImplementedException();
        }

        public IList<User> UserGetParentID(string Id)
        {
            throw new NotImplementedException();
        }

        public int ValidateUser(User user)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", user.ActionCode);
                aParam.Add("v_vchUserName", user.UserName);
                aParam.Add("v_vchPassword", user.UserPasswaord);
                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<SuccessMessage>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.AsList()[0].successid;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }
        public int LDAPValidateUser(User user)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("v_chrActionCode", user.ActionCode);
                aParam.Add("v_vchUserName", CommonFunction.DecryptData(user.UserName));

                aParam.Add("v_intOutput", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("p_msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_USERMASTER_MANAGE";
                var result = Connection.Query<SuccessMessage>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.AsList()[0].successid;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }
        }
        //For Login
        public async Task<CodeGenLogin> GetLoginDetails(CodeGenLogin log)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@VCHUSERNAME", log.VCHUSERNAME);
                p.Add("@VCHPASSWORD", log.VCHPASSWORD);
                p.Add("@action", "LOGIN");
                //p.Add("@output", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = await Connection.QueryAsync<CodeGenLogin>("USP_CPKG_1_USERELOGIN1", p, commandType: CommandType.StoredProcedure);
                //var cc = Convert.ToInt32(p.Get<string>("@output"));
                return results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
