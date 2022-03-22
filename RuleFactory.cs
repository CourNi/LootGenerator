using System.Collections.Generic;

public class RuleFactory
{
    private Dictionary<RuleType, System.Func<RuleData, IRule>> _ruleDictionary = new Dictionary<RuleType, System.Func<RuleData, IRule>>()
    {
        { RuleType.Contain, (data) => new ContainRule(data) },
        { RuleType.Amount, (data) => new AmountRule(data)  },
        { RuleType.Random, (data) => new RandomRule(data)  }
    };

    public IRule CreateRule(RuleType type, RuleData data)
    {
        return _ruleDictionary[type](data);
    }
}
