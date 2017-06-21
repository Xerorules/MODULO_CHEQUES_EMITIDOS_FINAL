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
    public partial class Formulario_web16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string res = Session["ID_EMPRESA"].ToString();
                    if (Session["ID_EMPRESA"].ToString() == "" || Session["ID_EMPLEADO"].ToString() == "" || Session["USUARIO"].ToString() == "")
                    {
                        Response.Redirect("default.aspx");
                    }
                    TXTFECHAINI.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    CBOBANCOS.SelectedIndex = 0;
                    CBOMONEDA.SelectedIndex = 0;
                    llenar_grilla();
                    llenar_combo_bancos();
                }
                catch
                {
                    Response.Redirect("default.aspx");
                }
                
            }

        }

        #region OBJETOS
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();
        #endregion

        void llenar_grilla()
        {
            string fecha_ini = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");

            DataTable dt = OBJLOGUEO.LLENAR_GRILLA("01-01-2017","01-01-2017","", 0,"","TODO",Session["ID_EMPRESA"].ToString(),"");
            dgvMOVIMIENTOS.DataSource = dt;
            dgvMOVIMIENTOS.DataBind();

            for (int i = 0; i < dgvMOVIMIENTOS.Rows.Count; i++)
            {
                dgvMOVIMIENTOS.Rows[i].Cells[1].Text = Convert.ToDateTime(dgvMOVIMIENTOS.Rows[i].Cells[1].Text).ToShortDateString();
                if (dgvMOVIMIENTOS.Rows[i].Cells[9].Text == "0") { dgvMOVIMIENTOS.Rows[i].Cells[9].Text = "NO TIENE AMARRE"; dgvMOVIMIENTOS.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Green; }
                else
                {
                    if (dgvMOVIMIENTOS.Rows[i].Cells[9].Text == "0") { dgvMOVIMIENTOS.Rows[i].Cells[9].Text = "TIENE AMARRE"; dgvMOVIMIENTOS.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Red; }
                }
            }
        }

        void filtrar_grilla()
        {
            try
            {
                string banco = CBOBANCOS.SelectedValue.ToString();
                string moneda = CBOMONEDA.SelectedValue.ToString();
                string ID_EMPRESA = Session["ID_EMPRESA"].ToString();
                string fecha_ini = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                string operacion = TXTOPE.Text.Trim();
                string OBS = "";
                if (fecha_ini != "" && moneda != "" && operacion != "" && banco != "")
                {
                    dgvMOVIMIENTOS.DataSource = OBJLOGUEO.LLENAR_GRILLA(fecha_ini, fecha_ini, banco, 0, moneda, operacion, Session["ID_EMPRESA"].ToString(), OBS);
                    dgvMOVIMIENTOS.DataBind();

                    for (int i = 0; i < dgvMOVIMIENTOS.Rows.Count; i++)
                    {
                        dgvMOVIMIENTOS.Rows[i].Cells[1].Text = Convert.ToDateTime(dgvMOVIMIENTOS.Rows[i].Cells[1].Text).ToShortDateString();

                        if (dgvMOVIMIENTOS.Rows[i].Cells[9].Text == "0") { dgvMOVIMIENTOS.Rows[i].Cells[9].Text = "NO TIENE AMARRE"; dgvMOVIMIENTOS.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Green; }
                        else
                        {
                            if (dgvMOVIMIENTOS.Rows[i].Cells[9].Text != "0") { dgvMOVIMIENTOS.Rows[i].Cells[9].Text = "TIENE AMARRE"; dgvMOVIMIENTOS.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Red; }
                        }
                    }
                }
                else
                {
                    //Response.Write("<script>'DEBE LLENAR TODOS LOS FILTROS'</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                

                
            }
            catch
            {
                Response.Write("<script>'DEBE LLENAR LOS FILTROS CORRECTAMENTE'</script>");
            }
            
        }

        void llenar_combo_bancos()
        {
            DataTable dt = OBJLOGUEO.CONSULTA_LISTA_BANCOS();

            CBOBANCOS.DataSource = dt;
            CBOBANCOS.DataValueField = "ID_BANCOS";
            CBOBANCOS.DataTextField = "NOMBRE";
            CBOBANCOS.DataBind();
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            filtrar_grilla();
        }

        protected void dgvMOVIMIENTOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                e.Row.Cells[8].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;width:280px");
            }
        }

        protected void cboFTBV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtserie_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtnumero_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtfinal_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}