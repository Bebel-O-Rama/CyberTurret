using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Conditions {

	[Category("_CustomBT/Conditions")]
	[Description("Check if the selected UnitInstance can be targeted")]
	public class IsUnitTargetable : ConditionTask {

		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> targetToTest;
		
		protected override bool OnCheck()
		{
			return targetToTest.value.IsUnitTargetable();
		}
	}
}