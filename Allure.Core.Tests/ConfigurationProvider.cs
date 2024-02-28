using Allure.Core.Tests.ConfigModels;
using Allure.Logger;
using Newtonsoft.Json;

namespace Allure.Core.Tests;

public static class ConfigurationProvider<T> where T: BaseConfig
{
    public static T Configuration => InitializeConfiguration();
    private static NLog.Logger Logger => LoggingManager.GetInstance();

    private static T InitializeConfiguration()
    {
#if QA
            var json = File.ReadAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}config.qa.json");
#elif STAGE
            var json = File.ReadAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}config.stage.json");
#else
        var json = File.ReadAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}config.json");
#endif

        return JsonConvert.DeserializeObject<T>(json);
    }
}