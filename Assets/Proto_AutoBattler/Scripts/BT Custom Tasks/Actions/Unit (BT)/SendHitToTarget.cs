using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace CustomBT.Unit {

	[Category("_CustomBT/Unit")]
	[Description("Sends a HitInstance towards a target.")]
	public class SendHitToTarget : ActionTask {

		public BBParameter<UnitInstance> currentTarget;
		public BBParameter<HitInstance> hitInstance;
		
		protected override void OnExecute()
		{
			currentTarget.value.AddHitInstance(hitInstance.value);
			EndAction(true);
		}
	}
}