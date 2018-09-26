using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidaFacturassSAT.CFDI;

namespace ValidaFacturassSAT.Clases
{
    public class CProcessManager
    {
        private string _connectionstring;
        private CDatos _ConsultasSql;
        private CMail _Correos;

        public CProcessManager()
        {
            _connectionstring = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            _ConsultasSql = new CDatos(_connectionstring);
            _Correos = new CMail();
        }

        public bool ValidaFacturaSAT()
        {
            bool result = true;

            //?re =[RFC_Emisor]
            //& rr =[RFC_Receptor]
            //& tt = [Total]
            //& id =[UUID]

            try
            {
                DataTable dt = new DataTable();
                dt = _ConsultasSql.EjecutaReporte();

                foreach (DataRow item in dt.Rows)
                { 
                    using (CFDI.ConsultaCFDIServiceClient
                        client = new CFDI.ConsultaCFDIServiceClient("BasicHttpBinding_IConsultaCFDIService"))
                    {
                        
                        CFDI.Acuse acuse = client.Consulta(item["cadena"].ToString());
                        if (acuse == null)
                        {
                            Console.Write("ERROR:: Acuse is null");
                        }
                        else
                        {
                            Console.Write("\r\nEstado:: " + acuse.Estado + "\r\nCodigoEstatus::" + acuse.CodigoEstatus);
                            _ConsultasSql.UpdateEnviado(item["UUID"].ToString(), acuse.Estado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }


            return result;
        }





    }

}
