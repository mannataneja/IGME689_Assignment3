using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField] float sunrise;
    [SerializeField] float sunset;
    [SerializeField] GameObject[] lamps;
    [Header("Use actual time of day or simulated time")]
    [SerializeField] bool useActualTime;
    [SerializeField, Range(0, 24)] private float SimulatedTimeOfDay;
    [SerializeField] ArcGISMapComponent arcGISMap;

    float timePercent;
    void Update()
    {
        if (Preset == null) return;

        if (!useActualTime)
        {
            if (Application.isPlaying)
            {
                SimulatedTimeOfDay += Time.deltaTime;
                SimulatedTimeOfDay %= 24;
                UpdateLighting(SimulatedTimeOfDay);
            }
            else
            {
                UpdateLighting(SimulatedTimeOfDay);

            }
        }
        else
        {
            UpdateLighting(System.DateTime.Now.Hour);
        }
    }

    private void UpdateLighting(float time)
    {
        timePercent = time / 24;
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        double longitude = arcGISMap.OriginPosition.X;
        if (longitude < 0) longitude += 360f;

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) + (float)longitude - 90f, 0f, 0f));
        }
    }
}