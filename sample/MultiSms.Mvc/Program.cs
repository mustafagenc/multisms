var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors",
        builder => builder.WithOrigins("http://localhost")
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
.UseTwilio(
    username: builder.Configuration.GetValue<string>("MultiSms:Twilio:Username"),
    password: builder.Configuration.GetValue<string>("MultiSms:Twilio:Password")
)
.UseJetSms(
    username: builder.Configuration.GetValue<string>("MultiSms:JetSms:Username"),
    password: builder.Configuration.GetValue<string>("MultiSms:JetSms:Password"),
    orginator: builder.Configuration.GetValue<string>("MultiSms:JetSms:Orginator")
)
.UseNetGsm(
    username: builder.Configuration.GetValue<string>("MultiSms:NetGsm:Username"),
    password: builder.Configuration.GetValue<string>("MultiSms:NetGsm:Password"),
    orginator: builder.Configuration.GetValue<string>("MultiSms:NetGsm:Orginator")
)
.UseIletiMerkezi(
    username: builder.Configuration.GetValue<string>("MultiSms:IletiMerkezi:Username"),
    password: builder.Configuration.GetValue<string>("MultiSms:IletiMerkezi:Password"),
    orginator: builder.Configuration.GetValue<string>("MultiSms:IletiMerkezi:Orginator")
)
.UseMasGsm(
    username: builder.Configuration.GetValue<string>("MultiSms:MasGsm:Username"),
    orginator: builder.Configuration.GetValue<string>("MultiSms:MasGsm:Orginator"),
    password: builder.Configuration.GetValue<string>("MultiSms:MasGsm:Password")
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();