## AddScoped vs AddTransient vs AddSingleton in ASP .NET Core MVC | ASPNETCOREMVC.
## Base de Datos CrudApDbFirst sql Sql Server
### Tecnologías utilizadas


<details> 

<summary>Ver Tecnologías utilizadas</summary>

- C#
- .NET 10
- MVC Client Razor

</details>

<details>

<summary>Ver Video</summary>

```
https://www.youtube.com/watch?v=vDJMzs1OYqo

```

</details>

<details>

<summary>Ver Prueba  AddScoped vs AddTransient vs AddSingleto</summary>


  ![Sin título](MVCNetCore_Service_Scope_Transient_Singleton/Images/Sin%20t%C3%ADtulo.png)

</details>

<details> 

<summary>Ver ServiceController</summary>

```
using Microsoft.AspNetCore.Mvc;
using MVCNetCore_Service_Scope_Transient_Singleton.Models;

namespace MVCNetCore_Service_Scope_Transient_Singleton.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IMyServiceSingleton _myServiceSingleton;
        private readonly IMyServiceSingleton _myServiceSingleton2;
        private readonly IMyServiceTransient _myServiceTransient;
        private readonly IMyServiceTransient _myServiceTransient2;
        private readonly IMyServiceScoped _myServiceScoped;
        private readonly IMyServiceScoped _myServiceScoped2;


        //public ServiceController(IMyServiceSingleton myServiceSingleton)
        //{   this._myServiceSingleton = myServiceSingleton;}

        public ServiceController(IMyServiceSingleton myServiceSingleton,  IMyServiceSingleton myServiceSingleton2
            , IMyServiceTransient myServiceTransient, IMyServiceTransient myServiceTransient2
            , IMyServiceScoped myServiceScoped, IMyServiceScoped myServiceScoped2)

        {
            this._myServiceSingleton = myServiceSingleton;
            this._myServiceSingleton2 = myServiceSingleton2;

            this._myServiceTransient = myServiceTransient;
            this._myServiceTransient2 = myServiceTransient2;

            this._myServiceScoped = myServiceScoped;
            this._myServiceScoped2 = myServiceScoped2;

        }
        public IActionResult Index()
        {
            ViewBag.SingletonMessage = _myServiceSingleton.GetMessage();
            ViewBag.SingletonCount = _myServiceSingleton.GetCount();
            ViewBag.SingletonMessage2 = _myServiceSingleton2.GetMessage();
            ViewBag.SingletonCount2 = _myServiceSingleton2.GetCount();

            ViewBag.TransientMessage = _myServiceTransient.GetMessage();
            ViewBag.TransientCount = _myServiceTransient.GetCount();
            ViewBag.TransientMessage2 = _myServiceTransient2.GetMessage();
            ViewBag.TransientCount2 = _myServiceTransient2.GetCount();

            ViewBag.ScopedMessage = _myServiceScoped.GetMessage();
            ViewBag.ScopedCount = _myServiceScoped.GetCount();
            ViewBag.ScopedMessage2 = _myServiceScoped2.GetMessage();
            ViewBag.ScopedCount2 = _myServiceScoped2.GetCount();

            return View();
        }
    }
}

```

</details>

<details>

<summary>Ver Model  MyService</summary>

```
namespace MVCNetCore_Service_Scope_Transient_Singleton.Models
{
    //Mantiene el 0bjeto  cada vez que hau una peticio HTTP o un request
    public interface IMyServiceSingleton
    {
        string GetMessage();
        int GetCount();
    }

    //Es lo opuesto a Singleton destruye los objetos cada vez peticio HTTP o un request
    public interface IMyServiceTransient
    {
        string GetMessage();
        int GetCount();
    }

    public interface IMyServiceScoped
    {
        string GetMessage();
        int GetCount();
    }

    public class MyService : IMyServiceSingleton, IMyServiceTransient , IMyServiceScoped
    //, IMyServiceScoped
    {
        private int _count;

        private string _message ;
        public MyService()
        {
            _count = 0;
            _message = $"Hello from MyService!{DateTime.Now}";
        }
        public string GetMessage()
        {
            return _message;
        }
        public int GetCount()
        {
            return _count++;
        }

    }
}


```

</details>

<details>

<summary>Ver Views  Index.cshtml</summary>

```
<table class="table table-bordered">
    <thead>
        <tr>
            <th>#</th>
            <th>Add Singleton-It creates a single object and reuses it throughout the entire lifetime of the application.</th>
            <th>Add Trasient-Creates a new object each time it is requested.</th>
            <th>Add Scoped-Create an object for each HTTP request.</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td scope="row">1.</td>
            <td>@ViewBag.SingletonMessage <br /> Count: @ViewBag.SingletonCount</td>
            <td>@ViewBag.TransientMessage <br /> Count: @ViewBag.TransientCount</td>
            <td>@ViewBag.ScopedMessage <br /> Count: @ViewBag.ScopedCount</td>
        </tr>

        <tr>
            <td scope="row">2.</td>
            <td>@ViewBag.SingletonMessage2 <br /> Count: @ViewBag.SingletonCount2</td>
            <td>@ViewBag.TransientMessage2 <br /> Count: @ViewBag.TransientCount2</td>
            <td>@ViewBag.ScopedMessage2 <br /> Count: @ViewBag.ScopedCount2</td>
        </tr>

    </tbody>
    </table>

```

</details>

<details>

<summary>Ver Views  Program.cs</summary>

```

using MVCNetCore_Service_Scope_Transient_Singleton.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IMyServiceSingleton, MyService>();
builder.Services.AddTransient<IMyServiceTransient, MyService>();
builder.Services.AddScoped<IMyServiceScoped, MyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Service}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

```

</details>

<details> 

<summary>Ver Conclusion</summary>

| Característica | AddTransient | AddScoped | AddSingleton |
|---|---|---|---|
| **Creación** | Cada vez que se solicita | Una por solicitud HTTP | Una para toda la aplicación |
| **Reutilización** | No | Dentro de la solicitud | Entre todas las solicitudes |
| **Usuarios** | Instancias nuevas | Instancia por solicitud | Comparten instancia |
| **Uso habitual** | Servicios pequeños | DbContext y servicios web | Caché compartida y servicios sin estado |


## 1. Transient — `AddTransient`

**Crea un objeto nuevo cada vez que se solicita.**

- Ejemplo: si solicitas el servicio 3 veces, se crean 3 instancias.
- Cada vez que el contenedor de dependencias necesite `ServicesTransient`, creará una nueva instancia.

```csharp
builder.Services.AddTransient<ServicesTransient>();
```

---

## 2. Scoped — `AddScoped`

**Crea un objeto por cada solicitud HTTP.**

En una aplicación MVC, cuando un usuario entra a una página, se produce una solicitud HTTP.

| Solicitud HTTP 1 | Solicitud HTTP 2 |
|---|---|
| Objeto A | Objeto B |
| Se reutiliza dentro de la misma solicitud. | Se crea otro objeto para la siguiente solicitud. |

- Cada solicitud HTTP tiene su propio objeto Scoped.
- Es el tipo que utiliza normalmente Entity Framework Core para `DbContext` cuando usas `AddDbContext`.

```csharp
builder.Services.AddScoped<ServicesScoped>();
```

---

## 3. Singleton — `AddSingleton`

**Crea un único objeto y lo reutiliza durante toda la vida de la aplicación.**

### Aplicación ASP.NET Core

- Único objeto A.
- Usuario 1, Usuario 2 y Usuario 3 comparten la misma instancia.

Aunque entren 100 usuarios, el servicio Singleton será la misma instancia para todos, mientras la aplicación siga funcionando.

```csharp
builder.Services.AddSingleton<ServicesSingleton>();
```
