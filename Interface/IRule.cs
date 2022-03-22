using System.Collections.Generic;

public interface IRule
{
    bool Evaluate(Dictionary<Item, int> itemsOnScene, out ItemContainer container);
}