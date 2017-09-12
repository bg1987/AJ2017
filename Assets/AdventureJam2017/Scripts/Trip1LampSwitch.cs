using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trip1LampSwitch : MonoBehaviour {
    public GameObject[] objects;
    public int localVariableId;
    public bool isTarget; //to create a mirror image of the target
    public int currentIndex;
	// Use this for initialization
	void Start () {
        currentIndex = AC.LocalVariables.GetIntegerValue(localVariableId);

        if(isTarget)
        {
            currentIndex = (9 - currentIndex) % 5; //mirror the value.
        }

        SetObjectsToIndex();
	}

    void SetObjectsToIndex()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }

    public void Switch()
    {
        currentIndex = (currentIndex + 1) % objects.Length;

        AC.LocalVariables.SetIntegerValue(localVariableId, currentIndex);

        SetObjectsToIndex();
    }
	

}
