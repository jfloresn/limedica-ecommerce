
using QueryContracts.Common;
namespace QueryContracts.Xmarket.Account.Results
{
    public class ObtenerUsuarioResult1 : QueryResult
    {
        public int usr_int_id { get; set; }
        public string usr_str_nombre { get; set; }
        public string usr_str_apellidos { get; set; }
        public string usr_str_red {get;set;}
        public string usr_str_email { get; set; }
        public int usr_int_bloqueado { get; set; }
        public int usr_bit_aprobado { get; set; }
        public string usr_str_tipoacceso { get; set; }


        public int CO_AREA { get; set; }
        public int CO_SEDE { get; set; }
        public int CO_EMPRESA { get; set; }
        public string NU_CELULAR { get; set; }
        public string NO_AREA { get; set; }
        public string NO_EMPRESA { get; set; }
        public string NO_SEDE { get; set; }




    }
}
