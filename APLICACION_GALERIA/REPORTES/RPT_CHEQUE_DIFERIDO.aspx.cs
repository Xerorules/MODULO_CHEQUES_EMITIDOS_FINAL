using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace APLICACION_GALERIA
{
    public partial class Formulario_web15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String id_cheque = Request.QueryString["ID_CHEQUE"].ToString();
                String tipo = Request.QueryString["TIPO"].ToString();
                
                imprimir(id_cheque, tipo);
            }
        }

        void imprimir(string id_cheque, string tipo)
        {
            try
            {
                N_LOGUEO objnego = new N_LOGUEO();

                DataSet ds = objnego.REPORTE_CHEQUES_EMITIDOS_DIFERIDO(id_cheque, tipo);

                DataSet ds_reporte = new DataSet();
                
                DataTable dtcliente = ds.Tables[0].Copy();
                dtcliente.TableName = "DIFERIDO";
                ds_reporte.Tables.Add(dtcliente);


                

                ReportDocument rp = new ReportDocument();
                rp.Load(Server.MapPath("../REPORTES/CHEQUE_BBVA_DIFERIDO.rpt"));
                rp.SetDataSource(ds_reporte);
                CrystalReportViewer_CHEQUE_DIFERIDO.ReportSource = rp;
                CrystalReportViewer_CHEQUE_DIFERIDO.DataBind();
                //exportar a pdf
                ReportDocument pdf = rp;
                pdf.ExportToHttpResponse(
                     ExportFormatType.PortableDocFormat, Response, false, "CHEQUE_BBVA_DIFERIDO");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
                Response.Write("<script>window.alert('ERROR, NO HAY DATOS QUE IMPRIMIR, O BIEN LOS DATOS SON ERRONEOS');</script>");
            }
        }

    }
}