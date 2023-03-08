using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using restAPI_Swagger_demo.API;
using restAPI_Swagger_demo.DB;
using restAPI_Swagger_demo.DBContext;
using restAPI_Swagger_demo.Depository;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);
//很重要-沒有swagger會異常
builder.Services.AddEndpointsApiExplorer();

//json patch註冊
//https://learn.microsoft.com/zh-tw/aspnet/core/web-api/jsonpatch?view=aspnetcore-7.0
builder.Services.AddControllers().AddNewtonsoftJson();

//swagger註冊
/*builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" })
);*/
builder.Services.AddSwaggerGen();

//MS SQ註冊
//builder.Services.AddDbContext<>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MSSql")));
//SQLite註冊
builder.Services.AddDbContext<SQLiteContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
//註冊自己的服務
//builder.Services.AddTransient<ICRUD, CRUD_Test>();
builder.Services.AddTransient<ICRUD_sqlite, CRUD_Test_sqlite>();

/************************************************************************/

var app = builder.Build();
//swagger
app.UseSwagger();
/*app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1")
);*/
app.UseSwaggerUI();
app.UseRouting();

//手動加路由
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));
//controller路由
app.MapControllers();

app.Run();
