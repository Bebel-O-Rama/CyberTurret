using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Get the position of the RVOController of a UnitInstance")]
	public class GetUnitRVOPosition : ActionTask {
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> currentTarget;
		
		[BlackboardOnly]
		public BBParameter<Vector3> rvoPosition;

		protected override void OnExecute()
		{
			rvoPosition.value = currentTarget.value.GetRVOPosition();
			if (rvoPosition.value == null)
				EndAction(false);
			EndAction(true);
		}
	}
}