

namespace QueryHandlers.Common
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.ServiceModel;

    using log4net;

    using QueryContracts.Common;

    using StructureMap;

    public class QueryDispatcher
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private readonly IContainer container;

        public QueryDispatcher(IContainer container)
        {

            this.container = container;
        }


        public QueryResult Dispatch<T>(T parameter) where T : QueryParameter
        {
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                var handlerType = typeof(IQueryHandler<>).MakeGenericType(parameter.GetType());
                dynamic handler = container.GetInstance(handlerType);
                var totalMemoriaInicial = Process.GetCurrentProcess().WorkingSet64 / 1024;

                var rnd = new Random();

                var idThread = rnd.Next();
                var parametros = this.ObtenerParametros(parameter);

                log.Debug(string.Format("|Ini|Query|{0}|{1}|{2}{3}", idThread, handler.ToString(), totalMemoriaInicial.ToString(CultureInfo.InvariantCulture), parametros));

                QueryResult result = handler.Handle(parameter as dynamic);
                sw.Stop();

                var totalMemoriaFinal = Process.GetCurrentProcess().WorkingSet64 / 1024;

                GC.Collect();

                var totalMemoriaReducida = Process.GetCurrentProcess().WorkingSet64 / 1024;
                var elapsedTime = sw.Elapsed;

                log.Debug(string.Format("|Fin|Query|{0}|{1}|{2}|{3}|{4}|{5}", idThread, handler.ToString(), elapsedTime, totalMemoriaInicial.ToString(CultureInfo.InvariantCulture), totalMemoriaFinal.ToString(), totalMemoriaReducida.ToString(CultureInfo.InvariantCulture)));

                return result;

            }
            catch (Exception e)
            {
                sw.Stop();
                GC.Collect();
                log.Error(e.Message, e);
                e = this.Unwrap(e);
                log.Error(e.Message, e);
                throw new FaultException("Se ha producido un error");
            }
        }

        private  Exception Unwrap(Exception ex)
        {
            while (null != ex.InnerException)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        private  string ObtenerParametros(object objeto)
        {
            var parametros = string.Empty;
            try
            {
                foreach (var infoMiembro in objeto.GetType().GetMembers())
                {
                    if (infoMiembro.MemberType == MemberTypes.Property)
                    {
                        var valorParam = string.Empty;
                        if (((PropertyInfo)infoMiembro).GetValue(objeto, null) != null)
                        {
                            valorParam = ((PropertyInfo)infoMiembro).GetValue(objeto, null).ToString();
                        }
                        parametros = parametros + "|" + (infoMiembro).Name + ": " + valorParam;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                parametros = ex.Message;
            }

            return parametros;
        }

    }
}
