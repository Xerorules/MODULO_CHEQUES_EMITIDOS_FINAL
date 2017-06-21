using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_DATOS;
using System.Data;

namespace CAPA_NEGOCIO
{
    public class NPropietario
    {
        public static DataTable NPropietarios_combo()
        {
            return DPropietario.DLISTA_COMBO_PROPIETARIOS();
        }
    }
}
