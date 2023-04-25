using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGenerator : SingletonPersistent<RecipeGenerator>
{
    // List of all available recipes
    public Recipe[] possibleRecipes;

    // Amount of recipes needed to complete level
    [Range(1, 10)]
    public int numRecipes = 4;
    [SerializeField] private AudioClip gamecompleteSound;

    // A list of recipes for the current playthrough
    [HideInInspector]
    public static Recipe[] recipesToComplete;
    // Index that indicates which recipe the player is currently working on
    private int currentRecipe = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (possibleRecipes == null || possibleRecipes.Length == 0 || numRecipes <= 0)
        {
            Debug.LogError("Missing or invalid parameters.");
            return;
        }
        if(numRecipes > possibleRecipes.Length)
        {
            Debug.LogWarning("The number of recipes to generate is larger than the amount of possible recipes. Limiting number of recipes");
            numRecipes = possibleRecipes.Length;
        }
        // Generate random recipes
        recipesToComplete = GenerateRecipes();
        InGameUILogic.Instance.UpdateOrderOverviewUI(recipesToComplete, currentRecipe + 1);
    }
    // Method that checks current recipe and reveals next one if completed.
    public void CheckCurrentRecipe()
    {
        bool success = CheckRecipe(recipesToComplete[currentRecipe]);
        if(success)
        {
            currentRecipe++;
            InGameUILogic.Instance.UpdateOrderOverviewUI(recipesToComplete, currentRecipe + 1);
            if(currentRecipe == recipesToComplete.Length)
            {
                // All orders completed
                Debug.Log("All orders completed");
                SoundManager.Instance.PlaySound(gamecompleteSound);
                InGameUILogic.Instance.GameOver();
            }
        }
    }

    public void RegenerateRecipes()
    {
        recipesToComplete = GenerateRecipes();
    }

    // Return an array of "numRecipes" amount of unique recipes.
    private Recipe[] GenerateRecipes()
    {
        List<Recipe> result = new List<Recipe>();
        for(int i = 0; i < numRecipes; i++)
        {
            // pick random recipe
            int index = 0;
            do
            {
                index = UnityEngine.Random.Range(0, possibleRecipes.Length);
            } while(result.Contains(possibleRecipes[index]));
            result.Add(possibleRecipes[index]);
        }
        return result.ToArray();
    }

    // Check if the player has collected all the necessary ingredients for the current recipe
    private bool CheckRecipe(Recipe recipe)
    {
        Debug.Log("check recipe");
        bool recipeComplete = true;
        foreach (Ingredient ingredient in recipe.ingredients)
        {
            bool ingredientFound = DogInventory.Instance.InventoryContainsItem(ingredient);
            if (!ingredientFound)
            {
                recipeComplete = false;
                Debug.Log($"You don't have {ingredient}, go back into the city!");
                break;
            }
        }
        if(recipeComplete)
        {
            foreach(Ingredient ingredient in recipe.ingredients)
            {
                DogInventory.Instance.RemoveIngredientFromInventory(ingredient);
            }
        }
        recipe.isComplete = recipeComplete;
        return recipeComplete;
    }
    public int AmountOfOrdersCompleted()
    {
        return currentRecipe;
    }
}
