using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoomSceneChanger : MonoBehaviour {

    [SerializeField]
    AC.ActionList ShowGreen;
    [SerializeField]
    GameObject GreenDoors;
    [SerializeField]
    AC.ActionList ShowPurple;
    [SerializeField]
    GameObject PurpleDoors;
    [SerializeField]
    AC.ActionList ShowYellow;
    [SerializeField]
    GameObject YellowDoors;
    [SerializeField]
    AC.ActionList ShowNone;
    // Use this for initialization
    void OnEnable () {
        StaffHeadColor.OnColorChanged += ColorChanged;
	}

    void OnDisable()
    {
        StaffHeadColor.OnColorChanged -= ColorChanged;
    }

    void ColorChanged()
    {
        switch (StaffHeadColor.CurrentColorIndex)
        {
            case 0:
                SwitchToNone();
                break;
            case 1:
                SwitchToGreen();
                break;
            case 2:
                SwitchToPurple();
                break;
            case 3:
                SwitchToYellow();
                break;
            default:
                break;
        }
    }

    void SwitchToGreen()
    {
        ShowGreen.Interact();
        GreenDoors.SetActive(true);
        PurpleDoors.SetActive(false);
        YellowDoors.SetActive(false);
    }

    void SwitchToYellow()
    {
        ShowYellow.Interact();
        GreenDoors.SetActive(false);
        PurpleDoors.SetActive(false);
        YellowDoors.SetActive(true);
    }

    void SwitchToPurple()
    {
        ShowPurple.Interact();
        GreenDoors.SetActive(false);
        PurpleDoors.SetActive(true);
        YellowDoors.SetActive(false);
    }

    void SwitchToNone()
    {
        ShowNone.Interact();
        GreenDoors.SetActive(false);
        PurpleDoors.SetActive(false);
        YellowDoors.SetActive(false);
    }
}
