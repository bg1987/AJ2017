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
    Yellow = 3
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

    LilyState currentState;

    internal void UpdateAffectedLilies()
    {
        foreach (var lily in AffectedLillies)
        {
            UpdateLily(lily);
        }
    }

    public static void UpdateLily(LilypadsPetal lily)
    {
        if (StaffHeadColor.CurrentColorIndex == (int)lily.lilyColor)
        {
            lily.ActivateLilyState(lily.lilyColor);
        }
        else if (lily.currentState != LilyState.Closed)
        {
            lily.ActivateLilyState(LilyState.Closed);
        }
    }

    public void OpenLily()
    {
        //	Move if they match
        if (StaffHeadColor.CurrentColorIndex == (int)currentState && currentState != LilyState.Closed)
        {
            KickStarter.player.transform.position = transform.position;
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
