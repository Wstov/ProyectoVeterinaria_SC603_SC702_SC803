using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using BC;
using BW;
using DA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenConfig = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfig.Issuer,
            ValidAudience = tokenConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Key))
        };
    });
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://veterinariaelganadero.azurewebsites.net")
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IUsuarioBW, UsuarioBW>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<IProductoBW, ProductoBW>();
builder.Services.AddScoped<IProductoImagen, ProductoImagenBC>();
builder.Services.AddScoped<ICitaDA, CitaDA>();
builder.Services.AddScoped<ICitaBW, CitaBW>();
builder.Services.AddScoped<IMascotaDA, MascotaDA>();
builder.Services.AddScoped<IMascotaBW, MascotaBW>();
builder.Services.AddScoped<IPersonaDA, PersonaDA>();
builder.Services.AddScoped<IPersonaBW, PersonaBW>();
builder.Services.AddScoped<IExpedienteDA, ExpedienteDA>();
builder.Services.AddScoped<IExpedienteBW, ExpedienteBW>();
builder.Services.AddScoped<IRolesDA, RolesDA>();
builder.Services.AddScoped<IRolesBW, RolesBW>();
builder.Services.AddScoped<ICarritoDA, CarritoDA>();
builder.Services.AddScoped<ICarritoBW, CarritoBW>();
builder.Services.AddScoped<IRolesxUsuarioDA, RolesxUsuarioDA>();
builder.Services.AddScoped<IRolesxUsuarioBW, RolesxUsuarioBW>();
builder.Services.AddScoped<IFacturaBW, FacturaBW>();
builder.Services.AddScoped<IFacturaDA, FacturaDA>();
builder.Services.AddScoped<IEmpleadoDA, EmpleadoDA>();
builder.Services.AddScoped<IEmpleadoBW, EmpleadoBW>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
