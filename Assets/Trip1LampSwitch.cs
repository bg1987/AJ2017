using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trip1LampSwitch : MonoBehaviour {
    public GameObject[] objects;
    public int localVariableId;

    public int currentIndex;
	// Use this for initialization
	void Start () {
        currentIndex = AC.LocalVariables.GetIntegerValue(localVariableId);
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
