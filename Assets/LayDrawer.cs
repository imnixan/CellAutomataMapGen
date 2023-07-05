public abstract class LayDrawer
{
    protected int _layerId;

    public int LayerId
    {
        get { return _layerId; }
        protected set { _layerId = value; }
    }
    public abstract TileTypes.Types GetTileType(int x, int y, CellTile[,] gameFieldCells);
}
