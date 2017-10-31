using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Sistema_AQLocal;
namespace Sistema_AQLocal
{
    public class Data_Bata
    {


        private static  void actualizaFlag()
        {
            try
            {
                string ruta = @"N:\sistemas\comun\";
            //string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
            //                       ";Data Source = " + ruta +
            //                       ";Extended Properties = dBASE IV" +
            //                       ";User ID=Admin;Password=;";
            string strConnDbase = @"Provider = vfpoledb" +
                                  ";Data Source = " + ruta +
                                  ";Collating Sequence=general;";
            using (OleDbConnection cn = new OleDbConnection(strConnDbase))
            {
                // Realizo un par de pruebas de inserción de datos en la tabla.

                //RECIBOS
                string sqlCommand = "select *  from scccrpr c, scddrpr d, scamart m where c.crpr_recib=d.drpr_recib and '6'+d.drpr_artic=m.mart_secci+m.mart_artic and c.crpr_ftxol='' and c.crpr_secci='1' and d.drpr_almac='1';";
                OleDbCommand cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                OleDbDataAdapter da = new OleDbDataAdapter(cmdInsertar);
                DataSet dt = new DataSet();
                //da.Fill(dt, "AREC");
                //cn.Close();


                //ACTUALIZANDO FLAG 
                //sqlCommand = "update \\Sbata01/vol2/SISTEMAS/COMUN/scccrpr set crpr_ftxol='X' where crpr_ftxol='' and crpr_secci='1' ";
                sqlCommand = "update " + ruta + "\\scccrpr set crpr_ftxol='X' where crpr_ftxol='' and crpr_secci='1' ";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                cn.Open();
                cmdInsertar.ExecuteNonQuery();
                cn.Close();


                //ACTUALIZANDO FLAG 
                sqlCommand = "update " + ruta + "\\scccixn set cixn_ftxaq='X' where cixn_ftxaq='' and cixn_secci='1'  ";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                cn.Open();
                cmdInsertar.ExecuteNonQuery();
                cn.Close();


                //ACTUALIZANDO FLAG 
                sqlCommand = "update " + ruta + "\\scccsxn set csxn_ftxaq='X' where csxn_ftxaq='' and csxn_secci='1'  ";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                cn.Open();
                cmdInsertar.ExecuteNonQuery();
                cn.Close();

                //ACTUALIZANDO FLAG 
                sqlCommand = "update " + ruta + "\\scccdev set cdev_ftxaq='X' where cdev_ftxaq='' and cdev_secci='1'  ";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                cn.Open();
                cmdInsertar.ExecuteNonQuery();
                cn.Close();

                //ACTUALIZANDO FLAG 
                sqlCommand = "update " + ruta + "\\sccccam set ccam_ftxaq='X' where ccam_ftxaq='' and ccam_secci='1'  ";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                cn.Open();
                cmdInsertar.ExecuteNonQuery();
                cn.Close();


            }
            }
            catch
            {

            }
        }
        public static string _importar_mov_bata(DataTable dt_mov)
        {
            string sqlquery = "USP_Importar_Mov_Bata";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string _error="";
            try
            {
                cn = new SqlConnection(Conexion.conexion_local);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tabla_mov", dt_mov);
                cmd.ExecuteNonQuery();
                actualizaFlag();
            }
            catch(Exception exc)
            {
                _error = exc.Message;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return _error;
        }


        public static string _importar_stk_bata(string _alm_id, DataTable dt_stk)
        {
            string sqlquery = "USP_Cuadrar_Stock_Aquarella";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string _error = "";
            try
            {
                cn = new SqlConnection(Conexion.conexion_local);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idalmacen", _alm_id);
                cmd.Parameters.AddWithValue("@tmp_Stock_Bata", dt_stk);
                cmd.ExecuteNonQuery();
                //actualizaFlag();
            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return _error;
        }


        public static DataSet getAlmacenes()
        {
            string sqlquery = "USP_Leer_Almacen_Estanterias";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Conexion.conexion_local);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        private static string medida(string regla, string medida)
        {
            string med = "";
            Int32 ini = 0;
            switch (regla)
            {
                case "A":
                    ini = 16;
                    break;
                case "B":
                    ini = 26;
                    break;
                case "C":
                    ini = 32;
                    break;
                case "D":
                    ini = 37;
                    break;
                case "E":
                    ini = 0;
                    break;
                case "0":
                    ini = 6;
                    break;
                case "G":
                    ini = 0;
                    break;
                case "H":
                    ini = 26;
                    break;
                case "I":
                    ini = 0;
                    break;
                case "K":
                    ini = 2;
                    break;
                case "L":
                    ini = 34;
                    break;
            }
            switch (regla)
            {
                case "F":
                    switch (medida)
                    {
                        case "00":
                            med = "XC";
                            break;
                        case "01":
                            med = "XS";
                            break;
                        case "02":
                            med = "S";
                            break;
                        case "03":
                            med = "M";
                            break;
                        case "04":
                            med = "L";
                            break;
                        case "05":
                            med = "XL";
                            break;
                        case "06":
                            med = "XG";
                            break;
                    }
                    break;
                case "G":
                    switch(medida)
                    {
                        case "01":
                            med = "01";
                            break;
                        case "02":
                            med = "02";
                            break;
                        case "03":
                            med = "03";
                            break;
                        case "04":
                            med = "04";
                            break;
                        case "05":
                            med = "05";
                            break;
                        case "06":
                            med = "06";
                            break;
                        case "07":
                            med = "08";
                            break;
                        case "08":
                            med = "10";
                            break;
                        case "09":
                            med = "12";
                            break;                                
                    }
                
                    break;
                case "H":
                    med = Basico.Right("0".ToString().PadLeft(1,'0') + (ini + Convert.ToInt32(medida) * 2), 2);
                    //med =Basico.Right(replicate('0', 1) + CAST(@ini + @aux * 2 AS varchar(12)), 2);
                    break;
                case "I":
                    med = Basico.Right("0".ToString().PadLeft(1, '0') + (ini + Convert.ToInt32(medida) * 3), 2);
                    //med = right(replicate('0', 1) + CAST(@ini + @aux * 3 AS varchar(12)), 2);
                    break;
                case "K":
                    med = (ini + Convert.ToInt32(medida) * 2).ToString();
                    //med =Basico.Right(replicate('0', 1) + CAST(@ini + @aux * 2 AS varchar(12)), 2);
                    break;
                case "L":
                    med = Basico.Right("0".ToString().PadLeft(1, '0') + (ini + Convert.ToInt32(medida) * 2), 2);
                    //med =Basico.Right(replicate('0', 1) + CAST(@ini + @aux * 2 AS varchar(12)), 2);
                    break;
                default:
                    med = (Convert.ToInt32(medida) + ini).ToString().PadLeft(2, '0');
                    break;
            }

            //if (regla == "F")
            //{
               
            //}
            //else
            //{ 
            //    med = (Convert.ToInt32(medida) + ini).ToString().PadLeft(2, '0');
            //}


            return med;
        }

        private static string medida2(string regla, string medida)
        {
            string med = "";
            Int32 ini = 0;
            switch (regla)
            {
                case "A":
                    ini = 16;
                    break;
                case "B":
                    ini = 26;
                    break;
                case "C":
                    ini = 32;
                    break;
                case "D":
                    ini = 37;
                    break;
                case "E":
                    ini = 0;
                    break;

            }
            med = (Convert.ToInt32(medida) - ini).ToString().PadLeft(2, '0');
            return med;
        }

        private static DataTable genera_Stocks(ref string _error, ref string _codigoalmacen)
        {
            string _codalm = _codigoalmacen;
            DataTable dt_stk = null;
            string ruta = "";
            try
            {
                ruta = @"N:\sistemas\comun\";
                //string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                //                       ";Data Source = " + ruta +
                //                       ";Extended Properties = dBASE IV" +
                //                       ";User ID=Admin;Password=;";
                string strConnDbase = @"Provider = vfpoledb" +
                                      ";Data Source = " + ruta +
                                      ";Collating Sequence=general;";
                using (OleDbConnection cn = new OleDbConnection(strConnDbase))
                {
                    // Realizo un par de pruebas de inserción de datos en la tabla.

                    //SEMANA
                    string sqlCommand = "select p_year, p_peri from ftperi01 where date()>=p_fini and date()<=p_ffin;";

                    //string sqlCommand = "select * from ftperi01 ;";
                    //string sqlCommand = "select s.csal_ano, s.csal_seman, s.csal_codal, s.csal_artic, s.csal_rmed, sum(csal_stkto) as csal_stkto, sum(csal_med00) as csal_med00, sum(csal_med01) as csal_med01, sum(csal_med02) as csal_med02, sum(csal_med03) as csal_med03, sum(csal_med04) as csal_med04, sum(csal_med05) as csal_med05, sum(csal_med06) as csal_med06, sum(csal_med07) as csal_med07, sum(csal_med08) as csal_med08, sum(csal_med09) as csal_med09, sum(csal_med10) as csal_med10, sum(csal_med11) as csal_med11 from scacsal s where csal_ano+csal_seman='201519' and csal_codal='1' and csal_calid!='2' group by csal_ano, csal_seman, csal_codal, csal_artic, csal_rmed;";
                    OleDbCommand cmdInsertar = new OleDbCommand(sqlCommand, cn);
                    cmdInsertar.CommandTimeout = 0;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmdInsertar);
                    DataSet dt = new DataSet();
                    da.Fill(dt, "AMART");
                    //cn.Close();

                    string semana = "";

                    if (dt.Tables["AMART"].Rows.Count != 0)
                    {
                        semana = dt.Tables["AMART"].Rows[0]["p_year"].ToString() + dt.Tables["AMART"].Rows[0]["p_peri"].ToString();
                    }

                    //MAESTRO ARTICULOS
                    //sqlCommand = "select mart_artic, mart_rmed, mart_serin, mart_serfi, mart_pvta1 from scamart m where m.mart_secci='6';";
                    //cmdInsertar = new OleDbCommand(sqlCommand, cn);
                    //da = new OleDbDataAdapter(cmdInsertar);
                    //da.Fill(dt, "AMART");

                    //STOCK
                    sqlCommand = "select s.csal_ano, s.csal_seman, s.csal_codal, s.csal_artic, s.csal_rmed, m.mart_pvta1, sum(csal_stkto) as csal_stkto, sum(csal_med00) as csal_med00, sum(csal_med01) as csal_med01, sum(csal_med02) as csal_med02, sum(csal_med03) as csal_med03, sum(csal_med04) as csal_med04, sum(csal_med05) as csal_med05, sum(csal_med06) as csal_med06, sum(csal_med07) as csal_med07, sum(csal_med08) as csal_med08, sum(csal_med09) as csal_med09, sum(csal_med10) as csal_med10, sum(csal_med11) as csal_med11 from scacsal s, scamart m where s.csal_artic=m.mart_artic and m.mart_secci='6' and s.csal_ano+s.csal_seman='" + semana + "' and s.csal_codal='" + _codalm + "' and (s.csal_calid!='2') group by s.csal_ano, s.csal_seman, s.csal_codal, s.csal_artic, s.csal_rmed, m.mart_pvta1;";
                    //_codalm = "1";
                    //sqlCommand = "select s.csal_ano, s.csal_seman, '7' as csal_codal, s.csal_artic, s.csal_rmed, m.mart_pvta1, sum(csal_stkto) as csal_stkto, sum(csal_med00) as csal_med00, sum(csal_med01) as csal_med01, sum(csal_med02) as csal_med02, sum(csal_med03) as csal_med03, sum(csal_med04) as csal_med04, sum(csal_med05) as csal_med05, sum(csal_med06) as csal_med06, sum(csal_med07) as csal_med07, sum(csal_med08) as csal_med08, sum(csal_med09) as csal_med09, sum(csal_med10) as csal_med10, sum(csal_med11) as csal_med11 from scacsal s, scamart m where s.csal_artic=m.mart_artic and m.mart_secci='6' and s.csal_ano+s.csal_seman='" + semana + "' and s.csal_codal='" + _codalm + "' and (s.csal_calid!='2') group by s.csal_ano, s.csal_seman, s.csal_codal, s.csal_artic, s.csal_rmed, m.mart_pvta1;";
                    //_codalm = "7";
                    cmdInsertar = new OleDbCommand(sqlCommand, cn);
                    da = new OleDbDataAdapter(cmdInsertar);
                    da.Fill(dt, "ACSAL");

                    cn.Close();

                    DataTable dt_tabla = new DataTable();
                    dt_tabla.Columns.Add("cod_artic", typeof(string));
                    dt_tabla.Columns.Add("talla", typeof(string));
                    dt_tabla.Columns.Add("precio", typeof(double));
                    dt_tabla.Columns.Add("stock", typeof(int));
                    
                    //RECIBOS
                    for (Int32 i = 0; i < dt.Tables["ACSAL"].Rows.Count; ++i)
                    {
                        for (Int32 j = 0; j <= 11; ++j)
                        {
                            string cmp;
                            if (j < 10)
                            {
                                cmp = String.Concat("0", j.ToString());
                            }
                            else
                            {
                                cmp = j.ToString();
                            }
                            int val = Convert.ToInt32(dt.Tables["ACSAL"].Rows[i]["csal_med" + cmp].ToString());
                            int val1 = 0;
                            if (val < 0)
                            {
                                val1 = 0;
                            }
                            else
                            {
                                val1 = val;
                            }

                            string regla = dt.Tables["ACSAL"].Rows[i]["csal_rmed"].ToString();

                            DataRow dr = dt_tabla.NewRow();
                            dr[0] = dt.Tables["ACSAL"].Rows[i]["csal_artic"].ToString();
                            dr[1] = medida(regla, cmp.ToString());
                            dr[2] = Convert.ToDecimal(dt.Tables["ACSAL"].Rows[i]["mart_pvta1"].ToString());
                            dr[3] = val1;

                            if (dr[1].ToString() != "")
                            {
                                dt_tabla.Rows.Add(dr);
                            }
                            //dt_tabla.Rows.Add(dt.Rows[i]["talla"].ToString(), dt.Rows[i]["campo"].ToString());
                            //}
                            //catch (Exception e) { }
                        }
                    }

                    dt_stk=dt_tabla;
                }
            }
            catch(Exception exc)
            {
                _error = exc.Message + " - " + ruta;
                dt_stk = null;
            }
            return dt_stk;
        }

        public static DataTable tabla_stock(ref string _error, ref string _codigoalmacen)
        {
            DataTable dt_importar = null;
            try
            {
                DataTable dt_stk = genera_Stocks(ref _error, ref _codigoalmacen);

                if (_error.Length == 0)
                {
                    if (dt_stk != null)
                    {

                        DataTable dt = new DataTable();

                        dt.Columns.Add("Alm_id", typeof(string));
                        dt.Columns.Add("Con_Bata", typeof(string));
                        dt.Columns.Add("Doc_Bata", typeof(string));
                        dt.Columns.Add("Est_Bata", typeof(string));

                        dt.Columns.Add("Con_Id", typeof(string));
                        dt.Columns.Add("Art_Id", typeof(string));
                        dt.Columns.Add("Tal_Id", typeof(string));
                        dt.Columns.Add("Cantidad", typeof(Int32));


                        //DataTable dt = dt_stk.Clone();
                        //DataRow[] dr01 = dt_stk.Select("cantidad > 0");
                        foreach (DataRow vrow in dt_stk.Rows)
                        {
                            dt.Rows.Add(_codigoalmacen, "", "", "", "", vrow["COD_ARTIC"].ToString().Trim(), vrow["TALLA"].ToString().Trim(), Convert.ToDecimal(vrow["STOCK"].ToString()));
                        }

                        //foreach (DataRow dr0 in dt_stk.Rows)
                        //{
                        //    dt.ImportRow(dr0);
                        //}
                        dt_importar = dt;
                    }
                }

            }
            catch (Exception exc)
            {
                _error = exc.Message;
                dt_importar = null;
            }
            return dt_importar;
        }

        public static  DataTable tabla_movimiento(ref string _error)
        {      
           DataTable dt_importar=null;
           try
           {
                DataTable dt_mov = generaMovimientos(ref _error);

               if (_error.Length==0)
               {                
                    if (dt_mov!=null)
                    {
                        DataTable dt = dt_mov.Clone();
                        DataRow[] dr01 = dt_mov.Select("cantidad > 0");
                        foreach (DataRow dr0 in dr01)
                        {
                            dt.ImportRow(dr0);
                        }
                        dt_importar = dt;
                    }
                }

           }
           catch(Exception exc)
           {
               _error = exc.Message;
               dt_importar=null;
           }
           return dt_importar;
        }
        private static DataTable generaMovimientos(ref string _error)
        {
            DataTable dt_mov = null;
            string ruta="";
           try
           {

                //ruta = @"\\Sbata01\vol2\SISTEMAS\COMUN\";
               ruta = @"N:\sistemas\comun\";
            //string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
            //                       ";Data Source = " + ruta +
            //                       ";Extended Properties = dBASE IV" +
            //                       ";User ID=Admin;Password=;";
            string strConnDbase = @"Provider = vfpoledb" +
                                  ";Data Source = " + ruta +
                                  ";Collating Sequence=general;";
            using (OleDbConnection cn = new OleDbConnection(strConnDbase))
            {
                // Realizo un par de pruebas de inserción de datos en la tabla.

                //RECIBOS
                string sqlCommand = "select *  from scccrpr c, scddrpr d, scamart m where c.crpr_recib=d.drpr_recib and '6'+d.drpr_artic=m.mart_secci+m.mart_artic and c.crpr_ftxol='' and c.crpr_secci='1' and d.drpr_almac='1';";
                //string sqlCommand = "select *  from scccrpr";
                OleDbCommand cmdInsertar = new OleDbCommand(sqlCommand, cn);
                cmdInsertar.CommandTimeout = 0;
                OleDbDataAdapter da = new OleDbDataAdapter(cmdInsertar);
                DataSet dt = new DataSet();
                da.Fill(dt, "AREC");
                //cn.Close();

                //INGRESOS POR NOTA
                sqlCommand = "select *  from scccixn c, scdrixn d, scamart m where c.cixn_codig=d.rixn_codig and '6'+d.rixn_artic=m.mart_secci+m.mart_artic and c.cixn_ftxaq='' and c.cixn_secci='1' and d.rixn_secci='1';";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                da = new OleDbDataAdapter(cmdInsertar);
                da.Fill(dt, "AIXN");

                //SALIDAS POR NOTA
                sqlCommand = "select *  from scccsxn c, scdrsxn d, scamart m where c.csxn_codig=d.rsxn_codig and '6'+d.rsxn_artic=m.mart_secci+m.mart_artic and c.csxn_ftxaq='' and c.csxn_secci='1' and d.rsxn_secci='1';";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                da = new OleDbDataAdapter(cmdInsertar);
                da.Fill(dt, "ASXN");

                //DEVOLUCIONES
                sqlCommand = "select *  from scccdev c, scdevalm d, scamart m where c.cdev_codig=d.dval_codig and '6'+d.dval_artic=m.mart_secci+m.mart_artic and c.cdev_ftxaq='' and c.cdev_secci='1' and d.dval_secci='1';";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                da = new OleDbDataAdapter(cmdInsertar);
                da.Fill(dt, "ADEV");

                //CAMBIOS
                sqlCommand = "select *  from sccccam c, scdrcam d, scamart m where c.ccam_codig=d.rcam_codig and '6'+d.rcam_artic=m.mart_secci+m.mart_artic and c.ccam_ftxaq='' and c.ccam_secci='1' and d.rcam_secci='1';";
                cmdInsertar = new OleDbCommand(sqlCommand, cn);
                da = new OleDbDataAdapter(cmdInsertar);
                da.Fill(dt, "ACAM");

                cn.Close();

                DataTable dt_tabla = new DataTable();
                dt_tabla.Columns.Add("id", typeof(string));
                dt_tabla.Columns.Add("nroid", typeof(string));
                dt_tabla.Columns.Add("estado", typeof(string));
                dt_tabla.Columns.Add("codigo", typeof(string));
                dt_tabla.Columns.Add("articulo", typeof(string));
                dt_tabla.Columns.Add("talla", typeof(string));
                dt_tabla.Columns.Add("cantidad", typeof(int));

                //RECIBOS
                for (Int32 i = 0; i < dt.Tables["AREC"].Rows.Count; ++i)
                {
                    for (Int32 j = 0; j <= 11; ++j)
                    {
                        string cmp;
                        if (j < 10)
                        {
                            cmp = String.Concat("0", j.ToString());
                        }
                        else
                        {
                            cmp = j.ToString();
                        }
                        string val = dt.Tables["AREC"].Rows[i]["drpr_med" + cmp].ToString();
                        string regla = dt.Tables["AREC"].Rows[i]["mart_rmed"].ToString();

                        DataRow dr = dt_tabla.NewRow();
                        dr[0] = "REC";
                        dr[1] = dt.Tables["AREC"].Rows[i]["crpr_recib"].ToString();
                        dr[2] = dt.Tables["AREC"].Rows[i]["crpr_estad"].ToString();
                        if (dt.Tables["AREC"].Rows[i]["crpr_estad"].ToString() != "A")
                        {
                            dr[3] = "30";
                        }
                        else
                        {
                            dr[3] = "31";
                        }
                        dr[4] = dt.Tables["AREC"].Rows[i]["drpr_artic"].ToString();
                        dr[5] = medida(regla, cmp.ToString());
                        dr[6] = val;
                        dt_tabla.Rows.Add(dr);                      
                    }
                }

                //INGRESOS POR NOTA
                for (Int32 i = 0; i < dt.Tables["AIXN"].Rows.Count; ++i)
                {
                    for (Int32 j = 0; j <= 11; ++j)
                    {
                        string cmp;
                        if (j < 10)
                        {
                            cmp = String.Concat("0", j.ToString());
                        }
                        else
                        {
                            cmp = j.ToString();
                        }
                        //try
                        //{
                        string val = dt.Tables["AIXN"].Rows[i]["rixn_med" + cmp].ToString();
                        string regla = dt.Tables["AIXN"].Rows[i]["mart_rmed"].ToString();

                        DataRow dr = dt_tabla.NewRow();
                        dr[0] = "IXN";
                        dr[1] = dt.Tables["AIXN"].Rows[i]["cixn_codig"].ToString();
                        dr[2] = dt.Tables["AIXN"].Rows[i]["cixn_gguia"].ToString();
                        if (dt.Tables["AIXN"].Rows[i]["cixn_gguia"].ToString() != "A")
                        {
                            dr[3] = "30";
                        }
                        else
                        {
                            dr[3] = "31";
                        }
                        dr[4] = dt.Tables["AIXN"].Rows[i]["rixn_artic"].ToString();
                        dr[5] = medida(regla, cmp.ToString());
                        dr[6] = val;
                        dt_tabla.Rows.Add(dr);                       
                    }
                }

                //SALIDAS POR NOTA
                for (Int32 i = 0; i < dt.Tables["ASXN"].Rows.Count; ++i)
                {
                    for (Int32 j = 0; j <= 11; ++j)
                    {
                        string cmp;
                        if (j < 10)
                        {
                            cmp = String.Concat("0", j.ToString());
                        }
                        else
                        {
                            cmp = j.ToString();
                        }
                        //try
                        //{
                        string val = dt.Tables["ASXN"].Rows[i]["rsxn_med" + cmp].ToString();
                        string regla = dt.Tables["ASXN"].Rows[i]["mart_rmed"].ToString();

                        DataRow dr = dt_tabla.NewRow();
                        dr[0] = "SXN";
                        dr[1] = dt.Tables["ASXN"].Rows[i]["csxn_codig"].ToString();
                        dr[2] = dt.Tables["ASXN"].Rows[i]["csxn_gguia"].ToString();
                        if (dt.Tables["ASXN"].Rows[i]["csxn_gguia"].ToString() != "A")
                        {
                            dr[3] = "31";
                        }
                        else
                        {
                            dr[3] = "30";
                        }
                        dr[4] = dt.Tables["ASXN"].Rows[i]["rsxn_artic"].ToString();
                        dr[5] = medida(regla, cmp.ToString());
                        dr[6] = val;
                        dt_tabla.Rows.Add(dr);                        
                    }
                }

                //DEVOLUCIONES
                for (Int32 i = 0; i < dt.Tables["ADEV"].Rows.Count; ++i)
                {
                    for (Int32 j = 0; j <= 11; ++j)
                    {
                        string cmp;
                        if (j < 10)
                        {
                            cmp = String.Concat("0", j.ToString());
                        }
                        else
                        {
                            cmp = j.ToString();
                        }
                        //try
                        //{
                        string val = dt.Tables["ADEV"].Rows[i]["dval_med" + cmp].ToString();
                        string regla = dt.Tables["ADEV"].Rows[i]["mart_rmed"].ToString();

                        DataRow dr = dt_tabla.NewRow();
                        dr[0] = "DEV";
                        dr[1] = dt.Tables["ADEV"].Rows[i]["cdev_codig"].ToString();
                        dr[2] = dt.Tables["ADEV"].Rows[i]["cdev_gguia"].ToString();
                        if (dt.Tables["ADEV"].Rows[i]["cdev_gguia"].ToString() != "A")
                        {
                            dr[3] = "30";
                        }
                        else
                        {
                            dr[3] = "31";
                        }
                        dr[4] = dt.Tables["ADEV"].Rows[i]["dval_artic"].ToString();
                        dr[5] = medida(regla, cmp.ToString());
                        dr[6] = val;
                        dt_tabla.Rows.Add(dr);                     
                    }
                }

                //CAMBIOS
                for (Int32 i = 0; i < dt.Tables["ACAM"].Rows.Count; ++i)
                {
                    for (Int32 j = 0; j <= 11; ++j)
                    {
                        string cmp;
                        if (j < 10)
                        {
                            cmp = String.Concat("0", j.ToString());
                        }
                        else
                        {
                            cmp = j.ToString();
                        }
                        //try
                        //{
                        string val = dt.Tables["ACAM"].Rows[i]["rcam_med" + cmp].ToString();
                        string regla = dt.Tables["ACAM"].Rows[i]["mart_rmed"].ToString();

                        DataRow dr = dt_tabla.NewRow();
                        dr[0] = "CAM";
                        dr[1] = dt.Tables["ACAM"].Rows[i]["ccam_codig"].ToString();
                        dr[2] = dt.Tables["ACAM"].Rows[i]["ccam_estad"].ToString();
                        dr[4] = dt.Tables["ACAM"].Rows[i]["rcam_artic"].ToString();
                        dr[5] = medida(regla, cmp.ToString());
                        if (dt.Tables["ACAM"].Rows[i]["rcam_treg"].ToString() == "I")
                        {
                            if (dt.Tables["ACAM"].Rows[i]["ccam_estad"].ToString() != "A")
                            {
                                dr[3] = "30";
                            }
                            else
                            {
                                dr[3] = "31";
                            }

                            dr[6] = val;
                        }
                        else
                        {
                            if (dt.Tables["ACAM"].Rows[i]["rcam_treg"].ToString() == "S")
                            {
                                if (dt.Tables["ACAM"].Rows[i]["ccam_estad"].ToString() != "A")
                                {
                                    dr[3] = "31";
                                }
                                else
                                {
                                    dr[3] = "30";
                                }
                                dr[6] = val;
                            }
                        }
                        dt_tabla.Rows.Add(dr);
                    }
                  }
                dt_mov=dt_tabla;
                  }
                }
            catch(Exception exc)
            {
                _error = exc.Message + " - " + ruta;
                dt_mov = null;
            }
           return dt_mov;
        }
    }
}