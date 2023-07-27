var builder = WebApplication.CreateBuilder(args);

//Orleans Silo: Nastavte prostředí pro Orleans Silo. Toto je základní krok, který je nezbytný pro další práci s Orleans.
builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddAzureTableGrainStorage(name: "authorStore", configureOptions: options =>
    {
        // Configure the storage connection key
        options.ConfigureTableServiceClient(
            "DefaultEndpointsProtocol=https;AccountName=ales.matejka;AccountKey=TmF6ZGFyLCBzdsSbdGUhIFDFmcOtbGnFoSDFvmx1xaVvdcSNa8O9IGvFr8WIIMO6cMSbbCDEj8OhYmVsc2vDqSDDs2R5Lg==");
    });
    // Orleans Dashboard: Integrujte svůj projekt s Orleans Dashboard pro lepší monitorování a ladění.
    siloBuilder.UseDashboard(x => x.HostSelf = true);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Map("/dashboard", x => x.UseOrleansDashboard());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();