using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Enums
{
    public enum CSVStatus
    {

        [Description("Approved")]
        Approved = 1,
        [Description("Failed")]
        Failed = 2,
        [Description("Finishedk")]
        Finished = 3
    }
}
