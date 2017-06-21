using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using System.Data;

namespace APLICACION_GALERIA
{
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected String TITULO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                Master.label.Text = "LOGIN"; // seteamos el valor
                Response.Write(Master.label.Text);

                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
                LISTAR_EMPRESA();

                Session["ID_EMPRESA"] = "";
                Session["ID_EMPLEADO"] = "";
                Session["USUARIO"] = "";
                Session["ID_CUENTA"] = "";
                Session["CUENTA"] = "";
            }
        }


        #region OBJETOS
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();

        #endregion

        void validar_login()
        {
            try
            {
                DataTable dt = OBJLOGUEO.VALIDAR_USUARIO(txtusuario.Text, txtcontraseña.Text, CBOEMPRESA.SelectedValue);
                if (dt.Rows[0][0].ToString() == CBOEMPRESA.SelectedValue.ToString() && dt.Rows[0][1].ToString() != "" && dt.Rows[0][2].ToString() != "")
                {
                    Session["ID_EMPRESA"] = CBOEMPRESA.SelectedValue;
                    Session["ID_EMPLEADO"] = dt.Rows[0][1].ToString();
                    Session["USUARIO"] = dt.Rows[0][2].ToString();
                    ESTRUCTURA_GRILLA_DOCS();
                    ESTRUCTURA_GRILLA_MOVIS();
                    ESTRUCTURA_GRILLA_DOCS2();
                    Response.Redirect("FORM_CHEQUERAS.aspx");
                    Server.Transfer("FORM_CHEQUERAS.aspx", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal5();", true); 
            }
    
        }

        private void LISTAR_EMPRESA()
        {
            CBOEMPRESA.DataSource = OBJLOGUEO.LISTAR_EMPRESA();
            CBOEMPRESA.DataValueField = "ID_EMPRESA";
            CBOEMPRESA.DataTextField = "DESCRIPCION";
            CBOEMPRESA.DataBind();
        }

        protected void btnACEPTAR_Click1(object sender, EventArgs e)
        {
            validar_login();
        }

        void ESTRUCTURA_GRILLA_DOCS()
        {
            DataTable dt_DOCS = new DataTable();
            DataColumn colum = dt_DOCS.Columns.Add("ID", typeof(String));
            dt_DOCS.Columns.Add(new DataColumn("NUMERO", typeof(String)));
            dt_DOCS.Columns.Add(new DataColumn("OBSERVACION", typeof(String)));
            Session["GRILLA_CHEQUERAS"] = dt_DOCS;
        }

        void ESTRUCTURA_GRILLA_MOVIS()
        {
            DataTable dt_MOVS = new DataTable();
            DataColumn colum = dt_MOVS.Columns.Add("ID", typeof(String));
            dt_MOVS.Columns.Add(new DataColumn("FECHA", typeof(String)));
            dt_MOVS.Columns.Add(new DataColumn("OPERACION", typeof(String)));
            dt_MOVS.Columns.Add(new DataColumn("DESCRIPCION", typeof(String)));
            dt_MOVS.Columns.Add(new DataColumn("IMPORTE", typeof(decimal)));
            Session["GRILLA_MOVS"] = dt_MOVS;
        }

        void ESTRUCTURA_GRILLA_DOCS2()
        {
            DataTable dt_DOCS = new DataTable();
            DataColumn colum = dt_DOCS.Columns.Add("DOC", typeof(String));
            dt_DOCS.Columns.Add(new DataColumn("SERIE", typeof(String)));
            dt_DOCS.Columns.Add(new DataColumn("NUMERO", typeof(String)));
            Session["GRILLA_DOCS"] = dt_DOCS;
        }

    }
}