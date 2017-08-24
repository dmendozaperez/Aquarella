using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;
//using System.Windows.Forms;

namespace CatalogoVirtual
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    ///     private
    public partial class MainWindow : Window
    {
        private Int32 estado = 0;
        string _folder_principal_path = Basico._ruta_catalogo("IN");//@"C:\inetpub\wwwroot\AQ\JA\CATALOGOS";//System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void limpiar()
        {
            cboseleccionar.SelectedIndex = -1;
            txtvirtual.Text = "";
            txtheader.Text = "";
            txtpag.Text = "";
            txtfoto.Text = "";
            lblestado.Content = "";

        }
        private void combo()
        {
            DataTable dt = null;
            try
            {
                
                string[] _list_ruta_folder = Directory.GetDirectories(_folder_principal_path);

                if (_list_ruta_folder.Length>0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("cod", typeof(string));
                    dt.Columns.Add("des", typeof(string));                    
                    for (Int32 i=0;i<_list_ruta_folder.Length;++i)
                    {
                        string _folder_name = new DirectoryInfo(_list_ruta_folder[i]).Name;
                        if (_folder_name!= "catalogo_prueba_html")                        
                        dt.Rows.Add(_folder_name, _folder_name);
                    }
                    cboseleccionar.ItemsSource = dt.DefaultView;
                    cboseleccionar.DisplayMemberPath = "des";
                    cboseleccionar.SelectedValuePath = "cod";
                   
                }                   

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void activa_desactiva()
        {

            switch(estado)
            {
                case 0:
                    cboseleccionar.IsEnabled = true;
                    txtvirtual.IsEnabled = false;
                    txtheader.IsEnabled = false;
                    txtpag.IsEnabled = false;
                    txtfoto.IsEnabled = false;
                    chkhtml.IsEnabled = false;
                    chkpdf.IsEnabled = false;
                    btnfoto.IsEnabled = false;
                    btneditar.IsEnabled = true;
                    btnaceptar.IsEnabled = false;
                    btncancelar.IsEnabled = false;
                    btnnuevo.IsEnabled = true;
                    chkhtml.IsChecked = false;
                    chkpdf.IsChecked = false;
                    break;
                case 1:
                case 2:
                    cboseleccionar.IsEnabled = false;
                    txtvirtual.IsEnabled = true;
                    txtheader.IsEnabled = true;
                    txtpag.IsEnabled = true;
                    txtfoto.IsEnabled = true;
                    chkhtml.IsEnabled = true;
                    chkpdf.IsEnabled = true;
                    btnfoto.IsEnabled = true;
                    btneditar.IsEnabled = false;
                    btnaceptar.IsEnabled = true;
                    btncancelar.IsEnabled = true;
                    btnnuevo.IsEnabled = false;
                    chkhtml.IsChecked = true;
                    chkpdf.IsChecked = true;
                    txtvirtual.Focus();

                    break;
                //case 2:
                //    break;
            }
        }
        private void sbinicio()
        {

            limpiar();
            estado = 0;
            activa_desactiva();
            combo();
            
            cboseleccionar.Focus();
        }
        private void btnsalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkpdf_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkhtml_Checked(object sender, RoutedEventArgs e)
        {
            //if (chkhtml.IsChecked.Value)
            //{
            //    txtheader.IsEnabled = true;
            //    txtpag.IsEnabled = true;
            //}
            //else
            //{
            //    txtheader.IsEnabled = false;
            //    txtpag.IsEnabled = false;
            //}
        }

        private void chkhtml_Click(object sender, RoutedEventArgs e)
        {
            if (chkhtml.IsChecked.Value)
            {
                txtheader.IsEnabled = true;
                txtpag.IsEnabled = true;
            }
            else
            {
                txtheader.IsEnabled = false;
                txtpag.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sbinicio();
        }

        private void txtpag_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                
            }
        }

        //private void btnaceptar_Click(object sender, RoutedEventArgs e)
        //{
        //    Basico.ejecuta_pdf(@"D:\David\AQUARELLA SQL\Sistema\Vb C# 2013\ELECTRONICO_CARVAJAL\ELECTRONICO\Sistema\www.aquarella.com.pe\CatalogoVirtual\bin\Debug\pdf\catalogo.html", @"D:\David\AQUARELLA SQL\Sistema\Vb C# 2013\ELECTRONICO_CARVAJAL\ELECTRONICO\Sistema\www.aquarella.com.pe\CatalogoVirtual\bin\Debug\pdf\catalogo.pdf");
        //}

        private void btnnuevo_Click(object sender, RoutedEventArgs e)
        {
            estado = 1;
            limpiar();
            activa_desactiva();

        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            sbinicio();
        }

        private void btneditar_Click(object sender, RoutedEventArgs e)
        {
            //estado = 2;
            //activa_desactiva();
        }
        private Boolean fvalida()
        {
            if (txtvirtual.Text.Trim().Length==0)
            {
                MessageBox.Show("Ingrese una carpeta virtual", "Administrador", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtvirtual.Focus();
                return false;
            }
            if (txtheader.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ingrese una Titulo a la Pagina Index", "Administrador", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtheader.Focus();
                return false;
            }

            Int32 npag =0;
            Int32.TryParse(txtpag.Text,out npag);
            if (npag == 0)
            {
                MessageBox.Show("Ingrese el numero de pagina", "Administrador", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtpag.Focus();
                return false;
            }

            /*verifica si la carpeta existe */
            if (estado==1)
            { 
                if (Directory.Exists(@_folder_principal_path + "\\" + txtvirtual.Text.Trim().ToString()))
                {
                    MessageBox.Show("El Nombre de la carpeta virtual existe", "Administrador", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    txtvirtual.Focus();
                    return false;
                }
            }

            return true;
        }
        private void aceptar(ref string index_default,ref string _folder_path_destino)
        {
            try
            { 
                switch (estado)
                {
                    case 1:
                        string _folder_defecto = "catalogo_prueba_html";
                        
                        string _folder_path_defecto = _folder_principal_path + "\\" + _folder_defecto;
                         _folder_path_destino = _folder_principal_path + "\\" + txtvirtual.Text.Trim().ToString();

                        index_default = _folder_path_destino + "\\index.html";

                        DirectoryInfo sourceDir = new DirectoryInfo(@_folder_path_defecto);
                        DirectoryInfo destinationDir = new DirectoryInfo(@_folder_path_destino);


                        if (Directory.Exists(@_folder_path_defecto))
                        {
                            CopyDirectory(sourceDir, destinationDir);
                        }
                                        
                        break;
                }
            }
            catch
            {
                throw;
            }
        }
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(System.IO.Path.Combine(destination.FullName,
                    file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir =System.IO.Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }
        private void editar_html(string _ruta_index)
        {
            try
            {
                string _titulo_default = "{AQUARELLA - PERU} - " +  txtheader.Text.Trim().ToString();
                string _pagina = "pages: " + txtpag.Text;
                string readContentsprincipal;
                using (StreamReader streamReader = new StreamReader(_ruta_index, Encoding.UTF8))
                {
                    readContentsprincipal = streamReader.ReadToEnd();
                }
                readContentsprincipal = readContentsprincipal.Replace("xxxx", _titulo_default);
                readContentsprincipal = readContentsprincipal.Replace("pages: 0", _pagina);

                using (var fileStream = File.Create(@_ruta_index))
                {
                    var texto = new UTF8Encoding(true).GetBytes(readContentsprincipal);
                    fileStream.Write(texto, 0, texto.Length);
                    fileStream.Flush();
                }
            }
            catch
            {
                throw;
            }
        }
        private void btnaceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Basico.ejecuta_pdf(@"D:\David\AQUARELLA SQL\Sistema\Vb C# 2013\ELECTRONICO_CARVAJAL\ELECTRONICO\Sistema\www.aquarella.com.pe\CatalogoVirtual\bin\Debug\catalogo-peru\pdf\catalogo.html", @"D:\David\AQUARELLA SQL\Sistema\Vb C# 2013\ELECTRONICO_CARVAJAL\ELECTRONICO\Sistema\www.aquarella.com.pe\CatalogoVirtual\bin\Debug\catalogo-peru\pdf\catalogo.pdf");
                //return;
                //Basico.ejecuta_pdf(@"C:\inetpub\wwwroot\AQ\JA\CATALOGOS\catalogo-peru\pdf\catalogo.html", @"C:\inetpub\wwwroot\AQ\JA\CATALOGOS\catalogo-peru\pdf\catalogo.pdf");
                //return;
                if (!fvalida()) return;

                string msg = (estado == 1) ? "Esta seguro de generar un nuevo catalogo: " + txtvirtual.Text : "Esta seguro de editar el catalogo: " + txtvirtual.Text;

                System.Windows.Forms.DialogResult resulado = System.Windows.Forms.MessageBox.Show(msg
                            , "Administrador", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Information);
                ///

                if (resulado == System.Windows.Forms.DialogResult.OK)
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    string index_default = "";
                    string path_destino = "";
                    aceptar(ref index_default,ref path_destino);
                    editar_html(index_default);
                    cargar_fotos(path_destino);
                    crear_pdf(path_destino);
                    copiarserver(path_destino);
                    sbinicio();
                    MessageBox.Show("Se Genero correctamente el catalogo {Aquarella}", "Administrador", MessageBoxButton.OK, MessageBoxImage.Information);
                    Mouse.OverrideCursor = null;
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Administrador", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = null;
            }

        }

        private void copiarserver(string _origen)
        {
            string _ruta_Server = Basico._ruta_catalogo("OU") + "\\" + txtvirtual.Text.Trim();
            try
            {
                DirectoryInfo sourceDir = new DirectoryInfo(@_origen);
                DirectoryInfo destinationDir = new DirectoryInfo(@_ruta_Server);

                CopyDirectory(sourceDir, destinationDir);
            }
            catch
            {
                throw;
            }
        }

        private void crear_pdf(string path_destino)
        {
            try
            {

                //Basico.ejecuta_pdf(@"D:\David\AQUARELLA SQL\Sistema\Vb C# 2013\ELECTRONICO_CARVAJAL\ELECTRONICO\Sistema\www.aquarella.com.pe\CatalogoVirtual\bin\Debug\catalogo-peru\pdf\catalogo.html", @"D:\David\AQUARELLA SQL\Sistema\Vb C# 2013\ELECTRONICO_CARVAJAL\ELECTRONICO\Sistema\www.aquarella.com.pe\CatalogoVirtual\bin\Debug\catalogo-peru\pdf\catalogo.pdf");

                string ruta_foto = path_destino + "\\pages";
                string[] _foto = null;
                _foto = Directory.GetFiles(@ruta_foto, "*.jpg");


                //DirectoryInfo dir = new DirectoryInfo(@ruta_foto);
                //FileInfo[] files = dir.GetFiles("*.jpg").OrderBy(p => p.CreationTime).ToArray();

                //string[] _fotoorder=Directory.GetFiles(@ruta_foto, "*.jpg").Select(a => new
                //{
                //    FileName = System.IO.Path.GetFileName(a),                   
                //}).OrderByDescending(a => a.FileName);

                //var sorted = Directory.GetFiles(".").OrderBy(f => f);

                if (_foto.Length > 0)
                {                
                    string path_destino_pdf = path_destino + "\\pdf";
                    string file_detino_html_pdf = path_destino_pdf + "\\catalogo.html";
                    string file_destino_pdf= path_destino_pdf + "\\catalogo.pdf";
                    string readContentsprincipal;
                    using (StreamReader streamReader = new StreamReader(file_detino_html_pdf, Encoding.UTF8))
                    {
                        readContentsprincipal = streamReader.ReadToEnd();
                    }

                  
                    string _img_html = "";

                    for (Int32 i = 0; i < _foto.Length; ++i)
                    {
                        string fileorigen = _foto[i].ToString();
                        FileInfo fi_name = new FileInfo(fileorigen);
                        string num = (i + 1).ToString() + ".jpg";
                        string filedestino = ruta_foto + "\\" + num;//fi_name.Name;
                        _img_html += "<img src='file:///" + filedestino + "' width='573' height='780' border='2' />";

                        //break;
                    }
                    readContentsprincipal = readContentsprincipal.Replace("xxxx", _img_html);                   

                    using (var fileStream = File.Create(@file_detino_html_pdf))
                    {
                        var texto = new UTF8Encoding(true).GetBytes(readContentsprincipal);
                        fileStream.Write(texto, 0, texto.Length);
                        fileStream.Flush();
                    }
                    Basico.ejecuta_pdf(@file_detino_html_pdf, @file_destino_pdf);
                }
            }
            catch
            {
                throw;
            }
        }

        private void cargar_fotos(string path_destino)
        {
            try
            {

                /*esta opcion entra siempre y cuando la caja se mayor a 0 y si es que existe fotos en la ruta seleccionada*/

                string path_foto_origen = txtfoto.Text.Trim().ToString();

                if (path_foto_origen.Length>0)
                {
                    string[] _fotos_file_orig = Directory.GetFiles(path_foto_origen, "*.jpg");

                    /*si hay fotos entonces entra a la condicion*/
                    if (_fotos_file_orig.Length>0)
                    { 
                        string ruta_foto = path_destino + "\\pages";
                        string[] _foto = null;
                        _foto = Directory.GetFiles(@path_destino, "*.jpg");
                
                        if (_foto.Length>0)
                        {
                            /*borrando de la carpeta principal*/
                            for(Int32 i=0;i<_foto.Length;++i)
                            {
                                File.Delete(@_foto[i].ToString());
                            }
                        }
                        /*una vez borrado le hacemos un upload copiamos*/
                        for(Int32 a=0;a<_fotos_file_orig.Length;++a)
                        {
                            string fileorigen = _fotos_file_orig[a].ToString();
                            FileInfo fi_name = new FileInfo(fileorigen);
                            string filedestino = ruta_foto + "\\" + fi_name.Name;
                            File.Copy(@fileorigen, @filedestino);
                        }
                    }
                }
                


            }
            catch
            {
                throw;
            }
        }
        private void btnfoto_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of FolderBrowserDialog.
            System.Windows.Forms.FolderBrowserDialog folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            // A new folder button will display in FolderBrowserDialog.
            folderBrowserDlg.ShowNewFolderButton = true;
            folderBrowserDlg.Description = "Por favor seleccione la carpeta para cargar las fotos";
            //Show FolderBrowserDialog
            System.Windows.Forms.DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.OK)
            {



                //Show selected folder path in textbox1.
                txtfoto.Text = folderBrowserDlg.SelectedPath;

                string[] _foto = null;
                _foto = Directory.GetFiles(@txtfoto.Text, "*.jpg");
                if (_foto.Length>0)
                {
                    decimal _totalpages = _foto.Length;// + 1;
                    txtpag.Text = _totalpages.ToString();
                }

                //Browsing start from root folder.
                //Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
            }
        }

        //private void btnaceptar_Click_1(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
