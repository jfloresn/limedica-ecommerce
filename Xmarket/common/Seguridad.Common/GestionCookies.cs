using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Seguridad.Common
{

    public class Crud_Cookie
    {

        #region METODOS CRUD COOKIE

        public void EliminarCookieXKey(String KEY)
        {
            StateManagement stateManagement = new StateManagement();
            //obtengo
            Dictionary<string, string> keyValAnterior = new Dictionary<string, string>();
            Dictionary<string, string> keyVal2 = new Dictionary<string, string>();
            keyVal2 = stateManagement.GetMultipleUsingSingleKeyCookies("Data_orden");
            //Print
            foreach (KeyValuePair<string, string> keyValuePair in keyVal2)
            {
                keyValAnterior.Add(keyValuePair.Key, keyValuePair.Value);
                //Response.Write(keyValuePair.Key + ":" + keyValuePair.Value + "<br/>");
            }
            //elimino
            stateManagement.DeleteMultipleUsingSingleKeyCookies("Data_orden", keyValAnterior);
            //nuevamente asigno
            keyVal2.Remove(KEY.ToString());
            stateManagement.SetMultipleUsingSingleKeyCookies("Data_orden", keyVal2);

        }
        public String BuscarCookieXkey(String Key)
        {
            String Valor = "";
            var list = new List<KeyValuePair<string, string>>();
            StateManagement stateManagement = new StateManagement();
            Dictionary<string, string> keyVal = new Dictionary<string, string>();
            keyVal = stateManagement.GetMultipleUsingSingleKeyCookies("Data_orden");
            try
            {
                String strValue;
                if (keyVal.ContainsKey(Key.ToString()))
                {
                    strValue = keyVal[Key.ToString()];
                    Valor = strValue;
                }

                //foreach (var value in keyVal[Key.ToString()])
                //{
                //Valor = value.ToString();
                //// do something
                //}
            }
            catch (Exception ex)
            {
                Valor = ex.Message.ToString();
            }
            return Valor;
        }
        public void agregarNuevoCookie(String Key, String Value)
        {

            StateManagement stateManagement = new StateManagement();
            //stateManagement.SetMultipleUsingSingleKeyCookies("Data_orden", keyVal);
            //obtengo
            Dictionary<string, string> keyValAnterior = new Dictionary<string, string>();
            Dictionary<string, string> keyVal2 = new Dictionary<string, string>();
            keyVal2 = stateManagement.GetMultipleUsingSingleKeyCookies("Data_orden");
            //Print
            foreach (KeyValuePair<string, string> keyValuePair in keyVal2)
            {
                keyValAnterior.Add(keyValuePair.Key, keyValuePair.Value);
                //Response.Write(keyValuePair.Key + ":" + keyValuePair.Value + "<br/>");
            }
            //elimino
            stateManagement.DeleteMultipleUsingSingleKeyCookies("Data_orden", keyValAnterior);
            //nuevamente asigno
            keyVal2.Add(Key.ToString(), Value.ToString());
            stateManagement.SetMultipleUsingSingleKeyCookies("Data_orden", keyVal2);
        }
        #endregion
    }

    public class StateManagement
    {

        /// <summary>
        ///Se utiliza para obtener la cookie individual utilizando la clave.
        /// </summary>
        public string GetIndividualCookies(string cookieKey)
        {
            return GetHttpRequest().Cookies[cookieKey].Value;
        }

        /// <summary>
        /// Configuración de cookies de valor único.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetIndividualCookies(string name, string value)
        {
            GetHttpResponse().Cookies[name].Value = value;
        }

        /// <summary>
        ///       Se utiliza para obtener múltiples valores de una sola cookie.
        /// </summary>
        public Dictionary<string, string> GetMultipleUsingSingleKeyCookies(string cookieName)
        {
            // creando dic para volver como colección.
            Dictionary<string, string> dicVal = new Dictionary<string, string>();

            //  Compruebe si la cookie está disponible o no.
            if (GetHttpRequest().Cookies[cookieName] != null)
            {
                //Creando una cookie.
                HttpCookie cok = GetHttpRequest().Cookies[cookieName];

                if (cok != null)
                {
                    //    Obteniendo múltiples valores de una sola cookie.
                    string val = "";
                    NameValueCollection nameValueCollection = cok.Values;
                    try
                    {
                        //Iterar las claves únicas.
                        foreach (string key in nameValueCollection.AllKeys)
                        {
                            dicVal.Add(key, cok[key]);
                        }
                    }
                    catch (Exception ex)
                    {
                        val = ex.Message.ToString();
                    }

                }
            }

            return dicVal;
        }
        public void DeleteMultipleUsingSingleKeyCookies(string cookieName, Dictionary<string, string> dic)
        {
            if (GetHttpRequest().Cookies[cookieName] != null)
            {
                HttpCookie hc = new HttpCookie(cookieName);

                //Esto agrega múltiples cookies en la misma clave.
                foreach (KeyValuePair<string, string> val in dic)
                {
                    hc[val.Key] = val.Value;
                }
                //La configuración de -5  días caduca. 

                hc.Expires = DateTime.Now.AddDays(-5);
                GetHttpResponse().Cookies.Add(hc);
            }
        }
        //Establecer múltiples valores para la cookie única.
        public void SetMultipleUsingSingleKeyCookies(string cookieName, Dictionary<string, string> dic)
        {
            //if (GetHttpRequest().Cookies[cookieName] != null)
            //{
            HttpCookie hc = new HttpCookie(cookieName);

            //Esto agrega múltiples cookies en la misma clave.
            foreach (KeyValuePair<string, string> val in dic)
            {
                hc[val.Key] = val.Value;
            }

            //La configuración de 2 días caduca.
            //hc.Expires.Add(new TimeSpan(2, 0, 0, 0));
            GetHttpResponse().Cookies.Add(hc);
            //}
        }



        //Creating Http request.
        public static HttpRequest GetHttpRequest()
        {
            return HttpContext.Current.Request;
        }

        //Creating http response.
        public static HttpResponse GetHttpResponse()
        {
            return HttpContext.Current.Response;
        }
    }

}