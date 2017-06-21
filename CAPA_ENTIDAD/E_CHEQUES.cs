using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDAD
{
    public class E_CHEQUES
    {
        public string id_chequera { get; set; }
        public string id_cuentasbancarias { get; set; }
        public int n_inicial { get; set; }
        public int n_final { get; set; }
        public string f_emision { get; set; }
        public string f_vcto { get; set; }
        public int n_correlativo { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
        public string obs { get; set; }
        public string usuario { get; set; }
    }
}
