using System.Collections.Generic;

public enum RuleType { Contain, Amount, Random }

public class ContainRule : IRule
{
    private readonly Item _item;

    public ContainRule(RuleData data)
    {
        _item = data.Item;
    }

    public bool Evaluate(Dictionary<Item, int> itemsOnScene, out ItemContainer result)
    {
        if (!itemsOnScene.ContainsKey(_item))
        {
            result = new ItemContainer(_item, 1);
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }
}

public class AmountRule : IRule
{
    private readonly Item _item;
    private readonly int _amount;

    public AmountRule(RuleData data)
    {
        _item = data.Item;
        _amount = data.Amount;
    }

    public bool Evaluate(Dictionary<Item, int> itemsOnScene, out ItemContainer result)
    {
        if (itemsOnScene.TryGetValue(_item, out int value) && value > _amount)
        {
            result = null;
            return false;
        }
        else
        {
            result = new ItemContainer(_item, UnityEngine.Random.Range(1, _item.Stack + 1));
            return true;
        }
    }
}

public class RandomRule : IRule
{
    private readonly Item.ItemType _pool;
    private readonly Item.ItemTier _tier;

    public RandomRule(RuleData data)
    {
        _pool = data.Pool;
        _tier = data.Tier;
    }

    public bool Evaluate(Dictionary<Item, int> itemsOnScene, out ItemContainer result)
    {
        Item itemFromPool = InventoryManager.GetItemByType(_pool, _tier);
        result = new ItemContainer(itemFromPool, UnityEngine.Random.Range(1, itemFromPool.Stack + 1));
        return true;
    }
}