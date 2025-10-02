using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] List<DistanceTracker> cars = new List<DistanceTracker>();
    [SerializeField] List<DistanceTracker> rankedCars = new List<DistanceTracker>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rankedCars = cars.OrderByDescending(x => x.distance).ToList();
        foreach (var car in rankedCars)
        {
            car.rank = rankedCars.IndexOf(car) + 1;
        }
    }
}
