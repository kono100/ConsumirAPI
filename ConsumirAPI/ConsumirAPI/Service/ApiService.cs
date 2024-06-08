using WebAPI_Swagger.Model;

namespace ConsumirAPI.Service
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }


        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            var response = await _httpClient.GetAsync("api/Produtos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Produto>>();
        }


        public async Task<Produto> GetProdutoAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Produtos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Produto>();
        }


        public async Task<HttpResponseMessage> PostProdutoAsync(Produto produto)
        {
            return await _httpClient.PostAsJsonAsync("api/Produtos",produto);
        }

        public async Task<HttpResponseMessage> PutProdutoAsync(int id, Produto produto)
        {
            return await _httpClient.PutAsJsonAsync($"api/Produtos/{id}", produto);
        }


        public async Task<HttpResponseMessage> DeleteProdutoAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/Produtos/{id}");
        }





        //==========================================================================



        public async Task<List<Fornecedor>> GetAllFornecedoresAsync()
        {
            var response = await _httpClient.GetAsync("api/Fornecedores");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Fornecedor>>();
        }


        public async Task<Fornecedor> GetFornecedorAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Fornecedores/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Fornecedor>();
        }


        public async Task<HttpResponseMessage> PostFornecedorAsync(Fornecedor fornecedor)
        {
            return await _httpClient.PostAsJsonAsync("api/Fornecedores", fornecedor);
        }

        public async Task<HttpResponseMessage> PutFornecedorAsync(int id, Fornecedor fornecedor)
        {
            return await _httpClient.PutAsJsonAsync($"api/Fornecedores/{id}", fornecedor);
        }


        public async Task<HttpResponseMessage> DeleteFornecedorAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/Fornecedores/{id}");
        }



    }
}
