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
        int forestNeighbors = 0;
        for (int xMod = -1; xMod < 2; xMod++)
        {
            for (int yMod = -1; yMod < 2; yMod++)
            {
                if (yMod == 0 && xMod == 0)
                {
                    continue;
                }

                int newX = xMod + coords.x;
                int newY = yMod + coords.y;
                if (
                    gameFieldCells[
                        Normalize(newX, fieldSize.x),
                        Normalize(newY, fieldSize.y)
                    ].TileType == TileTypes.Types.Forest
                )
                {
                    forestNeighbors++;
                }
            }
        }
        if (gameFieldCells[coords.x, coords.y].TileType == TileTypes.Types.Forest)
        {
            if (forestNeighbors >= 5)
            {
                return TileTypes.Types.Forest;
            }
            else
            {
                return TileTypes.Types.Field;
            }
        }
        else
        {
            if (forestNeighbors >= 4)
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
