using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Conditions {

	[Category("_CustomBT/Conditions")]
	[Description("Check if a target is in a circular range. If canBeBlocked is set to true, a target behind a specific type of collider (hence the LayerMask) will never be set as in range. It is set by default to true")]
	public class IsTargetInCircularRange : ConditionTask {
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Vector3> position;
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<UnitInstance> currentTarget;
		
		[RequiredField]
		public BBParameter<float> targetingRange;
		
		public BBParameter<bool> canBeBlocked = true;

		public LayerMask mask;

		protected override bool OnCheck()
		{
			if (currentTarget.value == null)
				return false;

			float distance = Vector3.Distance(position.value, currentTarget.value.GetPosition());
			Debug.Log(distance);
			if (distance > targetingRange.value)
				return false;

			if (canBeBlocked.value &&
			    Physics2D.Linecast(position.value, currentTarget.value.GetPosition(), mask))
				return false;

			return true;
		}
	}
}