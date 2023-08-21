namespace TestSaberInteractive.Base;

public class ListRand
{
    public ListNode Head;
    public ListNode Tail;
    public int Count;

    public void Serialize(FileStream s)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(s);
            writer.WriteLine(Count);

            var nodeDictionary = new Dictionary<ListNode, int>();

            int i = 0;
            for (var node = Head; node != null; node = node.Next)
            {
                nodeDictionary.Add(node, i);
                i++;
            }
            for (var node = Head; node != null; node = node.Next)
            {
                writer.WriteLine(node.Data);
                writer.WriteLine(nodeDictionary[node.Rand]);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Ошибка при попытке записи в файл");
        }
    }

    public void Deserialize(FileStream s)
    {
        try
        {
            using StreamReader reader = new StreamReader(s);
            int count = int.Parse(reader.ReadLine());
            
            int[] randArray = new int[count];
            ListNode[] nodeArray = new ListNode[count];
            for (int i = 0; i < count; i++)
            {
                nodeArray[i] = new ListNode(reader.ReadLine());
                randArray[i] = int.Parse(reader.ReadLine());
                
                if (i == 0)
                {
                    Head = nodeArray[i];
                }
                else
                {
                    Tail = nodeArray[i];
                    nodeArray[i].Prev = nodeArray[i - 1];
                    nodeArray[i - 1].Next = nodeArray[i];
                }
            }
            for (int i = 0; i < count; i++)
            {
                var node = nodeArray[i];
                node.Rand = nodeArray[randArray[i]];
            }
            Count = count;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine("Ошибка при попытке чтения файла");
        }
    }

    public void Add(string nodeData)
    {
        if (Head == null)
        {
            Head = new ListNode(nodeData);
            Tail = Head;
        }
        else
        {
            var newNode = new ListNode(nodeData)
            {
                Prev = Tail
            };

            Tail.Next = newNode;
            Tail = newNode;
        }
        
        Count++;
        
        RecalculateRands();
    }

    private void RecalculateRands()
    {
        var random = new Random();
        for (var node = Head; node != null; node = node.Next)
        {
            int i = 0;
            int randPosition = random.Next(0, Count);
            ListNode randNode = Head;
            while (i < randPosition)
            {
                randNode = randNode.Next;
                i++;
            }

            node.Rand = randNode;
        }
    }
}