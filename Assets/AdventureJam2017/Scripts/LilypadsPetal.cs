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

    [Space]
    public GameObject YellowEmitter;
    public GameObject PurpleEmitter;
    public GameObject GreenEmitter;
    public GameObject CompleteEmitter;

    [Space]
    public Animator LilyEffectAnimator;

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
                LilyEffectAnimator.Play("LilyCloseAnim");
                CompleteEmitter.SetActive(false);
                GreenEmitter.SetActive(false);
                PurpleEmitter.SetActive(false);
                YellowEmitter.SetActive(false);
                break;
		    case LilyState.Green:
			    currentState = LilyState.Green;
                LilyEffectAnimator.Play("LilyOpenAnim");
                GreenEmitter.SetActive(true);
                PurpleEmitter.SetActive(false);
                YellowEmitter.SetActive(false);
                CompleteEmitter.SetActive(false);
                break;
		    case LilyState.Purple:
			    currentState = LilyState.Purple;
                LilyEffectAnimator.Play("LilyOpenAnim");
                GreenEmitter.SetActive(false);
                PurpleEmitter.SetActive(true);
                YellowEmitter.SetActive(false);
                CompleteEmitter.SetActive(false);
                break;
		    case LilyState.Yellow:
			    currentState = LilyState.Yellow;
                LilyEffectAnimator.Play("LilyOpenAnim");
                GreenEmitter.SetActive(false);
                PurpleEmitter.SetActive(false);
                YellowEmitter.SetActive(true);
                CompleteEmitter.SetActive(false);
                break;
			case LilyState.Complete:
				currentState = LilyState.Complete;
                LilyEffectAnimator.Play("LilyOpenAnim");
                GreenEmitter.SetActive(false);
                PurpleEmitter.SetActive(false);
                YellowEmitter.SetActive(false);
                CompleteEmitter.SetActive(true);
                break;
		    default:
			    break;
		}
	}
}
