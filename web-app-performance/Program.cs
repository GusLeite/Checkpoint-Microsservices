using web_app_repository;
using web_app_repository.Interfaces;

var appBuilder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao contêiner.
appBuilder.Services.AddControllers();

// Mais informações sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddSwaggerGen();

// Configuração de injeção de dependência
appBuilder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
appBuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Configuração do CORS
appBuilder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirOrigensEspecificas",
        policy =>
        {
            policy.WithOrigins("http://localhost:5500") // Endereço do frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = appBuilder.Build();

// Configuração do pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirOrigensEspecificas");
app.UseAuthorization();

app.MapControllers();

app.Run();
