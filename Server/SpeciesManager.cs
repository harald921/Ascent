using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public static class SpeciesManager
{
    static Dictionary<Creature.Species, Creature.Data> _speciesData = new Dictionary<Creature.Species, Creature.Data>();
    
    public static Creature.Data GetSpeciesData(Creature.Species inSpecies) =>
        _speciesData[inSpecies];


    // Constructor
    static SpeciesManager()
    {
        Console.WriteLine("Loading species data from disk...");
        string[] speciesFiles = Directory.GetFiles(Constants.Directory.SPECIES);
        foreach (string speciesFile in speciesFiles)
        {
            Creature.Data deserializedSpeciesData = JsonConvert.DeserializeObject<Creature.Data>(File.ReadAllText(speciesFile));
            _speciesData.Add(deserializedSpeciesData.species, deserializedSpeciesData);
        }
        
        // Log every templated species
        foreach (KeyValuePair<Creature.Species, Creature.Data> entry in _speciesData)
            Console.WriteLine(entry.Key.ToString());
    }



    public static void CreateNewSpecies(Creature.Data inData) =>
        File.WriteAllText(Constants.Directory.SPECIES + @"\Human.json", JsonConvert.SerializeObject(inData, Formatting.Indented));
}

