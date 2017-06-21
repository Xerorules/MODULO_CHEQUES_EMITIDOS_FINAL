using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using CAPA_ENTIDAD;
using System.Data;

namespace APLICACION_GALERIA
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected String TITULO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
                    try
                    {
                        string res = Session["ID_EMPRESA"].ToString();
                        if (Session["ID_EMPRESA"].ToString() == "" || Session["ID_EMPLEADO"].ToString() == "" || Session["USUARIO"].ToString() == "")
                        {
                            Response.Redirect("index.aspx");
                        }
                        
                    }
                    catch
                    {
                        Response.Redirect("index.aspx");
                    }

                

                Master.label.Text = "CHEQUERAS"; // seteamos el valor
                Response.Write(Master.label.Text);

                TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Session["ID_CHEQUE"] = "";
                




                llenar_datos();
                TXTCUENTA.Focus();
            }
            
        }

        #region OBJETOS
        N_LOGUEO OBJVENTA = new N_LOGUEO();
        E_CHEQUES OBJCHEQUE = new E_CHEQUES();

        #endregion

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                try
                {
                        OBJCHEQUE.id_cuentasbancarias = txtCUENTACH.Text;
                        OBJCHEQUE.f_emision = Convert.ToDateTime(TXTFECHAEMISION.Text).ToShortDateString();
                        OBJCHEQUE.f_vcto = Convert.ToDateTime(TXTFECHAVCTO.Text).ToShortDateString();
                        OBJCHEQUE.n_inicial = Convert.ToInt32(TXTXNUMINI.Text);
                        OBJCHEQUE.n_final = Convert.ToInt32(TXTNUMFIN.Text);
                        OBJCHEQUE.n_correlativo = Convert.ToInt32(TXTXNUMINI.Text);
                        OBJCHEQUE.tipo = CBOTIPO.SelectedValue;
                        OBJCHEQUE.estado = "ACTIVO";
                        OBJCHEQUE.obs = TXTOBS.Text;
                        OBJCHEQUE.usuario = "";
                        String cond = "2";
                        string id_empresa = Session["ID_EMPRESA"].ToString();


                        string res = OBJVENTA.NREGISTRARCHEQUE(OBJCHEQUE,cond);

                        if (res == "ok")
                        {
                            //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                            llenar_datos();
                            
                            TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
                            TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                            TXTCUENTA.Focus();
                            txtCUENTACH.Text = "";
                        }
                        else
                        {
                            //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                        }
                   
                }
                catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true); }

            }
        }

        protected void dgvBANCOS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void llenar_datos()
        {
            OBJCHEQUE.id_cuentasbancarias = "2018";
            OBJCHEQUE.f_emision = "";
            OBJCHEQUE.f_vcto = "";
            OBJCHEQUE.n_inicial = 0;
            OBJCHEQUE.n_final = 0;
            OBJCHEQUE.n_correlativo = 0;
            OBJCHEQUE.tipo = "";
            OBJCHEQUE.estado = "ACTIVO";
            OBJCHEQUE.obs = "";
            OBJCHEQUE.usuario = "";
            DataTable dt =  OBJVENTA.NLLENARGRILLACHEQUERAS(OBJCHEQUE);
            dgvBANCOS.DataSource = dt;
            dgvBANCOS.DataBind();
        }
    }
}