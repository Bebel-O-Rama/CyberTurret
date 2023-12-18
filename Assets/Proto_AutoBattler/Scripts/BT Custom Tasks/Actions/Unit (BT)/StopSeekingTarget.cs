using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Manually stops the unit from moving towards a target using the pathfinding package. Clears the path and destination. Always return Success.")]
	public class StopSeekingTarget : ActionTask {

		private IAstarAI ai;

		protected override string OnInit()
		{
			ai = agent.GetComponent<IAstarAI>();
			if (ai == null)
				return "The agent can't find the IAstarAI component";
			return null;
		}

		protected override void OnExecute()
		{
			ai.isStopped = true;
			ai.SetPath(null);
			ai.destination = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
			EndAction(true);
		}
	}
}