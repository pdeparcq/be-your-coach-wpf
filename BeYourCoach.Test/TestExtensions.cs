using System;
using Deparcq.Common.Json;
using Newtonsoft.Json;

namespace BeYourCoach.Test
{
    public static class TestExtensions
    {
        public static void PrettyPrint(this object o)
        {
            Console.Out.WriteLine(o.Serialize(Formatting.Indented));
        }

        public static void Times(this int count, Action<int> action)
        {
            for (var i = 0; i < count; i++)
            {
                action(i);
            }
        }
    }
}
