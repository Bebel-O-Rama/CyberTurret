using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Update the currentTarget of this unit with a new target. Also tell the target to add this unit as a targeter. Return Failure is the new target is null, otherwise return Success.")]
	public class UpdateTarget : ActionTask {
		
		[BlackboardOnly]
		public BBParameter<UnitInstance> currentTarget;
		
		[BlackboardOnly]
		public BBParameter<UnitInstance> bestTarget;
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> thisUnit;

		protected override void OnExecute() {
			if (bestTarget.isNull)
				EndAction(false);
			else
			{
				bestTarget.value.AddUnitTargeter(thisUnit.value);
				currentTarget.value = bestTarget.value;
				EndAction(true);
			}
		}
	}
}