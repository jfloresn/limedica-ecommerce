

using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using Seguridad.Common;
using System.Collections.Generic;
using ServiceAgents.Common;
using System.Linq;

namespace Web.Xmarket.DataAccess.Seguridad
{
    public  class MenuOpcionData
    {
        public  List<MenuOpcion> GetListaTotalMenu(int prol_int_id)
        {
            var listamenu = GetListarMenu(prol_int_id);
            var resmenu = ObtenerItemsMenu(null, listamenu);
            return resmenu;

        }


        public  List<MenuOpcion> GetListaTotalMenuAdmin(int prol_int_id)
        {
            var listamenu = GetListarMenuAdmin(prol_int_id);
            var resmenu = ObtenerItemsMenu(null, listamenu);
            return resmenu;

        }

        private  List<MenusxRolesDTO> GetListarMenuAdmin(int prol_int_id)
        {
            var parameter = new ListarMenusxRolesAdminParameter() { rol_int_id = prol_int_id, sis_str_siglas = Constantes.GetModuleAcronym() };

            var result = (ListarMenusxRolesAdminResult)parameter.Execute();
            return result == null ? new List<MenusxRolesDTO>() : result.Hits.ToList();
        }


        private  List<MenusxRolesDTO> GetListarMenu(int prol_int_id)
        {
            var parameter = new ListarMenusxRolesParameter() { rol_int_id = prol_int_id, sis_str_siglas = Constantes.GetModuleAcronym() };
            
            var result = (ListarMenusxRolesResult)parameter.Execute();
            return result == null ? new List<MenusxRolesDTO>() : result.Hits.ToList();
        }

        private  List<MenuOpcion> ObtenerItemsMenu(string codigopadre, List<MenusxRolesDTO> listamenu)
        {
            var listamenures = new List<MenuOpcion>();
            foreach (var menu in listamenu.Where(x => x.pag_str_codmenu_padre == codigopadre).ToList())
            {
                MenuOpcion mnu = new MenuOpcion();
                mnu.NombreMenu = menu.pag_str_nombre;
                mnu.Url = menu.pag_str_url;
                mnu.IdMenuOpcion = menu.pag_int_id;
                mnu.Nivel = menu.pag_int_nivel;
                mnu.CodigoMenu = menu.pag_str_codmenu;
                mnu.TipoMenu = menu.pag_str_tipomenu;
                mnu.MenuItem = ObtenerItemsMenu(menu.pag_str_codmenu, listamenu);
                mnu.ItemSeleccionado = menu.srp_seleccion == 0 ? false : true;
                mnu.CodigoPermiso = menu.srp_str_codpermiso;

                listamenures.Add(mnu);
            }
            return listamenures;
        }


    }


}