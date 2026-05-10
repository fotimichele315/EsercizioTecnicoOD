var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
 
var app = builder.Build();

const string ApiKey = "my-secret-key";

app.MapGet("/api/commesse", (HttpRequest request) =>
{
    if (!request.Headers.TryGetValue("X-API-KEY", out var providedApiKey))
    {
        return Results.Unauthorized();
    }

    if (providedApiKey != ApiKey)
    {
        return Results.Unauthorized();
    }

    var xml = """
    <Commesse>
      <Commessa>
        <Id>COM-001</Id>
        <Jobs>
          <Job>
            <Id>J-1001</Id>
            <Item>
              <Id>I-01</Id>
              <Tipo>Cintura</Tipo>
              <Colore>Nero</Colore>
              <Taglia>M</Taglia>
              <Modello>Classic</Modello>
            </Item>
            <FaseDiLavorazione>Taglio</FaseDiLavorazione>
            <StatoAvanzamento>In corso</StatoAvanzamento>
          </Job>
        </Jobs>
      </Commessa>
    </Commesse>
    """;

    return Results.Content(xml, "application/xml");
});

app.Run();