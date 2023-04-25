using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUILogic : SingletonPersistent<InGameUILogic>
{
    // INVENTORY UI
    public Transform inventoryContent;
    private float originalOpacity;
    private Image inventoryContentImage;
    public GameObject inventoryItemPrefab;
    // ORDER UI
    public Transform orderOverview;
    public GameObject orderPrefab;
    public GameObject orderItemPrefab;
    // FOR GAME OVER SCREEN
    public GameObject gameOverPanel;
    public GameObject inGamePanel;
    public TMP_Text scoreResultText;
    public int timeLeftMultiplier = 50;
    public Sprite orderCompleteSprite;
    private void Start()
    {
        gameOverPanel.SetActive(false);
        inventoryContentImage = inventoryContent.GetComponent<Image>();
        originalOpacity = inventoryContentImage.color.a;
        UpdateInventoryUI(null);
    }

    public void UpdateInventoryUI(Dictionary<Ingredient, int> inventory)
    {
        Debug.Log(inventoryContentImage);
        Color tempColor = inventoryContentImage.color;
        // Remove all existing inventory list items
        foreach (Transform inventoryItem in inventoryContent)
        {
            Destroy(inventoryItem.gameObject);
        }
        if (inventory == null || inventory.Count == 0)
        {
            tempColor.a = 0;
            inventoryContentImage.color = tempColor;
            return;
        }
        tempColor.a = originalOpacity;
        inventoryContentImage.color = tempColor;
        // Create a new inventory list item for each item in the inventory
        foreach (KeyValuePair<Ingredient, int> item in inventory)
        {
            // Instantiate a new inventory list item prefab
            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContent);
            Image icon = newItem.GetComponent<Image>();
            TMP_Text countText = newItem.GetComponentInChildren<TMP_Text>();
            icon.sprite = item.Key.icon;
            countText.text = item.Value.ToString();
        }
    }

    public void UpdateOrderOverviewUI(Recipe[] orders, int amountToShow)
    {
        // Remove all existing order list items
        foreach (Transform order in orderOverview)
        {
            Destroy(order.gameObject);
        }
        int showingRecipe = 0;
        // TODO: keep in mind if order should be visible. show ingredients or question mark depending on that.
        // Or just show orders that are current or already completed
        foreach (Recipe order in orders)
        {
            Debug.Log(order);
            if (showingRecipe < amountToShow)
            {
                // Create prefab
                GameObject newOrderPrefab = Instantiate(orderPrefab, orderOverview);
                // Als order complete, stel je in op groen
                if (order.isComplete)
                {
                    Debug.Log("Order is completed");
                    newOrderPrefab.GetComponent<Image>().sprite = orderCompleteSprite;
                }
                foreach (Ingredient ingredient in order.ingredients)
                {
                    GameObject newOrderItemPrefab = Instantiate(orderItemPrefab, newOrderPrefab.transform);
                    newOrderItemPrefab.GetComponent<Image>().sprite = ingredient.icon;
                    // Create image as parent of prefab for ingredient
                }
                showingRecipe++;
            }
        }
    }

    public void GameOver()
    {
        float timeSpent = Timer.Instance.timeSpent;
        int score = CalculateScore(timeSpent);
        scoreResultText.text = score.ToString("D4");
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    private int CalculateScore(float timeSpent)
    {
        // Time spent multiplier
        int score = (int)timeSpent * timeLeftMultiplier;
        Debug.Log($"Time spent: {timeSpent}");
        return score;
    }
    public void NewGame()
    {
        Debug.Log("New Game");
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        SceneChanger.Instance.TransitionToScene("BakeryScene", () =>
        {
            RecipeGenerator.Instance.RegenerateRecipes();
            DogInventory.Instance.ClearInventory();
            SpawnZoneManager.Instance.RandomizeLevel();
            inGamePanel.SetActive(true);   
        });
    }
    public void QuitGame()
    {
        // Wrapper around existing QuitGame implementation.
        PersistentUILogic.Instance.QuitGame();
    }
}
