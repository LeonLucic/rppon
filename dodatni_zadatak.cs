using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//koristi se oblikovni obrazac "Flyweight"
interface ITextCharacter
{
    void Display(int fontSize);
}

class ConcreteTextCharacter : ITextCharacter
{
    private char character;
    private string font;
    private ConsoleColor color;

    public ConcreteTextCharacter(char character, string font, ConsoleColor color)
    {
        this.character = character;
        this.font = font;
        this.color = color;
    }

    public void Display(int fontSize)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"Character: {character}, Font: {font}, Size: {fontSize}");
        Console.ResetColor();
    }
}

class TextCharacterFactory
{
    private Dictionary<char, ITextCharacter> characters = new Dictionary<char, ITextCharacter>();

    public ITextCharacter GetCharacter(char character, string font, ConsoleColor color)
    {
        if (!characters.ContainsKey(character))
        {
            characters[character] = new ConcreteTextCharacter(character, font, color);
        }
        return characters[character];
    }
}

class Program
{
    static void Main()
    {
        TextCharacterFactory factory = new TextCharacterFactory();

        ITextCharacter charA = factory.GetCharacter('A', "Arial", ConsoleColor.Red);
        ITextCharacter charB = factory.GetCharacter('B', "Times New Roman", ConsoleColor.Blue);
        ITextCharacter charA2 = factory.GetCharacter('A', "Arial", ConsoleColor.Red);

        charA.Display(12);
        charB.Display(14);
        charA2.Display(16);
    }
}
