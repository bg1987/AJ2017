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
	public float magnituteFromStatue = 5f;
	public Animator pathAnimator;

    [Space]
    public AC.Interaction winInteraction;

	Canvas puzzleCanvas;
	AudioSource moveSoundSource;
	bool isFuckingMoving;

	bool midAnimationTransition;
	bool lastState;
	float lastUpdate;
	Transform currentPlayer;
	Transform currPos;


	void Start() {
		moveSoundSource = GetComponent<AudioSource> ();
		puzzleCanvas = GetComponent<Canvas>();
		puzzleCanvas.enabled = false;
		currentPlayer = AC.KickStarter.player.transform;
		currPos = transform.parent;

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

	void Update() {

		if (!lastState) {
			if ((currentPlayer.position - currPos.position).magnitude < magnituteFromStatue) {
				ActivatePuzzle ();
				lastState = true;
			}
		} else {
			if ((currentPlayer.position - currPos.position).magnitude > magnituteFromStatue) {
				DeactivatePuzzle ();
				lastState = false;
			}
		}

		if (lastUpdate > 1f) {
			lastUpdate = 0f;
			//Debug.Log (name + " [" + (currentPlayer.position - currPos.position).magnitude.ToString () + "]");
		}

		lastUpdate += Time.deltaTime;
	}

	void PlayerInActiveArea() {
	}

    private void OnStaffColorChange()
    {
		if (StaffHeadColor.CurrentColorIndex == staffColorIndex && (currentPlayer.position - currPos.position).magnitude < magnituteFromStatue) {
			borderLines.SetActive (true);
			pathAnimator.Play ("PathFadeIn");
		} else {
			if (borderLines.activeSelf) {
				pathAnimator.Play ("PathFadeOut");
				StartCoroutine (DisablePathDelayed ());
			}
		}
    }

	//	Called by an event. When one moves, all of them move.
    private void MoveLightPoint(PuzzlePathComponent destination)
    {
		if(isFuckingMoving)
			return;

            var path = paths.FirstOrDefault(t => t.xpos == destination.xpos && t.ypos == destination.ypos);

            if (path != null)
            {
				StartCoroutine(MoveToPoint(LightPoint.transform, path));
				currentLocation = path;
            }
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
		StartCoroutine(MoveToPoint(LightPoint.transform, destination, completePath));
    }

	IEnumerator MoveToPoint(Transform currentPoint, PuzzlePathComponent destination, List<PuzzlePathComponent> completePath)
	{
		AC.KickStarter.player.Halt ();
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

        /*TODO: Babin, this settings controls whether you need to click exactly inside the navmesh
         * if you set it to zero while there is a puzzle thats turned on (i.e. visible maze)
         * and off when there isnt any, this should give the exact behaviour we want regarding walking in this scene.
         * i though this is the right place but i cant get it to work just right.
         */
        //AC.KickStarter.settingsManager.walkableClickRange = 0; 

        if (!puzzleCanvas.enabled)
		{

			OnStaffColorChange();
			puzzleCanvas.enabled = true;
			StaffHeadColor.OnColorChanged += OnStaffColorChange;
		}
	}

	void DeactivatePuzzle() {
		Debug.Log("Out of puzzle " + name);
        //AC.KickStarter.settingsManager.walkableClickRange = 1f;

        if (puzzleCanvas.enabled)
		{

			OnStaffColorChange();
			puzzleCanvas.enabled = false;
			StaffHeadColor.OnColorChanged -= OnStaffColorChange;
		}
	}
		
	IEnumerator DisablePathDelayed ()
	{
		yield return new WaitForSeconds (0.5f);
		if (StaffHeadColor.CurrentColorIndex != staffColorIndex && puzzleCanvas.enabled) {
			borderLines.SetActive(false);
		}
	}
}
