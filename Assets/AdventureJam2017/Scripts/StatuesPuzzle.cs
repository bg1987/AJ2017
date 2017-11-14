using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatuesPuzzle : MonoBehaviour {

    public static Action<PuzzlePathComponent> OnLightpointMove;

    public GameObject LightPoint;
	public List<PuzzlePathComponent> paths;
	public PuzzlePathComponent currentLocation;

	Canvas puzzleCanvas;

	void Start() {
		puzzleCanvas = GetComponent<Canvas>();
		puzzleCanvas.enabled = false;

		foreach (var path in paths) {
			path.OnButtonClick += TryMoveToPos;
		}

        OnLightpointMove += MoveLightPoint;
	}

    private void MoveLightPoint(PuzzlePathComponent destination)
    {
        LightPoint.transform.position = destination.transform.position;

        if (paths.Contains(destination))
        {
            //  Move with animation
        }
        else
        {
            var path = paths.FirstOrDefault(t => t.xpos == destination.xpos && t.ypos == destination.ypos);

            if (path != null)
            {
                //  Teleport
                LightPoint.transform.position = path.transform.position;
                currentLocation = path;
            }
        }
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
        //LightPoint.transform.position = destination.transform.position;
        //currentLocation = destination;
        
        if(OnLightpointMove != null)
            OnLightpointMove(destination);
    }

	void ActivatePuzzle() {
		puzzleCanvas.enabled = true;
	}

	void DeactivatePuzzle() {
		puzzleCanvas.enabled = false;
	}
}
