using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _tilePrefab;
    private GameCamera _gameCamera;

    [SerializeField] private List<Tile> _tiles = new List<Tile>();


    // Start is called before the first frame update
    void Start()
    {
        _tiles = new List<Tile>();
        _grid = GetComponent<Grid>();
        _gameCamera = FindObjectOfType<GameCamera>();



        CreateGridLayer(5, 5);

        _gameCamera.SetCameraToPosition(new Vector3(10, 1, 10));





        GenerateFarmlands(2, 4);
        GenerateFarmlands(0, 2);

        TimeManager.SetTime(0.36f);


    }



    void CreateGridLayer(int verticalSize, int horizontalSize)
    {
        for (int i = 0; i < verticalSize; i++)
        {
            for (int j = 0; j < horizontalSize; j++)
            {
                var worldPosition = _grid.GetCellCenterWorld(new Vector3Int(i, j));
                _tiles.Add(Tile.CreateNewTile("Grass", new Vector3Int(i, j), worldPosition, _tiles.Count));
            }
        }
        transform.Rotate(0, 45, 0);
    }

    Vector3 CalculateMiddlePoint()
    {
        var middlePoint = new Vector3();
        foreach (var tile in _tiles)
        {
            middlePoint += tile.transform.position;
        }
        middlePoint /= _tiles.Count;
        return middlePoint;
    }


    public void ChangeTile<T>(Tile tile) where T : Tile
    {
        //find object tile from _tiles list
        var tileFromList = _tiles.Find(t => t.Id == tile.Id);
        var newTile = Tile.ChangeTileType<T>(tile);
        _tiles[tileFromList.Id] = newTile;
    }


    List<Tile> Get4Neighbors(Tile tile)
    {
        List<Tile> neighbors = new List<Tile>();

        // Add neighboring tiles based on their positions (up, down, left, right)
        Vector3Int[] offsets = new Vector3Int[]
        {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
        };

        foreach (Vector3Int offset in offsets)
        {
            Vector3Int neighborPosition = tile.GridPosition + offset;
            Tile neighbor = _tiles.Find(t => t.GridPosition == neighborPosition);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }


    List<Tile> Get8Neighbors(Tile tile)
    {
        List<Tile> neighbors = new List<Tile>();

        // Add neighboring tiles based on their positions (up, down, left, right)
        Vector3Int[] offsets = new Vector3Int[]
        {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right,
        new Vector3Int(-1, 1, 0),
        new Vector3Int(1, -1, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(1, 1, 0)
        };

        foreach (Vector3Int offset in offsets)
        {
            Vector3Int neighborPosition = tile.GridPosition + offset;
            Tile neighbor = _tiles.Find(t => t.GridPosition == neighborPosition);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    void GenerateFarmlands(int min, int max)
    {
        int maxSpread = UnityEngine.Random.Range(min, max); // Set a random maximum spread distance

        if(maxSpread == 0)
        {
            return;
        }


        List<Tile> grassTiles = _tiles.FindAll(tile => tile.GetType().Name == "Grass"); // Find all available grass tiles

        int randomIndex = UnityEngine.Random.Range(0, grassTiles.Count);
        Tile startingGrassTile = grassTiles[randomIndex]; // Choose a random grass tile

        ChangeTile<Farmland>(startingGrassTile); // Convert the starting grass tile

        grassTiles.RemoveAt(randomIndex); // Remove the converted grass tile

        List<Tile> neighbors = Get8Neighbors(startingGrassTile);
        foreach (Tile neighbor in neighbors)
        {
            if (neighbor.GetType().Name != "Grass")
                grassTiles.Remove(neighbor);
        }
        

        if (maxSpread > grassTiles.Count)
            maxSpread = grassTiles.Count;

        if (maxSpread > neighbors.Count)
            maxSpread = neighbors.Count;

        if (maxSpread > 1)
            SpreadFarmland(neighbors, maxSpread); // Spread farmland to neighboring grass tiles
    }

    void SpreadFarmland(List<Tile> grassTiles, int maxSpread)
    {
        HashSet<Tile> farmlandTiles = new HashSet<Tile>();

        foreach (Tile grassTile in grassTiles)
        {
            ChangeTile<Farmland>(grassTile); // Convert the grass tile
            farmlandTiles.Add(grassTile);

            if (farmlandTiles.Count >= maxSpread)
                break;
        }
    }






    // Update is called once per frame
    void Update()
    {

    }
}
