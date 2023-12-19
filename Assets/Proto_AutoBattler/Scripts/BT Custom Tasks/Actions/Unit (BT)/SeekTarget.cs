using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Move towards a target and returns Running, unless the target is dead. In that case, stop the AI from moving, clear the path and return Failure. The Task StopSeekingTarget must be called manually to stop the unit from moving towards the target.")]
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
			if (target.isNull)
			{
				ClearPath();
				EndAction(false);
			}
			else
			{
				ai.destination = target.value.GetPosition();
			}
		}

		private void ClearPath(bool stopMoving = true)
		{
			if (stopMoving)
				ai.isStopped = true;
			ai.SetPath(null);
			ai.destination = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}
	}
}