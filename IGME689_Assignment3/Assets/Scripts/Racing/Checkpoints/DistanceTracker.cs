using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    public float distance;
    public float checkpointDistance;
    public Checkpoint checkpoint;
    public int rank;
    public bool isPlayer;
    public TMP_Text rankText;
    public bool isFinished;
    public RaceManager raceManager;

    private void Start()
    {
        if (isPlayer)
        {
            isFinished = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "checkpoint" || other.gameObject.tag == "finish")
        {
            if(checkpoint.checkpointDistance + 1000 == other.gameObject.GetComponent<Checkpoint>().checkpointDistance)
            {
                checkpoint = other.gameObject.GetComponent<Checkpoint>();
                checkpointDistance = other.gameObject.GetComponent<Checkpoint>().checkpointDistance;

                if (other.gameObject.tag == "finish" && isPlayer)
                {
                    isFinished = true;
                    raceManager.Finished();
                }
            }
        }

    }
    private void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, checkpoint.transform.position) + checkpointDistance;
        if (isPlayer && !isFinished)
        {
            rankText.text = "Rank: " + rank;
        }
    }
}
