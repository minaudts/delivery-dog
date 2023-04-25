using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient")]
public class Ingredient : ScriptableObject
{
    public IngredientType type;
    public Sprite icon;

    // Override the GetHashCode method to use the properties of the Ingredient for hashing
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + this.type.GetHashCode();
        return hash;
    }

    // Override the Equals method to compare the properties of the Ingredient for equality
    public override bool Equals(object obj)
    {
        Ingredient other = obj as Ingredient;
        if (other == null)
        {
            return false;
        }
        return this.type == other.type;
    }
}

public class IngredientWithWeight : Ingredient
{
    [HideInInspector]
    public int weight;
}

[Serializable]
public enum IngredientType
{
    Butter,
    Cherry,
    Egg,
    Flour,
    Lemon,
    Milk,
    Peach,
    Salt,
    Sugar
}
