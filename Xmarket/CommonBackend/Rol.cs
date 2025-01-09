using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common
{
   public class Rol
    {
    }

    public enum RolesPermisos
    {
        #region acceso libre
        Cliente_Lectura = 1,
        Cliente_Escritura = 2,
        Cliente_Actualizar = 3,
        Cliente_Eliminar = 4,
        #endregion

    }

}
