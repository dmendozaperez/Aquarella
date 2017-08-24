using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
namespace www.aquarella.com.pe.bll
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
                cn = new SqlConnection(Conexion.myconexion());
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
            }

            med = (Convert.ToInt32(medida) + ini).ToString().PadLeft(2, '0');
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