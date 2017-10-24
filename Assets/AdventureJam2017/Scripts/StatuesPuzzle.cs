using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuesPuzzle : MonoBehaviour {

	List<PuzzlePathComponent> paths;

	public PuzzlePathComponent currentLocation;

	void Start() {
		foreach (var path in paths) {
			path.OnButtonClick += TryMoveToPos;
		}
	}

	void TryMoveToPos (PuzzlePathComponent pathPoint)
	{
		List<string> scanned = new List<string>();
		List<PuzzlePathComponent> completePath  = new List<PuzzlePathComponent>();

		ScanForPath(currentLocation, pathPoint, completePath);
	}

	bool ScanForPath (PuzzlePathComponent currentLocation, PuzzlePathComponent pathPoint, List<PuzzlePathComponent> completePath)
	{
		throw new System.NotImplementedException ();
	}
}
