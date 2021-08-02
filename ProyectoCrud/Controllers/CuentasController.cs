using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoCrud.Data;
using ProyectoCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCrud.Controllers
{
    public class CuentasController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CuentasController ( ApplicationDbContext context )
        {
            _context=context;
        }

        // GET: CuentasController
        public ActionResult Index()
        {
            List<Cuenta> listaCuenta = new List<Cuenta>();
            listaCuenta = _context.Cuentas.ToList();
            return View(listaCuenta);
            
        }

        // GET: CuentasController/Details/5
        public ActionResult Details(string id)
        {
            Cuenta cuenta = _context.Cuentas.Where(V => V.Numero == id).FirstOrDefault();
            return View(cuenta);
        }

        // GET: CuentasController/Create
        public ActionResult Create()
        {
            ViewData["CodigoSocio"] = new SelectList(_context.Socios.Where(v =>v.Estado==1).ToList(), "Cedula", "Cedula");
            return View();
        }

        // POST: CuentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenta cuenta)
        {
            try
            {
                cuenta.Estado = 1;
                _context.Add(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentasController/Edit/5
        public ActionResult Edit(string id)
        {
            Cuenta cuenta = _context.Cuentas.Where(V => V.Numero == id).FirstOrDefault ();
            ViewData["CodigoSocio"] = new SelectList(_context.Socios.Where(v => v.Estado == 1).ToList(), "Cedula", "Cedula", cuenta.CodigoSocio);
            return View(cuenta);
        }

        // POST: CuentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Cuenta cuenta)
        {
            if(id == cuenta.CodigoSocio)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(cuenta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["CodigoSocio"] = new SelectList(_context.Socios.Where(v => v.Estado == 1).ToList(), "Cedula", "Cedula", cuenta.CodigoSocio);
                return View(cuenta);
            }
        }

        // GET: CuentasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CuentasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Activar(string id)
        {
            Cuenta cuenta = _context.Cuentas.Where(V => V.Numero == id).FirstOrDefault();
            cuenta.Estado = 1;
            _context.Update(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Desactivar(string id)
        {
            Cuenta cuenta = _context.Cuentas.Where(V => V.Numero == id).FirstOrDefault();
            cuenta.Estado = 0;
            _context.Update(cuenta);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
