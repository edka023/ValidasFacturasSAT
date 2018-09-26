using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidaFacturassSAT.Clases;

namespace ValidaFacturassSAT
{
    class Program
    {
        static void Main(string[] args)
        {
            CProcessManager cp = new CProcessManager();
            cp.ValidaFacturaSAT();
        }
    }
}
