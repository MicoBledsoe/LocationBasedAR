using UnityEngine;
using System.Collections;
using TMPro;

public class GPSManager : MonoBehaviour
{
    public LocationProcessor locationProcessor;
    public bool continuousUpdate = false;

    private void Start()
    {
        if (locationProcessor == null)
        {
            locationProcessor = FindObjectOfType<LocationProcessor>();
        }

        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location services are not enabled by the user.");
            yield break;
        }

        // Start location services with desired accuracy and update distance
        Input.location.Start(10f, 1f);  // 10 meters accuracy!  update every 1 meter

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            Debug.Log("Initializing location services...");
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.LogError("Location services initialization timed out.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location.");
            yield break;
        }
        else
        {
            Debug.Log("Location services started successfully.");
            if (continuousUpdate)
            {
                StartCoroutine(UpdateLocation());
            }
        }
    }

    private IEnumerator UpdateLocation()
    {
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                float latitude = Input.location.lastData.latitude;
                float longitude = Input.location.lastData.longitude;
                float altitude = Input.location.lastData.altitude;

                locationProcessor.UpdateGPSDisplay(latitude, longitude, altitude);
            }
            else
            {
                Debug.LogError("Location services stopped.");
            }
            yield return new WaitForSeconds(1);  // Update every second
        }
    }

    public (float latitude, float longitude, float altitude) GetCurrentLocation()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            return (Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);
        }
        else
        {
            Debug.LogError("Location services are not running.");
            return (0f, 0f, 0f);
        }
    }
}
