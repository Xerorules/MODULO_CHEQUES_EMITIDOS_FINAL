﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_ENTIDAD;

namespace CAPA_DATOS
{
    public class D_LOGIN
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);




        // VALIDACION DE USUARIOS CON BASE DE DATOS
        // ==========================================

        public DataTable VALIDAR_USUARIO(string USUARIO, string CONTRASENA, string ID_EMPRESA)
        {
            SqlCommand cmd = new SqlCommand("SP_VALIDAR_LOGIN_BANCOS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DNI_USUARIO", USUARIO);
            cmd.Parameters.AddWithValue("@CONTRASENA", CONTRASENA);
            cmd.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable LLENAR_GRILLA(string FECHAINI, string FECHAFIN, string ID_BANCO, decimal IMPO, string MON, string OPE, string ID_EMPRESA,string OBS)
        {
            SqlCommand cmd = new SqlCommand("SP_CONSULTA_CAJA_BANCOS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FECHAINI", FECHAINI);
            cmd.Parameters.AddWithValue("@FECHAFIN", FECHAFIN);
            cmd.Parameters.AddWithValue("@ID_BANCO", ID_BANCO);
            cmd.Parameters.AddWithValue("@IMPORTE", IMPO);
            cmd.Parameters.AddWithValue("@MONEDA", MON);
            cmd.Parameters.AddWithValue("@OPERACION", OPE);
            cmd.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            cmd.Parameters.AddWithValue("@OBS", OBS);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }


        public DataTable LISTAR_EMPRESA()
        {

            SqlCommand cmd = new SqlCommand("SP_LISTAR_EMPRESA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable LISTAR_SEDE(string ID_EMPRESA)
        {
            SqlCommand cmd = new SqlCommand("SP_LISTAR_SEDE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable PUNTO_VENTA(string ID_SEDE)
        {
            SqlCommand cmd = new SqlCommand("LISTAR_PUNTOVENTA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_SEDE", ID_SEDE);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);


            return dt;
        }

        public DataTable CONSULTAR_VISTA_EMPRESA(String ID_EMPRESA)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT ID_EMPRESA,DESCRIPCION,RUC ,TELEFONO_1,TELEFONO_2,DIRECCION,EMAIL,WEB_SITE,
		                        ESTADO,UBIDST,UBIPAI,UBIDEP,UBIDEN,UBIPRV,UBIPRN,UBIDSN  FROM V_EMPRESA WHERE ID_EMPRESA='" + ID_EMPRESA + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable CONSULTAR_VISTA_SEDE(String ID_SEDE)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT ID_SEDE, DESCRIPCION, DIRECCION, 
                                TELEFONO_1, TELEFONO_2, CONTACTO, ESTADO, ID_EMPRESA, UBIDST, UBIDSN, UBIDEN,
                                UBIPRN  FROM V_SEDE WHERE ID_SEDE='" + ID_SEDE + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            con.Close();
            return dt;
        }


        public DataTable CONSULTA_LISTA_BANCOS()
        {
            SqlCommand cmd = new SqlCommand("SP_LLENAR_COMBO_CUENTA_BANCOS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string DREGISTRARCHEQUERAS(E_CHEQUES CH,string cond,string id_chequera)
        {
            string res = "";
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_MANTENIMIENTO_CHEQUERAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CUENTA", CH.id_cuentasbancarias);
                cmd.Parameters.AddWithValue("@N_INICIAL",CH.n_inicial );
                cmd.Parameters.AddWithValue("@N_FINAL", CH.n_final);
                cmd.Parameters.AddWithValue("@F_EMISION",CH.f_emision);
                cmd.Parameters.AddWithValue("@F_VCTO", CH.f_vcto);
                cmd.Parameters.AddWithValue("@N_CORRELATIVO",CH.n_correlativo);
                cmd.Parameters.AddWithValue("@TIPO", CH.tipo);
                cmd.Parameters.AddWithValue("@ESTADO", CH.estado);
                cmd.Parameters.AddWithValue("@OBS", CH.obs);
                cmd.Parameters.AddWithValue("@USUARIO", CH.usuario);
                cmd.Parameters.AddWithValue("@CONDICION", cond);
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", id_chequera);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    res = "ok";
                }
            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return res;
        }

        public DataTable DREGISTRAR_CHEQUES_EM(E_CH_EMITIDOS CH, string cond)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_MANTENIMIENTO_CHEQUES_EMITIDOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUEEMITIDO", CH.id_chequesem);
                cmd.Parameters.AddWithValue("@ID_PROVEEDOR", CH.id_proveedor);
                cmd.Parameters.AddWithValue("@F_GIRO", CH.f_giro);
                cmd.Parameters.AddWithValue("@F_COBRO", CH.f_cobro);
                cmd.Parameters.AddWithValue("@F_COBRADO", CH.f_cobrado);
                cmd.Parameters.AddWithValue("@N_CORRELATIVO", CH.n_cheque);
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", CH.id_chequera);
                cmd.Parameters.AddWithValue("@IMPORTE", CH.importe);
                cmd.Parameters.AddWithValue("@ESTADO", CH.estado);
                cmd.Parameters.AddWithValue("@ID_MOVIMIENTOS", CH.id_movimiento);
                cmd.Parameters.AddWithValue("@OBS", CH.observacion);
                cmd.Parameters.AddWithValue("@CONDICION", cond);
                int a = cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        public DataTable DLLENAR_GRILLA_MOV_CHEQUESEMIT(string id_cuenta,decimal importe, string fecha)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_MOVS_CHEQUES_EMITIDOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CTA", id_cuenta);
                cmd.Parameters.AddWithValue("@IMPORTE", importe);
                cmd.Parameters.AddWithValue("@FECHA", fecha);
                
                int a = cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }


        public DataTable DLLENAR_GRILLA_MOV_S(string movs)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_MOVS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MOVS", movs);
                int a = cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }



        public string DREGISTRAR_CHEQUES_EM2(E_CH_EMITIDOS CH, string cond)
        {
            string res = "";
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_MANTENIMIENTO_CHEQUES_EMITIDOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUEEMITIDO", CH.id_chequesem);
                cmd.Parameters.AddWithValue("@ID_PROVEEDOR", CH.id_proveedor);
                cmd.Parameters.AddWithValue("@F_GIRO", CH.f_giro);
                cmd.Parameters.AddWithValue("@F_COBRO", CH.f_cobro);
                cmd.Parameters.AddWithValue("@F_COBRADO", CH.f_cobrado);
                cmd.Parameters.AddWithValue("@N_CORRELATIVO", CH.n_cheque);
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", CH.id_chequera);
                cmd.Parameters.AddWithValue("@IMPORTE", CH.importe);
                cmd.Parameters.AddWithValue("@ESTADO", CH.estado);
                cmd.Parameters.AddWithValue("@ID_MOVIMIENTOS", CH.id_movimiento);
                cmd.Parameters.AddWithValue("@OBS", CH.observacion);
                cmd.Parameters.AddWithValue("@CONDICION", cond);
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    res = "ok";
                }
            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return res;
        }



        public DataTable DLLENARGRILLACHEQUERAS(E_CHEQUES CH)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_MANTENIMIENTO_CHEQUERAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CUENTA", CH.id_cuentasbancarias);
                cmd.Parameters.AddWithValue("@N_INICIAL", CH.n_inicial);
                cmd.Parameters.AddWithValue("@N_FINAL", CH.n_final);
                cmd.Parameters.AddWithValue("@F_EMISION", CH.f_emision);
                cmd.Parameters.AddWithValue("@F_VCTO", CH.f_vcto);
                cmd.Parameters.AddWithValue("@N_CORRELATIVO", CH.n_correlativo);
                cmd.Parameters.AddWithValue("@TIPO", CH.tipo);
                cmd.Parameters.AddWithValue("@ESTADO", CH.estado);
                cmd.Parameters.AddWithValue("@OBS", CH.obs);
                cmd.Parameters.AddWithValue("@USUARIO", CH.usuario);
                cmd.Parameters.AddWithValue("@CONDICION", "1");
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", "1004");
                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //TRAEMOS EL NUMERO MINIMO QUE DEBE IR DE INICIO PAR ALA SIGUIEN CHEQUERA
        public DataTable DNUMERO_DEINICIO_CHEQUERA(string tipo,string id_cuentas, string estado)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_CHEQUERAS_NUMEROS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TIPO", tipo);
                cmd.Parameters.AddWithValue("@ID_CUENTAS", id_cuentas);
                cmd.Parameters.AddWithValue("@ESTADO", estado);
                
                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }



        //LLENA LAS GRILLAS DEL POPUP DE SELECCIONAR CHEQUERA
        public DataTable DLLENARGRILLACHEQUERAS_EMIT(string id_cuenta,string tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_CHEQUERAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CUENTA", id_cuenta);
                cmd.Parameters.AddWithValue("@TIPO", tipo);
                
                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //NUMERO CORRELATIVO
        public DataTable DCORRELATIVO(string CHEQUERA)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_CORRELATIVO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", CHEQUERA);
               
                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //LISTAMOS LOS NUMEROS DE CHEQUES PARA QUE NO PERMITA REGISTRAR REPETIDOS
        public DataTable DLISTAR_CORRELATIVO_CHEQUERAS_EMIT(string id_chequeras)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_CORRELATIVOS_CH_EMITIDOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", id_chequeras);

                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //LISTAMOS LOS NUMEROS DE CHEQUES PARA QUE NO PERMITA REGISTRAR REPETIDOS2
        public DataTable DLISTAR_CORRELATIVO_CHEQUERAS_EMIT2(string id_chequeras,int nume)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_CORRELATIVOS_CH_EMITIDOS2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", id_chequeras);
                cmd.Parameters.AddWithValue("@NUME", nume);
                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }


        //LISTAMOS LOS NUMEROS DE CHEQUES PARA QUE NO PERMITA REGISTRAR REPETIDOS
        public DataTable DLISTAR_CORRELATIVO_INI_FIN(string id_chequeras)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_LISTAR_INI_FIN_CH_EMITIDOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", id_chequeras);

                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //TRAEMOS EL TOTAL GIRADO
        public DataTable DTOTAL_GIRADO(string id_chequeras)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_TOTAL_GIRADOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUERA", id_chequeras);

                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }


        //LISTAMOS EL PROVEEDOR PARA EL EDITAR DE LA GRILLA CHEQUES EMITIDOS
        public DataTable DLISTAR_PROVEEDOR(string id_cheque)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_PROVEEDOR_EMISION_CHEQUES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_CHEQUE", id_cheque);

                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //FILTRAMOS LA GRILLA CHEQUES EMITIDOS
        public DataTable DFILTRAR_GRILLA(string FECHA_INI,string FECHA_FIN, int ENTERO, decimal DECI_MIN,decimal DECI_MAX, string STRIN, string TIPO)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_FILTRO_TODOS_LOS_CAMPOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FECHA_INI", FECHA_INI);
                cmd.Parameters.AddWithValue("@FECHA_FIN", FECHA_FIN);
                cmd.Parameters.AddWithValue("@ENTERO", ENTERO);
                cmd.Parameters.AddWithValue("@DECIMAL_MIN", DECI_MIN);
                cmd.Parameters.AddWithValue("@DECIMAL_MAX", DECI_MAX);
                cmd.Parameters.AddWithValue("@STRING", STRIN);
                cmd.Parameters.AddWithValue("@TIPO", TIPO);
                int a = cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return dt;
        }

        //LLENAR CABECERA CHE_EMITIDOS
        public DataTable DLLENAR_CABECERA_MOVIMIENTOS(string ID_CTA)
        {
            SqlCommand cmd = new SqlCommand("LLENAR_DATOS_CUENTA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_CUENTA", ID_CTA);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //REGISTRAR NUEVO PROVEEDOR
        public string DREGISTRAR_NUEVO_PROVEEDOR(E_PROVEEDOR PR, int cond)
        {
            string res = "";
            try
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("SP_PROVEEDOR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_PROVEEDOR", PR.id_proveedor);
                cmd.Parameters.AddWithValue("@TIPO_PROVEEDOR", PR.tipo_pro);
                cmd.Parameters.AddWithValue("@ORIGEN_PROVEEDOR", PR.origen_pro);
                cmd.Parameters.AddWithValue("@DESCRIPCION", PR.descripcion_pro);
                cmd.Parameters.AddWithValue("@RUC_DNI", PR.ruc_dni_pro);
                cmd.Parameters.AddWithValue("@DIRECCION", PR.direc_pro);
                cmd.Parameters.AddWithValue("@TELEFONO_1", PR.tele1_pro);
                cmd.Parameters.AddWithValue("@TELEFONO_2", PR.tele2_pro);
                cmd.Parameters.AddWithValue("@MOVIL", PR.movil_pro);
                cmd.Parameters.AddWithValue("@FECHA_NAC", PR.fecha_nac_pro);
                cmd.Parameters.AddWithValue("@EMAIL", PR.email_pro);
                cmd.Parameters.AddWithValue("@WEB_SITE", PR.wesite_pro);
                cmd.Parameters.AddWithValue("@ESTADO", PR.estado_pro);
                cmd.Parameters.AddWithValue("@UBIDST", PR.ubigeo_pro);
                cmd.Parameters.AddWithValue("@ACCION", cond);
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    res = "ok";
                }
            }
            catch (Exception ex)
            {

                System.Console.Write(ex.Message);
            }

            if (con.State == ConnectionState.Open) { con.Close(); }
            return res;
        }



        public DataTable DLISTAR_PAIS(string id_pais)
        {
            SqlCommand cmd = new SqlCommand("SP_LISTARPAIS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_PAIS", id_pais);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable DLISTAR_DEPARTAMENTO(string cod_pais)
        {
            SqlCommand cmd = new SqlCommand("SP_LISTARDEPARTAMENTO_PORPAIS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PAIS", cod_pais);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable DLISTAR_PROVINCIA(string cod_dep)
        {
            SqlCommand cmd = new SqlCommand("SP_LISTARPROVINCIA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dep", cod_dep);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable DLISTAR_DISTRITO(string cod_prov)
        {
            SqlCommand cmd = new SqlCommand("SP_LISTARDISTRITO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@distrito", cod_prov);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataSet REPORTE_CHEQUES_DIFERIDO(string ID_CHEQUE, string TIPO)
        {
            SqlCommand cmd = new SqlCommand("SP_IMPRIMIR_CHEQUES", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_CHEQUE", ID_CHEQUE);
            cmd.Parameters.AddWithValue("@TIPO", TIPO);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataTable DVALIDAR_PRO(string PRO)
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlCommand cmd = new SqlCommand("SP_VALIDAR_PRO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_PROVEEDOR", PRO);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

    }
}
