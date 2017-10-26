using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuesPuzzle : MonoBehaviour {

    public static Action<PuzzlePathComponent> OnLightpointMove;

    public GameObject LightPoint;
	public List<PuzzlePathComponent> paths;
	public PuzzlePathComponent currentLocation;

	void Start() {
		foreach (var path in paths) {
			path.OnButtonClick += TryMoveToPos;
		}

        OnLightpointMove += MoveLightPoint;
	}

    private void MoveLightPoint(PuzzlePathComponent destination)
    {
        LightPoint.transform.position = destination.transform.position;
    }

    void TryMoveToPos (PuzzlePathComponent pathPoint)
	{
		List<PuzzlePathComponent> completePath  = new List<PuzzlePathComponent>();
        List<PuzzlePathComponent> totalScanned = new List<PuzzlePathComponent>();

        if (ScanForPath(currentLocation, pathPoint, completePath, totalScanned))
            MoveLightPointToEnd(pathPoint, completePath);
	}

	bool ScanForPath (PuzzlePathComponent currentLocation, PuzzlePathComponent pathPoint, List<PuzzlePathComponent> completePath, List<PuzzlePathComponent> totalScanned)
	{
        if (totalScanned.Contains(currentLocation))
            return false;

        totalScanned.Add(currentLocation);

        if (currentLocation == pathPoint)
        {
            completePath.Add(currentLocation);

            return true;
        }

        foreach (var path in currentLocation.connectedTo)
        {
            if (ScanForPath(path, pathPoint, completePath, totalScanned))
            {
                completePath.Add(path);

                return true;
            }
        }

        return false;
	}

    private void MoveLightPointToEnd(PuzzlePathComponent destination, List<PuzzlePathComponent> completePath)
    {
        LightPoint.transform.position = destination.transform.position;
    }
}
