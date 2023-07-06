using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameField : MonoBehaviour
{
    public Sprite square;

    [SerializeField]
    private int DrawLandRepeats = 200;

    [SerializeField]
    private int Seed = 1337;

    [SerializeField]
    private int _width,
        _height;

    public int Width
    {
        get { return _width; }
    }

    public int Height
    {
        get { return _height; }
    }
    private CellTile[,] gameFieldCells;
    private Tilemap tileMap;
    private Vector2Int gameFieldSize;
    private Vector2Int currentTileCoordinates;
    public int ForestPercentChanse;

    private void CreateMap()
    {
        CreateTileMap();
        CreateStartChaos();
        StartCoroutine(GenerateMap());
    }

    private void CreateTileMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        gameFieldCells = new CellTile[Width, Height];
        gameFieldSize = new Vector2Int(Width, Height);
        Grid grid = CreateGrid(transform);
        tileMap = CreateTilemap(grid.transform, "TileMap");
        tileMap.tileAnchor = new Vector3(0.5f, 0.5f, 0);
    }

    private void CreateStartChaos()
    {
        System.Random randomGenerator = new System.Random(Seed);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                CellTile groundTile = ScriptableObject.CreateInstance<CellTile>();
                groundTile.sprite = Resources.Load<Sprite>(ResourcesAdressBook.FieldTile);
                int randomTile = randomGenerator.Next(0, 100);
                if (randomTile < ForestPercentChanse)
                {
                    groundTile.TileType = TileTypes.Types.Forest;
                    groundTile.color = Color.green;
                }
                else
                {
                    groundTile.TileType = TileTypes.Types.Field;
                    groundTile.color = Color.yellow;
                }
                gameFieldCells[x, y] = groundTile;
                tileMap.SetColliderType(new Vector3Int(x, y, 0), Tile.ColliderType.None);
            }
        }
        FillCells();
    }

    IEnumerator GenerateMap()
    {
        float startTime = Time.time;
        for (int i = 0; i < DrawLandRepeats; i++)
        {
            GenerateTiles(new LandDrawer());
            FillCells();
            yield return null;
        }
        Debug.Log($"FinishGenerating, tooks {Time.time - startTime} secs");
        DrawMap();
    }

    public void FillCells()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                gameFieldCells[x, y].UpdateCell();
            }
        }
    }

    private void DrawMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), gameFieldCells[x, y]);
            }
        }
        Debug.Log("finishDraw");
    }

    private void UpdateCells() { }

    private void GenerateTiles(LayDrawer drawer)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                currentTileCoordinates.x = x;
                currentTileCoordinates.y = y;
                gameFieldCells[x, y].TileType = drawer.GetTileType(
                    currentTileCoordinates,
                    gameFieldSize,
                    gameFieldCells
                );
            }
        }
    }

    private Grid CreateGrid(Transform parent)
    {
        GameObject gameObject = new GameObject("Grid");
        gameObject.transform.SetParent(parent);
        Grid grid = gameObject.AddComponent<Grid>();

        return grid;
    }

    private Tilemap CreateTilemap(Transform parent, string name)
    {
        GameObject gameObject = new GameObject(name);
        gameObject.transform.parent = parent;
        gameObject.transform.localPosition = new Vector2(Width / 2, Height / 2) * -1;
        TilemapRenderer tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();
        Tilemap tilemap = gameObject.GetComponent<Tilemap>();

        tilemap.tileAnchor = new Vector3();

        return tilemap;
    }

    private void Start()
    {
        CreateMap();
    }
}
