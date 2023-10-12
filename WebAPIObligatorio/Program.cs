using CasosUso.CU_AlquilerCaba�a.CasosUso;
using CasosUso.CU_AlquilerCaba�a.InterfacesCU;
using CasosUso.CU_Caba�a.CasosUso;
using CasosUso.CU_Caba�a.InterfacesCU;
using CasosUso.CU_Mantenimiento.CasosUso;
using CasosUso.CU_Mantenimiento.InterfacesCU;
using CasosUso.CU_Parametros;
using CasosUso.CU_TipoCaba�a.CasosUso;
using CasosUso.CU_TipoCaba�a.InterfacesCU;
using CasosUso.CU_Usuario.CUInterfaces;
using CasosUso.CU_Usuario.UserCases;
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
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioAlquilerCaba�a,RepositorioAlquilerCaba�a>() ;

//INYECCION Casos de uso caba�a
builder.Services.AddScoped<IAltaCaba�a, CU_AltaCaba�a>();
builder.Services.AddScoped<IBuscarCaba�aPorid, CU_BuscarCaba�aPorId>();
builder.Services.AddScoped<IEliminarCaba�a, CU_EliminarCaba�a>();
builder.Services.AddScoped<IListarEnRangoFechas, CU_ListarEnRangoFechas>();
builder.Services.AddScoped<IListarPorCantPersonas, CU_ListarPorCantPersonas>();
builder.Services.AddScoped<IListarPorNOHabilitadas, CU_ListarPorNOHabilitadas>();
builder.Services.AddScoped<IListarPorMonto, CU_ListarPorMonto>();
builder.Services.AddScoped<IListarPorTexto, CU_ListarPorTexto>();
builder.Services.AddScoped<IListarPorTipo, CU_ListarPorTipo>();
builder.Services.AddScoped<IListarTodas, CU_ListarTodas>();
builder.Services.AddScoped<IConvertCaba�aToDTO, CU_ConvertCaba�aToDTO>();
builder.Services.AddScoped<IEditarCaba�a, CU_EditarCaba�a>();
builder.Services.AddScoped<IHabilitarCaba�a, CU_HabilitarCaba�a>();
builder.Services.AddScoped<IDeshabilitarCaba�a, CU_DeshabilitarCaba�a>();
//INYECCION Casos de uso tipo caba�as
builder.Services.AddScoped<IActualizarTipoCaba�a, CU_ActualizarTipoCaba�a>();
builder.Services.AddScoped<IAltaTipoCaba�a, CU_AltaTipoCaba�a>();
builder.Services.AddScoped<IEliminarTipoCaba�a, CU_BajaTipoCaba�a>();
builder.Services.AddScoped<IBuscarTipoPorId, CU_BuscarPorId>();
builder.Services.AddScoped<IBuscarPorNomre, CU_BuscarPorNombre>();
builder.Services.AddScoped<IListarTiposCaba�as, CU_ListarTiposCaba�as>();
//INYECCION Casos de uso mantenimientos
builder.Services.AddScoped<IAltaMantenimiento, CU_AltaMantenimiento>();
builder.Services.AddScoped<IListarMantenimientoPorCaba�a, CU_ListarMantenimientoPorCaba�a>();
builder.Services.AddScoped<IListarMantenimientoPorCaba�aYFecha, CU_ListarMantenimientoPorCaba�aYFecha>();
builder.Services.AddScoped<IObtenerMantenimientos, CU_ObtenerMantenimientos>();
builder.Services.AddScoped<IBuscarMantenimientoPorId, CU_ObtenerMantenimientoPorId>();
builder.Services.AddScoped<IObtenerMantenimientosPorValores, CU_ObtenerMantenimientosPorValores>();
//INYECCION Casos de uso Alquileres
builder.Services.AddScoped<IAltaAlquilerCaba�a, CU_AltaAlquilerCaba�a>();
builder.Services.AddScoped<IEliminarAlquilerCaba�a, CU_BajaAlquilerCaba�a>();
builder.Services.AddScoped<IEditarAlquilerCaba�a, CU_EditarAlquilerCaba�a>();
builder.Services.AddScoped<IBuscarAlquilerPorId, CU_BuscarAlquilerPorId>();
builder.Services.AddScoped<IListarAlquileresDeMiCaba�aDue�o, CU_ListarAlquileresDeMiCaba�aDue�o>();
builder.Services.AddScoped<IListarAlquileresRealizadosPorUsuario, CU_ListarAlquileresRealizadosPorUsuario>();
//Usuarios
builder.Services.AddScoped<I_iniciarSesionUsuario, CU_IniciarSesionUsuario>();
builder.Services.AddScoped<I_RegistrarUsuario, CU_RegistrarUsuario>();
builder.Services.AddScoped<I_ListarUsuarios, CU_ListarUsuarios>();
builder.Services.AddScoped<IGetUsuarioByEmail,CU_GetUsuarioByEmail>();
builder.Services.AddScoped<IListarCaba�asListadasPorDue�o, CU_ListarCaba�asListadasPorDue�o>();
builder.Services.AddScoped<IConvertUserToDTO, CU_ConvertUserToDTO>();
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

//app.UseAuthorization();

app.MapControllers();

app.Run();
