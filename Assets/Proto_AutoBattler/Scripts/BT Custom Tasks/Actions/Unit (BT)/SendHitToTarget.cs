using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Sends a HitInstance towards a target. Can also use a List<UnitInstance> if there are multiple targets. Returns Failure if both the target and targets are null, otherwise return Success.")]
	public class SendHitToTarget : ActionTask {
		//[ParadoxNotion.Design.Header("Needs to fill at least one target field")]
		
		[BlackboardOnly]
		public BBParameter<UnitInstance> target;
		

		//// Might now be necessary...
		// [BlackboardOnly]
		// public BBParameter<List<UnitInstance>> targets;

		[RequiredField]
		[BlackboardOnly]
		public BBParameter<HitInstance> hitInstance;

		protected override void OnExecute()
		{
			//// Testing without dealing with lists
			// if (target.value == null && targets.value == null)
			// 	EndAction(false);
			// if (target.value != null)
			// 	target.value.AddHitInstance(hitInstance.value);
			// if (targets.value != null)
			// {
			// 	foreach (var target in targets.value)
			// 	{
			// 		target.AddHitInstance(hitInstance.value);
			// 	}
			// }
			if (target.isNull)
				EndAction(false);
			else
			{
				target.value.AddHitInstance(hitInstance.value);
				EndAction(true);
			}
		}
	}
}