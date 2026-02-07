
/// <summary>
/// <summary>
/// Character Data Tool : A simple tool to manage character data. Can create, edit, and delete characters.
/// </summary>
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;




//build path to json file
var filePath = Path.Combine("data", "characters.json");

//make sure the file exists
if (!File.Exists(filePath))
{
    Console.WriteLine($"File not found: {filePath}");
    return;
}


//read the json text
string jsonText = File.ReadAllText(filePath);

//deserialize into a list of characters
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};


List<Character>? characters = JsonSerializer.Deserialize<List<Character>>(jsonText, options);

if (characters == null)
{
    Console.WriteLine("No characters found.");
    return;
}

Console.WriteLine("Characters found:");
foreach (var character in characters)
{
    Console.WriteLine($"{character.Name} - {character.Class} - Level {character.Level}");
}


ShowMenu();

void ShowMenu()
{
    Console.WriteLine("What do you want to do?");


    Console.WriteLine("1. Add Character");
    Console.WriteLine("2. Edit Character");
    Console.WriteLine("3. Delete Character");
    Console.WriteLine("4. Exit");
    string? addCharacterResponse = Console.ReadLine();

    if (addCharacterResponse != null && addCharacterResponse == "1")
    {
        AddCharacter(characters);
    }

    else if (addCharacterResponse != null && addCharacterResponse == "2")
    {
        EditCharacter(characters);
    }

    else if (addCharacterResponse != null && addCharacterResponse == "3")
    {
        DeleteCharacter(characters);
    }

    else if (addCharacterResponse != null && addCharacterResponse == "4")
    {
        Console.WriteLine("Exiting...");
        return;
    }
    else
    {
        Console.WriteLine("Invalid option. Please try again.");
    }

}




void AddCharacter(List<Character> characters)
{
    Console.WriteLine("Enter the character's name:");
    string? name = Console.ReadLine();

    Console.WriteLine("Enter the character's class:");
    string? className = Console.ReadLine();

    Console.WriteLine("Enter the character's level:");
    string? levelInput = Console.ReadLine();
    int level = 1;

    if (!int.TryParse(levelInput, out level))
    {
        Console.WriteLine("Invalid level. Please enter a valid number:");
    }

    //Create and add the new character
    if (name != null && className != null)
    {
        var newCharacter = new Character
        {
            Name = name,
            Class = className,
            Level = level
        };

        characters.Add(newCharacter);
    }
    else
    {
        Console.WriteLine("Name and class cannot be empty.");
    }

    //serialize the list of characters back to json
    var writeOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string updatedJson = JsonSerializer.Serialize(characters, writeOptions);

    //write the updated json back to the file
    File.WriteAllText(filePath, updatedJson);

    Console.WriteLine("Character added successfully.");

}




void EditCharacter(List<Character> characters)
{
    Console.WriteLine("Enter the name of the character you want to edit:");

    int number = 1;

    foreach (var character in characters)
    {
        Console.WriteLine($"{number}. {character.Name}");
        number++;
    }

    if (int.TryParse(Console.ReadLine(), out int index))
    {
        if (index > 0 && index <= characters.Count)
        {
            var character = characters[index - 1];

            Console.WriteLine($"Editing character: {character.Name}");

            Console.WriteLine("Enter the new name (leave blank to keep current):");
            string? newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                character.Name = newName;
            }
            else
            {
                Console.WriteLine("Name cannot be empty. Keeping current name.");
            }

            Console.WriteLine("Enter the new class (leave blank to keep current):");
            string? newClass = Console.ReadLine();
            if (!string.IsNullOrEmpty(newClass))
            {
                character.Class = newClass;
            }
            else
            {
                Console.WriteLine("Class cannot be empty. Keeping current class.");
            }

            Console.WriteLine("Enter the new level (leave blank to keep current):");
            string? newLevelInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newLevelInput))
            {
                if (int.TryParse(newLevelInput, out int newLevel))
                {
                    character.Level = newLevel;
                }
                else
                {
                    Console.WriteLine("Invalid level. Keeping current level.");
                }
            }

            //serialize the list of characters back to json
            var writeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string updatedJson = JsonSerializer.Serialize(characters, writeOptions);

            //write the updated json back to the file
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine("Character updated successfully.");
        }
    }

}


void DeleteCharacter(List<Character> characters)
{
    //first ask which character to delete
    Console.WriteLine("Enter the number of the character you want to delete:");

    //create a list of the character names
    int number = 1;
    foreach (var character in characters)
    {
        Console.WriteLine($"{number}. {character.Name}");
        number++;
    }

    string? input = Console.ReadLine();
    if (int.TryParse(input, out int index))
    {
        if (index > 0 && index <= characters.Count)
        {
            characters.RemoveAt(index - 1);

            //serialize the list of characters back to json
            var writeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string updatedJson = JsonSerializer.Serialize(characters, writeOptions);

            //write the updated json back to the file
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine("Character deleted successfully.");
        }
    }
}



//The character class that contains the properties of a character
public class Character
{
    public string Name { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public int Level { get; set; }
}


