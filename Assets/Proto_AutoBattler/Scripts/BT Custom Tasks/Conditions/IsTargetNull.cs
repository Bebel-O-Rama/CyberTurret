using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;


namespace CustomBT.Conditions {

	[Category("_CustomBT/Conditions")]
	[Description("Checks if the target is null (dead). If yes, interrupt and return Failure. If cleanPathIfNull is set to true, it'll also fetch the astar path and clean it.")]
	public class IsTargetNull : ConditionTask {

		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> target;
		
		public bool cleanPathIfNull;
		
		private IAstarAI ai;
		
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			if (cleanPathIfNull)
			{
				ai = agent.GetComponent<IAstarAI>();
				if (ai == null)
					return "The parameter cleanPathINull is set to true, but the agent can't find the IAstarAI component";
				return null;
			}
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck()
		{
			if (target != null)
				return false;
			if (cleanPathIfNull)
				ai.SetPath(null);
			return true;
		}
	}
}