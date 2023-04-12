using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auvo.ProcessoSeletivo.DadosCandidato.Controllers
{
    public class CandidatoController : Controller
    {
        // GET: ConditadoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ConditadoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConditadoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConditadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ConditadoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConditadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ConditadoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConditadoController/Delete/5
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
    }
}
