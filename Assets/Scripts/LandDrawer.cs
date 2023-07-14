using UnityEngine;

public class LandDrawer : LayDrawer
{
    public LandDrawer()
    {
        LayerId = 0;
    }

    public override TileTypes.Types GetTileType(Vector2Int coords, CellTile[,] gameFieldCells)
    {
        int forestNeighbors = TileNeighborsChecker
            .GetNeighborsInfo(TileTypes.Types.Forest, coords, gameFieldCells)
            .sameNeighborsCount;
        if (gameFieldCells[coords.x, coords.y].TileType == TileTypes.Types.Forest)
        {
            if (Masks.CheckMask(forestNeighbors, Masks.LandSurviveMask))
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
            if (Masks.CheckMask(forestNeighbors, Masks.LandBirthMask))
            {
                return TileTypes.Types.Forest;
            }
            else
            {
                return TileTypes.Types.Field;
            }
        }
    }
}
