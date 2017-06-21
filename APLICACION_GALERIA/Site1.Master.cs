using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace APLICACION_GALERIA
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected String TITULO = "";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public Label label
        {
            get { return lblTITULO; }
            set { lblTITULO = value; }
        }

        void bloquear_controles()
        {

        }
    }
}