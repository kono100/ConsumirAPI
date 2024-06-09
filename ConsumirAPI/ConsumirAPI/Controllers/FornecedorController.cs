using ConsumirAPI.Service;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Swagger.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsumirAPI.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ApiService _apiService;

        public FornecedorController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedores = await _apiService.GetAllFornecedoresAsync();
            var produtos = await _apiService.GetAllProdutosAsync();

            // Preenche o Nome_Produto de cada fornecedor
            foreach (var fornecedor in fornecedores)
            {
                var produto = produtos.FirstOrDefault(p => p.Id == fornecedor.ID_Produto);
                fornecedor.Nome_Produto = produto?.Nome_Produto;
            }

            return View(fornecedores);
        }

        public async Task<IActionResult> Create()
        {
            var produtos = await _apiService.GetAllProdutosAsync();
            ViewBag.Produtos = produtos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                var response = await _apiService.PostFornecedorAsync(fornecedor);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Erro ao criar o fornecedor.");
            }
            return View(fornecedor);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var fornecedor = await _apiService.GetFornecedorAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fornecedor = await _apiService.GetFornecedorAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            var produtos = await _apiService.GetAllProdutosAsync();
            ViewBag.Produtos = produtos;
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _apiService.PutFornecedorAsync(id, fornecedor);
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            var produtos = await _apiService.GetAllProdutosAsync();
            ViewBag.Produtos = produtos;
            return View(fornecedor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var fornecedor = await _apiService.GetFornecedorAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _apiService.DeleteFornecedorAsync(id);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
