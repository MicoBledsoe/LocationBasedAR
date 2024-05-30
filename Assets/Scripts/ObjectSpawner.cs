using UnityEngine;
using UnityEngine.UI;
//My directives

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab of the 3D object to spawn
    public Transform player; // Reference to the player's transform
    public Button spawnMeshButton; // Reference to the "Spawn Mesh" button

    void Start()
    {
        if (spawnMeshButton != null)
        {
            spawnMeshButton.onClick.AddListener(SpawnObjectAtPlayerLocation);
        }
    }

    // Method to spawn object at the player's current location aka the orgin
    public void SpawnObjectAtPlayerLocation()
    {
        if (player != null && objectPrefab != null)
        {
            Vector3 playerPosition = player.position; // Get the player's current position
            Instantiate(objectPrefab, playerPosition, Quaternion.identity); // Instantiate the 3D object at the player's position
        }
        else
        {
            Debug.LogWarning("Player or objectPrefab is not set in the ObjectSpawner script.");
        }
    }
}
