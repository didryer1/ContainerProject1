using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Containers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        //public ITeacher _tr { get; set; }
        ITeacher _tr;
        IAdministrator _ad;
        List<Teacher> tc = new List<Teacher>();
        // GET: Default

        public DefaultController(ITeacher tr)
        {
            _tr = tr;
        }
        public ActionResult Index()
        {



            Teacher rt = (Teacher)_tr;
            rt.FirstName = _tr.FirstName;
            tc.Add(rt);
            return View(tc);


        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                IocContainer container = new IocContainer();

               Teacher td = (Teacher)container.Resolve<ITeacher>();
                // TODO: Add insert logic here
                //Teacher tr = (Teacher)_tr;
                td.FirstName = Request.Form["FirstName"];
                tc.Add(td);


                return RedirectToAction("Index",tc);
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
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

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
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
