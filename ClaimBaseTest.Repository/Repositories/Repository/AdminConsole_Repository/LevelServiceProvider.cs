using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Model.LevelMasterModel;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;

namespace CodeGen.Repository.Repository
{
    public class LevelServiceProvider: WizTestRepositoryBase,ILevelServiceProvider
    {
      
        public LevelServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
          public string AddLevel(CreateLevelMaster objlevel)
          {
            try
            {
                var p = new DynamicParameters();
                p.Add("@P_INTHIERARCHYID", objlevel.INTHIERARCHYID);
                p.Add("@P_NVCHLABEL", objlevel.NVCHLABEL);
                p.Add("@P_VCHALLIAS", objlevel.VCHALLIAS);
                p.Add("@P_INTPARENTID", objlevel.INTPARENTID);
                p.Add("@P_INTCREATEDBY", objlevel.INTCREATEDBY);
                p.Add("@P_INTUPDATEDBY", objlevel.INTUPDATEDBY);
                p.Add("@P_ACTION", "A");
                p.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_LEVELMASTER_MANAGE", p, commandType: CommandType.StoredProcedure);
                var cc = Convert.ToInt32(p.Get<string>("@P_Msg"));
                return cc.ToString();
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            
          }
 public async Task<CreateLevelMaster> GetAllLevelByHierarchyId(int id)

        {
            try
            {

                var p = new DynamicParameters();
                p.Add("@P_INTHIERARCHYID", id);
                p.Add("@P_Action", "VL");
                p.Add("@cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var result = await Connection.QueryMultipleAsync("USP_CPKG_1_LEVELMASTER_VIEW", p, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<LevelMaster>();
                CreateLevelMaster viewModel = new CreateLevelMaster();
                viewModel.ParentLevelList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
           
            
           
        }
        public async Task<LevelMasterModel> GetAllLevel(int id)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("@P_intStatus", id);
                p.Add("@P_Action", "V");
                p.Add("@cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var result = await Connection.QueryMultipleAsync("USP_CPKG_1_LEVELMASTER_VIEW", p, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<LevelMaster>();
                LevelMasterModel viewModel = new LevelMasterModel();
                viewModel.LevelList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
          
        }
        public async Task<IEnumerable<CreateLevelMaster>> GetLevelById(int id)
        {
            try
            {
                var dyParam = new DynamicParameters();

                dyParam.Add("@P_INTLEVELID", id);
                dyParam.Add("@P_Action", "VI");
                dyParam.Add("@cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_LEVELMASTER_VIEW";
                var result = await Connection.QueryAsync<CreateLevelMaster>(query, dyParam, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public string EditLevel(CreateLevelMaster objlevel)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("@P_INTLEVELID", objlevel.INTLEVELID);
                p.Add("@P_INTHIERARCHYID", objlevel.INTHIERARCHYID);
                p.Add("@P_NVCHLABEL", objlevel.NVCHLABEL);
                p.Add("@P_VCHALLIAS", objlevel.VCHALLIAS);
                p.Add("@P_INTPARENTID", objlevel.INTPARENTID);
                p.Add("@P_INTUPDATEDBY", objlevel.INTUPDATEDBY);
                p.Add("@P_ACTION", "E");
                p.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_LEVELMASTER_MANAGE", p, commandType: CommandType.StoredProcedure);
                return results.ToString();
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
           
        }

        public string MarkInactive(LevelMasterModel objmodel)
        {
            try
            {

                var dyParam = new DynamicParameters(); ;
                dyParam.Add("@P_INTLEVELID", objmodel.INTLEVELID);
                dyParam.Add("@P_ACTION", "EA");
                dyParam.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_LEVELMASTER_MANAGE", dyParam, commandType: CommandType.StoredProcedure);
                var cc = Convert.ToInt32(dyParam.Get<string>("@P_Msg"));
                return results.ToString();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public string MarkActive(LevelMasterModel objmodel)
        {
            try
            {
                var dyParam = new DynamicParameters();
                
                dyParam.Add("@P_INTLEVELID", objmodel.INTLEVELID);
                dyParam.Add("@P_ACTION", "I");
                dyParam.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_LEVELMASTER_MANAGE", dyParam, commandType: CommandType.StoredProcedure);
                var cc = Convert.ToInt32(dyParam.Get<string>("@P_Msg"));
                return results.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        
               
    }
}
