using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Model.HirerachyMaster;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;

namespace CodeGen.Repository.Repository
{
    public class HierarchyServiceProviderRepository: WizTestRepositoryBase,IHierarchyServiceProviderRepository
    {
      
        public HierarchyServiceProviderRepository(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
        public string AddHierarchy(Hierarchy objhierarchy)
        {
            var p = new DynamicParameters();
            p.Add("@P_NVCHHIERARCHYNAME", objhierarchy.NVCHHIERARCHYNAME);
            p.Add("@P_VCHHIERARCHYALIAS", objhierarchy.VCHHIERARCHYALIAS);
            p.Add("@P_INTNOLEVEL", objhierarchy.INTNOLEVEL);
            p.Add("@P_NVCHADDRESS", objhierarchy.NVCHADDRESS);
            p.Add("@P_INTCREATEDBY", objhierarchy.INTCREATEDBY);
            p.Add("@P_ACTION", "A");
            p.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            var results =  Connection.Execute("USP_CPKG_1_HierarchyMaster_MANAGE", p, commandType: CommandType.StoredProcedure);
            var cc = Convert.ToInt32(p.Get<string>("@P_Msg"));
            return cc.ToString();
          
        }

        public string EditHierarchy(Hierarchy objhierarchy)
        {
            try
            {

                var p = new DynamicParameters();
            p.Add("@P_INTHIERARCHYID", objhierarchy.INTHIERARCHYID);
            p.Add("@P_NVCHHIERARCHYNAME", objhierarchy.NVCHHIERARCHYNAME);
            p.Add("@P_VCHHIERARCHYALIAS", objhierarchy.VCHHIERARCHYALIAS);
            p.Add("@P_INTNOLEVEL", objhierarchy.INTNOLEVEL);
            p.Add("@P_NVCHADDRESS", objhierarchy.NVCHADDRESS);
            p.Add("@P_INTCREATEDBY", objhierarchy.INTCREATEDBY);
            p.Add("@P_ACTION", "E");
            p.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            var results = Connection.Execute("USP_CPKG_1_HierarchyMaster_MANAGE", p, commandType: CommandType.StoredProcedure);
            return results.ToString();               
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
        }

        public async Task<HierarchyModel> GetAllHierarchy(int id)
        {
            try
            {
            
                var p = new DynamicParameters();
                p.Add("@P_intStatus",id);
                
                p.Add("@P_Action", "V");
                p.Add("@cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var result = await Connection.QueryMultipleAsync("USP_CPKG_1_HierarchyMaster_VIEW", p, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<Hierarchy>();
                HierarchyModel viewModel = new HierarchyModel();
                viewModel.HierarchyList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public async Task<HierarchyModel> GetById(int id)
        {
            try
            {
                var dyParam = new DynamicParameters();

                dyParam.Add("@P_INTHIERARCHYID", id);
                dyParam.Add("@P_Action", "VI");
                dyParam.Add("@cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                 var query = "USP_CPKG_1_HierarchyMaster_VIEW";

                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<Hierarchy>();
                HierarchyModel viewModel = new HierarchyModel();
                viewModel.HierarchyList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }

      

        public string MarkActive(Hierarchy objhierarchy)
        {
           
            
            try
            {
                 var dyParam = new DynamicParameters();           
          
                dyParam.Add("@P_INTHIERARCHYID", objhierarchy.INTHIERARCHYID);
                dyParam.Add("@P_ACTION", "I");
                dyParam.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_HierarchyMaster_MANAGE", dyParam, commandType: CommandType.StoredProcedure);
                var cc = Convert.ToInt32(dyParam.Get<string>("@P_Msg"));
                return results.ToString();
                         
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        public string MarkInactive(Hierarchy objhierarchy )
        {
            try
            {

                var dyParam = new DynamicParameters(); ;
                dyParam.Add("@P_INTHIERARCHYID",objhierarchy.INTHIERARCHYID);
                dyParam.Add("@P_ACTION", "EA");
                dyParam.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_HierarchyMaster_MANAGE", dyParam, commandType: CommandType.StoredProcedure);
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
