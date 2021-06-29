using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AnagramSolver.Contracts
{
    public class DBConnectionConfig
    {
        public const string DBConnection = "DBConnection";
        public string Database { get; set; }

        //Doesn't work
        //[JsonProperty("Integrated_security")]
        //[JsonPropertyName("Integrated_security")]
        public bool Integrated_security { get; set; }

        public string Server { get; set; }
    }
}
