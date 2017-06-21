using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_DATOS;


namespace CAPA_NEGOCIO
{
    public class NBien
    {

        public static DataTable NBienes_Listado(string id_prop)
        {
            return DBienes.LISTAR_BIENES(id_prop);
        }
    }
}
