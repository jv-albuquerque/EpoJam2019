using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private GameObject tile = null;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;

    [Header("Variables")]
    [SerializeField] private float delayToCreateACity = 1f;

    private Cooldown citySpawnCD;
    private GameObject[,] tiles;

    private List<Tile> alreadyPassed;
    private List<Tile> isGrass;


    // Start is called before the first frame update
    void Awake()
    {
        alreadyPassed = new List<Tile>();
        isGrass = new List<Tile>();

        citySpawnCD = new Cooldown(delayToCreateACity);
        citySpawnCD.Start();

        tiles = new GameObject[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject newTile = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                newTile.GetComponent<Tile>().SetToDesert();
                tiles[x , y] = newTile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(citySpawnCD.IsFinished)
        {
            int a, n;
            n = 0;
            do
            {
                a = Random.Range(0, isGrass.Count);
                n++;
            } while (isGrass[a].GetComponent<Tile>().IsCity &&
                     !SurroundedByForest((int)isGrass[a].transform.position.x, (int)isGrass[a].transform.position.y) &&
                     n < isGrass.Count);

            if(SurroundedByForest((int)isGrass[a].transform.position.x, (int)isGrass[a].transform.position.y))
            {
                isGrass[a].GetComponent<Tile>().SetToCity();

                RemoveGrass = isGrass[a];

                citySpawnCD.Start();
            }
        }
    }



    private bool SurroundedByForest(int CellX, int CellY)
    {
        if (!(CellX + 1 >= width))
            if (!tiles[CellX + 1, CellY].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellX == 0))
            if (!tiles[CellX - 1, CellY].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellY + 1 >= height))
            if (!tiles[CellX, CellY + 1].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellY == 0))
            if (!tiles[CellX, CellY - 1].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellX + 1 >= width) && !(CellY + 1 >= height))
            if (!tiles[CellX + 1, CellY + 1].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellX + 1 >= width) && !(CellY == 0))
            if (!tiles[CellX + 1, CellY - 1].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellX == 0) && !(CellY + 1 >= height))
            if (!tiles[CellX - 1, CellY + 1].GetComponent<Tile>().IsNature)
                return false;

        if (!(CellX == 0) && !(CellY == 0))
            if (!tiles[CellX - 1, CellY - 1].GetComponent<Tile>().IsNature)
                return false;

        return true;
    }

    public bool FindGrass(int CellX, int CellY, out Tile tile)
    {
        List<Tile> grass = new List<Tile>();


        if (CellX + 1 < width)
        {
            if (tiles[CellX + 1, CellY].GetComponent<Tile>().IsNature)
                grass.Add(tiles[CellX + 1, CellY].GetComponent<Tile>());

            if (CellY + 1 < height)
            {
                if (tiles[CellX + 1, CellY + 1].GetComponent<Tile>().IsNature)
                    grass.Add(tiles[CellX + 1, CellY + 1].GetComponent<Tile>());
            }
            if (CellY - 1 >= 0)
            {
                if (tiles[CellX + 1, CellY - 1].GetComponent<Tile>().IsNature)
                    grass.Add(tiles[CellX + 1, CellY - 1].GetComponent<Tile>());
            }
        }


        if (CellY + 1 < height)
        {
            if (tiles[CellX, CellY + 1].GetComponent<Tile>().IsNature)
                grass.Add(tiles[CellX, CellY + 1].GetComponent<Tile>());
        }

        if (CellY - 1 >= 0)
        {
            if (tiles[CellX, CellY - 1].GetComponent<Tile>().IsNature)
                grass.Add(tiles[CellX, CellY - 1].GetComponent<Tile>());
        }

        if (CellX - 1 >= 0)
        {
            if (tiles[CellX - 1, CellY].GetComponent<Tile>().IsNature)
                grass.Add(tiles[CellX - 1, CellY].GetComponent<Tile>());

            if (CellY + 1 < height)
            {
                if (tiles[CellX - 1, CellY + 1].GetComponent<Tile>().IsNature)
                    grass.Add(tiles[CellX - 1, CellY + 1].GetComponent<Tile>());
            }
            if (CellY - 1 >= 0)
            {
                if (tiles[CellX - 1, CellY - 1].GetComponent<Tile>().IsNature)
                    grass.Add(tiles[CellX - 1, CellY - 1].GetComponent<Tile>());
            }
        }

        if(grass.Count > 0)
        {
            tile = grass[Random.Range(0, grass.Count)];
            return true;
        }

        tile = null;
        return false;
    }

    public bool SetToDesert(int CellX, int CellY, out Tile tile)
    {
        List<Tile> deserts = new List<Tile>();

        //look up
        if (CellX + 1 < width)
        {
            if (tiles[CellX + 1, CellY].GetComponent<Tile>().IsDesert)
                deserts.Add(tiles[CellX + 1, CellY].GetComponent<Tile>());
        }

        //look right
        if (CellY + 1 < height)
        {
            if (tiles[CellX, CellY + 1].GetComponent<Tile>().IsDesert)
                deserts.Add(tiles[CellX, CellY + 1].GetComponent<Tile>());
        }

        //look down
        if (CellX - 1 >= 0)
        {
            if (tiles[CellX - 1, CellY].GetComponent<Tile>().IsDesert)
                deserts.Add(tiles[CellX - 1, CellY].GetComponent<Tile>());
        }

        //look left
        if (CellY - 1 >= 0)
        {
            if (tiles[CellX, CellY - 1].GetComponent<Tile>().IsDesert)
                deserts.Add(tiles[CellX, CellY - 1].GetComponent<Tile>());
        }

        if(deserts.Count > 0)
        {
            tile = deserts[(int)Random.Range(0, deserts.Count)];
            return true;
        }

        tile = null;
        return false;
    }

    public Tile AddGrass
    {
        set
        {
            isGrass.Add(value);
        }
    }

    public Tile RemoveGrass
    {
        set
        {
            isGrass.Remove(value);
        }
    }
}
