using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LilyState
{
    Closed = 0,
    Green = 1,
    Purple = 2,
    Yellow = 3
}

public class LilypadsPetal : MonoBehaviour {

	public const float timeBetweenLilies = 1f;

    [Header("Yellow Path")]
    public LilypadsPetal nextYellowLily;

    [Header("Purple Path")]
    public LilypadsPetal nextPurpleLily;

    [Header("Return Path")]
    public LilypadsPetal nextGreenLily;

    [Space]
    [Header("Refs")]
    public GameObject ClosedState;
    public GameObject OpenedState;
    public GameObject Gem;

    private LilyState currentState;

    // Use this for initialization
    void Start () {
		
	}

    public void OpenLily()
    {
		//	Move if they match
		if(StaffHeadColor.CurrentColorIndex == (int)currentState && currentState != LilyState.Closed)
		{
			StartCoroutine(StartMovingSequence());
		}
		else
		{
			//	Switch the color if they don't
			switch (StaffHeadColor.CurrentColorIndex)
			{
			case 0:
				StartCoroutine(ActivateAllLilliesSequence(LilyState.Closed));
				break;
			case 1:
				StartCoroutine(ActivateAllLilliesSequence(LilyState.Green));
				break;
			case 2:
				StartCoroutine(ActivateAllLilliesSequence(LilyState.Purple));
				break;
			case 3:
				StartCoroutine(ActivateAllLilliesSequence(LilyState.Yellow));
				break;
			default:
				break;
			}
		}

    }

	IEnumerator StartMovingSequence() {
		LilypadsPetal currentLily = this;
		Transform player = AC.KickStarter.player.transform;

		while(currentLily != null)
		{
			player.transform.position = currentLily.transform.position;

			yield return new WaitForSeconds(1f);

			switch (currentState) {
			case LilyState.Green:
				currentLily = currentLily.nextGreenLily;
				break;
			case LilyState.Purple:
				currentLily = currentLily.nextPurpleLily;
				break;
			case LilyState.Yellow:
				currentLily = currentLily.nextYellowLily;
				break;
			default:
				currentLily = null;
				break;
			}
		}
	}
		
	IEnumerator ActivateAllLilliesSequence(LilyState newState)
	{
		LilypadsPetal currentLily = this;
		LilyState oldState = currentLily.currentState;

		while(currentLily != null)
		{
			currentLily.ActivateLilyState(newState, currentLily);

			yield return new WaitForSeconds(timeBetweenLilies);

			//	Traverse according to old state
			switch (oldState) {
			case LilyState.Green:
				currentLily = currentLily.nextGreenLily;
				break;
			case LilyState.Purple:
				currentLily = currentLily.nextPurpleLily;
				break;
			case LilyState.Yellow:
				currentLily = currentLily.nextYellowLily;
				break;
			default:
				currentLily = null;
				break;
			}
		}
	}

	void ActivateLilyState(LilyState state, LilypadsPetal lilypad)
	{
		switch (state) {
		case LilyState.Closed:
			lilypad.currentState = LilyState.Closed;
			lilypad.ClosedState.SetActive(true);
			lilypad.OpenedState.SetActive(false);
			break;
		case LilyState.Green:
			lilypad.currentState = LilyState.Green;
			lilypad.ClosedState.SetActive(false);
			lilypad.OpenedState.SetActive(true);
			break;
		case LilyState.Purple:
			lilypad.currentState = LilyState.Purple;
			lilypad.ClosedState.SetActive(false);
			lilypad.OpenedState.SetActive(true);
			break;
		case LilyState.Yellow:
			lilypad.currentState = LilyState.Yellow;
			lilypad.ClosedState.SetActive(false);
			lilypad.OpenedState.SetActive(true);
			break;
		default:
			break;
		}
	}
}
