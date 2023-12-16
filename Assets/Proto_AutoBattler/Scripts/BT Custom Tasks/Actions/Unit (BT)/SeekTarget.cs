using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Move towards a preselected target. Returns success once the target is in a specified range")]
	public class 
		SeekTarget : ActionTask
	{
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> target;
		
		private IAstarAI ai;
		
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
		{
			ai = agent.GetComponent<IAstarAI>();
			if (ai == null)
				return "The agent can't find the IAstarAI component";
			return null;
		}
		private int testIndex = 0; 
		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		
		
		protected override void OnExecute()
		{
			ai.isStopped = false;

			// if (target.value == null)
			// 	EndAction(false);
			// ai.destination = target.value.GetPosition();
			// if (ai.reachedDestination)
			// 	EndAction(true);
			// EndAction(false);
		}

		//Called once per frame while the action is active.
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

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}