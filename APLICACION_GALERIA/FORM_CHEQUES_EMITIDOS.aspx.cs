using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using CAPA_ENTIDAD;
using CAPA_DATOS;
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
                Session["ESTADO"] = "";
                Button2.Enabled = false;
                btnRegistrar.Enabled = false;
                btnCancelar.Enabled = false;
                BTNNUEVOPROVEE2.Enabled = false;
                
                llenar_combos();
                //dgvBANCOS.DataSource = "";
                //dgvBANCOS.DataBind();

                dgvMOV_CHE.DataSource = "";
                dgvMOV_CHE.DataBind();

                txtID_CUENTA.Text = "";
                TXTCUENTA2.Text = "";

                Session["CHECK_TODOS"] = "";

                Master.label.Text = "EMISION DE CHEQUES";// seteamos el valor
                Response.Write(Master.label.Text);

                Master.label2.Text = " - " + Session["NOM_EMPRESA"].ToString();
                Response.Write(Master.label2.Text);

                TXTCUENTA2.Focus();
                inhabilitar();
                TXTFECHAINI.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFECHAFIN.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFECHAPROV.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Session["VARIABLE_ACTUALIZAR"] = "";
                TXTRUC_DNI_PROV.Enabled = false;

                LBLCHEQUERA21.Text = "SELECCIONE UNA CUENTA";
                LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;
            }
        }

        #region OBJETOS
        N_LOGUEO OBJVENTA = new N_LOGUEO();
        E_CH_EMITIDOS OBJCHEQUES = new E_CH_EMITIDOS();
        E_PROVEEDOR OBJPROV = new E_PROVEEDOR();
        D_LOGIN OBJDATOS = new D_LOGIN();
        #endregion

        void inhabilitar()
        {
            TXTPROOVEEDOR.Enabled = false;
            TXTFGIRO.Enabled = false;
            TXTFCOBRO.Enabled = false;
            TXTNUMERO.Enabled = false;
            TXTIMPORTE.Enabled = false;
            
            TXTOBS.Enabled = false;
            txtserie.Enabled = false;
            txtnumero1.Enabled = false;
            txtnumero2.Enabled = false;
            cboFVBV.Enabled = false;
            btnagregar.Enabled = false;
            TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        void habilitar()
        {
            TXTPROOVEEDOR.Enabled = true;
            TXTFGIRO.Enabled = true;
            TXTFCOBRO.Enabled = true;
            TXTNUMERO.Enabled = true;
            TXTIMPORTE.Enabled = true;
            
            TXTOBS.Enabled = false;
            txtserie.Enabled = true;
            txtnumero1.Enabled = true;
            txtnumero2.Enabled = true;
            cboFVBV.Enabled = true;
            btnagregar.Enabled = true;
            TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        void llenar_combos()
        {
            var tb_pais = OBJVENTA.NLISTAR_PAIS("PP");
            CBOPAIS.DataSource = tb_pais;
            CBOPAIS.DataTextField = "UBIPAN";                           
            CBOPAIS.DataValueField = "UBIPAI";
            CBOPAIS.DataBind();

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
                        DataTable dtr = OBJVENTA.NCLI_VALIDAR(txtPROO.Text);
                        if (dtr.Rows.Count == 1)
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
                                lblmensajeproveedor.Visible = false;
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



                                    TXTFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                    TXTFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                    limpiar();
                                    inhabilitar();
                                    txtID_CUENTA.Text = Session["ID_CUENTA"].ToString();
                                    TXTCUENTA2.Text = Session["CUENTA"].ToString();


                                    DataTable dt3 = (DataTable)Session["GRILLA_DOCS"];
                                    dt3.Clear();
                                    dgvDATOS2.DataSource = "";
                                    dgvDATOS2.DataBind();
                                    btnRegistrar.Enabled = false;
                                    btnCancelar.Enabled = false;

                                    llenar_datos();
                                    TXTCUENTA2.Focus();
                                }
                                else
                                {
                                    //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(dt1.Rows[0][1].ToString()) < Convert.ToInt32(TXTNUMERO.Text))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMAX();", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal01();", true);//mensaje de numero repetido
                                }
                                
                            }



                        }
                        else
                        {
                            lblmensajeproveedor.Visible = true;
                            lblmensajeproveedor.Text = "(*)Debe ingresar un proveedor valido";
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
                            string obf = cadena;
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
                                TXTCUENTA2.Text = Session["CUENTA"].ToString();
                            btnRegistrar.Enabled = false;
                            btnCancelar.Enabled = false;

                            DataTable dt3 = (DataTable)Session["GRILLA_DOCS"];
                            dt3.Clear();
                            dgvDATOS2.DataSource = "";
                            dgvDATOS2.DataBind();

                            TXTCUENTA2.Focus();
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
            LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
            mp1.Dispose();
            mp1.Hide(); 
        }

        protected void btnAceptarPopUp_Click(object sender, EventArgs e)
        {
            
        }


        void llenar_labels_cabecera()
        {
            try
            {/*---------------------------------------------------------------------------*/
                DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(Session["ID_CUENTA"].ToString());
                string mone = dt.Rows[0][0].ToString();

                LBLBANCOCTA.Text = dt.Rows[0][1].ToString();
                if (mone == "S")
                {

                    LBLSALDOCONT.Text = "S/." + Convert.ToDecimal(dt.Rows[0][2].ToString()).ToString("#,###0.00");
                    LBLSALDODIP.Text = "S/." + Convert.ToDecimal(dt.Rows[0][3].ToString()).ToString("#,###0.00");
                    //LBLTOTALGIRADO.Text = "S/." + Convert.ToDecimal(dt.Rows[0][4].ToString()).ToString("#,###0.00");
                }
                else if (mone == "D")
                {

                    LBLSALDOCONT.Text = "US$  " + Convert.ToDecimal(dt.Rows[0][2].ToString()).ToString("#,###0.00");
                    LBLSALDODIP.Text = "US$  " + Convert.ToDecimal(dt.Rows[0][3].ToString()).ToString("#,###0.00");
                    //LBLTOTALGIRADO.Text = "$" + Convert.ToDecimal(dt.Rows[0][4].ToString()).ToString("#,###0.00");
                }

                //LBLNCUENTA.Text = dt.Rows[0][4].ToString();
                /*---------------------------------------------------------------------------*/
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true);
            }
        }


        protected void BTNCHEQUERA_Click(object sender, EventArgs e)
        {

            if (txtID_CUENTA.Text != string.Empty && TXTCUENTA2.Text.Length > 12)
            {
                string id_cuenta = txtID_CUENTA.Text;
                Session["ID_CUENTA"] = txtID_CUENTA.Text;
                Session["CUENTA"] = TXTCUENTA2.Text;
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
                llenar_labels_cabecera();
                mp1.Show();
            }
            else
            {
                LBLCHEQUERA21.Text = "SELECCIONE UNA CUENTA";
                LBLBANCOCTA.Text = "-";
                LBLSALDOCONT.Text = "-";
                LBLSALDODIP.Text = "-";

            }

            

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
                /*-----------ESCOJER CHEQUERA----------*/
                if (dgvDATOS.Rows.Count == 1)
                {

                    LBLCHEQUERA21.Text = "CHEQUERA SELECCIONADA";
                    LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;


                    txtIDCHEQUERA.Text = dgvDATOS.Rows[0].Cells[1].Text;
                    Session["ID_CHEQUERA"] = dgvDATOS.Rows[0].Cells[1].Text;
                    TXTNUMERO.Text = dgvDATOS.Rows[0].Cells[2].Text;
                    llenar_datos();

                    //CARGAR LBL DE TOTALES GIRADOS
                    DataTable dt3 = OBJVENTA.NTOTALGIRADO(Session["ID_CHEQUERA"].ToString());
                    if (LBLSALDOCONT.Text.Substring(0, 1) == "S")
                    {
                        try
                        {
                            LBLTOTALGIRADO.Text = "S/." + Convert.ToDecimal(dt3.Rows[0][0].ToString()).ToString("#,###0.00");
                        }
                        catch
                        {

                        }

                    }
                    else if (LBLSALDOCONT.Text.Substring(0, 1) == "U")
                    {
                        try
                        {
                            LBLTOTALGIRADO.Text = "US$ " + Convert.ToDecimal(dt3.Rows[0][0].ToString()).ToString("#,###0.00");
                        }
                        catch
                        {

                        }

                    }

                    Button2.Enabled = true;
                   

                    try
                    {
                        if (Session["ID_CHEQUERA"].ToString() != "" || Session["ID_CHEQUERA"].ToString() != null)
                        {
                            DataTable dt1 = OBJVENTA.NLISTAR_INI_FIN(Session["ID_CHEQUERA"].ToString());
                            LBLRANGOINI2.Text = dt1.Rows[0][0].ToString();
                            LBLRANGOFIN2.Text = dt1.Rows[0][1].ToString();
                        }
                        else
                        {
                            LBLRANGOINI2.Text = "-";
                            LBLRANGOFIN2.Text = "-";
                        }
                    }
                    catch
                    {
                        LBLRANGOINI2.Text = "-";
                        LBLRANGOFIN2.Text = "-";
                    }

                }
                else
                {
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
                    LBLRANGOINI2.Text = "-";
                    LBLRANGOFIN2.Text = "-";
                    LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;
                    txtIDCHEQUERA.Text = "";
                    inhabilitar();

                }

                limpiar();
                inhabilitar();
                lblmensajeproveedor.Visible = false;


                DataTable df = (DataTable)Session["GRILLA_CHEQUERAS"];
                df.Clear();
                mp1.Hide();
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

                /*-----------ESCOJER CHEQUERA----------*/
                if (dgvDATOS.Rows.Count == 1)
                {

                    LBLCHEQUERA21.Text = "CHEQUERA SELECCIONADA";
                    LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;


                    txtIDCHEQUERA.Text = dgvDATOS.Rows[0].Cells[1].Text;
                    Session["ID_CHEQUERA"] = dgvDATOS.Rows[0].Cells[1].Text;
                    TXTNUMERO.Text = dgvDATOS.Rows[0].Cells[2].Text;
                    llenar_datos();

                    //CARGAR LBL DE TOTALES GIRADOS
                    DataTable dt3 = OBJVENTA.NTOTALGIRADO(Session["ID_CHEQUERA"].ToString());
                    if (LBLSALDOCONT.Text.Substring(0, 1) == "S")
                    {
                        try
                        {
                            LBLTOTALGIRADO.Text = "S/." + Convert.ToDecimal(dt3.Rows[0][0].ToString()).ToString("#,###0.00");
                        }
                        catch
                        {

                        }

                    }
                    else if (LBLSALDOCONT.Text.Substring(0, 1) == "U")
                    {
                        try
                        {
                            LBLTOTALGIRADO.Text = "US$ " + Convert.ToDecimal(dt3.Rows[0][0].ToString()).ToString("#,###0.00");
                        }
                        catch
                        {

                        }

                    }

                    Button2.Enabled = true;


                    try
                    {
                        if (Session["ID_CHEQUERA"].ToString() != "" || Session["ID_CHEQUERA"].ToString() != null)
                        {
                            DataTable dt1 = OBJVENTA.NLISTAR_INI_FIN(Session["ID_CHEQUERA"].ToString());
                            LBLRANGOINI2.Text = dt1.Rows[0][0].ToString();
                            LBLRANGOFIN2.Text = dt1.Rows[0][1].ToString();
                        }
                        else
                        {
                            LBLRANGOINI2.Text = "-";
                            LBLRANGOFIN2.Text = "-";
                        }
                    }
                    catch
                    {
                        LBLRANGOINI2.Text = "-";
                        LBLRANGOFIN2.Text = "-";
                    }

                }
                else
                {
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
                    LBLRANGOINI2.Text = "-";
                    LBLRANGOFIN2.Text = "-";
                    LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;
                    txtIDCHEQUERA.Text = "";
                    inhabilitar();

                }
                limpiar();
                inhabilitar();
                lblmensajeproveedor.Visible = false;


                DataTable df = (DataTable)Session["GRILLA_CHEQUERAS"];
                df.Clear();
                mp1.Hide();
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

                    if (HttpUtility.HtmlDecode(dgvBANCOS.Rows[i].Cells[4].Text.Trim()) == "01/01/1900")
                    {
                        dgvBANCOS.Rows[i].Cells[4].Text = "";
                    }
                    

                    dgvBANCOS.Rows[i].Cells[10].BackColor = System.Drawing.Color.Gold;
                }

                TXTPROOVEEDOR.Focus();

            }
            catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal4();", true); }
        }


        void limpiar()
        {
            TXTCUENTA2.Text = "";
            txtIDCHEQUERA.Text = "";
            //txtID_CUENTA.Text = "";
            TXTIMPORTE.Text = "";
            TXTNUMERO.Text = "";
            TXTOBS.Text = "";
            txtPROO.Text = "";
            TXTPROOVEEDOR.Text = "";
            TXTFECHAINI.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTFECHAFIN.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                Session["PROVEEDOR"] = HttpUtility.HtmlDecode(rew.Cells[1].Text.Trim());
                Session["ESTADO"] = HttpUtility.HtmlDecode(rew.Cells[7].Text.Trim());



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
            else if (e.CommandName == "IMAGEN")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                /*///////////////////REPORTE DE CHEQUE///////////////////*/
                
                string ID_CHEQUE = row.Cells[0].Text;
                string TIPO = "";
                String url = "";
                
                if (Convert.ToDateTime(row.Cells[2].Text) == (Convert.ToDateTime(row.Cells[3].Text)))
                {
                    TIPO = "CLASICO";
                    object[] args = new object[] { ID_CHEQUE, TIPO };
                    url = String.Format("REPORTES/RPT_CHEQUE_CLASICO.aspx?ID_CHEQUE={0}&TIPO={1}", args);
                    
                }
                else
                {
                    TIPO = "DIFERIDO";
                    object[] args = new object[] { ID_CHEQUE, TIPO };
                    url = String.Format("REPORTES/RPT_CHEQUE_DIFERIDO.aspx?ID_CHEQUE={0}&TIPO={1}", args);

                }

                /*-------------------------------------------------------------------*/


                string s = "window.open('" + url + "', 'popup_window', 'width=900,height=500,left=10%,top=10%,resizable=yes');"; //con esto muestro la venta en una nueva ventana 
                //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true); 
                //se usa scriptmanager porque esta dentro de un update panel
                ScriptManager.RegisterStartupScript(UPDGRILLA, UPDGRILLA.GetType(), "script", s, true);


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
                    TXTCUENTA2.Text = Session["CUENTA"].ToString();
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
                    TXTCUENTA2.Focus();
                }
                else
                {
                    //Response.Write("<script>alert('ERROR CHEQUE NO REGISTRADO')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            else if (e.CommandName == "EDITAR")
            {
                DataTable dt3 = (DataTable)Session["GRILLA_DOCS"];
                dt3.Clear();
                dgvDATOS2.DataSource = "";
                dgvDATOS2.DataBind();
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
                btnRegistrar.Enabled = true;
                btnCancelar.Enabled = true;
                BTNNUEVOPROVEE2.Enabled = true;

                /*----------------LLENAMOS LA GRILLA DE DOCUMENTOS DE VENTA---------------------------*/
                String cadena = "";
                string obsnom = HttpUtility.HtmlDecode(row.Cells[9].Text.Trim());
                string obsq = "&#160;";
                bool obsbool = obsnom.Contains(obsq);
                int indexobs = 0;
                if (obsbool) { indexobs = obsnom.IndexOf(obsq); }

                if(row.Cells[12].Text == "&#160;") { TXTOBS.Text = row.Cells[12].Text.Replace("&#160;", ""); }
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
                OBJCHEQUES.id_proveedor = Session["PROVEEDOR"].ToString();
                OBJCHEQUES.f_giro = "01-01-2017";
                OBJCHEQUES.f_cobro = "01-01-2017";
                OBJCHEQUES.f_cobrado = (dgvMOV_CHE.Rows[0].Cells[2].Text);
                OBJCHEQUES.n_cheque = 0;
                OBJCHEQUES.id_chequera = Session["ID_CHEQUERA"].ToString();
                OBJCHEQUES.importe = 0;
                OBJCHEQUES.estado = Session["ESTADO"].ToString();
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
                    TXTCUENTA2.Text = Session["CUENTA"].ToString();
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
                    TXTCUENTA2.Focus();
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
                OBJCHEQUES.estado = Session["ESTADO"].ToString();
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
                    TXTCUENTA2.Text = Session["CUENTA"].ToString();
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
                    TXTCUENTA2.Focus();
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
            filtrar_todos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            inhabilitar();
            lblmensajeproveedor.Visible = false;
            dgvDATOS2.DataSource = "";
            dgvDATOS2.DataBind();

            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = false;
            BTNNUEVOPROVEE2.Enabled = false;
            TXTFECHAINI.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTFECHAFIN.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            habilitar();
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
            BTNNUEVOPROVEE2.Enabled = true;
            limpiar();
            TXTFECHAINI.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTFECHAFIN.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable dt2 = OBJDATOS.DCORRELATIVO(Session["ID_CHEQUERA"].ToString());

            TXTNUMERO.Text = dt2.Rows[0][0].ToString();
        }

        protected void BTNAGREGARDOCVTA_Click(object sender, ImageClickEventArgs e)
        {
            
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

        protected void BTNNUEVOPROVEE2_Click(object sender, EventArgs e)
        {
            mp3.Show();
        }

        protected void BTNGRABARPROV_Click(object sender, EventArgs e)
        {
            int cond = 1;

            OBJPROV.id_proveedor = "";
            OBJPROV.tipo_pro = CBOTIPOPROV.SelectedValue;
            OBJPROV.origen_pro = CBONACIOPROV.SelectedValue;
            OBJPROV.descripcion_pro = TXTDESCRIP_PROV.Text;
            OBJPROV.ruc_dni_pro = TXTRUC_DNI_PROV.Text;
            OBJPROV.direc_pro = TXTDIR_PROV.Text;
            OBJPROV.tele1_pro = TXTTELEFONOPROV.Text;
            OBJPROV.tele2_pro = "";
            OBJPROV.movil_pro = TVTMOVILPRO.Text;
            try
            {
                OBJPROV.fecha_nac_pro = Convert.ToDateTime(TXTFECHAPROV.Text);
            }
            catch
            {
                OBJPROV.fecha_nac_pro = Convert.ToDateTime("01-01-1900");
            }
            
            OBJPROV.email_pro = TXTEMAILPROV.Text;
            OBJPROV.wesite_pro = TXTWEBSITEPROV.Text;
            OBJPROV.estado_pro = "1";
            if (CBONACIOPROV.SelectedValue == "PE")
            {
                OBJPROV.ubigeo_pro = "000000";
            }
            else
            {
                OBJPROV.ubigeo_pro = CBODISTRITO.SelectedValue;
            }
            

            string res = OBJVENTA.NREGISTRAR_NUEVO_PROVEEDOR(OBJPROV,cond);
            if (res == "ok")
            {
                
                LIMPIAR_CONTROLES_MANT_PROV();
                CBOPAIS_SelectedIndexChanged(sender, e);
                CBODEPARTAMENTO_SelectedIndexChanged(sender, e);
                CBOPROVINCIA_SelectedIndexChanged(sender, e);
                CBODISTRITO_SelectedIndexChanged(sender, e);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                mp3.Dispose();
                mp3.Hide();
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal5();", true); }
        }

        protected void BTNCANCELARPROV_Click(object sender, EventArgs e)
        {
            LIMPIAR_CONTROLES_MANT_PROV();
            CBOPAIS_SelectedIndexChanged(sender, e);
            CBODEPARTAMENTO_SelectedIndexChanged(sender, e);
            CBOPROVINCIA_SelectedIndexChanged(sender, e);
            CBODISTRITO_SelectedIndexChanged(sender, e);
            mp3.Dispose();
            mp3.Hide();
        }

        void LIMPIAR_CONTROLES_MANT_PROV()
        {
            CBOTIPOPROV.SelectedIndex = 0;
            CBONACIOPROV.SelectedIndex = 0;
            TXTDESCRIP_PROV.Text = "";
            TXTDIR_PROV.Text = "";
            
            TXTRUC_DNI_PROV.Text = "";
            TXTTELEFONOPROV.Text = "";
            TVTMOVILPRO.Text = "";
            TXTFECHAPROV.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TXTEMAILPROV.Text = "";
            TXTWEBSITEPROV.Text = "";
        }

        protected void CBOTIPOPROV_SelectedIndexChanged(object sender, EventArgs e)
        {
            TXTRUC_DNI_PROV.Text = "";
            if (CBOTIPOPROV.SelectedValue == "PN")
            {
                LBLRUC_DNI.Text = "N° DNI(*) : ";
                LBLRUC_DNI.ForeColor = Color.Red;
                TXTRUC_DNI_PROV.Enabled = true;
                TXTRUC_DNI_PROV.MaxLength = 8;
            }
            else if (CBOTIPOPROV.SelectedValue == "PJ")
            {
                LBLRUC_DNI.Text = "N° RUC(*): ";
                LBLRUC_DNI.ForeColor = Color.Red;
                TXTRUC_DNI_PROV.Enabled = true;
                TXTRUC_DNI_PROV.MaxLength = 11;
            }
            else if (CBOTIPOPROV.SelectedValue == "OTRO")
            {
                LBLRUC_DNI.Text = "N° DOC(*):";
                LBLRUC_DNI.ForeColor = Color.Black;
                TXTRUC_DNI_PROV.Enabled = false;

            }
            mp3.Show();
        }

        protected void CBOPAIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pais = CBOPAIS.SelectedValue.ToString();
            var tb_dep = OBJVENTA.NLISTAR_DEPARTAMENTO(pais);
            CBODEPARTAMENTO.DataSource = tb_dep;
            CBODEPARTAMENTO.DataTextField = "UBIDEN";
            CBODEPARTAMENTO.DataValueField = "UBIDEP";
            CBODEPARTAMENTO.DataBind();

            CBODEPARTAMENTO_SelectedIndexChanged(sender, e);
            CBOPROVINCIA_SelectedIndexChanged(sender, e);
            CBODISTRITO_SelectedIndexChanged(sender, e);

            mp3.Show();
        }

        protected void CBODEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dept = CBODEPARTAMENTO.SelectedValue.ToString();
            var tb_dep = OBJVENTA.NLISTAR_PROVINCIA(dept);
            CBOPROVINCIA.DataSource = tb_dep;
            CBOPROVINCIA.DataTextField = "UBIPRN";
            CBOPROVINCIA.DataValueField = "UBIPRV";
            CBOPROVINCIA.DataBind();

            CBOPROVINCIA_SelectedIndexChanged(sender, e);
            CBODISTRITO_SelectedIndexChanged(sender, e);
            mp3.Show();
        }

        protected void CBOPROVINCIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prov = CBOPROVINCIA.SelectedValue.ToString();
            var tb_dep = OBJVENTA.NLISTAR_DISTRITO(prov);
            CBODISTRITO.DataSource = tb_dep;
            CBODISTRITO.DataTextField = "UBIDSN";
            CBODISTRITO.DataValueField = "UBIDST";
            CBODISTRITO.DataBind();

           mp3.Show();
        }

        protected void CBODISTRITO_SelectedIndexChanged(object sender, EventArgs e)
        {
            mp3.Show();
        }

        protected void CBONACIOPROV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBONACIOPROV.SelectedValue == "PN")
            {
                CBODEPARTAMENTO.Enabled = true;
                CBOPROVINCIA.Enabled = true;
                CBODISTRITO.Enabled = true;

                DataTable tb_pais = OBJVENTA.NLISTAR_PAIS("PN");

                CBOPAIS.DataSource = tb_pais;
                CBOPAIS.DataTextField = "UBIPAN";
                CBOPAIS.DataValueField = "UBIPAI";
                CBOPAIS.DataBind();

                CBOPAIS.Enabled = true;

                var tb_dep = OBJVENTA.NLISTAR_DEPARTAMENTO("001");
                CBODEPARTAMENTO.DataSource = tb_dep;
                CBODEPARTAMENTO.DataTextField = "UBIDEN";
                CBODEPARTAMENTO.DataValueField = "UBIDEP";
                CBODEPARTAMENTO.DataBind();

                CBODEPARTAMENTO_SelectedIndexChanged(sender, e);
                CBOPROVINCIA_SelectedIndexChanged(sender, e);
                CBODISTRITO_SelectedIndexChanged(sender, e);
            }
            else if (CBONACIOPROV.SelectedValue == "PE")
            {
                CBODEPARTAMENTO.Enabled = false;
                CBOPROVINCIA.Enabled = false;
                CBODISTRITO.Enabled = false;

                DataTable tb_pais = OBJVENTA.NLISTAR_PAIS("PE");
                
                CBOPAIS.DataSource = tb_pais;
                CBOPAIS.DataTextField = "UBIPAN";
                CBOPAIS.DataValueField = "UBIPAI";
                CBOPAIS.DataBind();
                               
                CBOPAIS.SelectedIndex = 0;
                CBOPAIS.Enabled = true;


                CBODEPARTAMENTO.SelectedIndex = 0;
                CBOPROVINCIA.SelectedIndex = 0;
                CBODISTRITO.SelectedIndex = 0;

                CBODEPARTAMENTO_SelectedIndexChanged(sender, e);
                CBOPROVINCIA_SelectedIndexChanged(sender, e);
                CBODISTRITO_SelectedIndexChanged(sender, e);


            }
            else if (CBONACIOPROV.SelectedValue == "OTRO")
            {
                CBOPAIS.SelectedIndex = 0;
                CBOPAIS.Enabled = false;
                
                CBODEPARTAMENTO.SelectedIndex = 0;
                CBOPROVINCIA.SelectedIndex = 0;
                CBODISTRITO.SelectedIndex = 0;

                CBODEPARTAMENTO_SelectedIndexChanged(sender, e);
                CBOPROVINCIA_SelectedIndexChanged(sender, e);
                CBODISTRITO_SelectedIndexChanged(sender, e);

                CBODEPARTAMENTO.Enabled = false;
                CBOPROVINCIA.Enabled = false;
                CBODISTRITO.Enabled = false;
            }
            mp3.Show();
        }

        protected void chkMOSTRAR_TODOS_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkMOSTRAR_TODOS.Checked == true)
            {
                Session["CHECK_TODOS"] = "";
                Session["CUENTA"] = "";
                Session["ID_CUENTA"] = "";
                ScriptManager.RegisterStartupScript(UPDGRILLA, UPDGRILLA.GetType(), "Pop", "check();", true);
                LBLCHEQUERA21.Text = "MODO CONSULTA";
                LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;
                limpiar();
                inhabilitar();
                limpiarlabels();
                BTNCHEQUERA.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsKeys", "javascript:limpiarcuenta();", true);
                Session["CHECK_TODOS"] = "1";
                txtID_CUENTA.Text = "";
            }
            else if(chkMOSTRAR_TODOS.Checked == false)
            {
                Session["CHECK_TODOS"] = "";
                Session["CUENTA"] = "";
                Session["ID_CUENTA"] = "";
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsKeys", "javascript:reactivarcuenta();", true);
                ScriptManager.RegisterStartupScript(UPDGRILLA, UPDGRILLA.GetType(), "Pop", "check2();", true);

                if (Session["CUENTA"].ToString() != "")
                {
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CHEQUERA";
                    LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;
                }
                else if (Session["CUENTA"].ToString() == "")
                {
                    LBLCHEQUERA21.Text = "SELECCIONE UNA CUENTA";
                    LBLCHEQUERA21.ForeColor = System.Drawing.Color.Gold;
                }

                limpiar();
                inhabilitar();
                

                DataTable DT = new DataTable();
                dgvBANCOS.DataSource = DT;
                dgvBANCOS.DataBind();

                BTNCHEQUERA.Enabled = true;
            }
        }

        protected void btnPOSTBACK_Click(object sender, EventArgs e)
        {

        }

        void limpiarlabels()
        {
            LBLRANGOINI2.Text = "";
            LBLRANGOFIN2.Text = "";
            LBLBANCOCTA.Text = "";
            LBLSALDOCONT.Text = "";
            LBLSALDODIP.Text = "";
            LBLTOTALGIRADO.Text = "";
        }

        void filtrar_todos()
        {
            /*-----VALIDACIONES DEL TEXTBOX BUSQUEDA-----*/
            string FECHA_INI = "";
            string FECHA_FIN = "";
            int ENTERO = 0;
            decimal DECIMAL_MIN = 0;
            decimal DECIMAL_MAX = 0;
            string STRIN = "";
            string TIPO = "";
            string DATO_BUSQ = "";

            if ((TXTFECHAINI.Text != null && TXTFECHAFIN.Text != null) && (TXTFECHAINI.Text != "" && TXTFECHAFIN.Text != ""))
            {
                if ((TXTDECIMIN.Text != null && TXTDECIMAX.Text != null) && (TXTDECIMIN.Text != "" && TXTDECIMAX.Text != ""))
                {
                    if (txtBUSQUEDA.Text != "")
                    {
                        try
                        {
                            ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                            DATO_BUSQ = "E";
                        }
                        catch
                        {
                            STRIN = txtBUSQUEDA.Text.Trim();
                            DATO_BUSQ = "S";
                        }

                        if (DATO_BUSQ == "S")
                        {
                            FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                            FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                            DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                            DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                            TIPO = "7";
                        }
                        else if (DATO_BUSQ == "E")
                        {
                            FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                            FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                            DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                            DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                            TIPO = "8";
                        }

                    }
                    else if (txtBUSQUEDA.Text == "")
                    {
                        FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                        FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                        DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                        DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                        TIPO = "6";
                    }
                }
                else if (TXTDECIMIN.Text == "" && TXTDECIMAX.Text == "")
                {
                    if (txtBUSQUEDA.Text != "")
                    {
                        try
                        {
                            ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                            DATO_BUSQ = "E";
                        }
                        catch
                        {
                            STRIN = txtBUSQUEDA.Text.Trim();
                            DATO_BUSQ = "S";
                        }

                        if (DATO_BUSQ == "S")
                        {
                            FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                            FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                            TIPO = "10"; //CREAR UN FILTRO DE FECHAS Y STRING O ENTERO SOLO
                        }
                        else if (DATO_BUSQ == "E")
                        {
                            FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                            FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                            TIPO = "9"; //CREAR UN FILTRO DE FECHAS Y STRING O ENTERO SOLO
                        }


                    }
                    else if (txtBUSQUEDA.Text == "")
                    {
                        FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                        FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                        TIPO = "1"; //CREAR UN FILTRO DE FECHAS SOLO - LISTO
                    }
                }
            }
            else if (TXTFECHAINI.Text == "" && TXTFECHAFIN.Text == "")
            {
                if ((TXTDECIMIN.Text != null && TXTDECIMAX.Text != null) && (TXTDECIMIN.Text != "" && TXTDECIMAX.Text != ""))
                {
                    if (txtBUSQUEDA.Text != "")
                    {
                        try
                        {
                            ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                            DATO_BUSQ = "E";
                        }
                        catch
                        {
                            STRIN = txtBUSQUEDA.Text.Trim();
                            DATO_BUSQ = "S";
                        }

                        if (DATO_BUSQ == "S")
                        {
                            DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                            DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                            TIPO = "11"; //CREAR UN FILTRO DE RANGO DE IMPORTES Y STRING 
                        }
                        else if (DATO_BUSQ == "E")
                        {
                            DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                            DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                            TIPO = "12"; //CREAR UN FILTRO DE RANGO DE IMPORTES Y  ENTERO 
                        }


                    }
                    else if (txtBUSQUEDA.Text == "")
                    {

                        DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                        DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                        TIPO = "3"; //CREAR UN FILTRO DE RANGO DE IMPORTES SOLO
                    }
                }
                else if (TXTDECIMIN.Text == "" && TXTDECIMAX.Text == "")
                {
                    if (txtBUSQUEDA.Text != "")
                    {
                        try
                        {
                            ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                            TIPO = "2"; //CREAR UN FILTRO ENTERO SOLO
                        }
                        catch
                        {
                            STRIN = txtBUSQUEDA.Text.Trim();
                            TIPO = "4"; //CREAR UN FILTRO STRING SOLO
                        }


                    }
                    else if (txtBUSQUEDA.Text == "")
                    {
                        Response.Write("<script>alert('DEBE LLENAR LOS CAMPOS PARA LA BÚSQUEDA');</script>");
                    }
                }
            }



            DataTable tbl = OBJVENTA.NFILTRAR_TODOS(FECHA_INI, FECHA_FIN, ENTERO, DECIMAL_MIN, DECIMAL_MAX, STRIN, TIPO);
            dgvBANCOS.DataSource = tbl;
            dgvBANCOS.DataBind();

            for (int i = 0; i < dgvBANCOS.Rows.Count; i++)
            {

                string p2 = dgvBANCOS.Rows[i].Cells[8].Text;

                if (dgvBANCOS.Rows[i].Cells[8].Text == "1002")
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "";
                }

                if (HttpUtility.HtmlDecode(dgvBANCOS.Rows[i].Cells[4].Text.Trim()) == "01/01/1900")
                {
                    dgvBANCOS.Rows[i].Cells[4].Text = "";
                }


                dgvBANCOS.Rows[i].Cells[10].BackColor = System.Drawing.Color.Gold;
            }

        }

        protected void dgvBANCOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvBANCOS.PageIndex = e.NewPageIndex;

            if (Session["CHECK_TODOS"].ToString() == "")
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

                for (int i = 0; i < dgvBANCOS.Rows.Count; i++)
                {

                    string p2 = dgvBANCOS.Rows[i].Cells[8].Text;

                    if (dgvBANCOS.Rows[i].Cells[8].Text == "1002")
                    {
                        dgvBANCOS.Rows[i].Cells[8].Text = "";
                    }

                    if (HttpUtility.HtmlDecode(dgvBANCOS.Rows[i].Cells[4].Text.Trim()) == "01/01/1900")
                    {
                        dgvBANCOS.Rows[i].Cells[4].Text = "";
                    }


                    dgvBANCOS.Rows[i].Cells[10].BackColor = System.Drawing.Color.Gold;
                }
            }
            else if (Session["CHECK_TODOS"].ToString() == "1")
            {
                /*-----VALIDACIONES DEL TEXTBOX BUSQUEDA-----*/
                string FECHA_INI = "";
                string FECHA_FIN = "";
                int ENTERO = 0;
                decimal DECIMAL_MIN = 0;
                decimal DECIMAL_MAX = 0;
                string STRIN = "";
                string TIPO = "";
                string DATO_BUSQ = "";

                if ((TXTFECHAINI.Text != null && TXTFECHAFIN.Text != null) && (TXTFECHAINI.Text != "" && TXTFECHAFIN.Text != ""))
                {
                    if ((TXTDECIMIN.Text != null && TXTDECIMAX.Text != null) && (TXTDECIMIN.Text != "" && TXTDECIMAX.Text != ""))
                    {
                        if (txtBUSQUEDA.Text != "")
                        {
                            try
                            {
                                ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                                DATO_BUSQ = "E";
                            }
                            catch
                            {
                                STRIN = txtBUSQUEDA.Text.Trim();
                                DATO_BUSQ = "S";
                            }

                            if (DATO_BUSQ == "S")
                            {
                                FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                                FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                                DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                                DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                                TIPO = "7";
                            }
                            else if (DATO_BUSQ == "E")
                            {
                                FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                                FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                                DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                                DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                                TIPO = "8";
                            }

                        }
                        else if (txtBUSQUEDA.Text == "")
                        {
                            FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                            FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                            DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                            DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                            TIPO = "6";
                        }
                    }
                    else if (TXTDECIMIN.Text == "" && TXTDECIMAX.Text == "")
                    {
                        if (txtBUSQUEDA.Text != "")
                        {
                            try
                            {
                                ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                                DATO_BUSQ = "E";
                            }
                            catch
                            {
                                STRIN = txtBUSQUEDA.Text.Trim();
                                DATO_BUSQ = "S";
                            }

                            if (DATO_BUSQ == "S")
                            {
                                FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                                FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                                TIPO = "10"; //CREAR UN FILTRO DE FECHAS Y STRING O ENTERO SOLO
                            }
                            else if (DATO_BUSQ == "E")
                            {
                                FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                                FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                                TIPO = "9"; //CREAR UN FILTRO DE FECHAS Y STRING O ENTERO SOLO
                            }


                        }
                        else if (txtBUSQUEDA.Text == "")
                        {
                            FECHA_INI = Convert.ToDateTime(TXTFECHAINI.Text).ToString("dd-MM-yyyy");
                            FECHA_FIN = Convert.ToDateTime(TXTFECHAFIN.Text).ToString("dd-MM-yyyy");
                            TIPO = "1"; //CREAR UN FILTRO DE FECHAS SOLO - LISTO
                        }
                    }
                }
                else if (TXTFECHAINI.Text == "" && TXTFECHAFIN.Text == "")
                {
                    if ((TXTDECIMIN.Text != null && TXTDECIMAX.Text != null) && (TXTDECIMIN.Text != "" && TXTDECIMAX.Text != ""))
                    {
                        if (txtBUSQUEDA.Text != "")
                        {
                            try
                            {
                                ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                                DATO_BUSQ = "E";
                            }
                            catch
                            {
                                STRIN = txtBUSQUEDA.Text.Trim();
                                DATO_BUSQ = "S";
                            }

                            if (DATO_BUSQ == "S")
                            {
                                DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                                DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                                TIPO = "11"; //CREAR UN FILTRO DE RANGO DE IMPORTES Y STRING 
                            }
                            else if (DATO_BUSQ == "E")
                            {
                                DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                                DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                                TIPO = "12"; //CREAR UN FILTRO DE RANGO DE IMPORTES Y  ENTERO 
                            }


                        }
                        else if (txtBUSQUEDA.Text == "")
                        {

                            DECIMAL_MIN = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMIN.Text.Trim()));
                            DECIMAL_MAX = Convert.ToDecimal(HttpUtility.HtmlDecode(TXTDECIMAX.Text.Trim()));
                            TIPO = "3"; //CREAR UN FILTRO DE RANGO DE IMPORTES SOLO
                        }
                    }
                    else if (TXTDECIMIN.Text == "" && TXTDECIMAX.Text == "")
                    {
                        if (txtBUSQUEDA.Text != "")
                        {
                            try
                            {
                                ENTERO = Convert.ToInt32(HttpUtility.HtmlDecode(txtBUSQUEDA.Text.Trim()));
                                TIPO = "2"; //CREAR UN FILTRO ENTERO SOLO
                            }
                            catch
                            {
                                STRIN = txtBUSQUEDA.Text.Trim();
                                TIPO = "4"; //CREAR UN FILTRO STRING SOLO
                            }


                        }
                        else if (txtBUSQUEDA.Text == "")
                        {
                            Response.Write("<script>alert('DEBE LLENAR LOS CAMPOS PARA LA BÚSQUEDA');</script>");
                        }
                    }
                }



                DataTable tbl = OBJVENTA.NFILTRAR_TODOS(FECHA_INI, FECHA_FIN, ENTERO, DECIMAL_MIN, DECIMAL_MAX, STRIN, TIPO);
                dgvBANCOS.DataSource = tbl;
                dgvBANCOS.DataBind();

                for (int i = 0; i < dgvBANCOS.Rows.Count; i++)
                {

                    string p2 = dgvBANCOS.Rows[i].Cells[8].Text;

                    if (dgvBANCOS.Rows[i].Cells[8].Text == "1002")
                    {
                        dgvBANCOS.Rows[i].Cells[8].Text = "";
                    }

                    if (HttpUtility.HtmlDecode(dgvBANCOS.Rows[i].Cells[4].Text.Trim()) == "01/01/1900")
                    {
                        dgvBANCOS.Rows[i].Cells[4].Text = "";
                    }


                    dgvBANCOS.Rows[i].Cells[10].BackColor = System.Drawing.Color.Gold;
                }

            }
        }
    }
}