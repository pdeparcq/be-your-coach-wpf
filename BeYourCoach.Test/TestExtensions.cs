using System.Security.Policy;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BeYourCoach.Test
{
    public static class TestExtensions
    {
        public static void PrettyPrint(this object o)
        {
            System.Console.Out.WriteLine(JsonConvert.SerializeObject(o, Formatting.Indented, new StringEnumConverter()));
        }
    }
}
