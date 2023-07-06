using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/MapTiles", order = 1)]
public class CellTile : Tile
{
    private TileTypes.Types _tileType;

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

    public void UpdateCell()
    {
        switch (TileType)
        {
            case TileTypes.Types.Forest:
                this.sprite = Resources.Load<Sprite>(ResourcesAdressBook.ForestTile);
                this.color = Color.green;
                break;
            case TileTypes.Types.City:
                this.sprite = Resources.Load<Sprite>(ResourcesAdressBook.CityTile);
                this.color = Color.gray;
                break;
            case TileTypes.Types.Water:
                this.sprite = Resources.Load<Sprite>(ResourcesAdressBook.WaterTile);
                this.color = Color.blue;
                break;
            case TileTypes.Types.Field:
                this.sprite = Resources.Load<Sprite>(ResourcesAdressBook.FieldTile);
                this.color = Color.yellow;
                break;
        }
    }
}
