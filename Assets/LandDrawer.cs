using UnityEngine;

public class LandDrawer : LayDrawer
{
    public LandDrawer()
    {
        LayerId = 0;
    }

    public override TileTypes.Types GetTileType(int x, int y, CellTile[,] gameFieldCells)
    {
        int fieldNeighbors = 0;
        int xMod = 0;
        int yMod = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (j == 0 && i == 0)
                {
                    continue;
                }

                if (gameFieldCells[x + xMod, y + yMod].TileType == TileTypes.Types.Field)
                {
                    fieldNeighbors++;
                }
            }
        }
        if (gameFieldCells[x, y].TileType == TileTypes.Types.Forest)
        {
            if ((Masks.GetLandsMask() & (1 << fieldNeighbors)) != 0)
            {
                Debug.Log("Forest = > Field");
                return TileTypes.Types.Field;
            }
            else
            {
                Debug.Log("Forest = Forest");
                return TileTypes.Types.Forest;
            }
        }
        else
        {
            if ((Masks.GetLandsMask() & (1 << (8 - fieldNeighbors))) != 0)
            {
                Debug.Log("Fieldt = > Forest");
                return TileTypes.Types.Forest;
            }
            else
            {
                Debug.Log("Field = field");
                return TileTypes.Types.Field;
            }
        }
    }
}
