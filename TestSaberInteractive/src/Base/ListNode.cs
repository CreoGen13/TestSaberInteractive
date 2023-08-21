namespace TestSaberInteractive.Base;

public class ListNode
{
    public ListNode Prev;
    public ListNode Next;
    public ListNode Rand;
    public string Data;

    public ListNode(string data)
    {
        Data = data;
    }
}