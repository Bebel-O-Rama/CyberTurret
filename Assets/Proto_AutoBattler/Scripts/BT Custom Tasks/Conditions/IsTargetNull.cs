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