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
    public class CheckWinScenario : ActionCheck
    {
        public string complt = "11233545";

        public int constantID;
        public int parameterID;

        /**
		 * The default Constructor.
		 */
        public CheckWinScenario() : base()
        {
            category = ActionCategory.Custom;
            title = "Check Win Scenario";
        }

        /**
		 * <summary>Works out which of the two outputs should be run when this Action is complete.</summary>
		 * <returns>If True, then resultActionTrue will be used - otherwise resultActionFalse will be used</returns>
		 */
        public override bool CheckCondition()
        {
            return CheckLastPanelInput.totalinput == complt;
        }
    }
}