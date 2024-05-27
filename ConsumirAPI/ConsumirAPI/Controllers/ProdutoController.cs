using ConsumirAPI.Service;
using Microsoft.AspNetCore.Mvc;

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
    }
}
