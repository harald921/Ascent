using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


// Handles serializing and deserializing of species
public static class SpeciesManager
{
    static Dictionary<Species.Type, Species> _speciesData = new Dictionary<Species.Type, Species>();
    public static Species GetSpecies(Species.Type inSpecies) => _speciesData[inSpecies];



    // Constructor
    static SpeciesManager() =>
        LoadAndDeserializeSpecies();

    public static void CreateNewSpecies(Species inSpecies) =>
        File.WriteAllText(Constants.Directory.SPECIES + @"\" + inSpecies.data.type.ToString() + ".json", JsonConvert.SerializeObject(inSpecies, Formatting.Indented));


    static void LoadAndDeserializeSpecies()
    {
        string[] speciesFilePaths = Directory.GetFiles(Constants.Directory.SPECIES);
        foreach (string speciesFilePath in speciesFilePaths)
            DeserializeAndAddToDictionary(speciesFilePath);
    }

    static void DeserializeAndAddToDictionary(string inFilePath)
    {
        string jsonText = File.ReadAllText(inFilePath);
        Species deserializedSpecies = JsonConvert.DeserializeObject<Species>(jsonText);
        _speciesData.Add(deserializedSpecies.data.type, deserializedSpecies);
    }
}

