public class ItemContainer
{
    private readonly Item _item;
    private readonly int _amount;

    public ItemContainer(Item item, int amount)
    {
        _item = item;
        _amount = amount;
    }

    public Item Item { get => _item; }
    public int Amount { get => _amount; }
}