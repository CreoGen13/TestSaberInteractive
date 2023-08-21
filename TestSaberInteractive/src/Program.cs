namespace TestSaberInteractive;

using Base;

public class Program
{
    private const string Path = "list.txt";
    private static string GetRandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public static void Main(string [] args)
    {
        int length = 7;

        ListRand listRand = new ListRand();
        for (int i = 0; i < length; i++)
        {
            listRand.Add(GetRandomString(5));
            Console.WriteLine("Добавлен элемент " + listRand.Tail.Data);
        }
        ListRand newListRand = new ListRand();
        
        try
        {
            FileStream fs = new FileStream(Path, FileMode.OpenOrCreate);
            listRand.Serialize(fs);
            
            fs = new FileStream(Path, FileMode.Open);
            newListRand.Deserialize(fs);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка чтения файла");
            Console.WriteLine(e.Message);
            Console.Read();
            Environment.Exit(0);
        }

        var node = listRand.Head;
        var newNode = newListRand.Head;
        Console.WriteLine("Результат:");
        for (int i = 0; i < listRand.Count; i++)
        {
            Console.WriteLine("{0} ({1}) \t {2} ({3})", node.Data, node.Rand.Data, newNode.Data, newNode.Rand.Data);
            node = node.Next;
            newNode = newNode.Next;
        }
        Console.Read();

    }
}