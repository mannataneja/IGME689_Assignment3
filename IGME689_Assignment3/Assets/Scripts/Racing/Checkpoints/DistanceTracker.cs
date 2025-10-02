using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    public float distance;
    public float checkpointDistance;
    public GameObject checkpoint;
    public int rank;
    public bool isPlayer;
    public TMP_Text rankText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "checkpoint")
        {
            checkpoint = other.gameObject;
            checkpointDistance = other.gameObject.GetComponent<Checkpoint>().checkpoint;
        }
    }
    private void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, checkpoint.transform.position) + checkpointDistance;
        if (isPlayer)
        {
            rankText.text = "Rank: " + rank;
        }
    }
}
