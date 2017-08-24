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
using Aquarella.bll;
using Aquarella.bll.Util;
namespace Aquarella.Form.Control
{
    /// <summary>
    /// Lógica de interacción para LoginForm.xaml
    /// </summary>
    ///     
    public partial class LoginForm : Window
    {
        UsersViewModel _userVM;
        Usuario   _user;
        public LoginForm()
        {
            InitializeComponent();
        }
        public Usuario getUser()
        {
            return _user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _userVM = new UsersViewModel();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            /// Verificar campos en blanco
            /// 
            if (!String.IsNullOrEmpty(this.txtUserName.Text))
            {
                ///
                if (!String.IsNullOrEmpty(this.txtPassword.Password))
                {
                    ///
                    // Write code here to authenticate user
                    _user = _userVM.getAndLoadUserByUserName(txtUserName.Text);

                    if (_user != null)
                    {
                        String pass = this.txtPassword.Password;
                        String passDecrypt = Cryptographic.decrypt(_user._usu_contraseña);
                        ///
                        if (pass.Equals(passDecrypt))
                        {
                            ///
                            if (_user._usu_est_id.Equals(ValuesDB.acronymStatusActive))
                            {
                                // If authenticated, then set DialogResult=true
                                DialogResult = true;
                                this.Close();
                            }
                            else
                                MessageBox.Show("Usuario inactivo; imposible iniciar sesión.",
                                    ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            ///
                            this.txtUserName.Focus();
                            ///
                            MessageBox.Show("Usuario y/o contraseña invalidos.",
                                    ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        ///
                        this.txtUserName.Focus();
                        ///
                        MessageBox.Show("Usuario y/o contraseña invalidos.",
                                ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    ///
                    this.txtPassword.Focus();
                    ///
                    MessageBox.Show("Por favor, ingrese su contraseña.",
                            ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ///
                this.txtUserName.Focus();
                ///
                MessageBox.Show("Por favor, ingrese su nombre de usuario.",
                        ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
      
    }
}
