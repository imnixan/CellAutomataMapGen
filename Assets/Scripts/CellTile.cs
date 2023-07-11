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

    public void UpdateCell(int xPos, int yPos)
    {
        this.X = xPos;
        this.Y = yPos;
        switch (TileType)
        {
            case TileTypes.Types.Forest:
                this.sprite = SpriteManager.GetForestSprite();
                break;
            case TileTypes.Types.Tree:
                this.sprite = SpriteManager.GetTreeSprite();
                this.color = new Color(1, 1, 1, Random.Range(0.8f, 1));
                break;
            case TileTypes.Types.Field:
                this.sprite = SpriteManager.GetFieldSprite();
                break;
        }
    }
}
