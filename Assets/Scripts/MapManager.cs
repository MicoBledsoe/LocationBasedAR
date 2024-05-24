using UnityEngine;
//My dIRECTIVES

public class MapManager : MonoBehaviour
{
    public Transform playerMarker; // gaME mARKER

    public void PositionMarker(Vector2 gpsPosition)
    {
        Vector3 worldPosition = GPSToWorldPosition(gpsPosition);
        playerMarker.position = worldPosition;
    }

    private Vector3 GPSToWorldPosition(Vector2 gpsPosition)
    {

        float x = gpsPosition.x * 1000; 
        float z = gpsPosition.y * 1000;
        return new Vector3(x, 0, z); 
    }
}
