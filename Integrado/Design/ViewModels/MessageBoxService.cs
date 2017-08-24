using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Integrado.Design.ViewModels
{

    public interface IMessageBoxService
    {
        void ShowMessage(string message);
    }

    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
