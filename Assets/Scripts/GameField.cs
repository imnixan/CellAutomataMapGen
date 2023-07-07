using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UniRx;
using System.Threading;

public class GameField : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private FlightForward player;
    private const int DrawLandRepeats = 1;
    private const int ForestPercentChanse = 50;
    private const int TreeLevel = 2;
    private const int LandLevel = 0;
    private const int EnemiesLevel = 1;

    [SerializeField]
    private int Seed = 1337;

    [SerializeField]
    private int _startWidth,
        _startHeight;

    private int _extraHeight;

    private int width;
    private int height;
    private bool additionGenerate;
    private CellTile[,] gameFieldCells = new CellTile[0, 0];
    private CellTile[,] lastGameFieldCells = new CellTile[0, 0];
    private Tilemap landTileMap,
        treesTileMap;
    private Vector2Int currentTileCoordinates;

    private int heightAdjustment;
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

    private Vector3 camMax;
    private float screenHeight;
    private Vector3 camMin;
    private System.Random randomGenerator;
    private float startTime;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<FlightForward>();
        player.enabled = false;
        Application.targetFrameRate = 300;
        Masks.InitializeMasks();
        enemySpawner = GetComponent<EnemySpawner>();
        camMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        camMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        screenHeight = camMax.y * 2;
        width = StartWidth;
        height = StartHeight;
        _extraHeight = StartHeight / 2;
        CreateGrid();
        CreateMap();
    }

    private void CreateGrid()
    {
        Grid grid = GetGrid(transform);
        CreateTileMaps(grid.transform);
        landTileMap.tileAnchor = new Vector3(0.5f, 0.5f, 0);
    }

    private Grid GetGrid(Transform parent)
    {
        GameObject gameObject = new GameObject("Grid");
        gameObject.transform.SetParent(parent);
        Grid grid = gameObject.AddComponent<Grid>();

        return grid;
    }

    private void CreateTileMaps(Transform parent)
    {
        landTileMap = InstanceTileMap("LandTileMap", parent, LandLevel);
        treesTileMap = InstanceTileMap("TreesTileMap", parent, TreeLevel);
    }

    private Tilemap InstanceTileMap(string objectName, Transform parent, int layer)
    {
        GameObject gameObject = new GameObject(objectName);
        gameObject.transform.parent = parent;
        gameObject.transform.localPosition = new Vector2(width / 2, screenHeight / 2) * -1;
        TilemapRenderer tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();
        Tilemap tileMap = gameObject.GetComponent<Tilemap>();
        tilemapRenderer.sortingOrder = layer;
        return tileMap;
    }

    private void CreateMap()
    {
        startTime = Time.time;
        CreateGameField();
        // GenerateMap();
    }

    private void CreateGameField()
    {
        lastGameFieldCells = gameFieldCells;
        gameFieldCells = new CellTile[width, height];

        StartCoroutine(CreateStartChaos());
    }

    private IEnumerator CreateStartChaos()
    {
        randomGenerator = new System.Random(Seed);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CellTile groundTile = ScriptableObject.CreateInstance<CellTile>();
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
                landTileMap.SetColliderType(new Vector3Int(x, y, 0), Tile.ColliderType.None);
            }
            yield return null;
        }
        GenerateMap();
    }

    private void FillCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gameFieldCells[x, y].UpdateCell(x, y + heightAdjustment);
            }
        }
    }

    private void GenerateMap()
    {
        Observable
            .Start(() => GenerateMapOnOtherThread())
            .SubscribeOn(Scheduler.ThreadPool) // Указываем использование пула потоков для выполнения метода
            .ObserveOn(Scheduler.MainThread) // Указываем переключение на основной поток после выполнения метода
            .Subscribe(result =>
            {
                StartCoroutine(DrawMap());
            });
    }

    private void GenerateMapOnOtherThread()
    {
        for (int i = 0; i < DrawLandRepeats; i++)
        {
            GenerateTiles(new LandDrawer());
        }
    }

    private IEnumerator DrawMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gameFieldCells[x, y].UpdateCell(x, y + heightAdjustment);
                landTileMap.SetTile(
                    new Vector3Int(x, y + heightAdjustment, 0),
                    gameFieldCells[x, y]
                );
                GenerateLayers(x, y);
                yield return null;
            }
        }
        if (!additionGenerate)
        {
            additionGenerate = true;
            player.enabled = true;
        }
    }

    private void GenerateLayers(int x, int y)
    {
        switch (gameFieldCells[x, y].TileType)
        {
            case TileTypes.Types.Forest:
                SpawnForestLayers(x, y);
                break;
            case TileTypes.Types.Field:
                SpawnFieldLayers(x, y);
                break;
        }
    }

    private void SpawnForestLayers(int x, int y)
    {
        SpawnTrees(x, y);
    }

    private void SpawnTrees(int x, int y)
    {
        CellTile treeObject = ScriptableObject.CreateInstance<CellTile>();
        treeObject.TileType = TileTypes.Types.Tree;
        treeObject.UpdateCell(x, y);
        treesTileMap.SetTile(new Vector3Int(x, y + heightAdjustment, 0), treeObject);
    }

    private void SpawnFieldLayers(int x, int y)
    {
        SpawnFieldEnemie(x, y);
    }

    private void SpawnFieldEnemie(int x, int y)
    {
        int result = TileNeighborsChecker.GetNeighborsCount(
            TileTypes.Types.Forest,
            new Vector2Int(x, y),
            gameFieldCells
        );
        if (Masks.CheckMask(result, Masks.GunPlaceMask))
        {
            enemySpawner.SpawnGun(
                new Vector2(x, y + heightAdjustment),
                EnemiesLevel,
                randomGenerator
            );
        }
        else
        {
            enemySpawner.TrySpawnMovable(
                new Vector2(x, y + heightAdjustment),
                EnemiesLevel,
                randomGenerator
            );
        }
    }

    private void GenerateTiles(LayDrawer drawer)
    {
        Debug.Log($"Gen Tiles in thread {Thread.CurrentThread.ManagedThreadId}");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                currentTileCoordinates.x = x;
                currentTileCoordinates.y = y;
                gameFieldCells[x, y].TileType = drawer.GetTileType(
                    currentTileCoordinates,
                    gameFieldCells
                );
            }
        }
    }

    private void AddExtraHeight()
    {
        ClearBottom();
        heightAdjustment += height;
        height = ExtraHeight;
        CreateMap();
    }

    private void ClearBottom()
    {
        foreach (var cell in lastGameFieldCells)
        {
            landTileMap.SetTile(new Vector3Int(cell.X, cell.Y, 0), null);
            treesTileMap.SetTile(new Vector3Int(cell.X, cell.Y, 0), null);
        }
    }

    private void Update()
    {
        if (Camera.main.transform.position.y >= height / 5 + heightAdjustment)
        {
            AddExtraHeight();
            Seed++;
        }
    }
}
