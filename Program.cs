var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🚀 Endpoint principal do JetLag - IMPLEMENTADO!
app.MapPost("/api/jetlag/calculate", (JetLagRequest request) =>
{
    // Fusos horários dos principais países (UTC)
    var fusos = new Dictionary<string, int>
    {
        ["Brasil"] = -3,
        ["Estados Unidos"] = -5,    // East Coast
        ["Japão"] = 9,
        ["Reino Unido"] = 0,
        ["França"] = 1,
        ["Alemanha"] = 1,
        ["China"] = 8,
        ["Austrália"] = 10,         // Sydney
        ["Argentina"] = -3,
        ["México"] = -6
    };

    // Validação: países suportados
    if (!fusos.ContainsKey(request.PaisOrigem) || !fusos.ContainsKey(request.PaisDestino))
    {
        return Results.BadRequest(new
        {
            erro = "País não suportado",
            paisesDisponiveis = fusos.Keys.ToArray()
        });
    }

    // Cálculos baseados em regras médicas estabelecidas
    int diferencaFuso = fusos[request.PaisDestino] - fusos[request.PaisOrigem];
    int diasAdaptacao = Math.Abs(diferencaFuso); // 1 dia por hora (regra médica)

    // Dicas baseadas na diferença de fuso
    string dica = Math.Abs(diferencaFuso) switch
    {
        0 => "Sem diferença de fuso! Viagem tranquila.",
        >= 1 and <= 2 => "Diferença pequena. Seu corpo se adapta naturalmente em 1-2 dias.",
        >= 3 and <= 5 => "Diferença moderada. Evite cafeína 6h antes do novo horário de dormir.",
        >= 6 and <= 8 => "Diferença considerável. Comece a ajustar seu horário 2-3 dias antes da viagem.",
        _ => "Diferença extrema! Ajuste gradual do horário uma semana antes é recomendado."
    };

    // Monta resposta estruturada
    var response = new JetLagResponse(
        DiferencaFusoHoras: diferencaFuso,
        DiasParaAdaptacao: diasAdaptacao,
        DicaAdaptacao: dica,
        ResumoViagem: $"{request.PaisOrigem} → {request.PaisDestino}: {Math.Abs(diferencaFuso)}h de diferença"
    );

    return Results.Ok(response);
})
.WithName("CalculateJetLag")
.WithSummary("Calcula o tempo de adaptação ao jet lag entre países")
.WithOpenApi();

app.Run();

// Models simplificados do domínio JetLag - ATUALIZADOS!
public record JetLagRequest(
    string PaisOrigem,          // Ex: "Brasil"
    string PaisDestino          // Ex: "Japão" - REMOVIDO DateTime como discutimos
);

public record JetLagResponse(
    int DiferencaFusoHoras,     // Diferença entre os fusos (+12, -5, etc)
    int DiasParaAdaptacao,      // Quantos dias para se adaptar (sempre Math.Abs da diferença)
    string DicaAdaptacao,       // Uma dica personalizada baseada na diferença
    string ResumoViagem         // Resumo da viagem
);
