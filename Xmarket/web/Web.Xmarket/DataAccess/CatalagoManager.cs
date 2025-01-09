using log4net;
using QueryContracts.Xmarket.Editorial.Parameters;
using QueryContracts.Xmarket.Editorial.Result;
using QueryContracts.Xmarket.Especialidad.Parameters;
using QueryContracts.Xmarket.Especialidad.Result;
using ServiceAgents.Common;
using System;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;
using static Utilitario.Common.ConstanteGeneral;


namespace Web.Xmarket.DataAccess
{
    public class CatalagoManager
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Lazy<CatalagoManager> _instance =new Lazy<CatalagoManager>(() => new CatalagoManager());

        public static CatalagoManager Instance
        {
            get { return _instance.Value; }
        }
        public  object getCache(String key)
        {
                MemoryCache cache = MemoryCache.Default;
                var cachedItem = cache.Get(key);
                return cachedItem;
        }

        public  MenuLimedica obtenerMenus()
        {
            var editoriales = listarEditorialesAsyn();
            var especialidades = listarEspecialidadesAsyn();
            

            MenuLimedica menuLimedica = new MenuLimedica();
            menuLimedica.todoEditorial =  editoriales;
            menuLimedica.especialidad =  especialidades;

            return menuLimedica;
        }

        public MenuLimedica obtenerMenusAsync()
        {
            MenuLimedica menuLimedica = new MenuLimedica();

            Task.Run(() =>
            {
                var editoriales = listarEditorialesAsyn();
                var especialidades = listarEspecialidadesAsyn();

                menuLimedica.todoEditorial = editoriales;
                menuLimedica.especialidad = especialidades;
            });

            return menuLimedica;
        }

        public  void saveCache(Object objeto, String key)
        {
                    MemoryCache cache = MemoryCache.Default;
                    cache.Set(key, objeto, DateTimeOffset.Now.AddMinutes(300));         
        }

        public  ListarTodoEditorialResult listarEditorialesAsyn()
        {
            var result =  CatalagoManager.Instance.getCache(CACHE_EDITORIAL);

            if (result != null)
            {
                CatalagoManager.Instance.log.Info("lectura de editorial en cache");

                return (ListarTodoEditorialResult) result;
            }

            var resultBd = ((ListarTodoEditorialResult)new ListarTodoEditorialParameter().Execute());

            CatalagoManager.Instance.saveCache(resultBd, CACHE_EDITORIAL);

            return resultBd;

        }

        public  ListarTodoEspecialidadResult listarEspecialidadesAsyn()
        {
            var result = CatalagoManager.Instance.getCache(CACHE_ESPECIALIDAD);

            if (result != null)
            {
                CatalagoManager.Instance.log.Info("lectura de especialidad de cache");
                return (ListarTodoEspecialidadResult) result;
            }

            var resultBd = (ListarTodoEspecialidadResult)new ListarTodoEspecialidadParameter().Execute();

            CatalagoManager.Instance.saveCache(resultBd, CACHE_ESPECIALIDAD);

            return resultBd;


        }





    }
}