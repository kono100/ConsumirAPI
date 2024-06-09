using ConsumirAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var produto = await _apiService.GetProdutoAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _apiService.GetProdutoAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _apiService.PutProdutoAsync(id, produto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Erro ao atualizar o produto.");
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _apiService.GetProdutoAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _apiService.DeleteProdutoAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Erro ao excluir o produto.");
            var produto = await _apiService.GetProdutoAsync(id);
            return View(produto);
        }

    }
}
