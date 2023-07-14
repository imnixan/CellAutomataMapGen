using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour
{
    //private static Dictionary<int, int> LandMap = new Dictionary<int, int>
    //{
    //    {151,0 },
    //    {148,1 },
    //    {244,2 },
    //    {7,3 },
    //    {0,4 },
    //    {224,5 },
    //    {47,6 },
    //    {41,7 },
    //    {233,8 },
    //};

    private static Dictionary<int, int> LandMap = new Dictionary<int, int>
    {
        { 1, 6 },
        { 2, 3 },
        { 4, 0 },
        { 8, 7 },
        { 16, 1 },
        { 32, 8 },
        { 64, 5 },
        { 128, 2 },
    };

    public static Sprite GetTreeSprite()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>(ResourcesAdressBook.TreeSprites);
        Sprite treeSprite = allSprites[Random.Range(0, allSprites.Length)];
        return treeSprite;
    }

    public static Sprite GetForestSprite(NeighborsInfo neighborsInfo)
    {
        Texture2D forestTexture = Resources.Load<Sprite>(ResourcesAdressBook.ForestTile).texture;
        Sprite[] forestFrame = Resources.LoadAll<Sprite>(ResourcesAdressBook.ForestFrame);

        for (int i = 0; i < neighborsInfo.oppositeNeighborsWeights.Count; i++)
        {
            forestTexture = DrawTexture(
                neighborsInfo.oppositeNeighborsWeights[i],
                forestTexture,
                forestFrame
            );
        }

        return Sprite.Create(
            forestTexture,
            new Rect(0, 0, forestTexture.width, forestTexture.height),
            Vector2.zero
        );
    }

    private static Texture2D DrawTexture(
        int neighborPosition,
        Texture2D originalTexture,
        Sprite[] frameSheet
    )
    {
        Texture2D frameTexture = frameSheet[LandMap[neighborPosition]].texture;
        for (int x = 0; x < frameTexture.width; x++)
        {
            for (int y = 0; y < frameTexture.height; y++)
            {
                Color frameColor = frameTexture.GetPixel(x, y);
                if (frameColor.a > 0)
                {
                    originalTexture.SetPixel(x, y, frameColor);
                }
            }
        }
        return originalTexture;
    }

    public static Sprite GetFieldSprite()
    {
        return Resources.Load<Sprite>(ResourcesAdressBook.FieldTile);
    }
}
