using OrderManagerAPI.DALDBSQL;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductionSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//service BD
builder.Services.AddScoped<DALOrder>();
builder.Services.AddScoped<DALDataBase>();
builder.Services.AddScoped<DALProduction>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("permitirTudo",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Verificar se o banco de dados existe na inicialização
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dalSqlServer = services.GetRequiredService<DALDataBase>();
        dalSqlServer.DatabaseExists(); 
        Console.WriteLine("Fim da validação concluída.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao verificar o banco de dados: {ex.Message}");
    }
}

app.UseCors("permitirTudo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductionOrderFlowAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
