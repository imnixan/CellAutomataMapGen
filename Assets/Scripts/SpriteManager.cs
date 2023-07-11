using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static Sprite GetTreeSprite()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>(ResourcesAdressBook.TreeSprites);
        Sprite treeSprite = allSprites[Random.Range(0, allSprites.Length)];
        return treeSprite;
    }

    public static Sprite GetForestSprite()
    {
        return Resources.Load<Sprite>(ResourcesAdressBook.ForestTile);
    }

    public static Sprite GetFieldSprite()
    {
        return Resources.Load<Sprite>(ResourcesAdressBook.FieldTile);
    }
}
