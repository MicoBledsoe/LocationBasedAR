using UnityEngine;
using TMPro;

public class MapManager : MonoBehaviour
{
    public Transform playerMarker; // The marker representing the player's position
    public GameObject markerPrefab; // The prefab for the 3D marker

    public void PositionMarker(Vector2 gpsPosition)
    {
        Vector3 worldPosition = GPSToWorldPosition(gpsPosition);
        playerMarker.position = worldPosition;
    }

    public void DrawMarker(Vector2 gpsPosition, string label)
    {
        Vector3 worldPosition = GPSToWorldPosition(gpsPosition);
        GameObject marker = Instantiate(markerPrefab, worldPosition, Quaternion.identity);
        TextMeshPro labelComponent = marker.GetComponentInChildren<TextMeshPro>();
        if (labelComponent != null)
        {
            labelComponent.text = label;
        }
    }

    private Vector3 GPSToWorldPosition(Vector2 gpsPosition)
    {
        return GPSEncoder.GPSToUCS(gpsPosition);
    }
}
