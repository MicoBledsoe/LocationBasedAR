using UnityEngine;
using System.Collections;
//My Directives
public class GPSManager : MonoBehaviour
{
    private LocationProcessor locationProcessor;
    private MapManager mapManager;

    private void Start()
    {
        locationProcessor = FindObjectOfType<LocationProcessor>();
        mapManager = FindObjectOfType<MapManager>();

        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled by the user.");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Location services initialization timed out.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            yield break;
        }
        else
        {
            // Start a coroutine to update the location continuously
            StartCoroutine(UpdateLocation());
        }
    }

    private IEnumerator UpdateLocation()
    {
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                // Access granted and location value could be retrieved
                float latitude = Input.location.lastData.latitude;
                float longitude = Input.location.lastData.longitude;

                locationProcessor.UpdateGPSDisplay(latitude, longitude);
                mapManager.PositionMarker(new Vector2(latitude, longitude));
            }
            else
            {
                Debug.Log("Location services stopped.");
            }
            // Waiting before checking location again
            yield return new WaitForSeconds(1);
        }
    }
}
