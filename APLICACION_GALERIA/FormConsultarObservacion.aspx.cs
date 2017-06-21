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
    public partial class Formulario_web14 : System.Web.UI.Page
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
                    llenar_grilla();
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
            string fecha_ini = Convert.ToDateTime("01-01-2017").ToString("dd-MM-yyyy");

            DataTable dt = OBJLOGUEO.LLENAR_GRILLA("01-01-2017", "01-01-2017", "", 0, "", "TODO", Session["ID_EMPRESA"].ToString(), "");
            dgvMOVIMIENTOS1.DataSource = dt;
            dgvMOVIMIENTOS1.DataBind();

            for (int i = 0; i < dgvMOVIMIENTOS1.Rows.Count; i++)
            {
                dgvMOVIMIENTOS1.Rows[i].Cells[1].Text = Convert.ToDateTime(dgvMOVIMIENTOS1.Rows[i].Cells[1].Text).ToShortDateString();
                if (dgvMOVIMIENTOS1.Rows[i].Cells[9].Text == "0") { dgvMOVIMIENTOS1.Rows[i].Cells[9].Text = "NO TIENE AMARRE"; dgvMOVIMIENTOS1.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Green; }
                else
                {
                    if (dgvMOVIMIENTOS1.Rows[i].Cells[9].Text == "0") { dgvMOVIMIENTOS1.Rows[i].Cells[9].Text = "TIENE AMARRE"; dgvMOVIMIENTOS1.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Red; }
                }
            }
        }

        void filtrar_grilla()
        {
            try
            {
                string banco = "";
                string moneda = "";
                string ID_EMPRESA = Session["ID_EMPRESA"].ToString();
                string fecha_ini = Convert.ToDateTime("01-01-2017").ToString("dd-MM-yyyy");
                string operacion = "";
                string OBS = "";
                /*------------------*/
                int cbo = cboFTBV1.SelectedIndex;
                string cboval = cboFTBV1.SelectedValue;

                /*------------------*/

                if (cbo != 0 && txtserie1.Text != "" && txtnumero1.Text != "" && CheckBox11.Checked == false)
                {
                    OBS = OBS + cboval + "/" + txtserie1.Text + "/" + txtnumero1.Text;
                }
                else if (cbo != 0 && txtserie1.Text != "" && txtnumero1.Text != "" && CheckBox11.Checked == true && txtfinal1.Text != "")
                {
                    OBS = OBS + cboval + "/" + txtserie1.Text + "/" + txtnumero1.Text + "-" + txtfinal1.Text;
                }
                else
                {
                    OBS = "";
                }


                if (cbo != 0 && txtserie1.Text != "" && txtnumero1.Text != "")
                {
                    dgvMOVIMIENTOS1.DataSource = OBJLOGUEO.LLENAR_GRILLA(fecha_ini, fecha_ini, banco, 0, moneda, operacion, Session["ID_EMPRESA"].ToString(), OBS);
                    dgvMOVIMIENTOS1.DataBind();

                    for (int i = 0; i < dgvMOVIMIENTOS1.Rows.Count; i++)
                    {
                        dgvMOVIMIENTOS1.Rows[i].Cells[1].Text = Convert.ToDateTime(dgvMOVIMIENTOS1.Rows[i].Cells[1].Text).ToShortDateString();

                        if (dgvMOVIMIENTOS1.Rows[i].Cells[9].Text == "0") { dgvMOVIMIENTOS1.Rows[i].Cells[9].Text = "NO TIENE AMARRE"; dgvMOVIMIENTOS1.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Green; }
                        else
                        {
                            if (dgvMOVIMIENTOS1.Rows[i].Cells[9].Text != "0") { dgvMOVIMIENTOS1.Rows[i].Cells[9].Text = "TIENE AMARRE"; dgvMOVIMIENTOS1.Rows[i].Cells[9].ForeColor = System.Drawing.Color.Red; }
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

        }

        protected void btnConsulta2_Click(object sender, EventArgs e)
        {
            filtrar_grilla();
        }

        protected void cboFTBV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFTBV1.SelectedValue == "BV")
            {
                CheckBox11.Enabled = true;
                cboFTBV1.Focus();
            }
            else if (cboFTBV1.SelectedValue != "BV")
            {
                CheckBox11.Checked = false;
                CheckBox11.Enabled = false;
                txtfinal1.Visible = false;
                cboFTBV1.Focus();
            }
        }

        protected void txtserie1_TextChanged(object sender, EventArgs e)
        {
            string fmt = "000";
            txtserie1.Text = Convert.ToUInt32(txtserie1.Text).ToString(fmt);
            txtnumero1.Focus();
        }

        protected void txtnumero1_TextChanged(object sender, EventArgs e)
        {
            if (CheckBox11.Checked == true)
            {
                string fmt = "00000";
                txtnumero1.Text = Convert.ToInt32(txtnumero1.Text).ToString(fmt);
                txtfinal1.Focus();
            }
            else if (CheckBox11.Checked == false)
            {
                string fmt = "00000";
                txtnumero1.Text = Convert.ToUInt32(txtnumero1.Text).ToString(fmt);
                btnConsulta2.Focus();
            }
        }

        protected void txtfinal1_TextChanged(object sender, EventArgs e)
        {
            if (CheckBox11.Checked == true)
            {
                string fmt = "00000";
                txtfinal1.Text = Convert.ToInt32(txtfinal1.Text).ToString(fmt);
                btnConsulta2.Focus();
            }
            else if (CheckBox11.Checked == false)
            {

                txtnumero1.Text = "";
                btnConsulta2.Focus();

            }
        }

        protected void CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox11.Checked == true)
            {

                txtnumero1.MaxLength = 5;
                txtfinal1.Visible = true;
                txtserie1.Focus();

            }
            else if (CheckBox11.Checked == false)
            {
                txtnumero1.MaxLength = 5;
                txtfinal1.Visible = false;
                // txtnumero.Width = 55;
                if (txtnumero1.Text != "") { txtnumero1.Text = txtnumero1.Text; } else { txtnumero1.Text = ""; }
                txtserie1.Focus();

            }
        }
    }
}