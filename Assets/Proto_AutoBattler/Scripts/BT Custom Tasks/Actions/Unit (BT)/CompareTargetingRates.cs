using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Compare the targeting rate between the best target and the one currently tested. If the target tested has a higher rate, update the best target and return Success. Otherwise, return Failure")]
	public class CompareTargetingRates : ActionTask {
		
		[BlackboardOnly]
		public BBParameter<UnitInstance> bestTarget;
        
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<float> bestTargetRate;

		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> targetToTest;        
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<float> targetToTestRate;

		//Called whenever the condition gets enabled.
		protected override void OnExecute() {
			if (targetToTestRate.value > bestTargetRate.value)
			{
				bestTarget.value = targetToTest.value;
				bestTargetRate.value = targetToTestRate.value;
				EndAction(true);
			}

			EndAction(false);
		}
	}
}