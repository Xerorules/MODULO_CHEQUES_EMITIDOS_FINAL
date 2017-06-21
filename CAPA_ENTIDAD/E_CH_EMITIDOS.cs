using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDAD
{
    public class E_CH_EMITIDOS
    {
        public string id_chequesem { get; set; }
        public string id_proveedor { get; set; }
        public string f_giro { get; set; }
        public string f_cobro { get; set; }
        public string f_cobrado { get; set; }
        public int n_cheque { get; set; }
        public string id_chequera { get; set; }
        public decimal importe { get; set; }
        public string estado { get; set; }
        public string id_movimiento { get; set; }
        public string observacion { get; set; }
    }
}
