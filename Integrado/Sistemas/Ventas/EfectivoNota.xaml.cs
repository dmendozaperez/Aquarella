using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para EfectivoNota.xaml
    /// </summary>
    public partial class EfectivoNota : MetroWindow
    {
        public EfectivoNota()
        {
            InitializeComponent();
        }
        private List<Ent_Venta_PagoNota> lista_pago_nc = null;
        private Decimal _total_pago_nc = 0;
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Ent_Conexion._Base_Datos = "BDAQ";
            Dat_Venta_Directa dat_formanc = new Dat_Venta_Directa();
            List<Ent_Venta_PagoNota> forma_nc = dat_formanc.leer_formapago_nota(1);
            dgformanc.ItemsSource = forma_nc;

            lista_pago_nc = new List<Ent_Venta_PagoNota>();
            _total_pago_nc = 0;
            lbltotpagonc.Content = string.Format("{0:C2}", _total_pago_nc);
        }

        private void chkok_Click(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            if (check!=null)
            {
                var task = check.DataContext as Ent_Venta_PagoNota;
                if (task!=null)
                {
                    Ent_Venta_PagoNota notatmp = new Ent_Venta_PagoNota();
                    notatmp.doc_tra_id = task.doc_tra_id;                   
                    notatmp.total_nc = task.total_nc;
                    
                    
                    if (task.chknota)
                    {
                        lista_pago_nc.Add(notatmp);
                    }
                    else
                    {
                        for (Int32 i=0;i<lista_pago_nc.Count();++i)
                        {
                            if (task.doc_tra_id==lista_pago_nc[i].doc_tra_id)
                            {
                                lista_pago_nc.RemoveAt(i);
                            }
                        }

                    }

                    _total_pago_nc =Convert.ToDecimal(lista_pago_nc.Sum(s => s.total_nc));
                    lbltotpagonc.Content=string.Format("{0:C2}", _total_pago_nc);
                }
            }
        }
    }
}
