using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadContainer : MonoBehaviour {

    public static LilyPadContainer instance;

    public LilypadsPetal startingLily;
    public LilypadsPetal currentlySelectedLily;
	public LilypadsPetal[] AllLilies;

    public void Awake()
    {
        instance = this;
        //currentlySelectedLily = startingLily;
    }

    private void OnEnable()
    {
        StaffHeadColor.OnColorChanged += UpdateLilyState;
    }

    private void UpdateLilyState()
    {
        if(currentlySelectedLily == null)
        {
            LilypadsPetal.UpdateLily(startingLily);
        }
        else
        {
            currentlySelectedLily.UpdateAffectedLilies();
        }
    }

	public void ResetMaze ()
	{
		AC.KickStarter.player.transform.position = (startingLily.transform.position + Vector3.right);
		//	Resume player movement
		//AC.KickStarter.playerInput;

		foreach (var l in AllLilies) {
			l.Reset();
		}

		currentlySelectedLily = null;
		UpdateLilyState();
	}

    private void OnDisable()
    {
        StaffHeadColor.OnColorChanged -= UpdateLilyState;
    }
}
