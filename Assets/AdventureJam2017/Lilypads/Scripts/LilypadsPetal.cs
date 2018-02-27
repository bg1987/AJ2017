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
    public Animator LilyEffectAnimator;
	public SpriteRenderer baseColor;

    LilyState currentState;

	public int StepsToComplete;
	int currentSteps;

	void Start() {
		var rend = GetComponentsInChildren<SpriteRenderer> (true);
		int order = 20;

		switch (transform.parent.name) {
		case "Top":
			order = 5;
			break;
		case "CenterTop":
			order = 10;
			break;
		case "CenterBottom":
			order = 15;
			break;
		case "Bottom":
			order = 20;
			break;
		default:
			break;
		}

		foreach (var image in rend) {
			image.sortingOrder = order;
			baseColor.sortingOrder = order - 1;
		}
	}

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
                LilyEffectAnimator.Play("None");
                break;
		    case LilyState.Green:
			    currentState = LilyState.Green;
                LilyEffectAnimator.Play("LilyOpen3");
				baseColor.color = new Color32(43,254,97,255);		
                break;
		    case LilyState.Purple:
			    currentState = LilyState.Purple;
				LilyEffectAnimator.Play("LilyOpen2");
				baseColor.color = new Color32(237,22,254,255);
                break;
		    case LilyState.Yellow:
			    currentState = LilyState.Yellow;
				LilyEffectAnimator.Play("LilyOpen1");
				baseColor.color = new Color32(254,213,39,255);
                break;
			case LilyState.Complete:
				currentState = LilyState.Complete;
                LilyEffectAnimator.Play("LilyComplete");
				baseColor.color = Color.white;
                break;
		    default:
			    break;
		}
	}
}
