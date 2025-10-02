using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class Wall : MonoBehaviour
{
    [SerializeField] ArcGISFeatureLayerComponent featureLayerComponent;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private GameObject prefab;

    public float spacing = 2f;          // Distance between objects
    public float horizontalOffset = 0f; // Side offset
    public bool clearOldOnBuild = true;

    void Start()
    {
        splineContainer = featureLayerComponent.GetComponentInChildren<SplineContainer>();
    }

    public void Build()
    {
        if (splineContainer == null || prefab == null)
        {
            Debug.LogWarning("SplineObjectSpawner: Missing splineContainer or prefab.");
            return;
        }

        if (clearOldOnBuild)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        // Get spline length in world space
        float4x4 localToWorld = (float4x4)splineContainer.transform.localToWorldMatrix;
        float length = SplineUtility.CalculateLength(splineContainer.Spline, localToWorld);

        if (length <= 0.01f) return;

        // Step along spline by distance
        for (float distance = 0; distance < length; distance += spacing)
        {
            float t = distance / length;

            float3 pos, tangent, up;
            SplineUtility.Evaluate(splineContainer.Spline, t, out pos, out tangent, out up);

            Vector3 forward = ((Vector3)tangent).normalized;
            Vector3 right = Vector3.Cross((Vector3)up, forward).normalized;

            Vector3 spawnPos = (Vector3)pos + right * horizontalOffset;
            Quaternion rot = Quaternion.LookRotation(forward, (Vector3)up);

            GameObject go = Instantiate(prefab, spawnPos, rot, transform);
            go.name = prefab.name + "_" + Mathf.RoundToInt(distance);
        }
    }
}
