using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum StatueGemState
{
	idle,
	moving,
	returning
}

public class StatuesPuzzleLightpoint : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public PuzzlePathComponent initialPath;

	PuzzlePathComponent currentPath;
	PuzzlePathComponent destinationPath;
	public float moveAmountPerSecond = 0.1f;
	public float threshold = 0.2f;

	StatueGemState gemState;
	PointerEventData lastMouseData;

	public void Awake() {
		currentPath = initialPath;
		gemState = StatueGemState.idle;
	}
	public void Update() {

		if(gemState == StatueGemState.moving)
		{
			transform.position = transform.position + ((destinationPath.transform.position - transform.position) * moveAmountPerSecond * Time.deltaTime);

			if((destinationPath.transform.position - this.transform.position).magnitude < threshold )
			{
				SetNewPathPoint(destinationPath);
			}
			AC.KickStarter.player.Halt();
		}
		else if(gemState == StatueGemState.returning)
		{
			transform.position = transform.position + ((currentPath.transform.position - transform.position) * moveAmountPerSecond * Time.deltaTime);

			if((destinationPath.transform.position - this.transform.position).magnitude < threshold )
			{
				gemState = StatueGemState.idle;
			}
			AC.KickStarter.player.Halt();
		}
	}

	public void OnBeginDrag(PointerEventData eventData) {
		lastMouseData = eventData;
		gemState = StatueGemState.moving;
		SetNewPathPoint(currentPath);
		AC.KickStarter.player.Halt();
    }

    public void OnDrag(PointerEventData eventData) {
		lastMouseData = eventData;
		AC.KickStarter.player.Halt();
    }

    public void OnEndDrag(PointerEventData eventData) {
		lastMouseData = eventData;
		gemState = StatueGemState.returning;
		AC.KickStarter.player.Halt();
    }

	public void SetNewPathPoint(PuzzlePathComponent newStartingPoint) {
		currentPath = newStartingPoint;

		//	Choosing the closest point to the mouse pointer position
		destinationPath = currentPath.connectedTo[0];
		foreach (var point in currentPath.connectedTo)
		{
			if(Vector3.Distance(point.transform.position, currentPath.transform.position) < Vector3.Distance(destinationPath.transform.position, currentPath.transform.position))
			{
				destinationPath = point;
			}
		}

		Debug.Log(string.Format("Setting new point :: {0} is the closest path. Current path: {1}", destinationPath.name, currentPath.name));
	}
}
