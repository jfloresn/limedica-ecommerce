using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using Web.Xmarket.Models.Test;
using System.Management;
using Microsoft.Web.Administration;


namespace Web.Xmarket.Controllers
{
    [AllowAnonymous]
    public class TestController : Controller
    {
        public ActionResult memoryDetailIis()
        {
            Process proceso = Process.GetCurrentProcess();
            long memoriaUsada = proceso.WorkingSet64 / (1024 * 1024); // En MB

            ComputerInfo info = new ComputerInfo();
            long totalMemoria = (long)(info.TotalPhysicalMemory / (1024 * 1024));
            long memoriaDisponible = (long)(info.AvailablePhysicalMemory / (1024 * 1024));

            var modelo = new MemoriaViewModel
            {
                MemoriaUsada = memoriaUsada,
                MemoriaTotal = totalMemoria,
                MemoriaDisponible = memoriaDisponible
            };

            return View(modelo);
        }

        public ActionResult SystemInfo()
        {
            var model = new SystemInfoModel();

            // Obtener información del procesador
            model.ProcessorName = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
            model.LogicalCores = Environment.ProcessorCount;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                model.ProcessorFullName = obj["Name"].ToString();
                model.PhysicalCores = Convert.ToInt32(obj["NumberOfCores"]);
                model.Threads = Convert.ToInt32(obj["ThreadCount"]);
                model.SpeedMHz = Convert.ToInt32(obj["MaxClockSpeed"]);
                model.Socket = obj["SocketDesignation"]?.ToString();
            }

            // Obtener uso del procesador en IIS
            try
            {


                model.usuariosConectador = MvcApplication.ObtenerUsuariosConectados();


                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", "w3wp");
                cpuCounter.NextValue();
                System.Threading.Thread.Sleep(1000); // Esperar para obtener un valor real
                model.CpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Error obteniendo el uso de CPU en IIS: " + ex.Message;
            }

            return View(model);
        }




        public ActionResult systemPoolIis()
        {
            var model = new IISInfoViewModel
            {
                ApplicationPools = GetApplicationPools(),
                Websites = GetIISWebsites()
            };

            return View(model);
        }

        private List<string> GetApplicationPools()
        {
            List<string> appPools = new List<string>();
            try
            {
                using (ServerManager serverManager = new ServerManager())
                {
                    foreach (var pool in serverManager.ApplicationPools)
                    {
                        appPools.Add($"{pool.Name} (Estado: {pool.State})");
                    }
                }
            }
            catch (Exception ex)
            {
                appPools.Add("Error al obtener los Application Pools: " + ex.Message);
            }
            return appPools;
        }

        private List<string> GetIISWebsites()
        {
            List<string> sitios = new List<string>();
            try
            {
                using (ServerManager serverManager = new ServerManager())
                {
                    foreach (var site in serverManager.Sites)
                    {
                        sitios.Add($"{site.Name} - Estado: {site.State} - Puerto: {site.Bindings[0].EndPoint?.Port}");
                    }
                }
            }
            catch (Exception ex)
            {
                sitios.Add("Error al obtener los sitios en IIS: " + ex.Message);
            }
            return sitios;
        }
    }

}
