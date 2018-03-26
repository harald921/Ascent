using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public static class CharacterManager
{
    static Dictionary<Character.Species, Character> _characterTemplates = new Dictionary<Character.Species, Character>();

    public static Character GetCharacterTemplate(Character.Species inSpecies) => _characterTemplates[inSpecies];

    public static void DoTheThing()
    {
        string[] characterFiles = Directory.GetFiles(Constants.Directory.CHARACTERS);
        foreach (string characterFile in characterFiles)
        {
            Character deserializedCharacter = JsonConvert.DeserializeObject<Character>(File.ReadAllText(characterFile));
            _characterTemplates.Add(deserializedCharacter.species, deserializedCharacter);
        }

        foreach (KeyValuePair<Character.Species, Character> entry in _characterTemplates)
            Console.WriteLine(entry.Key.ToString());

        // Character deserializedCharacter = JsonConvert.DeserializeObject<Character>(System.IO.File.ReadAllText(Constants.Directory.CHARACTERS + @"/Human.json"));
        // Console.WriteLine(JsonConvert.SerializeObject(new Character(Character.Species.Human), Formatting.Indented));
    }
}

