using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatuesPuzzleLightpoint : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Vector3 startingPoint;

	public PuzzlePathComponent initialPath;

	PuzzlePathComponent currentPath;
	PuzzlePathComponent destinationPath;
	public float moveAmountPerSecond = 0.1f;

	bool isMoving;
	PointerEventData lastMouseData;
	public void Update() {

		

		if(isMoving)
		{
			transform.position = transform.position + ((destinationPath.transform.position - transform.position) * moveAmountPerSecond * Time.deltaTime);

			if((destinationPath.transform.position - this.transform.position).magnitude < 0.2f )
			{
				SetNewPathPoint(destinationPath);
			}
		}
		else
		{

		}
	}

	public void OnBeginDrag(PointerEventData eventData) {
		lastMouseData = eventData;
		isMoving = true;
       	startingPoint = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
		lastMouseData = eventData;
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
		lastMouseData = eventData;
		isMoving = false;
		this.transform.position = startingPoint;
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
	}
}
