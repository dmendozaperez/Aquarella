using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MahApps.Metro.Controls;
using Integrado.Sistemas.Maestro;
using Integrado.Design.WPF_Master;
namespace Integrado.Design.Menu
{
    public class Basico
    {
        public static MetroWindow lF_oFormulario(string vsNombre)
        {
            if (String.IsNullOrEmpty(vsNombre)) return null;
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            foreach (Type Tipo in asm.GetTypes())
            {
                if (Tipo.Name.ToUpperInvariant() == vsNombre.ToUpperInvariant())
                {
                    vsNombre = Tipo.Namespace + "." + Tipo.Name;
                    break;
                }
            }
            Object Objeto = asm.CreateInstance(vsNombre);
            return (MetroWindow)Objeto;
        }
    }
}
