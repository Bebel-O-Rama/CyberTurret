using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Move towards a preselected target. Returns success once the target is in a specified range")]
	public class SeekTarget : ActionTask
	{
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> target;
		
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
			ai.isStopped = false;
		}
		protected override void OnUpdate()
		{
			if (target.value == null)
			{
				ai.isStopped = true;
				ai.SetPath(null);
				ai.destination = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
				EndAction(false);
			}
			else
			{
				ai.destination = target.value.GetPosition();
				if (ai.reachedDestination)
					EndAction(true);
			}
		}
	}
}