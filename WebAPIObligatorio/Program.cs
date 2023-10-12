using CasosUso.CU_AlquilerCabaña.CasosUso;
using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using CasosUso.CU_Cabaña.CasosUso;
using CasosUso.CU_Cabaña.InterfacesCU;
using CasosUso.CU_Mantenimiento.CasosUso;
using CasosUso.CU_Mantenimiento.InterfacesCU;
using CasosUso.CU_Parametros;
using CasosUso.CU_TipoCabaña.CasosUso;
using CasosUso.CU_TipoCabaña.InterfacesCU;
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
builder.Services.AddScoped<IRepositorioCabaña, RepositorioCabaña>();
builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
builder.Services.AddScoped<IRepositorioTipoCabaña, RepositorioTipoCabaña>();
builder.Services.AddScoped<IRepositorioParametros, RepositorioParametros>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioAlquilerCabaña,RepositorioAlquilerCabaña>() ;

//INYECCION Casos de uso cabaña
builder.Services.AddScoped<IAltaCabaña, CU_AltaCabaña>();
builder.Services.AddScoped<IBuscarCabañaPorid, CU_BuscarCabañaPorId>();
builder.Services.AddScoped<IEliminarCabaña, CU_EliminarCabaña>();
builder.Services.AddScoped<IListarEnRangoFechas, CU_ListarEnRangoFechas>();
builder.Services.AddScoped<IListarPorCantPersonas, CU_ListarPorCantPersonas>();
builder.Services.AddScoped<IListarPorNOHabilitadas, CU_ListarPorNOHabilitadas>();
builder.Services.AddScoped<IListarPorMonto, CU_ListarPorMonto>();
builder.Services.AddScoped<IListarPorTexto, CU_ListarPorTexto>();
builder.Services.AddScoped<IListarPorTipo, CU_ListarPorTipo>();
builder.Services.AddScoped<IListarTodas, CU_ListarTodas>();
builder.Services.AddScoped<IConvertCabañaToDTO, CU_ConvertCabañaToDTO>();
builder.Services.AddScoped<IEditarCabaña, CU_EditarCabaña>();
builder.Services.AddScoped<IHabilitarCabaña, CU_HabilitarCabaña>();
builder.Services.AddScoped<IDeshabilitarCabaña, CU_DeshabilitarCabaña>();
//INYECCION Casos de uso tipo cabañas
builder.Services.AddScoped<IActualizarTipoCabaña, CU_ActualizarTipoCabaña>();
builder.Services.AddScoped<IAltaTipoCabaña, CU_AltaTipoCabaña>();
builder.Services.AddScoped<IEliminarTipoCabaña, CU_BajaTipoCabaña>();
builder.Services.AddScoped<IBuscarTipoPorId, CU_BuscarPorId>();
builder.Services.AddScoped<IBuscarPorNomre, CU_BuscarPorNombre>();
builder.Services.AddScoped<IListarTiposCabañas, CU_ListarTiposCabañas>();
//INYECCION Casos de uso mantenimientos
builder.Services.AddScoped<IAltaMantenimiento, CU_AltaMantenimiento>();
builder.Services.AddScoped<IListarMantenimientoPorCabaña, CU_ListarMantenimientoPorCabaña>();
builder.Services.AddScoped<IListarMantenimientoPorCabañaYFecha, CU_ListarMantenimientoPorCabañaYFecha>();
builder.Services.AddScoped<IObtenerMantenimientos, CU_ObtenerMantenimientos>();
builder.Services.AddScoped<IBuscarMantenimientoPorId, CU_ObtenerMantenimientoPorId>();
builder.Services.AddScoped<IObtenerMantenimientosPorValores, CU_ObtenerMantenimientosPorValores>();
//INYECCION Casos de uso Alquileres
builder.Services.AddScoped<IAltaAlquilerCabaña, CU_AltaAlquilerCabaña>();
builder.Services.AddScoped<IEliminarAlquilerCabaña, CU_BajaAlquilerCabaña>();
builder.Services.AddScoped<IEditarAlquilerCabaña, CU_EditarAlquilerCabaña>();
builder.Services.AddScoped<IBuscarAlquilerPorId, CU_BuscarAlquilerPorId>();
builder.Services.AddScoped<IListarAlquileresDeMiCabañaDueño, CU_ListarAlquileresDeMiCabañaDueño>();
builder.Services.AddScoped<IListarAlquileresRealizadosPorUsuario, CU_ListarAlquileresRealizadosPorUsuario>();
//Usuarios
builder.Services.AddScoped<I_iniciarSesionUsuario, CU_IniciarSesionUsuario>();
builder.Services.AddScoped<I_RegistrarUsuario, CU_RegistrarUsuario>();
builder.Services.AddScoped<I_ListarUsuarios, CU_ListarUsuarios>();
builder.Services.AddScoped<IGetUsuarioByEmail,CU_GetUsuarioByEmail>();
builder.Services.AddScoped<IListarCabañasListadasPorDueño, CU_ListarCabañasListadasPorDueño>();
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
