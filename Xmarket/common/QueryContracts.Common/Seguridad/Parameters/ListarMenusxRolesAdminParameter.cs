﻿
namespace QueryContracts.Common.Seguridad.Parameters
{
    public class ListarMenusxRolesAdminParameter : QueryParameter
    {
        public int? rol_int_id { get; set; }
        public string sis_str_siglas { get; set; }
    }
}
