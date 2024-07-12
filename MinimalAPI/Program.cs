using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("twillioWebhook",
    (TwillioAction validation) => Task.Run(() =>
    {
        using (var conn = new SqlConnection("Server=WALTER;Database=loja;Trusted_Connection=True;TrustServerCertificate=True;User Id=sa;Password=123"))
        using (var command = new SqlCommand("Procedure1", conn)
        {
            CommandType = CommandType.StoredProcedure
        })
        {
            conn.Open();
            command.ExecuteNonQuery();
            var i = command.ExecuteReaderAsync().Result;

        }
    }).ContinueWith(_ => Results.Ok(validation))).WithName("EmailAction");

app.Run();

public class TwillioAction
{
    public int Id { get; set; }
    public string Description { get; set; }
}
