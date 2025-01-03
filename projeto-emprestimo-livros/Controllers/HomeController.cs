using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace projeto_emprestimo_livros.Controllers
{
    public class HomeController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
