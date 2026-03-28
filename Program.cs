using API.MiddleWare;
using Application;
using Application.IRepository;
using Application.IService;
using Application.Mapping;
using Application.Serializer;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ التحقق من الإعدادات الأساسية (مهم جداً)
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is missing");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is missing");
var connectionString = builder.Configuration.GetConnectionString("default") ?? throw new InvalidOperationException("Connection string is missing");

// Database Context
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString));

// Services Registration
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookCopyService, BookCopyService>();
builder.Services.AddScoped<IBorrowingRecordService, BorrowingRecordService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPaswordHasher, PasswordHasher>();
builder.Services.AddScoped<IWalletService , WalletService>();
builder.Services.AddScoped<IWalletTransactionService, WalletTransactionService>();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<IAIService, AIService>();
//builder.Services.AddHttpClient<IAIService,AIService>();

// HttpClient for AI Service
builder.Services.AddHttpClient<IAIService, AIService>(client =>
{
    //client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(100);
});

// AutoMapper
builder.Services.AddAutoMapper(profiles =>
{
    profiles.AddProfile<BookCopyProfile>();
    profiles.AddProfile<BookProfile>();
    profiles.AddProfile<UserProfile>();
    profiles.AddProfile<CategoryProfile>();
    profiles.AddProfile<BorrowingRecordProfile>();
    profiles.AddProfile<MemberProfile>();
    profiles.AddProfile<PublishProfile>();
    profiles.AddProfile<AuthorProfile>();
    profiles.AddProfile<WalletProfile>();
    profiles.AddProfile<WalletTransactionProfile>();
});

// ✅ API Controllers and Swagger (مرة واحدة فقط)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Response Caching
builder.Services.AddResponseCaching();

// ✅ CORS - مصححة بالكامل
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        // ✅ إزالة AllowAnyOrigin واستخدام WithOrigins فقط
        policy.WithOrigins("http://localhost:50302", "https://localhost:7060")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // مهم لـ Authentication
    });

    // ✅ أو إذا أردت السماح للجميع (للاختبار فقط)
    //options.AddPolicy("AllowAll", policy =>
    //{
    //    policy.AllowAnyOrigin()
    //          .AllowAnyHeader()
    //          .AllowAnyMethod();
    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(

        c =>
        {
            // Adjust this path if hosted in a virtual directory, e.g., "/myapi/swagger/v1/swagger.json"
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library V1");
        }
        );
}

// ✅ Middleware Pipeline - الترتيب مهم جداً
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseResponseCaching();

// ✅ CORS - قبل Authentication
app.UseCors("AllowAll");

// ✅ Authentication و Authorization - بالترتيب الصحيح
app.UseAuthentication(); // <-- يجب أن يكون قبل Authorization
app.UseAuthorization();

app.MapControllers();

// ✅ Endpoint للاختبار
app.MapGet("/", () => Results.Ok(new { message = "API! is Working  😎😎😎😎 ", status = "OK" }));

app.Run();