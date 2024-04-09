using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Model.HirerachyMaster
{
     public class Hierarchy
    {
        public int INTHIERARCHYID { get; set; }
        public string NVCHHIERARCHYNAME { get; set; }
        public string VCHHIERARCHYALIAS { get; set; }
        public int INTNOLEVEL { get; set; }
        public string NVCHADDRESS { get; set; }
        public int INTCREATEDBY { get; set; }
        public int INTUPDATEDBY { get; set; }
    }
    public class CreateHierarchy
    {
     
        public string NVCHHIERARCHYNAME { get; set; }
        public string VCHHIERARCHYALIAS { get; set; }
        public int INTNOLEVEL { get; set; }
        public string NVCHADDRESS { get; set; }
        public int INTCREATEDBY { get; set; }
        public int INTUPDATEDBY { get; set; }

    }
    public class HierarchyModel
    {
        public List<Hierarchy>HierarchyList { get; set; }
    }
}

