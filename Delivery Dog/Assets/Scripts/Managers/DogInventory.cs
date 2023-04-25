using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInventory : SingletonPersistent<DogInventory>
{
    private static Dictionary<Ingredient, int> collectedIngredients;
    public int MaxInventoryCount;
    public List<Ingredient> initialInventory;
    private void Start()
    {
        collectedIngredients = new Dictionary<Ingredient, int>();
        initialInventory.ForEach(AddIngredientToInventory);
    }
    public void AddIngredientToInventory(Ingredient ingredient)
    {
        // If inventory full exit the function.
        if (GetTotalInventoryCount() == MaxInventoryCount)
        {
            Debug.Log("Inventory full");
            return;
        }
        else if (!collectedIngredients.TryGetValue(ingredient, out int current))
        {
            collectedIngredients.Add(ingredient, 1);
        }
        else
        {
            collectedIngredients[ingredient] += 1;
        }
        Debug.Log($"Collected {ingredient.type}\n");
        //LogInventory();
        InGameUILogic.Instance.UpdateInventoryUI(collectedIngredients);
    }
    public int GetTotalInventoryCount()
    {
        int sum = 0;
        foreach (int count in collectedIngredients.Values)
        {
            sum += count;
        }
        return sum;
    }
    public bool InventoryContainsItem(Ingredient ingredient)
    {
        return collectedIngredients.ContainsKey(ingredient);
    }
    public void ClearInventory()
    {
        collectedIngredients.Clear();
        InGameUILogic.Instance.UpdateInventoryUI(collectedIngredients);
    }
    public void RemoveIngredientFromInventory(Ingredient ingredient)
    {
        Debug.Log($"Removing {ingredient.name} from inventory");
        // if count equals 1 (or is smaller, which shouldn't be possible), remove the entry
        if (collectedIngredients[ingredient] <= 1)
        {
            collectedIngredients.Remove(ingredient); // remove the key from the dictionary
        }
        else // if the count is larger
        {
            collectedIngredients[ingredient] -= 1; // subtract one from the count value
        }
        InGameUILogic.Instance.UpdateInventoryUI(collectedIngredients);
    }
    void LogInventory()
    {
        Debug.Log("Inventory content:\n");
        foreach (KeyValuePair<Ingredient, int> item in collectedIngredients)
        {
            Debug.Log($"{item.Key.type}: {item.Value}");
        }
    }
}
