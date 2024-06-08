using ConsumirAPI.Service;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Swagger.Model;

namespace ConsumirAPI.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly ApiService _apiService;

        public ProdutoController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _apiService.GetAllProdutosAsync();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostProdutoAsync(produto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Erro ao criar o produto.");
            }
            return View(produto);
        }

    }
}
