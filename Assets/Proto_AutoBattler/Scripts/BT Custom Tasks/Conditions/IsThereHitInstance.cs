using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Conditions {

	[Category("_CustomBT/Conditions")]
	[Description("Checks if the HitInstances queue contains a HitInstance to process")]
	public class IsThereHitInstance : ConditionTask {
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Queue<HitInstance>> targetList;

		protected override string info {
			get { return string.Format("{0} has at least 1 hit", targetList); }
		}

		protected override bool OnCheck() {
			return targetList.value.Count > 0;
		}
	}
}