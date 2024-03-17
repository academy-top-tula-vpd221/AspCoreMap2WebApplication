var builder = WebApplication.CreateBuilder();
builder.Services.AddTransient<TimeNowService>();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


app.Use(async (context, next) =>
{
    Console.WriteLine("1 middlaware start\n");
    //await context.Response.WriteAsync("1 middlaware start\n");
    //if (context.Request.Path == "/stop")
    //    await context.Response.WriteAsync("Stop\n");
    //else
    //{
    await next.Invoke();
    Console.WriteLine("1 middlaware finish\n");
    if(context.Response.StatusCode == StatusCodes.Status404NotFound)
        await context.Response.WriteAsync("Resorce not found\n");
    //}

    //await context.Response.WriteAsync("1 middlaware finish\n");
});

//app.Map("/", () => 
//{ 
//    Console.WriteLine("Index work\n"); 
//    return "Hello World!"; 
//});


app.Use(async (context, next) =>
{
    //await context.Response.WriteAsync("2 middlaware start\n");
    Console.WriteLine("2 middlaware start\n");
    await next.Invoke();
    Console.WriteLine("2 middlaware finish\n");
    //await context.Response.WriteAsync("2 middlaware finish\n");
});

app.Map("/time", (TimeNowService timeNow) => {
    Console.WriteLine("Time work\n");
    return $"Current time: {timeNow.TimeNow}";
});

app.MapGet("/empl", (string? name, int? age) => $"Name {name}, Age {age}");

app.Run();



public class TimeNowService
{
    public string TimeNow => DateTime.Now.ToShortTimeString();
}
