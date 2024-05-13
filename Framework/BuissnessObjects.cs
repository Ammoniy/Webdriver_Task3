using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class ComputeEngine_BO
    {
        public int InstancesNum { get; set; }
        public string OS { get; set; }
        public string ProvisioningModel { get; set; }
        public string MachineFamily { get; set; }
        public string Series { get; set; }
        public string MachineType { get; set; }
    }

    public class GPU_BO
    {
        public string Type { get; set; }
        public string Num { get; set; }
        public string LocalSSD { get; set; }
        public string Region { get; set; }
    }

    public class Search_BO
    {
        public string SearchRequest { get; set; }
        public string SearchLinkName { get; set; }
    }
}
