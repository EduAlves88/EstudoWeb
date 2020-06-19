using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Repositorio;

namespace WebApplication2.Controllers
{//Controller dos times
    public class TimeController : Controller
    {
        private TimeRepositorio _repositorio;
        // GET: Obter Time
        public ActionResult ObterTimes()
        {
            _repositorio = new TimeRepositorio();
            ModelState.Clear();
            return View(_repositorio.ObterTimes());
        }
        //GET: Adicionar time
        [HttpGet]
        public ActionResult IncluirTime()
        {
            return View();
        }
        //POST: Adicionar time
        [HttpPost]
        public ActionResult IncluirTime(Times timeObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio = new TimeRepositorio();

                    if (_repositorio.AdicionarTime(timeObj))
                    {
                        ViewBag.Mensagem = "Time Cadastrado com Sucesso!!!";
                        
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return View("ObterTimes");
            }
        }
        //GET: Editar time
        [HttpGet]
        public ActionResult EditarTime(int id)
        {
            _repositorio = new TimeRepositorio();
            return View(_repositorio.ObterTimes().Find(t => t.TimeId == id));
        }
        //POST: Editar time
        [HttpPost]
        public ActionResult EditarTime(Times timeObj)
        {
            try
            {
                _repositorio = new TimeRepositorio();
                _repositorio.AtualizarTime(timeObj);
                return RedirectToAction("ObterTimes");
           
            }
            catch (Exception)
            {
                return View("ObterTimes");
            }
        }
        //GET: Excluir time
        
        public ActionResult ExcluiTime (int id)
        {
            try
            {
                _repositorio = new TimeRepositorio();
                if (_repositorio.ExcluirTime(id))
                {
                    ViewBag.Mensagem = "Time Excluído com sucesso!!!";
                }
                return RedirectToAction("ObterTimes");
            }
            catch (Exception)
            {
                return RedirectToAction("ObterTimes");
            }
        }
    }
}