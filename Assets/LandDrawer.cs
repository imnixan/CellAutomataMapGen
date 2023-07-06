using UnityEngine;

public class LandDrawer : LayDrawer
{
    public LandDrawer()
    {
        LayerId = 0;
    }

    public override TileTypes.Types GetTileType(
        Vector2Int coords,
        Vector2Int fieldSize,
        CellTile[,] gameFieldCells
    )
    {
        int fieldNeighbors = 0;
        for (int xMod = -1; xMod < 2; xMod++)
        {
            for (int yMod = -1; yMod < 2; yMod++)
            {
                if (yMod == 0 && xMod == 0)
                {
                    continue;
                }

                if (
                    gameFieldCells[
                        Normalize(coords.x + xMod, fieldSize.x),
                        Normalize(coords.y + yMod, fieldSize.y)
                    ].TileType == TileTypes.Types.Field
                )
                {
                    fieldNeighbors++;
                }
            }
        }
        if (gameFieldCells[coords.x, coords.y].TileType == TileTypes.Types.Forest)
        {
            if ((Masks.GetLandsMask() & (1 << fieldNeighbors)) != 0)
            {
                return TileTypes.Types.Field;
            }
            else
            {
                return TileTypes.Types.Forest;
            }
        }
        else
        {
            if ((Masks.GetLandsMask() & (1 << (8 - fieldNeighbors))) != 0)
            {
                return TileTypes.Types.Forest;
            }
            else
            {
                return TileTypes.Types.Field;
            }
        }
    }

    private int Normalize(int coordinate, int coordinateMax)
    {
        if (coordinate == coordinateMax)
        {
            coordinate = 0;
        }
        if (coordinate < 0)
        {
            coordinate = coordinateMax - 1;
        }
        return coordinate;
    }
}
