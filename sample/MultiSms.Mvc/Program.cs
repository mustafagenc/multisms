var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors",
                      builder => builder.WithOrigins(
                          "http://localhost",
                          "http://localhost:8100",
                          "http://192.168.1.99:8100",
                          "http://192.168.1.102:8100"
                      )
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
});


builder.Services.AddControllers();
builder.Services.AddMultiSms(options =>
{
    options.DefaultProvider = builder.Configuration.GetValue<string>("MultiSms:DefaultProvider"); //TwilioProvider.Name;
    options.DefaultOrginator = builder.Configuration.GetValue<string>("MultiSms:DefaultOrginator"); //"05355555555";
})
.UseNetGsm(
    username: builder.Configuration.GetValue<string>("MultiSms:NetGsm:Username"),
    password: builder.Configuration.GetValue<string>("MultiSms:NetGsm:Password"),
    orginator: builder.Configuration.GetValue<string>("MultiSms:NetGsm:Orginator")
)
.UseTwilio(
    username: builder.Configuration.GetValue<string>("MultiSms:Twilio:Username"),
    password: builder.Configuration.GetValue<string>("MultiSms:Twilio:Password")
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();