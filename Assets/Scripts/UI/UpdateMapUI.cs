using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMapUI : MonoBehaviour
{
    // MAP UI
    private MapZoneUI[] zoneTransforms;
    [SerializeField] private float mapItemSizeMultiplier = 2.2f;
    private void Start() 
    {
        zoneTransforms = GetComponentsInChildren<MapZoneUI>();
        UpdateMap(SpawnZoneManager.Instance.GetIngredientsForAllSpawnZones());
    }
    private void UpdateMap(List<IngredientWithWeight[]> ingredientsPerSpawnZone)
    {
        Debug.Log(ingredientsPerSpawnZone.Count);
        for (int zone = 0; zone < ingredientsPerSpawnZone.Count; zone++)
        {
            // 1. Get UI images for current zone.
            Image[] images = zoneTransforms[zone].gameObject.GetComponentsInChildren<Image>();
            // 2. Get ingredients for current zone.
            IngredientWithWeight[] ingredients = ingredientsPerSpawnZone[zone];
            if (images.Length == ingredients.Length)
            {
                for (int image = 0; image < images.Length; image++)
                {
                    IngredientWithWeight ing = ingredients[image];
                    images[image].sprite = ing.icon;
                    float imageSize = Mathf.Pow(ing.weight, 2) * mapItemSizeMultiplier;
                    images[image].rectTransform.sizeDelta = new Vector2(imageSize, imageSize);
                }
            }
            else
            {
                Debug.LogWarning("Map UI elements and generated ingredients per spawn zone mismatch");
            }
        }
    }
}
