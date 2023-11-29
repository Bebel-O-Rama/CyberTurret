using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Unit {

	[Name("Process HitInstance")]
	[Category("_CustomBT/Unit")]
	[Description("Process a HitInstance. Returns Success if the hit kills the unit, otherwise return Failure.")]
	public class ProcessHitAction : ActionTask 
	{
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> thisUnit;
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<HitInstance> hit;
		
		protected override void OnExecute()
		{
			hit.value.ProcessHit(thisUnit.value);
			if (hit.value.IsUnitDead(thisUnit.value))
				EndAction(true);
			
			EndAction(false);
		}
		
	}
}