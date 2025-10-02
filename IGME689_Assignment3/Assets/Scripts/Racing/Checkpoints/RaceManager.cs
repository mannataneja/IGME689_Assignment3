using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] List<DistanceTracker> cars = new List<DistanceTracker>();
    [SerializeField] List<DistanceTracker> rankedCars = new List<DistanceTracker>();
    [SerializeField] Canvas hud;
    [SerializeField] Canvas endScreen;

    private void Start()
    {
        hud.enabled = true;
        endScreen.enabled = false;
    }
    void Update()
    {
        rankedCars = cars.OrderByDescending(x => x.distance).ToList();
        foreach (var car in rankedCars)
        {
            car.rank = rankedCars.IndexOf(car) + 1;
        }
    }
    public void Finished()
    {
        hud.enabled = false;
        endScreen.enabled = true;
    }
}
