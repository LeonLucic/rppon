using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class FileSystemComponent
{
    public abstract void Open();
    public abstract void Close();
    public abstract void Delete();
}

public class File : FileSystemComponent
{
    private string fileName;

    public File(string name)
    {
        fileName = name;
    }

    public override void Open()
    {
        Console.WriteLine($"Opening file: {fileName}");
    }

    public override void Close()
    {
        Console.WriteLine($"Closing file: {fileName}");
    }

    public override void Delete()
    {
        Console.WriteLine($"Deleting file: {fileName}");
    }
}

public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> children = new List<FileSystemComponent>();
    private string directoryName;

    public Directory(string name)
    {
        directoryName = name;
    }

    public void Add(FileSystemComponent component)
    {
        children.Add(component);
    }

    public void Remove(FileSystemComponent component)
    {
        children.Remove(component);
    }

    public override void Open()
    {
        Console.WriteLine($"Opening directory: {directoryName}");
        foreach (var child in children)
        {
            child.Open();
        }
    }

    public override void Close()
    {
        Console.WriteLine($"Closing directory: {directoryName}");
        foreach (var child in children)
        {
            child.Close();
        }
    }

    public override void Delete()
    {
        Console.WriteLine($"Deleting directory: {directoryName}");
        foreach (var child in children)
        {
            child.Delete();
        }
    }
}

class Program
{
    static void Main()
    {
        var file1 = new File("file1.txt");
        var file2 = new File("file2.txt");
        var file3 = new File("file3.txt");

        var dir1 = new Directory("Folder1");
        var dir2 = new Directory("Folder2");

        dir1.Add(file1);
        dir1.Add(file2);
        dir2.Add(file3);

        var compositeDir = new Directory("CompositeFolder");
        compositeDir.Add(dir1);
        compositeDir.Add(dir2);

        compositeDir.Open();
        Console.WriteLine();
        compositeDir.Close();
        Console.WriteLine();
        compositeDir.Delete();
    }
}