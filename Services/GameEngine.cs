using Microsoft.EntityFrameworkCore;
using System.Linq;
using W9_assignment_template.Data;
using W9_assignment_template.Models;


namespace W9_assignment_template.Services;

public class GameEngine
{
    private readonly GameContext _context;

    public GameEngine(GameContext context)
    {
        _context = context;
    }

    public void DisplayRooms()
    {
        var rooms = _context.Rooms.Include(r => r.Characters).ToList();

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    public void AddRoom()
    {
        Console.Write("Enter room name: ");
        var name = Console.ReadLine();

        Console.Write("Enter room description: ");
        var description = Console.ReadLine();

        var room = new Room
        {
            Name = name,
            Description = description
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();

        Console.WriteLine($"Room '{name}' added to the game.");
    }

    public void DisplayCharacters()
    {
        var characters = _context.Characters.ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }

    public void AddCharacter()
    {
        Console.Write("Enter character name: ");
        var name = Console.ReadLine();

        Console.Write("Enter character level: ");
        var level = int.Parse(Console.ReadLine());

        Console.Write("Enter room ID for the character: ");
        var roomId = int.Parse(Console.ReadLine());

        var newCharacter = new Character { Name = name, Level = level, RoomId = roomId };

        var roomList = _context.Rooms.ToList();
        bool existFlag = roomList.Any(r => r.Id == roomId );

        if (existFlag == true)
        {
            _context.Characters.AddRange(newCharacter);
            _context.SaveChanges();
            Console.WriteLine($"{name} was added to Room {roomId}");
        }
        else
        {
            Console.WriteLine("Room does not exist.");
        }

    }

    public void FindCharacter()
    {
        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine();

        var foundChar = _context.Characters.Where(c => c.Name == name)
                                            .ToList();

        if (foundChar.Any() == true)
        {
            foreach (var character in foundChar)
            {
                Console.WriteLine($"\nName: {character.Name}, Character ID: {character.Id}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("Character was not found.");
        }
        // TODO Find the character by name
        // Use LINQ to query the database for the character
        // If the character exists, display the character's information
        // Otherwise, display a message indicating the character was not found
    }
}