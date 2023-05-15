using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Species", menuName = "BeeBreedingPrototype/Species")]
public class Species : ScriptableObject 
{
    public new string name;
    [ShowAssetPreview] public Sprite sprite;
}