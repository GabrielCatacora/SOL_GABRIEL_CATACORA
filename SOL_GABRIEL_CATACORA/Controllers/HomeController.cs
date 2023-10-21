using Microsoft.AspNetCore.Mvc;
using SOL_GABRIEL_CATACORA.Dao;
using SOL_GABRIEL_CATACORA.Models;
using System.Diagnostics;

namespace SOL_GABRIEL_CATACORA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Home_Dao dao = new Home_Dao();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult CargarCombos() 
        {
            List<ALUMNO_ENT> listAlumno = new List<ALUMNO_ENT>();
            List<CURSO_ENT> listaCurso = new List<CURSO_ENT>();
            List<SESSION_ENT> listaSession = new List<SESSION_ENT>();
            string respuesta = "";
            try
            {
                listAlumno = dao.ListarAlumnosDAO();
                listaCurso = dao.ListarCursoDAO();
                listaSession = dao.ListarSessionDAO();
                respuesta = "1";
            }
            catch (Exception ex)
            {
                respuesta = "2";
                throw ex;
            }

            return Json(new { respuesta, listAlumno, listaCurso, listaSession });
        }

        [HttpPost]
        public JsonResult RegistrarMatricula([FromBody] MATRICULA_ENT matri) 
        {
            string respuesta;
            List<MATRICULA_ENT> listMatricula = new List<MATRICULA_ENT>();
            try
            {
                respuesta = dao.RegistarMatriculaDAO_SP(matri);
                if(respuesta == "1")
                {
                    listMatricula = dao.ListarMatriculaDAO();
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { respuesta, listMatricula });
        }
    }
}