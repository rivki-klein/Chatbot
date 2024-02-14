using BasePlugin;
using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Multiplication
{ 

    record PersistentDataStructure(int result);
    public class MultiplicationGame : IPlugin
    {
        public static string _Id = "MultiplicationGame";
        public string Id => Id;
 
        public PluginOutput Execute(PluginInput input)
        {
            Random random = new Random();

          
            if (input.Message == "")
            {
                input.Callbacks.StartSession();
                return new PluginOutput("Game started.Enter 'new' for new exercise. Enter 'Exit' to stop.");
            }
            else if (input.Message.ToLower() == "exit")
            {
                input.Callbacks.EndSession();
                return new PluginOutput("Game stopped.");
            }
            else if (input.Message.ToLower() == "new")
            {

                int num1 = random.Next(1, 10);
                int num2 = random.Next(1, 10);
            
                int result = num1 * num2;
                var data = new PersistentDataStructure(result);
                return new PluginOutput($"{num1}*{num2}=", JsonSerializer.Serialize(data));
            }
            else if (int.TryParse(input.Message, out int useResult))
            {
                int result = JsonSerializer.Deserialize<PersistentDataStructure>(input.PersistentData).result;

                if (useResult != result)
                {
                    return new PluginOutput($"The result is error. Enter correct result.",JsonSerializer.Serialize(new PersistentDataStructure(result)));
                }
                else
                {
           
                    return new PluginOutput("Excellent!!!!");
                }
            }
            else
                return new PluginOutput("Enter something legal");





        }
    }
}

