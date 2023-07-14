using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/MapTiles", order = 1)]
public class CellTile : Tile
{
    private TileTypes.Types _tileType;
    private TileTypes.Types _oldType;
    private int _x,
        _y;
    public int X
    {
        get { return _x; }
        private set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        private set { _y = value; }
    }

    public Vector2Int Coords
    {
        get
        {
            return new Vector2Int(X, Y);
        }
    }

    public TileTypes.Types TileType
    {
        get
        {

            return _tileType;
        }
        set
        {

            _tileType = value;
        }
    }

    public TileTypes.Types OldType
    {
        get
        {

            return _oldType;
        }
        set
        {

            _oldType = value;
        }
    }

    public void UpdateCellCoords(int xPos, int yPos)
    {
        this.X = xPos;
        this.Y = yPos;
    }
}
