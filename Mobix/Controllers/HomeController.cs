using Microsoft.Build.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace MobixWebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly Logger<HomeController> _logger;
        public ApplicationDbContext _db;


        public HomeController(Logger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }
        public IActionResult PrikazProizvoda(string q)
        {
            HomePrikazProizvodaVM vm = new HomePrikazProizvodaVM();

            var p = _db.Proizvodi
                .Where(s => q == "" || q == null || (s.Naziv.StartsWith(q)))
                .Select(a => new HomePrikazProizvodaVM.Row
                {
                    ID = a.ProizvodID,
                    Cijena = a.Cijena,
                    Kolicina = a.Kolicina,
                    Stanje = a.Stanje,
                    Naziv = a.Naziv,
                    //dodati i sliku kad nastimas Slika = a.Slika
                }).ToList();
            vm.Zapisi = p;
            vm.Query = q;
            // izlistati proizvode na osnovu parametra q sa where !

            return View(vm);
        }
        public IActionResult DetaljiPrikaz(int ProizvodID)
        {

            var x = _db.Proizvodi.Where(p => p.ProizvodID == ProizvodID)
                .Select(a => new HomeDetaljiPrikazVM
                {
                    ID = a.ProizvodID,
                    Cijena = a.Cijena,
                    Kolicina = a.Kolicina,
                    Opis = a.Opis,
                    Naziv = a.Naziv,
                    Stanje = a.Stanje
                    //dodati jos sliku kad nastimas

                }).Single();

            return View(x);
        }
        public IActionResult Contact()
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
    }
}
