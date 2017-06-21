using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class DBienes
    {
        public static DataTable LISTAR_BIENES(string id_prop)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_BIENES_POR_PROPIETARIO", con);
            cmd.Parameters.AddWithValue("@ID_PROP", id_prop);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
