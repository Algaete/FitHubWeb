var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuración de Supabase
builder.Services.AddSingleton<Supabase.Client>(sp =>
{
    var options = new SupabaseOptions
    {
        Url = "https://mppqnucxvzegmzifptbf.supabase.co",
        Key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1wcHFudWN4dnplZ216aWZwdGJmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDc4ODAxMjIsImV4cCI6MjA2MzQ1NjEyMn0.Loo8gwqtcdIiicQfgVlnTSn-vPfBJlT6fc8pw27eM_I"
    };
    return new Supabase.Client(options);
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
