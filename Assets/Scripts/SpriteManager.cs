using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private static Dictionary<int, int> LandMap = new Dictionary<int, int>
    {
        {151,0 },
        {148,1 },
        {244,2 },
        {7,3 },
        {0,4 },
        {224,5 },
        {47,6 },
        {41,7 },
        {233,8 },

    };

    public static Sprite GetTreeSprite()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>(ResourcesAdressBook.TreeSprites);
        Sprite treeSprite = allSprites[Random.Range(0, allSprites.Length)];
        return treeSprite;
    }

    public static Sprite GetForestSprite(int neighborsWeight)
    {
        Sprite[] fieldSpriteSheet = Resources.LoadAll<Sprite>(ResourcesAdressBook.ForestTile);
        int spriteIndex = 4;
        if(LandMap.ContainsKey(neighborsWeight))
        {
            spriteIndex = LandMap[neighborsWeight];
        }
        return fieldSpriteSheet[spriteIndex];
    }

    public static Sprite GetFieldSprite()
    {
        return Resources.Load<Sprite>(ResourcesAdressBook.FieldTile);
    }
}
