using CasosUso.CU_Caba�a.CasosUso;
using CasosUso.CU_Caba�a.InterfacesCU;
using CasosUso.CU_Mantenimiento.CasosUso;
using CasosUso.CU_Mantenimiento.InterfacesCU;
using CasosUso.CU_Parametros;
using CasosUso.CU_TipoCaba�a.CasosUso;
using CasosUso.CU_TipoCaba�a.InterfacesCU;
using CasosUso.CU_Usuario;
using Datos.ContextoEF;
using Datos.Repositorios;
using Dominio_Interfaces.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(config => config.IncludeXmlComments("WebApi.xml"));//Creacion de archivo xml para documentacion



//CONFIGURACION DE JWT
var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE="; 

builder.Services.AddAuthentication(aut => 
{
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(aut => 
    {
        aut.RequireHttpsMetadata = false;
        aut.SaveToken = true; 
        aut.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });




// INYECCION DE DEPENDENCIAS
            //Repositorios
builder.Services.AddScoped<IRepositorioCaba�a, RepositorioCaba�a>();
builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
builder.Services.AddScoped<IRepositorioTipoCaba�a, RepositorioTipoCaba�a>();
builder.Services.AddScoped<IRepositorioParametros, RepositorioParametros>();
//INYECCION Casos de uso caba�a
builder.Services.AddScoped<IAltaCaba�a, CU_AltaCaba�a>();
builder.Services.AddScoped<IListarPorCantPersonas, CU_ListarPorCantPersonas>();
builder.Services.AddScoped<IListarPorHabilitadas, CU_ListarPorHabilitadas>();
builder.Services.AddScoped<IListarPorTexto, CU_ListarPorTexto>();
builder.Services.AddScoped<IListarPorTipo, CU_ListarPorTipo>();
builder.Services.AddScoped<IListarTodas, CU_ListarTodas>();
builder.Services.AddScoped<IBuscarCaba�aPorid, CU_BuscarCaba�aPorId>();
builder.Services.AddScoped<IListarPorTipoYMonto, CU_ListarPorTipoYMonto>();
//INYECCION Casos de uso tipo caba�as
builder.Services.AddScoped<IAltaTipoCaba�a, CU_AltaTipoCaba�a>();
builder.Services.AddScoped<IActualizarTipoCaba�a, CU_ActualizarTipoCaba�a>();
builder.Services.AddScoped<IBuscarPorNomre, CU_BuscarPorNombre>();
builder.Services.AddScoped<IEliminarTipoCaba�a, CU_BajaTipoCaba�a>();
builder.Services.AddScoped<IListarTiposCaba�as, CU_ListarTiposCaba�as>();
builder.Services.AddScoped<IBuscarTipoPorId, CU_BuscarPorId>();
//INYECCION Casos de uso mantenimientos
builder.Services.AddScoped<IAltaMantenimiento, CU_AltaMantenimiento>();
builder.Services.AddScoped<IListarMantenimientoPorCaba�a, CU_ListarMantenimientoPorCaba�a>();
builder.Services.AddScoped<IListarMantenimientoPorCaba�aYFecha, CU_ListarMantenimientoPorCaba�aYFecha>();
builder.Services.AddScoped<IObtenerMantenimientos, CU_ObtenerMantenimientos>();
builder.Services.AddScoped<IBuscarMantenimientoPorId, CU_ObtenerMantenimientoPorId>();
builder.Services.AddScoped<IObtenerMantenimientosPorValores, CU_ObtenerMantenimientosPorValores>();

//Usuarios
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<I_iniciarSesionUsuario, CU_IniciarSesionUsuario>();
//Parametros
builder.Services.AddScoped<IObtenerValorParam, CU_ObtenerValorParametro>();

//obtengo el string de conexion desde el archivo json
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
string miConexion = configuration.GetConnectionString("CadenaConexion");

builder.Services.AddDbContext<LibreriaContexto>(options => options.UseSqlServer(miConexion));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
