using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Tile : MonoBehaviour
{
    //holds tile data for the tile and also allows for the tile to be clicked and dragged and flinged
    [SerializeField] private TileData tileData;
    [SerializeField] private TextMeshProUGUI tileText;

    public Vector3 position;

    private bool isSolved = false;

    void Start()
    {
        // Assign a unique noise seed offset for each tile to ensure different drift patterns
        tileText.text = tileData.tileWord;
        position = gameObject.transform.position;
    }

}
