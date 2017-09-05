using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum LilyState
{
    Closed = 0,
    Green = 1,
    Purple = 2,
    Yellow = 3,
	Complete = 4
}

public class LilypadsPetal : MonoBehaviour {

    public const float timeBetweenLilies = 0.3f;

    public LilyState lilyColor;

    public LilypadsPetal[] AffectedLillies;

    [Space]
    [Header("Refs")]
    public GameObject ClosedState;
    public GameObject OpenedState;
    public GameObject Gem;
    public GameObject CompleteEmitter;

    LilyState currentState;

	public int StepsToComplete;
	int currentSteps;

    internal void UpdateAffectedLilies()
    {
        foreach (var lily in AffectedLillies)
        {
            UpdateLily(lily);
        }
    }

	public void Reset ()
	{
		currentSteps = 0;
        ActivateLilyState(LilyState.Closed);
	}

    public static void UpdateLily(LilypadsPetal lily)
    {
		switch (lily.StepsToComplete - lily.currentSteps) {
			case 0:
				lily.lilyColor = LilyState.Complete;
				break;
			case 1:
				lily.lilyColor = LilyState.Green;
				break;
			case 2:
				lily.lilyColor = LilyState.Purple;
				break;
			case 3:
				lily.lilyColor = LilyState.Yellow;
				break;
		}

        if (StaffHeadColor.CurrentColorIndex == (int)lily.lilyColor || lily.lilyColor == LilyState.Complete)
        {
            lily.ActivateLilyState(lily.lilyColor);
        }
        else if (lily.currentState != LilyState.Closed)
        {
            lily.ActivateLilyState(LilyState.Closed);
        }
    }

	public void StepAway(LilypadsPetal target) {
        foreach (var lily in AffectedLillies)
        {
            if(lily != target)
            {
                if (lily.lilyColor != LilyState.Complete)
                    lily.ActivateLilyState(LilyState.Closed);
                else
                    lily.ActivateLilyState(LilyState.Complete);
            }
        }
        currentSteps ++;
	}

    public void OpenLily()
    {
        if (currentState == LilyState.Complete)
        {
            LilyPadContainer.instance.ResetMaze();
            return;
        }

        //	Move if they match
        if (StaffHeadColor.CurrentColorIndex == (int)currentState && currentState != LilyState.Closed)
		{
            KickStarter.player.transform.position = transform.position;
            if(LilyPadContainer.instance.currentlySelectedLily != null)
			    LilyPadContainer.instance.currentlySelectedLily.StepAway(this);
            LilyPadContainer.instance.currentlySelectedLily = this;
            UpdateAffectedLilies();
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
                CompleteEmitter.SetActive(false);
                break;
		    case LilyState.Green:
			    currentState = LilyState.Green;
                Gem.SetActive(true);
                Gem.GetComponent<SpriteRenderer>().color = Color.green;
                ClosedState.SetActive(false);
			    OpenedState.SetActive(true);
                CompleteEmitter.SetActive(false);
                break;
		    case LilyState.Purple:
			    currentState = LilyState.Purple;
                Gem.SetActive(true);
                Gem.GetComponent<SpriteRenderer>().color = new Color(132, 115, 255);
                ClosedState.SetActive(false);
			    OpenedState.SetActive(true);
                CompleteEmitter.SetActive(false);
                break;
		    case LilyState.Yellow:
			    currentState = LilyState.Yellow;
                Gem.SetActive(true);
                Gem.GetComponent<SpriteRenderer>().color = Color.yellow;
                ClosedState.SetActive(false);
			    OpenedState.SetActive(true);
                CompleteEmitter.SetActive(false);
                break;
			case LilyState.Complete:
				currentState = LilyState.Complete;
				Gem.SetActive(false);
				ClosedState.SetActive(true);
				OpenedState.SetActive(false);
                CompleteEmitter.SetActive(true);
                break;
		    default:
			    break;
		}
	}
}
