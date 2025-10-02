using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class AICarController : MonoBehaviour
{
    [SerializeField]
    private SplineContainer splineContainer;

    public float baseSpeed = 1;
    public float speed;
    public float speedVariance;
    public float accelerationInterval;
    public bool accelerating;

    public float HorizontalOffset = 0;
    private float3 previousNearest = float3.zero;

    private void Start()
    {
        speed = 0;
        accelerating = true;
        StartCoroutine(nameof(Accelerate));
    }
    IEnumerator Accelerate()
    {
        while (true)
        {
            if (accelerating)
            {
                speed++;
            }
            yield return new WaitForSeconds(accelerationInterval);
        }

    }
    private void Update()
    {
        if(speed >= baseSpeed)
        {
            accelerating = false;
            StopCoroutine(nameof(Accelerate));
        }
        var localPoint = splineContainer.transform.InverseTransformPoint(transform.position);

        SplineUtility.GetNearestPoint(splineContainer.Spline, localPoint, out var nearest, out var ratio, 10, 4);

        var tangent = SplineUtility.EvaluateTangent(splineContainer.Spline, ratio);

        var rotation = Quaternion.LookRotation(tangent);
        transform.rotation = rotation;

        if (Vector3.SqrMagnitude(previousNearest - nearest) >= 0.0001)
        {
            var globalNearest = splineContainer.transform.TransformPoint(nearest);
            var perpendicular = Vector3.Cross(tangent, Vector3.up);
            var position = globalNearest + (perpendicular.normalized * HorizontalOffset);
            transform.position = position;

            previousNearest = nearest;
        }
        else
        {
            Debug.LogWarning("Same nearest point twice in a row! Previous: " + previousNearest + ", New: " + nearest);
        }
        if (!accelerating)
        {
            speed = baseSpeed + UnityEngine.Random.Range(-speedVariance, speedVariance);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
