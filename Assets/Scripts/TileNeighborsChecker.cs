using UnityEngine;

public static class TileNeighborsChecker
{
    public static int GetNeighborsCount(
        TileTypes.Types searchedNeighbor,
        Vector2Int coords,
        CellTile[,] gameFieldCells
    )
    {
        int sameNeighbors = 0;
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
                        CoordsNormalizer.Normalize(newX, gameFieldCells.GetLength(0)),
                        CoordsNormalizer.Normalize(newY, gameFieldCells.GetLength(1))
                    ].OldType == searchedNeighbor
                )
                {
                    sameNeighbors++;
                }
            }
        }
        return sameNeighbors;
    }
}
