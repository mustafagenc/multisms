namespace MultiSms.Provider.Test;

public static class EnvironmentVariable
{
    public static string Load(string name)
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process)
               ?? throw new ArgumentNullException(nameof(name), $"[{name}] adli degisken yuklenemedi");
    }
}

