using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Seguridad.Common;
using Web.Common;
using Web.Xmarket.Utilitario;
namespace Web.Xmarket.Controllers
{
    public class DefaultController : BaseController
    {
       
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("Index", "Home");
           
        }

        //
        // GET: /Default/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Default/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Default/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Default/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Default/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Default/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
