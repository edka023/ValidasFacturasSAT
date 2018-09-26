using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidaFacturassSAT.Clases
{
    public class CDatos
    {
        private string _connectionstring;
        private SqlConnection cnx;

        public CDatos(string cnn)
        {
            _connectionstring = cnn;
            cnx = new SqlConnection(_connectionstring);
        }

        public CDatos()
        {
        }

        public void ConexionOpen()
        {
            cnx.Open();
        }

        public void ConexionClose()
        {
            cnx.Close();
        }

        public DataTable EjecutaReporte()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cnx.Open();
                    cmd.Connection = cnx;
                    cmd.CommandText = "select * from vFacturas";
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

      

        public int UpdateEnviado(string UUID, string estatus)

        {
            int i = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cnx.Open();
                    cmd.Connection = cnx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Facturas set Estatus = @Estatus where UUID = @UUID";
                    cmd.Parameters.AddWithValue("@UUID", UUID);
                    cmd.Parameters.AddWithValue("@Estatus", estatus);
                    cmd.CommandTimeout = 0;
                    i = cmd.ExecuteNonQuery();
                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return i;
        }

    }

}
