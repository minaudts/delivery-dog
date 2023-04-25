using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemSpawnZone : MonoBehaviour
{
    private DogMovement player;
    float timer = 0f;
    public float spawnTime = 5f;
    private List<IngredientWithWeight> ingredients = new List<IngredientWithWeight>();
    private Image inSpawnZoneImage;
    public Sprite notInZoneSprite;
    public Sprite inZoneSprite;

    public IngredientWithWeight[] GetIngredients()
    {
        return this.ingredients.ToArray();
    }
    public void AddIngredient(IngredientWithWeight ingredient)
    {
        ingredients.Add(ingredient);
    }

    private void OnTriggerEnter(Collider other)
    {
        inSpawnZoneImage = GameObject.Find("InSpawnZone").GetComponent<Image>();
        player = other.GetComponent<DogMovement>();
        if (player)
        {
            Debug.Log("Entering spawnzone");
            inSpawnZoneImage.sprite = inZoneSprite;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals(player.tag))
        {
            // if player is moving, increment timer.
            if (player.IsMoving())
            {
                timer += Time.deltaTime;
            }

            // when timer reaches a certain value, spawn item.
            if (timer >= spawnTime)
            {
                Ingredient item = SpawnItem();
                ConfirmPickupUI.Instance.ShowConfirmDialog(item.icon, () =>
                {
                    // If player confirms
                    Debug.Log("Adding item to inventory");
                    DogInventory.Instance.AddIngredientToInventory(item);
                }, () => {
                    // If player cancels
                    Debug.Log("Don't pick up ingredient");
                });
                
                timer = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Exiting spawnzone");
            inSpawnZoneImage.sprite = notInZoneSprite;
        }
    }

    private IngredientWithWeight SpawnItem()
    {
        IngredientWithWeight output = ingredients[0];
        // Getting a random weight value
        int totalWeight = 0;
        foreach (IngredientWithWeight i in this.ingredients)
        {
            totalWeight += i.weight;
        }
        int rndWeightValue = Random.Range(1, totalWeight + 1);
        // Checking where random weight value falls
        int processedWeight = 0;
        foreach (IngredientWithWeight i in this.ingredients)
        {
            processedWeight += i.weight;
            if (rndWeightValue <= processedWeight)
            {
                output = i;
                break;
            }
        }
        return output;
    }
}
