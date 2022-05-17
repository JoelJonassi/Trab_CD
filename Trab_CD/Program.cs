using JobShopAPI;
using JobShopAPI.Data;
using JobShopAPI.Mappings;
using JobShopAPI.Repository;
using JobShopAPI.Repository.Interfaces;
using JobShopAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var app = Startup.InitializeApp(args);

app.Run();
