using System.Collections.Generic;

public interface IRule
{
    ItemContainer Evaluate(Dictionary<Item, int> dict);
}

public class ItemContainer
{
    public Item item { get; set; }
    public int amount { get; set; }
}

[System.Serializable]
public struct RuleData
{
    public RuleFactory.RuleType type;
    public Item item;
    public Item.ItemType pool;
    public Item.ItemTier tier;
    public int amount;
}