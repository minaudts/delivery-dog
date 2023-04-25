using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe")]
public class Recipe : ScriptableObject
{
    public RecipeType type;
    //public Sprite icon;
    public List<Ingredient> ingredients;
    public bool isComplete = false;
    public override string ToString()
    {
        return $"Type: {type}\nisComplete: {isComplete}";
    }
}

[SerializeField]
public enum RecipeType
{
    BrownBread,
    WhiteBread,
    Croissant,
    PeachCake,
    CherryCake,
    LemonCake
}