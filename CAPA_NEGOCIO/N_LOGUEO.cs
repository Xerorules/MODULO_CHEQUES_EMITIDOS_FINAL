using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_ENTIDAD;
using System.Data;

namespace CAPA_NEGOCIO
{
    public class N_LOGUEO
    {
        D_LOGIN OBJLOGUEO = new D_LOGIN();

        public DataTable VALIDAR_USUARIO(string USUARIO, string CONTRASENA, string ID_EMPRESA)
        {
            return OBJLOGUEO.VALIDAR_USUARIO(USUARIO, CONTRASENA, ID_EMPRESA);
        }

        public DataTable LLENAR_GRILLA(string FECHAINI, string FECHAFIN, string ID_BANCO,decimal IMPO,string MON, string OPE,string ID_EMPRESA,string OBS)
        {
            return OBJLOGUEO.LLENAR_GRILLA(FECHAINI, FECHAFIN, ID_BANCO, IMPO, MON, OPE, ID_EMPRESA,OBS);
        }

        public DataTable CONSULTA_LISTA_BANCOS()
        {
            return OBJLOGUEO.CONSULTA_LISTA_BANCOS();
        }

        public DataTable LISTAR_EMPRESA()
        {
            return OBJLOGUEO.LISTAR_EMPRESA();
        }
        public DataTable LISTAR_SEDE(string ID_EMPRESA)
        {
            return OBJLOGUEO.LISTAR_SEDE(ID_EMPRESA);
        }
        public DataTable PUNTO_VENTA(string ID_SEDE)
        {
            return OBJLOGUEO.PUNTO_VENTA(ID_SEDE);
        }
        public DataTable CONSULTAR_VISTA_EMPRESA(String ID_EMPRESA)
        {
            return OBJLOGUEO.CONSULTAR_VISTA_EMPRESA(ID_EMPRESA);
        }
        public DataTable CONSULTAR_VISTA_SEDE(String ID_SEDE)
        {
            return OBJLOGUEO.CONSULTAR_VISTA_SEDE(ID_SEDE);
        }

        public string NREGISTRARCHEQUE(E_CHEQUES CHQ,string cond,string id_chequera)
        {
            return OBJLOGUEO.DREGISTRARCHEQUERAS(CHQ,cond, id_chequera);
        }

        public DataTable NREGISTRARCHEQUE_EM(E_CH_EMITIDOS CHQ_EM, string cond)
        {
            return OBJLOGUEO.DREGISTRAR_CHEQUES_EM(CHQ_EM, cond);
        }

        public DataTable NLLENARGRILLA_MOVIMIENTOS_CHEMITIDOS(string id_cta,decimal importe,string fecha)
        {
            return OBJLOGUEO.DLLENAR_GRILLA_MOV_CHEQUESEMIT(id_cta, importe, fecha);
        }

        public DataTable NLLENARGRILLA_MOVIMIENTOS_S(string movs)
        {
            return OBJLOGUEO.DLLENAR_GRILLA_MOV_S(movs);
        }

        public string NREGISTRARCHEQUE_EM2(E_CH_EMITIDOS CHQ_EM, string cond)
        {
            return OBJLOGUEO.DREGISTRAR_CHEQUES_EM2(CHQ_EM, cond);
        }
        
        public DataTable NLLENARGRILLACHEQUERAS(E_CHEQUES CHQ)
        {
            return OBJLOGUEO.DLLENARGRILLACHEQUERAS(CHQ);
        }

        public DataTable NLLENARGRILLACHEQUERAS_EMIT(string id_cuenta,string tipo)
        {
            return OBJLOGUEO.DLLENARGRILLACHEQUERAS_EMIT(id_cuenta, tipo);
        }

        public DataTable NLISTAR_CORRELATIVO(string id_chequera)
        {
            return OBJLOGUEO.DLISTAR_CORRELATIVO_CHEQUERAS_EMIT(id_chequera);
        }

        public DataTable NLISTAR_CORRELATIVO2(string id_chequera,int nume)
        {
            return OBJLOGUEO.DLISTAR_CORRELATIVO_CHEQUERAS_EMIT2(id_chequera,nume);
        }

        public DataTable NLISTAR_INI_FIN(string id_chequera)
        {
            return OBJLOGUEO.DLISTAR_CORRELATIVO_INI_FIN(id_chequera);
        }

        public DataTable NTOTALGIRADO(string id_chequera)
        {
            return OBJLOGUEO.DTOTAL_GIRADO(id_chequera);
        }

        public DataTable NLISTAR_PROVEEDORES_CHEMIT(string id_cheque)
        {
            return OBJLOGUEO.DLISTAR_PROVEEDOR(id_cheque);
        }

        public DataTable NFILTRAR_TODOS(string FECHA_INI, string FECHA_FIN, int ENTERO, decimal DECI_MIN, decimal DECI_MAX, string STRIN, string TIPOO)
        {
            return OBJLOGUEO.DFILTRAR_GRILLA(FECHA_INI, FECHA_FIN, ENTERO, DECI_MIN, DECI_MAX, STRIN, TIPOO);
        }

        public DataTable NLLENAR_CABECERA_MOVIMIENTOS(string ID_CTA)
        {
            return OBJLOGUEO.DLLENAR_CABECERA_MOVIMIENTOS(ID_CTA);
        }

        public string NREGISTRAR_NUEVO_PROVEEDOR(E_PROVEEDOR PRO, int cond)
        {
            return OBJLOGUEO.DREGISTRAR_NUEVO_PROVEEDOR(PRO, cond);
        }

        public DataTable NLISTAR_PAIS(string id_pais)
        {
            return OBJLOGUEO.DLISTAR_PAIS(id_pais);
        }

        public DataTable NLISTAR_DEPARTAMENTO(string cod_pais)
        {
            return OBJLOGUEO.DLISTAR_DEPARTAMENTO(cod_pais);
        }

        public DataTable NLISTAR_PROVINCIA(string cod_dep)
        {
            return OBJLOGUEO.DLISTAR_PROVINCIA(cod_dep);
        }

        public DataTable NLISTAR_DISTRITO(string cod_prv)
        {
            return OBJLOGUEO.DLISTAR_DISTRITO(cod_prv);
        }

        public DataSet REPORTE_CHEQUES_EMITIDOS_DIFERIDO(string ID_CHEQUE, string TIPO)
        {
            return OBJLOGUEO.REPORTE_CHEQUES_DIFERIDO(ID_CHEQUE, TIPO);
        }

        public DataTable NCLI_VALIDAR(string ID_CLI)
        {
            return OBJLOGUEO.DVALIDAR_PRO(ID_CLI);
        }
    }
}
