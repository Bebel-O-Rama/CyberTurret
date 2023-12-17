using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Conditions {

	[Category("_CustomBT/Conditions")]
	[Description("Check if a target is in a circular range. If isBlockedByStructure is set to true, a target in range, but behind a structure element (layer) won't be set as in range.")]
	public class IsTargetInCircularRange : ConditionTask {
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Vector3> position;
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> currentTarget;
		
		[RequiredField]
		public BBParameter<float> targetingRange;
		
		public BBParameter<bool> isBlockedByStructure;

		public LayerMask mask;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			// mask = LayerMask.GetMask("Structure");
			return null;
		}
		
		protected override bool OnCheck() {
			float distance = Vector3.Distance(position.value, currentTarget.value.GetPosition());
			if (distance > targetingRange.value)
				return false;
			if (isBlockedByStructure.value)
			{
				RaycastHit hitInfo;
				Physics.Linecast(position.value, currentTarget.value.GetPosition(), out hitInfo);
				Debug.Log(hitInfo);
				if (Physics.Linecast(position.value, currentTarget.value.GetPosition(), out hitInfo, mask))
				{
					return false;
				}
				Debug.Log(hitInfo);
			}
			return true;
		}
	}
}