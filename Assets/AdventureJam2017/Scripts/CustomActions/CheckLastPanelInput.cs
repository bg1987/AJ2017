/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2016
 *	
 *	"ActionCheck.cs"
 * 
 *	This is an intermediate class for "checking" Actions,
 *	that have TRUE and FALSE endings.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

    /**
	 * An Action subclass that allows for two different outcomes based on a boolean check.
	 */
    [System.Serializable]
    public class CheckLastPanelInput : ActionCheck
    {
        public string input = "";
        public string solution = "1123354521";

        public int constantID;
        public int parameterID;

        public static string totalinput = "";

        /**
		 * The default Constructor.
		 */
        public CheckLastPanelInput() : base()
        {
            category = ActionCategory.Custom;
            title = "Check Panel Click";
        }

        /**
		 * <summary>Works out which of the two outputs should be run when this Action is complete.</summary>
		 * <returns>If True, then resultActionTrue will be used - otherwise resultActionFalse will be used</returns>
		 */
        public override bool CheckCondition()
        {
            var refr = AdvGame.GetReferences();
            var manager = refr.variablesManager;
            var varbl = manager.GetVariable(1);
            string input = varbl.GetValue();

            input = AC.LocalVariables.GetStringValue(1);

            totalinput += input;

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(solution))
                return false;

            if (solution.ToString().StartsWith(totalinput))
                return true;

            totalinput = "";
            return false;
        }
    }
}