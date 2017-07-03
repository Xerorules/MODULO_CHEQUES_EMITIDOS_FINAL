using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using CAPA_ENTIDAD;
using CAPA_DATOS;
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

                Session["EDITAR"] = "";
                txtCUENTACH.Text = "";
                Session["HABILITADO"] = "NO";
                deshabilitarcampos();
                BTNFILTRACHEQUERAS.ToolTip = "Escojer una cuenta";
                Master.label.Text = "CHEQUERAS"; // seteamos el valor
                Response.Write(Master.label.Text);

                /*----------------------------------------------------------------*/
                //Menu myMenu = (Menu)Master.FindControl("lista");

                //myMenu.Visible = false;
                /*----------------------------------------------------------------*/

                Master.label2.Text = " - " + Session["NOM_EMPRESA"].ToString();
                Response.Write(Master.label2.Text);

                TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Session["ID_CHEQUE"] = "";
                




               
                TXTCUENTA.Focus();
            }
            
        }

        #region OBJETOS
        N_LOGUEO OBJVENTA = new N_LOGUEO();
        E_CHEQUES OBJCHEQUE = new E_CHEQUES();
        D_LOGIN OBJDATOS = new D_LOGIN();

        #endregion

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                if (Convert.ToInt32(TXTXNUMINI.Text) < Convert.ToInt32(TXTNUMFIN.Text) && Convert.ToDateTime(TXTFECHAEMISION.Text) < Convert.ToDateTime(TXTFECHAVCTO.Text))
                {
                    //CARGAMOS LA TABLA QUE VALIDA EL NUMERO D EINICIO DE LAS CHEQUERAS
                    DataTable dtr = OBJDATOS.DNUMERO_DEINICIO_CHEQUERA(CBOTIPO.SelectedValue, txtCUENTACH.Text, "ACTIVO");

                    if (Session["EDITAR"].ToString() == "")
                    {
                        if (Convert.ToInt32(TXTXNUMINI.Text) > Convert.ToInt32(dtr.Rows[0][0].ToString()))
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
                                string id_chequera = "";


                                string res = OBJVENTA.NREGISTRARCHEQUE(OBJCHEQUE, cond, id_chequera);

                                if (res == "ok")
                                {
                                    //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                                    llenar_datos();

                                    TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                    TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                    limpiarcampos();

                                    TXTFECHAEMISION.Focus();

                                }
                                else
                                {
                                    //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                                }

                            }
                            catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true); }
                        }
                        else
                        {
                            Response.Write("<script>alert('EL NUMERO INICIAL DEBE SER CORRELATIVO Y MAYOR, QUE EL NUMERO FINAL DE LA ANTERIOR CHEQUERA')</script>");
                        }
                    }
                    else if (Session["EDITAR"].ToString() == "EDITAR")
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
                            String cond = "4";
                            string id_empresa = Session["ID_EMPRESA"].ToString();
                            string id_chequera = Session["ID_CHEQUERA"].ToString();

                            string res = OBJVENTA.NREGISTRARCHEQUE(OBJCHEQUE, cond, id_chequera);

                            if (res == "ok")
                            {
                                //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                                llenar_datos();

                                TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                limpiarcampos();

                                TXTFECHAEMISION.Focus();
                                Session["EDITAR"] = "";

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
                else
                {
                    if (Convert.ToInt32(TXTXNUMINI.Text) >= Convert.ToInt32(TXTNUMFIN.Text))
                    {
                        
                        ScriptManager.RegisterStartupScript(UPCUERPO, UPCUERPO.GetType(), "Pop", "openModal1();", true);
                    }
                    if (Convert.ToDateTime(TXTFECHAEMISION.Text) >= Convert.ToDateTime(TXTFECHAVCTO.Text))
                    {
                        
                        ScriptManager.RegisterStartupScript(UPCUERPO, UPCUERPO.GetType(), "Pop", "openModal1();", true);
                    }
                }

            }
        }

        protected void dgvBANCOS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void llenar_datos()
        {
            OBJCHEQUE.id_cuentasbancarias = txtCUENTACH.Text;
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

        protected void dgvBANCOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvBANCOS.PageIndex = e.NewPageIndex;
            OBJCHEQUE.id_cuentasbancarias = txtCUENTACH.Text;
            OBJCHEQUE.f_emision = "";
            OBJCHEQUE.f_vcto = "";
            OBJCHEQUE.n_inicial = 0;
            OBJCHEQUE.n_final = 0;
            OBJCHEQUE.n_correlativo = 0;
            OBJCHEQUE.tipo = "";
            OBJCHEQUE.estado = "ACTIVO";
            OBJCHEQUE.obs = "";
            OBJCHEQUE.usuario = "";
            DataTable dt = OBJVENTA.NLLENARGRILLACHEQUERAS(OBJCHEQUE);
            dgvBANCOS.DataSource = dt;
            dgvBANCOS.DataBind();
        }

        protected void BTNFILTRACHEQUERAS_Click(object sender, ImageClickEventArgs e)
        {
            //VALIDAR SI ESTA DESHABILITADO
            if (Session["HABILITADO"].ToString() == "NO")
            {
                if (TXTCUENTA.Text.Length > 20 && txtCUENTACH.Text.Length > 0)
                {
                    lblmensajecuenta.Visible = false;
                    habilitarcampos();
                    btnRegistrar.Enabled = true;
                    btnCancelar.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsKeys", "javascript:limpiarcuenta();", true);
                    BTNFILTRACHEQUERAS.ImageUrl = "/ICONOS/ELIMINAR.png";
                    BTNFILTRACHEQUERAS.ToolTip = "Cambiar cuenta";
                    Session["HABILITADO"] = "SI";
                    llenar_datos();
                }
                else
                {
                    lblmensajecuenta.Visible = true;
                }
            }
            else if (Session["HABILITADO"].ToString() == "SI")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsKeys", "javascript:reactivarcuenta();", true);
                limpiarcampos();
                deshabilitarcampos();
                limpiarcampos();
                BTNFILTRACHEQUERAS.ImageUrl = "/ICONOS/busqueda.png";
                BTNFILTRACHEQUERAS.ToolTip = "Escojer una cuenta";
                Session["HABILITADO"] = "NO";
                TXTCUENTA.Focus();

                DataTable dt = new DataTable();
                dgvBANCOS.DataSource = dt;
                dgvBANCOS.DataBind();
            }

            Session["EDITAR"] = "";

        }

        void habilitarcampos()
        {
            TXTFECHAEMISION.Enabled = true;
            TXTXNUMINI.Enabled = true;
            TXTNUMFIN.Enabled = true;
            TXTFECHAVCTO.Enabled = true;
            CBOTIPO.Enabled = true;
            TXTOBS.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        void deshabilitarcampos()
        {
            TXTFECHAEMISION.Enabled = false;
            TXTXNUMINI.Enabled = false;
            TXTNUMFIN.Enabled = false;
            TXTFECHAVCTO.Enabled = false;
            CBOTIPO.Enabled = false;
            TXTOBS.Enabled = false;
            btnCancelar.Enabled = false;
            btnRegistrar.Enabled = false;
        }

        void limpiarcampos()
        {
            TXTXNUMINI.Text = "";
            TXTNUMFIN.Text = "";
            CBOTIPO.SelectedIndex = 0;
            TXTOBS.Text = "";
            TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Session["EDITAR"] = "";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarcampos();
        }

        protected void dgvBANCOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                GridViewRow raw = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                TXTFECHAEMISION.Text = Convert.ToDateTime(HttpUtility.HtmlDecode(raw.Cells[7].Text.Trim())).ToString("yyyy-MM-dd");
                TXTFECHAVCTO.Text = Convert.ToDateTime(HttpUtility.HtmlDecode(raw.Cells[8].Text.Trim())).ToString("yyyy-MM-dd");
                TXTXNUMINI.Text = HttpUtility.HtmlDecode(raw.Cells[4].Text.Trim());
                TXTNUMFIN.Text = HttpUtility.HtmlDecode(raw.Cells[5].Text.Trim());
                CBOTIPO.SelectedValue = HttpUtility.HtmlDecode(raw.Cells[9].Text.Trim());
                TXTOBS.Text = HttpUtility.HtmlDecode(raw.Cells[10].Text.Trim());
                Session["EDITAR"] = "EDITAR";
                Session["ID_CHEQUERA"] = HttpUtility.HtmlDecode(raw.Cells[0].Text.Trim());

            }
            else if (e.CommandName == "ELIMINAR")
            {
                try
                {
                    GridViewRow raw = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    OBJCHEQUE.id_cuentasbancarias = "2018";
                    OBJCHEQUE.f_emision = Convert.ToDateTime("01-01-2017").ToShortDateString();
                    OBJCHEQUE.f_vcto = Convert.ToDateTime("01-01-2017").ToShortDateString();
                    OBJCHEQUE.n_inicial = Convert.ToInt32("1");
                    OBJCHEQUE.n_final = Convert.ToInt32("2");
                    OBJCHEQUE.n_correlativo = Convert.ToInt32("5");
                    OBJCHEQUE.tipo = CBOTIPO.SelectedValue;
                    OBJCHEQUE.estado = "ACTIVO";
                    OBJCHEQUE.obs = TXTOBS.Text;
                    OBJCHEQUE.usuario = "";
                    String cond = "3";
                    string id_empresa = Session["ID_EMPRESA"].ToString();
                    string id_chequera = HttpUtility.HtmlDecode(raw.Cells[0].Text.Trim());

                    string res = OBJVENTA.NREGISTRARCHEQUE(OBJCHEQUE, cond, id_chequera);

                    if (res == "ok")
                    {
                        //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                        llenar_datos();

                        TXTFECHAEMISION.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        TXTFECHAVCTO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        limpiarcampos();

                        TXTFECHAEMISION.Focus();
                        

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
    }
}