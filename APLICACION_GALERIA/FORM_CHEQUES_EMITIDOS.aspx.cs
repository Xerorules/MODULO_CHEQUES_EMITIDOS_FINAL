using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using CAPA_ENTIDAD;
using System.Drawing;

namespace APLICACION_GALERIA
{
    public partial class Formulario_web13 : System.Web.UI.Page
    {
        public string Codigo { get; set; }
        public string id_ch = "";
        public string num_ch = "";
        public string obs_ch = "";

        public string id_mov = "";
        public string f_mov = "";
        public string ope_mov = "";
        public string des_mov = "";
        public decimal impo_mov = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Page.IsPostBack)
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

                }

                dgvBANCOS.DataSource = "";
                dgvBANCOS.DataBind();

                dgvMOV_CHE.DataSource = "";
                dgvMOV_CHE.DataBind();

                txtID_CUENTA.Text = "";
                TXTCUENTA.Text = "";

                Master.label.Text = "EMISION DE CHEQUES";// seteamos el valor
                //if ()
                //{
                //    Master.label.Text = "EMISION DE CHEQUES";
                //}
                //HACER QUE CAMBIE EL LABEL DEL MASTER PAGE
                
                Response.Write(Master.label.Text);
                TXTCUENTA.Focus();
                inhabilitar();
                
                TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Session["VARIABLE_ACTUALIZAR"] = "";
            }
        }

        #region OBJETOS
        N_LOGUEO OBJVENTA = new N_LOGUEO();
        E_CH_EMITIDOS OBJCHEQUES = new E_CH_EMITIDOS();
        #endregion

        void inhabilitar()
        {
            TXTPROOVEEDOR.Enabled = false;
            TXTFGIRO.Enabled = false;
            TXTFCOBRO.Enabled = false;
            TXTNUMERO.Enabled = false;
            TXTIMPORTE.Enabled = false;
            
            TXTOBS.Enabled = false;
        }

        void habilitar()
        {
            TXTPROOVEEDOR.Enabled = true;
            TXTFGIRO.Enabled = true;
            TXTFCOBRO.Enabled = true;
            TXTNUMERO.Enabled = true;
            TXTIMPORTE.Enabled = true;
            
            TXTOBS.Enabled = true;
        }

        protected void txtID_CUENTA_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void dgvBANCOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvBANCOS.Rows)
            {
                if (row.RowIndex == dgvBANCOS.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        protected void dgvBANCOS_SelectedIndexChanging(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvBANCOS.Rows)
            {
                if (row.RowIndex == dgvBANCOS.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            
                if (Session["VARIABLE_ACTUALIZAR"].ToString() == "")
                {
                if (TXTPROOVEEDOR.Text != "")
                {
                    try
                    {
                        OBJCHEQUES.id_chequesem = "";
                        OBJCHEQUES.id_proveedor = txtPROO.Text;
                        OBJCHEQUES.f_giro = Convert.ToDateTime(TXTFGIRO.Text).ToShortDateString();
                        OBJCHEQUES.f_cobro = Convert.ToDateTime(TXTFCOBRO.Text).ToShortDateString();
                        OBJCHEQUES.f_cobrado = "";
                        OBJCHEQUES.n_cheque = Convert.ToInt32(TXTNUMERO.Text);
                        OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                        OBJCHEQUES.importe = Convert.ToDecimal(TXTIMPORTE.Text);
                        OBJCHEQUES.estado = "EMITIDO";
                        OBJCHEQUES.id_movimiento = "1002";

                        /*CAMBIOS EN OPERACION 19-05-2017 -- AGREGAMOS GRILLA CON AMARRE DE DOCUMENTOS DE VENTA*/
                        DataTable dbt = (DataTable)Session["GRILLA_DOCS"];
                        string cadena = "";
                        for (int y = 0; y < dbt.Rows.Count; y++)
                        {
                            cadena = cadena + dbt.Rows[y][0].ToString() + "/" + dbt.Rows[y][1].ToString() + "/" + dbt.Rows[y][2].ToString() + "//";
                        }


                        if (TXTOBS.Text == "" || TXTOBS.Text == "&nbsp;")
                        {
                            OBJCHEQUES.observacion = cadena;
                        }
                        else
                        {
                            string obf = cadena + "#" + TXTOBS.Text.ToUpper();
                            OBJCHEQUES.observacion = obf.ToUpperInvariant();
                        }

                        
                        /*------------------------------------------------------------------------------------*/


                        
                        int contador = 0;
                        string COND = "2";

                        DataTable dt = OBJVENTA.NLISTAR_CORRELATIVO(Session["ID_CHEQUERA"].ToString());
                        DataTable dt1 = OBJVENTA.NLISTAR_INI_FIN(Session["ID_CHEQUERA"].ToString());
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() == TXTNUMERO.Text)
                            {
                                contador = contador + 1;
                            }
                        }

                        if (contador > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal9();", true);//mensaje de numero repetido
                        }
                        else if (contador == 0 && Convert.ToInt32(dt1.Rows[0][0].ToString()) <= Convert.ToInt32(TXTNUMERO.Text) && Convert.ToInt32(dt1.Rows[0][1].ToString()) >= Convert.ToInt32(TXTNUMERO.Text))
                        {
                            string res = "";
                            if (Convert.ToDateTime(TXTFGIRO.Text) <= Convert.ToDateTime(TXTFCOBRO.Text))
                            {
                                res = OBJVENTA.NREGISTRARCHEQUE_EM2(OBJCHEQUES, COND);
                            }
                            if (res == "ok")
                            {
                                //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                                llenar_datos();

                                TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                limpiar();
                                inhabilitar();
                                txtID_CUENTA.Text = Session["ID_CUENTA"].ToString();
                                TXTCUENTA.Text = Session["CUENTA"].ToString();
                                LBLCHEQUERA2.Text = "SELECCIONE UNA CHEQUERA";

                                DataTable dt3 = (DataTable)Session["GRILLA_DOCS"];
                                dt3.Clear();
                                dgvDATOS2.DataSource = "";
                                dgvDATOS2.DataBind();

                                TXTCUENTA.Focus();
                            }
                            else
                            {
                                //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal01();", true);//mensaje de numero repetido
                        }





                    }
                    catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true); }
                    }
                    else
                    {
                        lblmensajeproveedor.Visible = true;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "desaparecer_lbl();", true);
                    }
                    
                }
                else
                {
                try
                {   //CAMBIAMOS LOS VALORES ASIGNADOS A LOS ATRIBUTOS,QUE SON LLENADOS EN VARIABLES DE SESSION Y TEXTBOX DESDE EL IMAGEBUTTON EDITAR
                    OBJCHEQUES.id_chequesem = Session["ID_CHEQUE_EMIT"].ToString();
                    string var = Session["ID_PROVEEDOR"].ToString();

                    OBJCHEQUES.id_proveedor = var;
                    OBJCHEQUES.f_giro = Convert.ToDateTime(TXTFGIRO.Text).ToShortDateString();
                    OBJCHEQUES.f_cobro = Convert.ToDateTime(TXTFCOBRO.Text).ToShortDateString();

                    try
                    {
                        OBJCHEQUES.f_cobrado = Convert.ToDateTime(Session["FECHA_COBRADO"].ToString()).ToShortDateString();
                    }
                    catch
                    {
                        OBJCHEQUES.f_cobrado = "";
                    }
                                      
                    OBJCHEQUES.n_cheque = Convert.ToInt32(TXTNUMERO.Text);
                    OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                    OBJCHEQUES.importe = Convert.ToDecimal(TXTIMPORTE.Text);
                    OBJCHEQUES.estado = Session["ESTADO"].ToString();
                    if (Session["ID_MOVS"].ToString() == "")
                    {
                        OBJCHEQUES.id_movimiento = "1002";
                    }
                    else
                    {
                        OBJCHEQUES.id_movimiento = Session["ID_MOVS"].ToString();
                    }

                    /*CAMBIOS EN OPERACION 19-05-2017 -- AGREGAMOS GRILLA CON AMARRE DE DOCUMENTOS DE VENTA*/
                    DataTable dbt = (DataTable)Session["GRILLA_DOCS"];
                    string cadena = "";
                    for (int y = 0; y < dbt.Rows.Count; y++)
                    {
                        cadena = cadena + dbt.Rows[y][0].ToString() + "/" + dbt.Rows[y][1].ToString() + "/" + dbt.Rows[y][2].ToString() + "//";
                    }


                    if (TXTOBS.Text == "" || TXTOBS.Text == "&nbsp;")
                    {
                        OBJCHEQUES.observacion = cadena;
                    }
                    else
                    {
                        string michi = "#";
                        bool ver = TXTOBS.Text.ToUpper().Contains(michi);
                        int indx = -1;
                        if (ver) { indx = TXTOBS.Text.ToUpper().IndexOf(michi); }

                        if (indx >= 0)
                        {
                            string obf = cadena + TXTOBS.Text.ToUpper();
                            OBJCHEQUES.observacion = obf.ToUpperInvariant();
                        }
                        else
                        {
                            string obf = cadena + "#" + TXTOBS.Text.ToUpper();
                            OBJCHEQUES.observacion = obf.ToUpperInvariant();
                        }

                    }
                    
                    /*------------------------------------------------------------------------------------*/




                    int contador = 0;
                        string COND = "5";
                        string PRO = Session["N_CHEQUE"].ToString();
                        DataTable dt = OBJVENTA.NLISTAR_CORRELATIVO2(Session["ID_CHEQUERA"].ToString(), Convert.ToInt32(Session["N_CHEQUE"].ToString()));
                        DataTable dt1 = OBJVENTA.NLISTAR_INI_FIN(Session["ID_CHEQUERA"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == TXTNUMERO.Text)
                        {
                            contador = contador + 1;
                        }
                    }

                    if (contador > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal9();", true);//mensaje de numero repetido
                        }
                        else if (contador == 0 && Convert.ToInt32(dt1.Rows[0][0].ToString()) <= Convert.ToInt32(TXTNUMERO.Text) && Convert.ToInt32(dt1.Rows[0][1].ToString()) >= Convert.ToInt32(TXTNUMERO.Text))
                        {
                            string res = "";
                        if (Convert.ToDateTime(TXTFGIRO.Text) <= Convert.ToDateTime(TXTFCOBRO.Text))
                        {
                            res = OBJVENTA.NREGISTRARCHEQUE_EM2(OBJCHEQUES, COND);
                        }
                        else
                        {
                            //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalF_MAYOR();", true);
                        }

                            if (res == "ok")
                            {
                                //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                                llenar_datos();

                                TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                limpiar();
                                inhabilitar();
                                txtID_CUENTA.Text = Session["ID_CUENTA"].ToString();
                                TXTCUENTA.Text = Session["CUENTA"].ToString();
                                LBLCHEQUERA2.Text = "SELECCIONE UNA CHEQUERA";

                            DataTable dt3 = (DataTable)Session["GRILLA_DOCS"];
                            dt3.Clear();
                            dgvDATOS2.DataSource = "";
                            dgvDATOS2.DataBind();

                            TXTCUENTA.Focus();
                                Session["VARIABLE_ACTUALIZAR"] = "";
                            }
                            else
                            {
                                //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal01();", true);//mensaje de numero repetido
                        }





                    }
                    catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true); }
                }

            
        }

        protected void btnAceptarForm_Click(object sender, EventArgs e)
        {
            habilitar();
        }

        void llamar_click()
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCancelarForm_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalFormClose();", true); 
        }

        protected void btnSalirPopUp_Click(object sender, EventArgs e)
        {
            mp1.Dispose();
            mp1.Hide(); 
        }

        protected void btnAceptarPopUp_Click(object sender, EventArgs e)
        {
            if (dgvDATOS.Rows.Count == 1)
            {
                                
                LBLCHEQUERA2.Text = "CHEQUERA SELECCIONADA";
                
                LBLCHEQUERA2.ForeColor = System.Drawing.Color.Green;
                txtIDCHEQUERA.Text = dgvDATOS.Rows[0].Cells[1].Text;
                Session["ID_CHEQUERA"] = dgvDATOS.Rows[0].Cells[1].Text;
                TXTNUMERO.Text = dgvDATOS.Rows[0].Cells[2].Text;
                llenar_datos();
                habilitar();

                try
                {
                    if (Session["ID_CHEQUERA"].ToString() != "" || Session["ID_CHEQUERA"].ToString() != null)
                    {
                        DataTable dt1 = OBJVENTA.NLISTAR_INI_FIN(Session["ID_CHEQUERA"].ToString());
                        LBLRANGOINI.Text = "N° DE INICIO: " + dt1.Rows[0][0].ToString();
                        LBLRANGOFIN.Text = "N° DE FIN: " + dt1.Rows[0][1].ToString();
                    }
                    else
                    {
                        LBLRANGOINI.Text = "-";
                        LBLRANGOFIN.Text = "-";
                    }
                }
                catch
                {
                    LBLRANGOINI.Text = "-";
                    LBLRANGOFIN.Text = "-";
                }

            }
            else
            {
                LBLCHEQUERA2.Text = "SELECCIONE UNA CHEQUERA";
                LBLRANGOINI.Text = "-";
                LBLRANGOFIN.Text = "-";
                LBLCHEQUERA2.ForeColor = System.Drawing.Color.Red;
                txtIDCHEQUERA.Text = "";
                inhabilitar();
               
            }
        }


        void llenar_labels_cabecera()
        {
            /*---------------------------------------------------------------------------*/
            DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(txtID_CUENTA.Text);
            string mone = dt.Rows[0][0].ToString();

            LBLBANCOCTA.Text = dt.Rows[0][1].ToString();
            if (mone == "S")
            {
                //LBLMONEDA.Text = "SOLES";
                LBLSALDOCONT.Text = "S/." + Convert.ToDecimal(dt.Rows[0][2].ToString()).ToString("#,###0.00");
                LBLSALDODIP.Text = "S/." + Convert.ToDecimal(dt.Rows[0][3].ToString()).ToString("#,###0.00");
            }
            else if (mone == "D")
            {
                //LBLMONEDA.Text = "DOLAR";
                LBLSALDOCONT.Text = "$  " + Convert.ToDecimal(dt.Rows[0][2].ToString()).ToString("#,###0.00");
                LBLSALDODIP.Text = "$  " + Convert.ToDecimal(dt.Rows[0][3].ToString()).ToString("#,###0.00");
            }

            //LBLNCUENTA.Text = dt.Rows[0][4].ToString();
            /*---------------------------------------------------------------------------*/
        }


        protected void BTNCHEQUERA_Click(object sender, EventArgs e)
        {
            dgvBANCOS.DataSource = "";
            dgvBANCOS.DataBind();

           

            if (txtID_CUENTA.Text != string.Empty && TXTCUENTA.Text.Length > 12)
            {
                string id_cuenta = txtID_CUENTA.Text;
                Session["ID_CUENTA"] = txtID_CUENTA.Text;
                Session["CUENTA"] = TXTCUENTA.Text;
                string tipo1 = "CLASICO";
                string tipo2 = "DIFERIDO";
                DataTable dt1 = OBJVENTA.NLLENARGRILLACHEQUERAS_EMIT(id_cuenta, tipo1);
                DataTable dt2 = OBJVENTA.NLLENARGRILLACHEQUERAS_EMIT(id_cuenta, tipo2);
                dgvDATOS.DataSource = "";
                dgvDATOS.DataBind();
                try
                {
                    dgvCHEQUERA_CLAS.DataSource = dt1;
                    dgvCHEQUERA_CLAS.DataBind();

                    dgvCHEQUERA_DIFE.DataSource = dt2;
                    dgvCHEQUERA_DIFE.DataBind();

                    
                }
                catch
                {

                }

                mp1.Show();
            }
            else
            {
                LBLCHEQUERA2.Text = "";
            }

            llenar_labels_cabecera();
        }

              


        void LLENAR_TABLA_SESSION()
        {
            DataTable dt = (DataTable)Session["GRILLA_CHEQUERAS"];

            if (dgvDATOS.Rows.Count == 0)
            {
                try
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = id_ch;
                    row["NUMERO"] = num_ch;
                    row["OBSERVACION"] = obs_ch;

                    dt.Rows.Add(row);
                    dt.AcceptChanges();

                    LLENAR_GRILLA();

                    //aqui limpio la data 
                    id_ch = "";
                    num_ch = "";
                    obs_ch = "";
                    mp1.Show();
                }
                catch (Exception)
                {



                }
            }
            else if(dgvDATOS.Rows.Count == 1)
            {
                dgvDATOS.DataSource = "";
                dgvDATOS.DataBind();

                try
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = id_ch;
                    row["NUMERO"] = num_ch;
                    row["OBSERVACION"] = obs_ch;

                    dt.Rows.Add(row);
                    dt.AcceptChanges();

                    LLENAR_GRILLA();

                    //aqui limpio la data 
                    id_ch = "";
                    num_ch = "";
                    obs_ch = "";
                    mp1.Show();


                }
                catch (Exception)
                {



                }
            }

           
        }


        void LLENAR_TABLA_SESSION_MOVS()
        {
            DataTable dt = (DataTable)Session["GRILLA_MOVS"];

            if (dgvMOV_CHE.Rows.Count == 0)
            {
                try
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = id_mov;
                    row["FECHA"] = f_mov;
                    row["OPERACION"] = ope_mov;
                    row["DESCRIPCION"] = des_mov;
                    row["IMPORTE"] = impo_mov;

                    dt.Rows.Add(row);
                    dt.AcceptChanges();

                    LLENAR_GRILLA_MOVS();

                    //aqui limpio la data 
                    id_mov = "";
                    f_mov = "";
                    ope_mov = "";
                    des_mov = "";
                    impo_mov = 0;

                    mp2.Show();
                }
                catch (Exception)
                {



                }
            }
            else if (dgvMOV_CHE.Rows.Count == 1)
            {
                dgvMOV_CHE.DataSource = "";
                dgvMOV_CHE.DataBind();

                try
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = id_mov;
                    row["FECHA"] = f_mov;
                    row["OPERACION"] = ope_mov;
                    row["DESCRIPCION"] = des_mov;
                    row["IMPORTE"] = impo_mov;

                    dt.Rows.Add(row);
                    dt.AcceptChanges();

                    //LLENAR_GRILLA_MOVS();

                    //aqui limpio la data 
                    id_mov = "";
                    f_mov = "";
                    ope_mov = "";
                    des_mov = "";
                    impo_mov = 0;

                    mp2.Show();


                }
                catch (Exception)
                {



                }
            }


        }







        void LLENAR_GRILLA()
        {
            DataTable dt = (DataTable)Session["GRILLA_CHEQUERAS"];
            dgvDATOS.DataSource = dt;
            dgvDATOS.DataBind();
            
        }

        void LLENAR_GRILLA_MOVS()
        {
            DataTable dt = (DataTable)Session["GRILLA_MOVS"];
            dgvMOV_CHE.DataSource = dt;
            dgvMOV_CHE.DataBind();

        }

        protected void dgvDATOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ELIM")
            {
                GridViewRow raw = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                dgvDATOS.DataSource = "";
                dgvDATOS.DataBind();
                mp1.Show();

            }
        }

        protected void dgvCHEQUERA_DIFE_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AGREGAR")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                id_ch = row.Cells[1].Text;
                num_ch = row.Cells[2].Text;
                obs_ch = row.Cells[3].Text;


                LLENAR_TABLA_SESSION();
                DataTable df = (DataTable)Session["GRILLA_CHEQUERAS"];
                df.Clear();
                mp1.Show();
            }
        }

        protected void dgvCHEQUERA_CLAS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AGREGAR")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                id_ch = row.Cells[1].Text;
                num_ch = row.Cells[2].Text;
                obs_ch = row.Cells[3].Text;


                LLENAR_TABLA_SESSION();
                DataTable df = (DataTable)Session["GRILLA_CHEQUERAS"];
                df.Clear();
                mp1.Show();
            }
        }

        void llenar_datos()
        {
            try
            {
                OBJCHEQUES.id_chequesem = "";
                OBJCHEQUES.id_proveedor = "00002";
                OBJCHEQUES.f_giro = Convert.ToDateTime("01-01-2017").ToShortDateString();
                OBJCHEQUES.f_cobro = Convert.ToDateTime("01-01-2017").ToShortDateString();
                OBJCHEQUES.f_cobrado = Convert.ToDateTime("01-01-2017").ToShortDateString();
                OBJCHEQUES.n_cheque = Convert.ToInt32(0);
                OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                OBJCHEQUES.importe = Convert.ToDecimal(0);
                OBJCHEQUES.estado = "EMITIDO";
                OBJCHEQUES.id_movimiento = "";
                OBJCHEQUES.observacion = "";

                string COND = "1";

                dgvBANCOS.DataSource = OBJVENTA.NREGISTRARCHEQUE_EM(OBJCHEQUES, COND);
                dgvBANCOS.DataBind();

                for (int i=0; i<dgvBANCOS.Rows.Count; i++)
                {
                    
                    string p2 = dgvBANCOS.Rows[i].Cells[8].Text;

                    if (dgvBANCOS.Rows[i].Cells[8].Text == "1002")
                    {
                        dgvBANCOS.Rows[i].Cells[8].Text = "";
                    }

                    dgvBANCOS.Rows[i].Cells[10].BackColor = System.Drawing.Color.Gold;
                }

                TXTPROOVEEDOR.Focus();

            }
            catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true); }
        }


        void limpiar()
        {
            TXTCUENTA.Text = "";
            txtIDCHEQUERA.Text = "";
            txtID_CUENTA.Text = "";
            TXTIMPORTE.Text = "";
            TXTNUMERO.Text = "";
            TXTOBS.Text = "";
            txtPROO.Text = "";
            TXTPROOVEEDOR.Text = "";
        }

        protected void dgvBANCOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onClick", "ChangeColor('" + "dgvBANCOS','" + (e.Row.RowIndex + 1).ToString() + "')");
                }
            
        }

        protected void dgvBANCOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ACTUALIZAR")
            {
                GridViewRow rew = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                string f_cobro = Convert.ToDateTime(rew.Cells[3].Text).ToShortDateString();
                string id_cuenta = Session["ID_CUENTA"].ToString();
                decimal importe = -1 * Convert.ToDecimal(rew.Cells[6].Text);

                Session["ID_CHEQUE_EMIT"] = rew.Cells[0].Text;
                Session["F_COBRO"] = Convert.ToDateTime(rew.Cells[3].Text).ToShortDateString();
                Session["IMPORTE"] = -1 * Convert.ToDecimal(rew.Cells[6].Text);
                

                try
                {
                    dgvMOV_CHE.DataSource = OBJVENTA.NLLENARGRILLA_MOVIMIENTOS_S(rew.Cells[8].Text);
                    dgvMOV_CHE.DataBind();
                }
                catch
                {
                    dgvMOV_CHE.DataSource = "";
                    dgvMOV_CHE.DataBind();
                }


                if (dgvMOV_CHE.Rows.Count == 0)
                {
                    dgvMOVIMIENTOS.DataSource = OBJVENTA.NLLENARGRILLA_MOVIMIENTOS_CHEMITIDOS(id_cuenta, importe, f_cobro);
                    dgvMOVIMIENTOS.DataBind();
                }
                else if (dgvMOV_CHE.Rows.Count > 0)
                {
                    dgvMOVIMIENTOS.DataSource = "";
                    dgvMOVIMIENTOS.DataBind();

                }



                mp2.Show();
            }
            else if(e.CommandName == "ELIMINAR")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                string f_cobro = Convert.ToDateTime(row.Cells[3].Text).ToShortDateString();
                string id_cuenta = Session["ID_CUENTA"].ToString();
                decimal importe = -1 * Convert.ToDecimal(row.Cells[6].Text);

                Session["ID_CHEQUE_EMIT"] = row.Cells[0].Text;
                OBJCHEQUES.id_chequesem = Session["ID_CHEQUE_EMIT"].ToString();
                OBJCHEQUES.id_proveedor = "";
                OBJCHEQUES.f_giro = "01-01-2017";
                OBJCHEQUES.f_cobro = "01-01-2017";
                OBJCHEQUES.f_cobrado = "01-01-2017";
                OBJCHEQUES.n_cheque = 0;
                OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                OBJCHEQUES.importe = 0;
                OBJCHEQUES.estado = "EMITIDO";
                OBJCHEQUES.id_movimiento = "1002";
                OBJCHEQUES.observacion = "";
                string COND = "4";

                string res = OBJVENTA.NREGISTRARCHEQUE_EM2(OBJCHEQUES, COND);

                if (res == "ok")
                {
                    //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);



                    llenar_datos();

                    TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    limpiar();

                    txtID_CUENTA.Text = Session["ID_CUENTA"].ToString();
                    TXTCUENTA.Text = Session["CUENTA"].ToString();
                    LBLCHEQUERA2.Text = "SELECCIONE UNA CHEQUERA";
                    TXTCUENTA.Focus();
                }
                else
                {
                    //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            else if (e.CommandName == "EDITAR")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                
                Session["ID_CHEQUE_EMIT"] = row.Cells[0].Text;
                //CARGAR PROVEEDOR
                DataTable tb = OBJVENTA.NLISTAR_PROVEEDORES_CHEMIT(Session["ID_CHEQUE_EMIT"].ToString());
                txtPROO.Text = tb.Rows[0][0].ToString();
                Session["ID_PROVEEDOR"] = tb.Rows[0][0].ToString();
                TXTPROOVEEDOR.Text = tb.Rows[0][1].ToString();
                ///////////////////
                TXTFGIRO.Text = Convert.ToDateTime(row.Cells[2].Text).ToString("yyyy-MM-dd");
                TXTFCOBRO.Text = Convert.ToDateTime(row.Cells[3].Text).ToString("yyyy-MM-dd");
                try
                {
                    Session["FECHA_COBRADO"] = Convert.ToDateTime(row.Cells[4].Text).ToString("yyyy-MM-dd");
                }
                catch { Session["FECHA_COBRADO"] = ""; }
                Session["N_CHEQUE"] = Convert.ToInt32(row.Cells[5].Text.Trim());
                TXTNUMERO.Text = row.Cells[5].Text.Trim();
                TXTIMPORTE.Text = row.Cells[6].Text.Trim();
                Session["ESTADO"] = row.Cells[7].Text.Trim();
                Session["ID_MOVS"] = row.Cells[8].Text.Trim();
                TXTOBS.Text = HttpUtility.HtmlDecode(row.Cells[9].Text.Trim());
                Session["VARIABLE_ACTUALIZAR"] = "1";
                habilitar();
            }




        }

        protected void dgvMOV_CHE_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "QUITAR")
            {
                GridViewRow raw = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                dgvMOV_CHE.DataSource = "";
                dgvMOV_CHE.DataBind();

                dgvMOVIMIENTOS.DataSource = OBJVENTA.NLLENARGRILLA_MOVIMIENTOS_CHEMITIDOS(Session["ID_CUENTA"].ToString(), Convert.ToDecimal(Session["IMPORTE"].ToString()), Session["F_COBRO"].ToString());
                dgvMOVIMIENTOS.DataBind();

                mp2.Show();

            }
        }

        protected void dgvMOVIMIENTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AGREGAR")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                id_mov = row.Cells[1].Text;
                f_mov = row.Cells[2].Text;
                ope_mov = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim()); 
                des_mov = HttpUtility.HtmlDecode(row.Cells[4].Text.Trim());
                impo_mov = Convert.ToDecimal(row.Cells[5].Text);


                LLENAR_TABLA_SESSION_MOVS();

                DataTable df = (DataTable)Session["GRILLA_MOVS"];
                df.Clear();

                dgvMOVIMIENTOS.DataSource = "";
                dgvMOVIMIENTOS.DataBind();
                mp2.Show();
            }
        }

        protected void dgvBANCOS_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onClick", "ChangeColor('" + "dgvBANCOS','" + (e.Row.RowIndex + 1).ToString() + "')");
            }
        }

        protected void btnGRABARMOV_Click(object sender, EventArgs e)
        {
            if (dgvMOV_CHE.Rows.Count > 0)
            {
                OBJCHEQUES.id_chequesem = Session["ID_CHEQUE_EMIT"].ToString();
                OBJCHEQUES.id_proveedor = "";
                OBJCHEQUES.f_giro = "01-01-2017";
                OBJCHEQUES.f_cobro = "01-01-2017";
                OBJCHEQUES.f_cobrado = (dgvMOV_CHE.Rows[0].Cells[2].Text);
                OBJCHEQUES.n_cheque = 0;
                OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                OBJCHEQUES.importe = 0;
                OBJCHEQUES.estado = "EMITIDO";
                OBJCHEQUES.id_movimiento = dgvMOV_CHE.Rows[0].Cells[1].Text;
                OBJCHEQUES.observacion = "";
                string COND = "3";

                string res = OBJVENTA.NREGISTRARCHEQUE_EM2(OBJCHEQUES, COND);

                if (res == "ok")
                {
                    //Response.Write("<script>alert('CHEQUE REGISTRADO CORRECTAMENTE..')</script>");

                   ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    
                   

                    llenar_datos();

                    TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    limpiar();

                    txtID_CUENTA.Text = Session["ID_CUENTA"].ToString();
                    TXTCUENTA.Text = Session["CUENTA"].ToString();
                    LBLCHEQUERA2.Text = "SELECCIONE UNA CHEQUERA";
                    TXTCUENTA.Focus();
                }
                else
                {
                    //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            else if (dgvMOV_CHE.Rows.Count == 0)
            {
                OBJCHEQUES.id_chequesem = Session["ID_CHEQUE_EMIT"].ToString();
                OBJCHEQUES.id_proveedor = "";
                OBJCHEQUES.f_giro = "01-01-2017";
                OBJCHEQUES.f_cobro = "01-01-2017";
                OBJCHEQUES.f_cobrado = "";
                OBJCHEQUES.n_cheque = 0;
                OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                OBJCHEQUES.importe = 0;
                OBJCHEQUES.estado = "EMITIDO";
                OBJCHEQUES.id_movimiento = "1002";
                OBJCHEQUES.observacion = "";
                string COND = "3";

                string res = OBJVENTA.NREGISTRARCHEQUE_EM2(OBJCHEQUES, COND);

                if (res == "ok")
                {
                    //Response.Write("<script>alert('..')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();

                    TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    limpiar();

                    txtID_CUENTA.Text = Session["ID_CUENTA"].ToString();
                    TXTCUENTA.Text = Session["CUENTA"].ToString();
                    LBLCHEQUERA2.Text = "SELECCIONE UNA CHEQUERA";
                    TXTCUENTA.Focus();
                }
            }
        }

        protected void btnCANCELARMOV_Click(object sender, EventArgs e)
        {
            mp2.Dispose();
            mp2.Hide();
        }

        protected void btnMOSTRARTODOS_Click(object sender, EventArgs e)
        {
            /*-----VALIDACIONES DEL TEXTBOX BUSQUEDA-----*/
            string FECHA = "";
            int ENTERO = 0;
            decimal DECIMAL = 0;
            string STRIN = "";
            string TIPO = "";

            try
            {
                FECHA = Convert.ToDateTime(txtBUSQUEDA.Text).ToString("dd-MM-yyyy");
                TIPO = "1";
            }
            catch
            {
                try
                {
                    ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                    TIPO = "2";
                }
                catch
                {
                    try
                    {
                        DECIMAL = Convert.ToDecimal(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                        TIPO = "3";
                    }
                    catch
                    {
                        try
                        {
                            if (txtBUSQUEDA.Text.Trim().Length > 0)
                            {
                                STRIN = txtBUSQUEDA.Text.Trim();
                                TIPO = "4";

                            }

                            if (txtBUSQUEDA.Text.Trim() == "") { TIPO = "5"; }

                        }
                        catch
                        {
                            if (txtBUSQUEDA.Text.Trim() == "") { TIPO = "5"; }
                            
                        }
                    }
                }
            }

            DataTable tbl = OBJVENTA.NFILTRAR_TODOS(FECHA, ENTERO, DECIMAL, STRIN, TIPO);
            dgvBANCOS.DataSource = tbl;
            dgvBANCOS.DataBind();
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            inhabilitar();
            lblmensajeproveedor.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            habilitar();
            limpiar();
        }

        protected void BTNAGREGARDOCVTA_Click(object sender, ImageClickEventArgs e)
        {
            String cadena = "";
            string obsnom = HttpUtility.HtmlDecode(TXTOBS.Text.Trim());
            string obsq = "&#160;";
            bool obsbool = obsnom.Contains(obsq);
            int indexobs = 0;
            if (obsbool) { indexobs = obsnom.IndexOf(obsq); }
            /*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
            string michi = "#";
            bool ver = obsnom.Contains(michi);
            int indx = 0;
            if (ver) { indx = obsnom.IndexOf(michi); }

            if (indexobs > 0)
            { TXTOBS.Text = obsnom.Substring(0, indexobs); }
            if (indx > 0) { TXTOBS.Text = HttpUtility.HtmlDecode(obsnom.Substring(indx + 1)); cadena = obsnom.Substring(0, indx); }
            else if (indx == 0)
            {
                try
                {
                    if (obsnom.Substring(0, 1) == "#")
                    {
                        cadena = obsnom.Substring(1);
                    }
                    else
                    {
                        cadena = obsnom.Substring(0);

                    }
                }
                catch { }

                
            }


            int ocurrenciasguion = 0;
            ocurrenciasguion = cadena.Split(new String[] { "-" }, StringSplitOptions.None).Length - 1;

            int ocurrencias = 0;
            ocurrencias = cadena.Split(new String[] { "//" }, StringSplitOptions.None).Length - 1;
            int final = 14;
            int inicio = 0;
            if (ocurrencias == 0)
            {

                try
                {
                    if (obsnom.Substring(0, 1) == "#")
                    {
                        TXTOBS.Text = obsnom.Substring(1);
                    }
                    else
                    {
                        TXTOBS.Text = obsnom.Substring(0);
                    }
                }
                catch { }


            }
            //validar_largo de cadenas
            int largo = obsnom.Length;

            for (int o = 0; o < ocurrencias; o++)
            {
                final = 14;
                string parte = obsnom.Substring(inicio, final);
                int cuentaguion = 0;
                cuentaguion = parte.Split(new String[] { "-" }, StringSplitOptions.None).Length - 1;

                if (cuentaguion > 0)
                {
                    final = 20;
                    string CADE_LARGA = obsnom.Substring(inicio, final);
                    inicio = inicio + final;

                    string doc = CADE_LARGA.Substring(0, 2);
                    string serie = CADE_LARGA.Substring(3, 3);
                    string numero = "";
                    numero = CADE_LARGA.Substring(7, 11);


                    /*llenamos la tabla de session*/
                    DataTable dts = (DataTable)Session["GRILLA_DOCS"];

                    DataRow raw = dts.NewRow();
                    raw["DOC"] = doc;
                    raw["SERIE"] = serie;
                    raw["NUMERO"] = numero;

                    dts.Rows.Add(raw);
                    dts.AcceptChanges();
                    LLENAR_GRILLA2();
                }
                else
                {
                    final = 14;
                    string CADE_LARGA = obsnom.Substring(inicio, final);
                    inicio = inicio + final;

                    string doc = CADE_LARGA.Substring(0, 2);
                    string serie = CADE_LARGA.Substring(3, 3);
                    string numero = "";
                    numero = CADE_LARGA.Substring(7, 5);



                    /*llenamos la tabla de session*/
                    DataTable dts = (DataTable)Session["GRILLA_DOCS"];

                    DataRow raw = dts.NewRow();
                    raw["DOC"] = doc;
                    raw["SERIE"] = serie;
                    raw["NUMERO"] = numero;

                    dts.Rows.Add(raw);
                    dts.AcceptChanges();
                    LLENAR_GRILLA2();
                }
            }




            /*----------------------------------------------------------------------------------------*/
            
        }

        protected void cboFVBV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFVBV.SelectedValue == "BV")
            {
                chkMULTIPLE.Enabled = true;
                cboFVBV.Focus();
            }
            else if (cboFVBV.SelectedValue != "BV")
            {
                chkMULTIPLE.Checked = false;
                chkMULTIPLE.Enabled = false;
                txtnumero2.Visible = false;
                cboFVBV.Focus();
            }
            
        }

        protected void txtserie_TextChanged(object sender, EventArgs e)
        {
            string fmt = "000";
            txtserie.Text = Convert.ToUInt32(txtserie.Text).ToString(fmt);
            txtnumero1.Focus();
            
        }

        protected void txtnumero2_TextChanged(object sender, EventArgs e)
        {
            if (chkMULTIPLE.Checked == true)
            {
                string fmt = "00000";
                txtnumero2.Text = Convert.ToInt32(txtnumero2.Text).ToString(fmt);
                btnagregar.Focus();
            }
            else if (chkMULTIPLE.Checked == false)
            {

                txtnumero1.Text = "";
                btnagregar.Focus();

            }
            
        }

        protected void txtnumero_TextChanged(object sender, EventArgs e)
        {
            if (chkMULTIPLE.Checked == true)
            {
                string fmt = "00000";
                txtnumero1.Text = Convert.ToInt32(txtnumero1.Text).ToString(fmt);
                txtnumero2.Focus();
            }
            else if (chkMULTIPLE.Checked == false)
            {
                string fmt = "00000";
                txtnumero1.Text = Convert.ToUInt32(txtnumero1.Text).ToString(fmt);
                btnagregar.Focus();
            }
            
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            LLENAR_TABLA_SESSION2();
            
        }

        void LLENAR_TABLA_SESSION2()
        {
            DataTable dt = (DataTable)Session["GRILLA_DOCS"];

            try
            {
                if (cboFVBV.SelectedIndex != 0 && txtserie.Text != "" && txtnumero1.Text != "")
                {
                    if (chkMULTIPLE.Checked == false)
                    {
                        DataRow row = dt.NewRow();
                        row["DOC"] = cboFVBV.SelectedValue;
                        row["SERIE"] = txtserie.Text;
                        row["NUMERO"] = txtnumero1.Text;

                        dt.Rows.Add(row);
                        dt.AcceptChanges();

                        LLENAR_GRILLA2();

                        //aqui limpio la data de ingreso de precio y cantidad de cada bien
                        txtserie.Text = string.Empty;
                        txtnumero1.Text = string.Empty;
                        txtnumero2.Text = string.Empty;
                        cboFVBV.SelectedIndex = 0;
                        cboFVBV.Focus();
                    }
                    else if (chkMULTIPLE.Checked == true)
                    {
                        if (txtnumero2.Text != "")
                        {
                            DataRow row = dt.NewRow();
                            row["DOC"] = cboFVBV.SelectedValue;
                            row["SERIE"] = txtserie.Text;
                            row["NUMERO"] = txtnumero1.Text + "-" + txtnumero2.Text;

                            dt.Rows.Add(row);
                            dt.AcceptChanges();

                            LLENAR_GRILLA2();

                            //aqui limpio la data de ingreso de precio y cantidad de cada bien
                            txtserie.Text = string.Empty;
                            txtnumero1.Text = string.Empty;
                            txtnumero2.Text = string.Empty;
                            cboFVBV.SelectedIndex = 0;
                            cboFVBV.Focus();
                        }
                    }

                }
                else { Response.Write("<script>window.alert('DEBE LLENAR LOS SIGUIENTES DATOS: FT ó BV,SERIE Y NUMERO');</script>"); }

            }
            catch (Exception)
            {

                // Response.Write("<script>window.alert('EL BIEN YA ESTA EN LA LISTA');</script>");

            }
        }

        void LLENAR_GRILLA2()
        {
            DataTable dt = (DataTable)Session["GRILLA_DOCS"];
            dgvDATOS2.DataSource = dt;
            dgvDATOS2.DataBind();
        }

        protected void chkMULTIPLE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMULTIPLE.Checked == true)
            {
                txtnumero1.MaxLength = 5;
                txtnumero2.Visible = true;
                txtserie.Focus();

            }
            else if (chkMULTIPLE.Checked == false)
            {
                txtnumero1.MaxLength = 5;
                txtnumero2.Visible = false;
                // txtnumero.Width = 55;
                if (txtnumero1.Text != "") { txtnumero1.Text = txtnumero1.Text; } else { txtnumero1.Text = ""; }
                txtserie.Focus();

            }
           

        }

        

        protected void dgvDATOS2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ELIM")
            {
                GridViewRow raw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                DataTable dt = (DataTable)Session["GRILLA_DOCS"];

                try
                {
                    int index = raw.RowIndex;
                    dt.Rows[index].Delete();
                    dt.AcceptChanges();

                    LLENAR_GRILLA2();

                    //aqui limpio la data de ingreso de precio y cantidad de cada bien
                    txtserie.Text = string.Empty; ;
                    txtnumero1.Text = string.Empty;
                    cboFVBV.SelectedIndex = 0;
                    cboFVBV.Focus();
                }
                catch (Exception) { }


            }
            
        }

        protected void txtnumero1_TextChanged(object sender, EventArgs e)
        {
            if (chkMULTIPLE.Checked == true)
            {
                string fmt = "00000";
                txtnumero1.Text = Convert.ToInt32(txtnumero1.Text).ToString(fmt);
                txtnumero2.Focus();
            }
            else if (chkMULTIPLE.Checked == false)
            {
                string fmt = "00000";
                txtnumero1.Text = Convert.ToUInt32(txtnumero1.Text).ToString(fmt);
                btnagregar.Focus();
            }
            
        }

       

       
    }
}