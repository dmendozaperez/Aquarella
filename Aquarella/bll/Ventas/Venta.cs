using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Variables;
using System.Windows.Forms;
namespace Aquarella.bll
{
    public class Venta
    {

        public static string _anular_ncredito(Decimal varnumcred, Int32 _usu_mod)
        {
            string sqlquery = "USP_Anular_NotaCredito";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string _valida = "";
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@not_id", varnumcred);
                cmd.Parameters.AddWithValue("@not_usu_mod", _usu_mod);
                cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                _valida = exc.Message;
            }
            return _valida;
        }
        public static string _anular_venta(string _ven_id)
        {
            string sqlquery = "USP_Anular_Venta";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string _error = "";
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _ven_id);
                cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                _error = exc.Message;
            }
            return _error;
        }
        public static Decimal insertar_leer_paquete(string _liqid)
        {
            string sqlquery="USP_InsertarLeer_LiqPaquete";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            Decimal id_paquete = 0;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paq_liqid", _liqid);
                cmd.Parameters.Add("@paq_id", SqlDbType.Decimal);
                cmd.Parameters["@paq_id"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                id_paquete =Convert.ToInt32(cmd.Parameters["@paq_id"].Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                id_paquete = 0;
            }
            return id_paquete;
        }

        public static string insertar_articulopaq(Decimal _paq_id,string _liqid,string _artid,string _talla,Decimal _cantidad,string _calidad,string _barra)
        {
            string sqlquery = "USP_InsertarArticuloPaq";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string valor="";
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paq_id", _paq_id);
                cmd.Parameters.AddWithValue("@paq_liqid", _liqid);
                cmd.Parameters.AddWithValue("@art_id", _artid);
                cmd.Parameters.AddWithValue("@tal_id", _talla);
                cmd.Parameters.AddWithValue("@Cant", _cantidad);
                cmd.Parameters.AddWithValue("@calidad", _calidad);
                cmd.Parameters.AddWithValue("@barra", _barra);
                //cmd.Parameters.AddWithValue("@Cant", _cantidad);
                cmd.Parameters.Add("@resulado", SqlDbType.VarChar,2);
                cmd.Parameters["@resulado"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                valor=cmd.Parameters["@resulado"].Value.ToString();
            }
            catch (Exception exc)
            {
                valor = "-1";
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valor;
        }
        public static DataTable leer_articulopacking_paquete(string _liqid,Decimal _paqid)
        {
            string sqlquery = "USP_LeerArtPackinPaquete";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liqid);
                cmd.Parameters.AddWithValue("@Paq_Id", _paqid);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable leerarticulopaqliq(string _liq)
        {
            string sqlquery = "USP_LeerArticulo_PaqLiq";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liq);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static string borrar_lineapaquete(decimal _paqid,string _articulo,string _talla)
        {
            string sqlquery = "USP_Borrar_LineaPaqueteDetalle";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paq_id", _paqid);
                cmd.Parameters.AddWithValue("@art_id", _articulo);
                cmd.Parameters.AddWithValue("@talla",_talla);
                cmd.ExecuteNonQuery();
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        public static decimal leer_maxnopaqliq(string _liq)
        {
            string sqlquery = "USP_Leer_MaxNoPaqLiq";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liq);
                cmd.Parameters.Add("@nopaquete", SqlDbType.Decimal);
                cmd.Parameters["@nopaquete"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                return Convert.ToDecimal(cmd.Parameters["@nopaquete"].Value);
            }
            catch
            {
                return 0;
            }
        }
        public static string insertar_venta(string _liq)
        {
            string sqlquery = "USP_Insertar_Venta";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string num_doc = "";
            try
            {
               
                  
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liq);
                cmd.Parameters.AddWithValue("@usu_creacion", Global._bas_id_codigo);
                cmd.Parameters.Add("@numero_venta", SqlDbType.VarChar, 20);
                cmd.Parameters["@numero_venta"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                num_doc=cmd.Parameters["@numero_venta"].Value.ToString() ;
            }
            catch (Exception exc)
            {
                num_doc= "-1";
            }
            return num_doc;
        }

        public static DataSet leer_venta_guia(string _ven_id)
        {
            string sqlquery = "USP_Leer_Guia_Venta";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _ven_id);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }
        public static Int32 valida_guia(string _idventa)
        {
            string sqlquery = "USP_Valida_Guia_Venta";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _idventa);
                cmd.Parameters.Add("@valida_banco", SqlDbType.Int);
                cmd.Parameters["@valida_banco"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@valida_banco"].Value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static DataTable Datos_art_tallaemp(string _liqid,string _art)
        {
            string sqlquery = "USP_Leer_ArtTallaLiqNoEmpaque";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liqid);
                cmd.Parameters.AddWithValue("@art_id", _art);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable dt_consulta_venta(Boolean _tipo,DateTime _fecha_ini,DateTime _fecha_fin,string _doc)
        {
            string sqlquery = "USP_Consultar_Documento_Anular";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipo", _tipo);
                cmd.Parameters.AddWithValue("@fechaini", _fecha_ini);
                cmd.Parameters.AddWithValue("@fechafin", _fecha_fin);
                cmd.Parameters.AddWithValue("@doc", _doc);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        //public static string generar_ticket(string _venid, int _tkoriginal)
        //{
        //    try
        //    {
        //        Impresora_Epson.Config_Imp.GenerarTicketFact(_venid, 1);
        //    }
        //    catch
        //    {
        //    }
        //}


    }
}
