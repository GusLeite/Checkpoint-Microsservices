using Moq;
using web_app_domain;
using web_app_repository.Interfaces;

namespace testes_unitarios
{
    public class TesteUsuarioRepository
    {
        [Fact]
        public async Task DeveListarUsuarios()
        {
            // Arrange
            var listaDeUsuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 3,
                    Nome = "Ana Clara",
                    Email = "ana.clara@email.com"
                },
                new Usuario
                {
                    Id = 4,
                    Nome = "Bruno Lima",
                    Email = "bruno.lima@email.com"
                }
            };

            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            mockUsuarioRepository.Setup(repo => repo.ListarUsuarios()).ReturnsAsync(listaDeUsuarios);
            var repositorioUsuario = mockUsuarioRepository.Object;

            // Act
            var resultado = await repositorioUsuario.ListarUsuarios();

            // Assert
            Assert.Equal(listaDeUsuarios, resultado);
        }
    }
}
