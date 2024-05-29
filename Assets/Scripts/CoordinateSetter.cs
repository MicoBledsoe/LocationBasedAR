using UnityEngine;
using UnityEngine.UI;
using TMPro;
//My directives

public class CoordinateSetter : MonoBehaviour
{
    public Button getCurrentButton;
    public Button setDestinationButton;
    public Button drawObjectButton;
    public Button placeMarkerButton;
    public Button calculateDistanceButton; // Button to calculate distance and update Unity local position
    public TextMeshProUGUI distanceDisplay;
    public TextMeshProUGUI unityLocalPositionDisplay; // New field to display Unity local position
    public TMP_InputField markerLabelInput; // Input field for the marker label

    private GPSManager gpsManager;
    private Vector2 destinationCoordinate;

    private void Start()
    {
        gpsManager = FindObjectOfType<GPSManager>(); // Checking to see if there is a instance of the compon

        getCurrentButton.onClick.AddListener(GetCurrentCoordinates);
        setDestinationButton.onClick.AddListener(SetDestinationCoordinates);
        drawObjectButton.onClick.AddListener(DrawObjectAtCoordinates);
        placeMarkerButton.onClick.AddListener(PlaceMarker);
        calculateDistanceButton.onClick.AddListener(CalculateDistanceAndDisplayLocalPosition);

        markerLabelInput.gameObject.SetActive(false); // Hide the input field initially
        placeMarkerButton.gameObject.SetActive(false); // Hide the place marker button initially
    }

    
    public void GetCurrentCoordinates() //fetch and display current GPS coords
    {
        Debug.Log("GetCurrentCoordinates button clicked.");
        var currentLocation = gpsManager.GetCurrentLocation();
        float latitude = currentLocation.latitude;
        float longitude = currentLocation.longitude;
        float altitude = currentLocation.altitude;

        Debug.Log($"Current Coordinates: Lat: {latitude}, Long: {longitude}, Alt: {altitude}");
        gpsManager.locationProcessor.UpdateGPSDisplay(latitude, longitude, altitude);
    }

    public void SetDestinationCoordinates() //set the destination coordinates
    {
        Debug.Log("SetDestinationCoordinates button clicked.");
        var currentLocation = gpsManager.GetCurrentLocation();
        destinationCoordinate = new Vector2(currentLocation.latitude, currentLocation.longitude);
        Debug.Log($"Destination set to: Lat: {destinationCoordinate.x}, Long: {destinationCoordinate.y}");
    }

    public void DrawObjectAtCoordinates()
    {
        Debug.Log("DrawObject button clicked.");
        ShowMarkerLabelInput();
    }

    public void PlaceMarker()
    {
        string markerLabel = markerLabelInput.text;
        var currentLocation = gpsManager.GetCurrentLocation();
        Vector2 currentCoordinate = new Vector2(currentLocation.latitude, currentLocation.longitude);

        MapManager mapManager = gpsManager.GetComponent<MapManager>();
        mapManager.DrawMarker(currentCoordinate, markerLabel);
        mapManager.DrawMarker(destinationCoordinate, "Destination");

        HideMarkerLabelInput();
    }

    public void CalculateDistanceAndDisplayLocalPosition()
    {
        if (gpsManager != null && destinationCoordinate != Vector2.zero)
        {
            Vector2 currentCoordinate = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            float distance = gpsManager.locationProcessor.CalculateDistance(currentCoordinate, destinationCoordinate);
            distanceDisplay.text = $"Distance: {distance:F2} meters";

            // Convert to Unity local position and update the display
            Vector3 localPosition = GPSEncoder.GPSToUCS(currentCoordinate);
            unityLocalPositionDisplay.text = $"Unity Local Position: {localPosition}";
        }
    }

    private void ShowMarkerLabelInput()
    {
        markerLabelInput.gameObject.SetActive(true);
        placeMarkerButton.gameObject.SetActive(true);
    }

    private void HideMarkerLabelInput()
    {
        markerLabelInput.gameObject.SetActive(false);
        placeMarkerButton.gameObject.SetActive(false);
    }
}
