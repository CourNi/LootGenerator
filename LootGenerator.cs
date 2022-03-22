using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    [SerializeField]
    private List<RuleData> _ruleData = new List<RuleData>();
    [SerializeField]
    private Item.ItemTier _maxTier;
    [SerializeField]
    private bool _generateRandom = true;

    private List<IRule> _rules = new List<IRule>();
    private Dictionary<Item, int> _scenePool = new Dictionary<Item, int>();
    private RuleFactory _factory = new RuleFactory();

    private void Reset()
    {
        _ruleData.Clear();
    }

    private void Start()
    {
        foreach (RuleData data in _ruleData)
        {
            RuleData currentData = data;
            currentData.Tier = _maxTier;
            _rules.Add(_factory.CreateRule(currentData.Type, currentData));
        }
    }

    private void AddItemToScenePool(Item item, int amount)
    {
        if (_scenePool.TryGetValue(item, out int value)) _scenePool[item] = value + amount;
        else _scenePool.Add(item, amount);
    }

    public void AddRuleData()
    {
        _ruleData.Add(new RuleData());
    }

    public Inventory Generate(int size, int fill)
    {
        Inventory inventory = new Inventory();
        inventory.SetInventorySize(size);
        ItemContainer container;

        foreach (IRule rule in _rules)
        {
            if (rule.Evaluate(_scenePool, out container))
            {
                inventory.AddItemToInventory(container.Item, container.Amount);
                AddItemToScenePool(container.Item, container.Amount);
                fill--;
                if (fill == 0) return inventory;
            }
            else _rules.Remove(rule);
        }

        while (_generateRandom && fill > 0)
        {
            Item itemFromPool = InventoryManager.GetRandomItem(_maxTier);
            int amount = Random.Range(1, itemFromPool.Stack + 1);
            inventory.AddItemToInventory(itemFromPool, amount);
            AddItemToScenePool(itemFromPool, amount);
            fill--;
        }

        return inventory;
    }
}

