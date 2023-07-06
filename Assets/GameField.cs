using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameField : MonoBehaviour
{
    private const int DrawLandRepeats = 1;
    private const int ForestPercentChanse = 50;

    [SerializeField]
    private int Seed = 1337;

    [SerializeField]
    private int _startWidth,
        _startHeight,
        _extraHeight;

    private int width;
    private int height;
    private int startDrawHeight;

    private CellTile[,] gameFieldCells;
    private Tilemap tileMap;
    private Vector2Int gameFieldSize;
    private Vector2Int currentTileCoordinates;
    private bool extraGenerate;
    private int StartWidth
    {
        get { return _startWidth; }
    }
    private int StartHeight
    {
        get { return _startHeight; }
    }
    private int ExtraHeight
    {
        get { return _extraHeight; }
    }

    private void Start()
    {
        width = StartWidth;
        height = StartHeight;
        CreateGrid();
        CreateMap();
    }

    private void CreateGrid()
    {
        Grid grid = CreateGrid(transform);
        tileMap = GetTilemap(grid.transform, "TileMap");
        tileMap.tileAnchor = new Vector3(0.5f, 0.5f, 0);
    }

    private void CreateMap()
    {
        CreateGameField();
        StartCoroutine(GenerateMap());
    }

    private void CreateGameField()
    {
        gameFieldCells = new CellTile[width, height];
        gameFieldSize = new Vector2Int(width, height);

        CreateStartChaos();
    }

    private void CreateStartChaos()
    {
        System.Random randomGenerator = new System.Random(Seed);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CellTile groundTile = ScriptableObject.CreateInstance<CellTile>();
                groundTile.sprite = Resources.Load<Sprite>(ResourcesAdressBook.FieldTile);
                int randomTile = randomGenerator.Next(0, 100);
                if (randomTile < ForestPercentChanse)
                {
                    groundTile.TileType = TileTypes.Types.Forest;
                }
                else
                {
                    groundTile.TileType = TileTypes.Types.Field;
                }
                gameFieldCells[x, y] = groundTile;
                tileMap.SetColliderType(new Vector3Int(x, y, 0), Tile.ColliderType.None);
            }
        }
        FillCells();
    }

    private void FillCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gameFieldCells[x, y].UpdateCell();
            }
        }
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

    private void DrawMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileMap.SetTile(new Vector3Int(x, y + startDrawHeight, 0), gameFieldCells[x, y]);
            }
        }
        Debug.Log("finishDraw");
        if (!extraGenerate)
        {
            startDrawHeight += ExtraHeight;
            extraGenerate = true;
        }
    }

    private void GenerateTiles(LayDrawer drawer)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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

    private Tilemap GetTilemap(Transform parent, string name)
    {
        GameObject gameObject = new GameObject(name);
        gameObject.transform.parent = parent;
        gameObject.transform.localPosition = new Vector2(width / 2, height / 2) * -1;
        TilemapRenderer tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();
        Tilemap tilemap = gameObject.GetComponent<Tilemap>();

        return tilemap;
    }

    private void AddExtraHeight()
    {
        startDrawHeight += ExtraHeight;
        height = ExtraHeight;
        CreateMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddExtraHeight();
            Seed++;
        }
    }
}
