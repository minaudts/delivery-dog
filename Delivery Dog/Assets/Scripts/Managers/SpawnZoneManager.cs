using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpawnZoneManager : SingletonPersistent<SpawnZoneManager>
{
    public ItemSpawnZone[] spawnZones;
    public Ingredient[] possibleIngredients;
    // This list starts the same as possible ingredients but items will be removed until empty.
    // When empty, any ingredient can be added to zone.
    private static List<Ingredient> ingredientsToChooseFrom;
    public static int itemsPerSpawnZone = 2;
    // Start is called before the first frame update
    void Start()
    {
        ingredientsToChooseFrom = possibleIngredients.ToList();
        RandomizeLevel();
    }

    public List<IngredientWithWeight[]> GetIngredientsForAllSpawnZones()
    {
        List<IngredientWithWeight[]> res = new List<IngredientWithWeight[]>();
        foreach(ItemSpawnZone zone in spawnZones)
        {
            res.Add(zone.GetIngredients());
        }
        return res;
    } 

    public void RandomizeLevel()
    {
        ShuffleList(ingredientsToChooseFrom);
        foreach (ItemSpawnZone zone in spawnZones)
        {
            // List of ingredients that will be added to current zone
            List<IngredientWithWeight> ingredientsToAdd = new List<IngredientWithWeight>();
            int ingredientCount = 0;
            while (ingredientsToAdd.Count < itemsPerSpawnZone)
            {
                IngredientWithWeight ingredient;
                // Pick random ingredient and add it
                if (ingredientCount == 0)
                {
                    ingredient = PickRandomIngredientFromList(7);
                }
                else
                {
                    ingredient = PickRandomIngredientFromList(5);
                }
                ingredientsToAdd.Add(ingredient);
                // If no more ingredients to choose from, all have been added so start again
                if (ingredientsToChooseFrom.Count == 0)
                {
                    ingredientsToChooseFrom = new List<Ingredient>(possibleIngredients);
                    ShuffleList(ingredientsToChooseFrom);
                }
                ingredientCount++;
            }
            foreach (IngredientWithWeight ingredient in ingredientsToAdd)
            {
                zone.AddIngredient(ingredient);
            }
        }
        PrintAllSpawnZones();
    }

    private static IngredientWithWeight PickRandomIngredientFromList(int weight)
    {
        int index = Random.Range(0, ingredientsToChooseFrom.Count);
        Ingredient ingredient = ingredientsToChooseFrom[index];
        IngredientWithWeight ingredientWithWeight = ScriptableObject.CreateInstance<IngredientWithWeight>();
        ingredientWithWeight.type = ingredient.type;
        ingredientWithWeight.icon = ingredient.icon;
        ingredientWithWeight.weight = weight;
        ingredientsToChooseFrom.RemoveAt(index);
        return ingredientWithWeight;
    }

    static void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private void PrintList<T>(List<T> list)
    {
        Debug.Log(string.Join(", ", list.ToArray()));
    }
    private void PrintAllSpawnZones()
    {
        StringBuilder sb = new StringBuilder();
        foreach (ItemSpawnZone zone in spawnZones)
        {
            sb.Append($"Zone {zone.name} contains the following items:\n");
            foreach (IngredientWithWeight ingredient in zone.GetIngredients())
            {
                sb.Append($" Type: {ingredient.type}\n Weight: {ingredient.weight}\n");
            }
            sb.Append("-----------\n");
        }
        Debug.Log(sb.ToString());
    }
}
