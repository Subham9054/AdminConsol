//using AdConsole11.Repository.USP_DIST;
using CodeGen.Model.PrimaryLink;
using CodeGen.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Repository.Repositories.Interfaces
{
    //Anshuman New --Full page
    public interface IMenueManageProvider
    {
        //Button Master
        string AddButtonMaster(ButtonTab objButtonTab);
        string UpdateButtonMaster(ButtonTab objButtonTab);
        Task<ButtonTab> GetAllFunctionMaster();
        Task<List<ButtonTab>> GetAllPrimaryLinkByGlobalLink(int INTGLINKID);
        Task<List<ButtonTab>> GetAllButtonMaster();
        Task<List<ButtonTab>> GetAllButtonMasterById(int BUTTONID);
        string DeleteButtonMaster(int BUTTONID);
        //-----------------------------------------------------------------
        //Tab Master
        string AddTabMaster(ButtonTab objButtonTab);
        string UpdateTabMaster(ButtonTab objButtonTab);
        Task<List<ButtonTab>> GetAllTabMaster();
        Task<List<ButtonTab>> GetAllTabMasterById(int TABID); 

        string DeleteTabMaster(int TABID);
        Task<ButtonTab> GetAllButton();
        //Task<ButtonTab> GetMaxId();
        public int GetTabMaxSortNo(int intsortnum);
        string ModifySlnoTabMaster(int globalId, int slno, int updatedby);


        //Added by Shubham for Set user permission
        public string ModifySlnoButtonLink(int buttonid, int slno, int updatedby);
        public int Getbuttonmax(int intsortnum);
    }
}





