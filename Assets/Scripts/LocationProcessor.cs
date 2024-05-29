using UnityEngine;
using TMPro;
//My directives

public class LocationProcessor : MonoBehaviour
{
    public TextMeshProUGUI gpsDisplay; // UGUI
    private Vector2 lastKnownPosition;

    public void UpdateGPSDisplay(float latitude, float longitude, float altitude)
    {
        lastKnownPosition = new Vector2(latitude, longitude);
        gpsDisplay.text = $"Lat: {latitude:F6}\nLong: {longitude:F6}\nAlt: {altitude:F1}";
    }

    public float CalculateDistance(Vector2 startPosition, Vector2 endPosition)
    {
        return Vector2.Distance(startPosition, endPosition);
    }
}
