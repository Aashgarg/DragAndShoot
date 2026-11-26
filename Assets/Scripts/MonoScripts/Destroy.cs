using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 respawnPosition = collision.gameObject.GetComponent<Tile>().position; // Get the position of the tile
            Respawen(respawnPosition, collision.gameObject);

        }
    }

    void Respawen(Vector3 position, GameObject gameObject)
    {
        gameObject.transform.position = position; // Move the player to the respawn positionS
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Reset the player's velocity
    }
}
