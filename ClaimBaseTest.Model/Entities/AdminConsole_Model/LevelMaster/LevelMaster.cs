using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGen.Model.HirerachyMaster;
namespace CodeGen.Model.LevelMasterModel
{
     public class LevelMaster
    {
        public string INTLEVELID { get; set; }
        public int INTHIERARCHYID { get; set; }
        public string NVCHLABEL { get; set; }
        public string VCHALLIAS { get; set; }
        public int INTPARENTID { get; set; }
        public string VCHHIERARCHYALIAS { get; set; }
        public string NVCHHIERARCHYNAME { get; set; }
        public int INTCREATEDBY { get; set; }
        public int INTUPDATEDBY { get; set; }
        public string parentlevel { get; set; }
    }
    public class CreateLevelMaster
    {
        public string INTLEVELID { get; set; }
        public int INTHIERARCHYID { get; set; }
        public string NVCHLABEL { get; set; }
        public string VCHALLIAS { get; set; }
        public int INTPARENTID { get; set; }
        public string VCHHIERARCHYALIAS { get; set; }
        public int INTCREATEDBY { get; set; }
        public int INTUPDATEDBY { get; set; }
        public List<Hierarchy> HierarchyList { get; set; }
        public List<LevelMaster> ParentLevelList { get; set; }
    }
    public class LevelMasterModel
    {
        public List<LevelMaster> ParentLevelList { get; set; }
        public List<Hierarchy> HierarchyList { get; set; }
        public List<LevelMaster> LevelList { get; set; }
        public string INTLEVELID { get; set; }
        public int INTUPDATEDBY { get; set; }
    }
}

