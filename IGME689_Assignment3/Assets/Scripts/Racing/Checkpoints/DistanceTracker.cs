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
    public bool isFinished;

    public Canvas endScreen;
    private void Start()
    {
        if (isPlayer)
        {
            isFinished = false;
            endScreen.enabled = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "checkpoint")
        {
            checkpoint = other.gameObject;
            checkpointDistance = other.gameObject.GetComponent<Checkpoint>().checkpoint;
        }
        if(other.gameObject.tag == "finished" && distance > 180000)
        {
            isFinished = true;
        }
    }
    private void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, checkpoint.transform.position) + checkpointDistance;
        if (isPlayer && !isFinished)
        {
            rankText.text = "Rank: " + rank;
            endScreen.enabled = true;
        }
    }
}
