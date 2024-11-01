using Moq;
using web_app_performance.Controllers;
using web_app_repository.Interfaces;
using web_app_domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace teste_unitario
{
    public class TesteProdutoController
    {
        private readonly Mock<IProdutoRepository> _mockProdutoRepo;
        private readonly ProdutoController _produtoController;

        public TesteProdutoController()
        {
            _mockProdutoRepo = new Mock<IProdutoRepository>();
            _produtoController = new ProdutoController(_mockProdutoRepo.Object);
        }

        [Fact]
        public async Task ObterProdutos_DeveRetornarOk()
        {
            // Arrange
            var listaProdutos = new List<Produto>
            {
                new Produto
                {
                    Id = 1,
                    Nome = "Tenis",
                    Preco = 99.0,
                    Quantidade_estoque = 5,
                    Data_criacao = "03/03/2022"
                }
            };
            _mockProdutoRepo.Setup(repo => repo.ListarProdutos()).ReturnsAsync(listaProdutos);

            // Act
            var resultado = await _produtoController.GetProduto();

            // Assert
            Assert.IsType<OkObjectResult>(resultado);
            var resultadoOk = resultado as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(listaProdutos), JsonConvert.SerializeObject(resultadoOk.Value));
        }

        [Fact]
        public async Task ObterProdutos_DeveRetornarNotFound()
        {
            // Arrange
            _mockProdutoRepo.Setup(repo => repo.ListarProdutos()).ReturnsAsync((IEnumerable<Produto>)null);

            // Act
            var resultado = await _produtoController.GetProduto();

            // Assert
            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact]
        public async Task SalvarProduto_DeveRetornarOk()
        {
            // Arrange
            var produto = new Produto
            {
                Id = 1,
                Nome = "Camiseta",
                Preco = 60.0,
                Quantidade_estoque = 100,
                Data_criacao = "20/12/2024"
            };
            _mockProdutoRepo.Setup(repo => repo.SalvarProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _produtoController.PostProduto(produto);

            // Assert
            Assert.IsType<OkObjectResult>(resultado);
            _mockProdutoRepo.Verify(repo => repo.SalvarProduto(It.IsAny<Produto>()), Times.Once);
        }
    }
}
