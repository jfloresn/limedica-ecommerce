using BaseCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Common
{
   public partial class Permiso
    {
        public Permiso()
        {
            Perfil = new List<Perfil>();
        }



        public RolesPermisos PermisoID { get; set; }

        [Required]
        [StringLength(20)]
        public string Modulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}
