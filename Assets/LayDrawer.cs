using UnityEngine;

public abstract class LayDrawer
{
    protected int _layerId;

    public int LayerId
    {
        get { return _layerId; }
        protected set { _layerId = value; }
    }
    public abstract TileTypes.Types GetTileType(
        Vector2Int coords,
        Vector2Int fieldSize,
        CellTile[,] gameFieldCells
    );
}
