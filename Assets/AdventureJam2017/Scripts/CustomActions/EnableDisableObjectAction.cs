using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

    [System.Serializable]
    public class EnableDisableCompon : AC.Action
    {

        // Declare variables here

        public int parameterID = -1;
        public int constantID = 0;
        public bool enableStateObj;

        public GameObject objectToAffect;
        public bool newEnableState;

        public EnableDisableCompon()
        {
            this.isDisplayed = true;
            category = ActionCategory.Custom;
            title = "Enable-Disable";
            description = "This is a blank Action template.";
        }

        override public void AssignValues(List<ActionParameter> parameters)
        {
            objectToAffect = AssignFile(parameters, parameterID, constantID, objectToAffect);
        }


        override public float Run()
        {
            
            if (objectToAffect)
            {
                objectToAffect.gameObject.SetActive(enableStateObj);
            }

            return 0f;

        }

#if UNITY_EDITOR

        override public void ShowGUI(List<ActionParameter> parameters)
        {
            parameterID = Action.ChooseParameterGUI("Object to affect:", parameters, parameterID, ParameterType.GameObject);
            if (parameterID >= 0)
            {
                constantID = 0;
                objectToAffect = null;
            }
            else
            {

                objectToAffect = (GameObject)EditorGUILayout.ObjectField
   ("Object to affect:", objectToAffect, typeof(GameObject), true);

                constantID = FieldToID(objectToAffect, constantID);
                objectToAffect = IDToField(objectToAffect, constantID, false);
            }

            enableStateObj = EditorGUILayout.Toggle("Enabled:", enableStateObj);

            AfterRunningOption();
        }
        override public string SetLabel()
        {
            string labelAdd = "";
            if (objectToAffect)
                labelAdd = " (" + objectToAffect.name + ")";
            return labelAdd;
        }
#endif
    }
}
