using web_app_repository;
using web_app_repository.Interfaces;

var appBuilder = WebApplication.CreateBuilder(args);

// Adicionando servi�os ao cont�iner.
appBuilder.Services.AddControllers();

// Mais informa��es sobre a configura��o do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddSwaggerGen();

// Configura��o de inje��o de depend�ncia
appBuilder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
appBuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Configura��o do CORS
appBuilder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirOrigensEspecificas",
        policy =>
        {
            policy.WithOrigins("http://localhost:5500") // Endere�o do frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = appBuilder.Build();

// Configura��o do pipeline de requisi��o HTTP.
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
