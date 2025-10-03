using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AccelerationSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.Play();
        }
    }
}
