using UnityEngine;
using System.Collections.Generic;

public class TileConnectTracker : MonoBehaviour
{
    public int connectedTileCount = 1;
    public List<GameObject> connectedTiles = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            Debug.Log("Tile connected: " + other.name);
            // Here you can add logic to track the connection, e.g., increment a counter or update a list
            //get gameobject of the tile that entered the trigger and add it to the list of connected tiles
            if (connectedTiles.Count <= connectedTileCount)
            {
                connectedTiles.Add(other.gameObject);
            }
            else
            {
                //go to game manager and tell it to check if the connected tiles are correct
            }
        }
    }
}
