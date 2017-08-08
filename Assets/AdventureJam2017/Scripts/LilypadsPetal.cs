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

	public const float timeBetweenLilies = 0.3f;

    [Header("Yellow Path")]
    public LilypadsPetal nextYellowLily;
    public LilypadsPetal prevYellowLily;

    [Header("Purple Path")]
    public LilypadsPetal nextPurpleLily;
    public LilypadsPetal prevPurpleLily;

    [Header("Return Path")]
    public LilypadsPetal nextGreenLily;
    public LilypadsPetal prevGreenLily;

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
            switch (StaffHeadColor.CurrentColorIndex)
            {
                case 1:
                    if (prevGreenLily != null)
                        StartCoroutine(MoveBackSequence());
                    else if (prevGreenLily == null && nextGreenLily != null)
                        StartCoroutine(StartMovingForwardSequence());
                    break;
                case 2:
                    if (prevPurpleLily != null)
                        StartCoroutine(MoveBackSequence());
                    else if (prevPurpleLily == null && nextPurpleLily != null)
                        StartCoroutine(StartMovingForwardSequence());
                    break;
                case 3:
                    if (prevYellowLily != null)
                        StartCoroutine(MoveBackSequence());
                    else if (prevYellowLily == null && nextYellowLily != null)
                        StartCoroutine(StartMovingForwardSequence());
                    break;
                default:
                    break;
            }
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
                if(nextGreenLily != null && prevGreenLily == null)
				    StartCoroutine(ActivateAllLilliesSequence(LilyState.Green));
                else if(nextGreenLily == null && prevGreenLily != null)
                    StartCoroutine(ActivateAllLilliesSequence(LilyState.Green));
                break;
			case 2:
                    if (nextPurpleLily != null && prevPurpleLily == null)
                        StartCoroutine(ActivateAllLilliesSequence(LilyState.Purple));
                    if (nextPurpleLily == null && prevPurpleLily != null)
                        StartCoroutine(ActivateAllLilliesSequence(LilyState.Purple));
                    break;
			case 3:
                    if (nextYellowLily != null && prevYellowLily == null)
                        StartCoroutine(ActivateAllLilliesSequence(LilyState.Yellow));
                    if (nextYellowLily == null && prevYellowLily != null)
                        StartCoroutine(ActivateAllLilliesSequence(LilyState.Yellow));
                    break;
			default:
				break;
			}
		}

    }

	IEnumerator StartMovingForwardSequence() {
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

    IEnumerator MoveBackSequence()
    {
        LilypadsPetal currentLily = this;
        Transform player = AC.KickStarter.player.transform;

        while (currentLily != null)
        {
            player.transform.position = currentLily.transform.position;

            yield return new WaitForSeconds(0.7f);

            switch (currentState)
            {
                case LilyState.Green:
                    currentLily = currentLily.prevGreenLily;
                    break;
                case LilyState.Purple:
                    currentLily = currentLily.prevPurpleLily;
                    break;
                case LilyState.Yellow:
                    currentLily = currentLily.prevYellowLily;
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

        //LilyState oldState = currentLily.currentState == LilyState.Closed ? newState : currentLily.currentState;
        LilyState oldState = currentState;
        currentLily.ActivateLilyState(newState);

        //  Close previous path
        while (currentLily != null)
        {
            switch (oldState)
            {
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

            if(currentLily != null)
            {
                currentLily.ActivateLilyState(LilyState.Closed);

                yield return new WaitForSeconds(timeBetweenLilies);     
            }
        }

        if (newState != LilyState.Closed)
        {
            currentLily = this;

            while (currentLily != null)
		    {
                //	Traverse according to old state
                switch (newState)
                {
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

                if(currentLily != null)
                {
                    currentLily.ActivateLilyState(newState);

			        yield return new WaitForSeconds(timeBetweenLilies);
                }
		    }
        }
	}

	void ActivateLilyState(LilyState state)
	{
		switch (state) {
		    case LilyState.Closed:
			    currentState = LilyState.Closed;
                Gem.SetActive(false);
			    ClosedState.SetActive(true);
			    OpenedState.SetActive(false);
			    break;
		    case LilyState.Green:
			    currentState = LilyState.Green;
                Gem.SetActive(true);
                Gem.GetComponent<SpriteRenderer>().color = Color.green;
                ClosedState.SetActive(false);
			    OpenedState.SetActive(true);
			    break;
		    case LilyState.Purple:
			    currentState = LilyState.Purple;
                Gem.SetActive(true);
                Gem.GetComponent<SpriteRenderer>().color = new Color(132, 115, 255);
                ClosedState.SetActive(false);
			    OpenedState.SetActive(true);
			    break;
		    case LilyState.Yellow:
			    currentState = LilyState.Yellow;
                Gem.SetActive(true);
                Gem.GetComponent<SpriteRenderer>().color = Color.yellow;
                ClosedState.SetActive(false);
			    OpenedState.SetActive(true);
			    break;
		    default:
			    break;
		}
	}
}
