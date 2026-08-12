using Microsoft.EntityFrameworkCore;
using HouseHold.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// アプリ起動時にデータ登録
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();        // マイグレーション適用
    db.SeedImagesFromWwwroot();    // 画像登録
    db.SeedPaymentTypeData(db);           // PaymentType データ登録
    db.SeedPaymentMethods(db);   // PaymentMethod データ登録
    db.SeedIncomeClassData(db);   // IncomeClass データ登録
    db.SeedIncomeTypeData(db);    // IncomeType データ登録
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
