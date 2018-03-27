using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Species
{
    [JsonProperty]
    public readonly Data data;


    public Species(Data inData) =>
        data = inData;


    public struct Data
    {
        public Type type;
        public int baseHealth;
        public int baseMoveSpeed;
    }

    public enum Type
    {
        Human,
        Cow
    }
}