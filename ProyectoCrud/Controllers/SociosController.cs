using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoCrud.Data;
using ProyectoCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCrud.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SociosController
        public ActionResult Index()
        {
            List<Socio> listaSocio = new List<Socio>();
            listaSocio = _context.Socios.ToList();
            return View(listaSocio);
        }

        // GET: SociosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SociosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SociosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Socio socio)
        {
            try
            {
                socio.Estado = 1;
                _context.Add(socio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(socio);
            }
        }

        // GET: SociosController/Edit/5
        public ActionResult Edit(string id)
        {
            Socio socio= _context.Socios.Where(v => v.Cedula == id).FirstOrDefault();
            return View(socio);
        }

        // POST: SociosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,Socio socio)
        {
            if (id != socio.Cedula)
            {
                return RedirectToAction("Index");
            }
                try
                {
                    _context.Add(socio);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                 return View(socio);
                }
        }

        // GET: SociosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SociosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Activar(string id)
        {
            Socio socio = _context.Socios.Where(v => v.Cedula == id).FirstOrDefault();
            socio.Estado = 1;
            _context.Add(socio);
            _context.SaveChanges();
            return View(socio);

        }
        public IActionResult Desactivar(string id)
        {
            Socio socio = _context.Socios.Where(v => v.Cedula == id).FirstOrDefault();
            socio.Estado = 0;
            _context.Add(socio);
            _context.SaveChanges();
            return View(socio);
        }
        
        
    }
}
