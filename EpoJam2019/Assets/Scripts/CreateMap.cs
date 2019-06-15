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

    // Start is called before the first frame update
    void Awake()
    {        
        citySpawnCD = new Cooldown(delayToCreateACity);
        citySpawnCD.Start();

        tiles = new GameObject[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject newTile = Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                tiles[x , y] = newTile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(citySpawnCD.IsFinished)
        {
            int a, b;
            do
            {
                a = Random.Range(0, width);
                b = Random.Range(0, height);

            } while (tiles[a, b].GetComponent<Tile>().IsCity());

            tiles[a, b].GetComponent<Tile>().SetToCity();

            citySpawnCD.Start();
        }
    }
}
