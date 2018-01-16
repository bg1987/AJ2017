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

	public float framesPerStep = 30f;
	public float marginOfError = 0.05f;
	public AudioClip movingSound;

    [Header("Staff reaction")]
    public GameObject borderLines;
    public int staffColorIndex;

    [Space]
    public AC.Interaction winInteraction;

	Canvas puzzleCanvas;
	AudioSource moveSoundSource;
	bool isFuckingMoving;

	void Start() {
		moveSoundSource = GetComponent<AudioSource> ();
		puzzleCanvas = GetComponent<Canvas>();
		puzzleCanvas.enabled = false;

		foreach (var path in paths) {
			path.OnButtonClick += TryMoveToPos;
		}

        OnLightpointMove += MoveLightPoint;
        OnStaffColorChange();
	}

	void OnDestroy() {
		if(puzzleCanvas.enabled)
			StaffHeadColor.OnColorChanged -= OnStaffColorChange;
	}

    private void OnStaffColorChange()
    {
		borderLines.SetActive(StaffHeadColor.CurrentColorIndex == staffColorIndex && puzzleCanvas.enabled);
    }

    private void MoveLightPoint(PuzzlePathComponent destination)
    {
		if(isFuckingMoving)
			return;
//        LightPoint.transform.position = destination.transform.position;
//
//        if (paths.Contains(destination))
//        {
//            //  Move with animation
//        }
//        else
//        {
            var path = paths.FirstOrDefault(t => t.xpos == destination.xpos && t.ypos == destination.ypos);

            if (path != null)
            {
                //  Teleport
//                LightPoint.transform.position = path.transform.position;
//                currentLocation = path;
				StartCoroutine(MoveToPoint(LightPoint.transform, path));
				currentLocation = path;
            }
//        }
    }

    void TryMoveToPos (PuzzlePathComponent pathPoint)
	{
		if (StaffHeadColor.CurrentColorIndex != staffColorIndex || isFuckingMoving)
            return;

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
		StartCoroutine(MoveToPoint(LightPoint.transform, destination, completePath));
    }

	IEnumerator MoveToPoint(Transform currentPoint, PuzzlePathComponent destination, List<PuzzlePathComponent> completePath)
	{
		isFuckingMoving = true;
		for (int i = completePath.Count-1; i > 0; i--) {
			if(OnLightpointMove != null)
				OnLightpointMove(completePath[i]);
			
			yield return MoveToPoint(currentPoint, completePath[i]);
			currentLocation = completePath[i];
			//Debug.Log("moved to " + completePath[i].transform.position);
		}

		//	Completed the puzzle
		if(destination.xpos == 1 && destination.ypos == 7)
		{
			//  Win scenario
			//moveAwayInteraction.Interact();
			if(winInteraction != null)
				winInteraction.Interact();
		}

//		if(OnLightpointMove != null)
//			OnLightpointMove(destination);

		isFuckingMoving = false;
	}

	IEnumerator MoveToPoint(Transform currentPoint, PuzzlePathComponent endPoint)
	{
		moveSoundSource.Play ();
		Transform dest = endPoint.transform;
		Vector3 step = (dest.position - currentPoint.position) / framesPerStep;

		while(Vector3.Magnitude(currentPoint.position - dest.position) > marginOfError)
		{
			currentPoint.position = currentPoint.position + step;
			yield return new WaitForSeconds(0.02f);
		}

		currentPoint.position = dest.position;
	}

	void ActivatePuzzle() {

		Debug.Log("Entered puzzle " + name);
		if(!puzzleCanvas.enabled)
		{
			puzzleCanvas.enabled = true;
			StaffHeadColor.OnColorChanged += OnStaffColorChange;
			OnStaffColorChange();
		}
	}

	void DeactivatePuzzle() {
		Debug.Log("Out of puzzle " + name);
		if(puzzleCanvas.enabled)
		{
			puzzleCanvas.enabled = false;
			StaffHeadColor.OnColorChanged -= OnStaffColorChange;
			OnStaffColorChange();
		}
	}
}
