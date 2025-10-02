using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] Checkpoint[] checkpoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].checkpointDistance = i * 1000;
        }
    }
}
