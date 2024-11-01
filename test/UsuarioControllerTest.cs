using Moq;
using web_app_performance.Controllers;
using web_app_repository.Interfaces;
using web_app_domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace testes_unitarios
{
    public class TesteUsuarioController
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly UsuarioController _usuarioController;

        public TesteUsuarioController()
        {
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _usuarioController = new UsuarioController(_mockUsuarioRepo.Object);
        }

        [Fact]
        public async Task ObterUsuarios_DeveRetornarOk()
        {
            // Arrange
            var listaUsuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "João Silva",
                    Email = "joao@email.com"
                }
            };
            _mockUsuarioRepo.Setup(repo => repo.ListarUsuarios()).ReturnsAsync(listaUsuarios);

            // Act
            var resultado = await _usuarioController.GetUsuario();

            // Assert
            Assert.IsType<OkObjectResult>(resultado);
            var resultadoOk = resultado as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject