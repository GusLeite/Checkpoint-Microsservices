using Moq;
using web_app_domain;
using web_app_repository.Interfaces;

namespace testes_unitarios
{
    public class TesteProdutoRepository
    {
        [Fact]
        public async Task DeveListarProdutos()
        {
            // Arrange
            var listaDeProdutos = new List<Produto>
            {
                new Produto
                {
                    Id = 1,
                    Nome = "Livro",
                    Preco = 35.0,
                    Quantidade_estoque = 150,
                    Data_criacao = "05/01/2025"
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Caderno",
                    Preco = 15.0,
                    Quantidade_estoque = 300,
                    Data_criacao = "15/02/2025"
                },
                new Produto
                {
                    Id = 3,
                    Nome = "Caneta",
                    Preco = 5.0,
                    Quantidade_estoque = 500,
                    Data_criacao = "25/03/2025"
                }
            };

            var mockProdutoRepository = new Mock<IProdutoRepository>();
            mockProdutoRepository.Setup(repo => repo.ListarProdutos()).ReturnsAsync(listaDeProdutos);
            var repositorioUsuario = mockProdutoRepository.Object;

            // Act
            var resultado = await repositorioUsuario.ListarProdutos();

            // Assert
            Assert.Equal(listaDeProdutos, resultado);
        }
    }
}
