using Atividade.Repository;
using Atividade.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SalesRepository>();
builder.Services.AddSingleton<SalesService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(op => {
    op.SwaggerEndpoint("/swagger/v1/swagger.json", "API de vendas V1");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
