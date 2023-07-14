using System.Collections.Generic;
using UnityEngine;

public static class TileNeighborsChecker
{
    public static NeighborsInfo GetNeighborsInfo(
        TileTypes.Types thisType,
        Vector2Int coords,
        CellTile[,] gameFieldCells
    )
    {
        int sameNeighborsCount = 0;
        List<int> oppositeNeighborsWeights = new List<int>();
        int neighBorWeight = 1;
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
                    ].OldType == thisType
                )
                {
                    sameNeighborsCount++;
                }
                else
                {
                    oppositeNeighborsWeights.Add(neighBorWeight);
                    neighBorWeight *= 2;
                }
            }
        }
        return new NeighborsInfo(sameNeighborsCount, oppositeNeighborsWeights);
    }
}
