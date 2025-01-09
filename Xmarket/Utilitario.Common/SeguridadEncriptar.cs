using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Utilitario.Common
{
    public class SeguridadEncriptar
    {
        private static readonly Lazy<SeguridadEncriptar> _instance =
        new Lazy<SeguridadEncriptar>(() => new SeguridadEncriptar());

        public static SeguridadEncriptar Instance
        {
            get { return _instance.Value; }
        }

        public string encriptar(string valor)
        {
            byte[] _textoCookie = Encoding.UTF8.GetBytes(valor);

            byte[] _TextoEncriptado = MachineKey.Protect(_textoCookie);

            return HttpServerUtility.UrlTokenEncode(_TextoEncriptado);
        }

        public string desemcriptar(string valor)
        {
            byte[] _textoCookie = HttpServerUtility.UrlTokenDecode(valor);

            byte[] _TextoDesencriptado = MachineKey.Unprotect(_textoCookie);
            return Encoding.UTF8.GetString(_TextoDesencriptado);
        }


    }
}
