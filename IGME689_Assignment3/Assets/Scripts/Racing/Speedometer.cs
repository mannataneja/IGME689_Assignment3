using UnityEngine;
using TMPro;
[RequireComponent(typeof(Rigidbody))]
public class Speedometer : MonoBehaviour
{
    [SerializeField] enum SpeedUnit { MetersPerSecond, KilometersPerHour, MilesPerHour }
    [SerializeField] SpeedUnit unit = SpeedUnit.MilesPerHour;
    [SerializeField] public float speed;
    [SerializeField] float convertedSpeed;
    [SerializeField] TMP_Text speedText;
    
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get horizontal velocity
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        speed = flatVelocity.magnitude; // meters per second

        convertedSpeed = speed;
        string unitLabel = "m/s";

        switch (unit)
        {
            case SpeedUnit.KilometersPerHour:
                convertedSpeed = Mathf.Round(speed * 3.6f);
                unitLabel = "km/h";
                break;

            case SpeedUnit.MilesPerHour:
                convertedSpeed =  Mathf.Round(speed * 2.237f);
                unitLabel = "mph";
                break;
        }

        if (speedText != null)
            speedText.text = convertedSpeed + " " + unitLabel;
    }
}
