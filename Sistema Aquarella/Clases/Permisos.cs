using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Aquarella
{
    public  class Permisos
    {
        public  static void SetPermissions()
        {
          

            //System.Security.Permissions.FileIOPermission f2 = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Read, "D:/David/AQUARELLA SQL/Sistema/Vb C# 2013/ELECTRONICO/Sistema/www.aquarella.com.pe/Sistema Aquarella/bin/Debug/exalib.dll");
            //f2.AddPathList(System.Security.Permissions.FileIOPermissionAccess.Write | System.Security.Permissions.FileIOPermissionAccess.Read, "D:/David/AQUARELLA SQL/Sistema/Vb C# 2013/ELECTRONICO/Sistema/www.aquarella.com.pe/Sistema Aquarella/bin/Debug/exalib.dll");
            try
            {
                System.Security.Permissions.FileIOPermission filePerm = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Read, "D:/David/AQUARELLA SQL/Sistema/Vb C# 2013/ELECTRONICO/Sistema/www.aquarella.com.pe/Sistema Aquarella/bin/Debug/exalib.dll");
                filePerm.Assert();
                //filePerm.Demand();
            }
            catch (System.Security.SecurityException s)
            {
                
                Console.WriteLine(s.Message);
            }
   
        }
    }
}
