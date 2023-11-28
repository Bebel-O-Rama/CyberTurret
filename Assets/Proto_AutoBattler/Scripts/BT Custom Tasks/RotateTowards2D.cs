using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Actions {

	[Category("_CustomBT/Actions")]
	[Description("Rotate the agent towards a target per frame. 2D version based on the action RotateTowards")]
	public class RotateTowards2D : ActionTask<Transform> {

		[RequiredField]
		public BBParameter<GameObject> target;
		public BBParameter<float> speed = 2;
		[SliderField(1, 180)]
		public BBParameter<float> angleDifference = 5;
		
		public BBParameter<Vector3> upVector = -Vector3.forward;
		
		public bool waitActionFinish;

		protected override void OnUpdate() {
			if ( Vector3.Angle(target.value.transform.position - agent.position, agent.up) <= angleDifference.value ) {
				EndAction(true);
				return;
			}
			
			// Got a bit lazy so I used the code here for the script's behaviour : https://forum.unity.com/threads/look-rotation-2d-equivalent.611044/#post-4092259
			var dir = target.value.transform.position - agent.position;
			Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: dir);
			agent.rotation = Quaternion.RotateTowards(agent.rotation, targetRotation, speed.value * Time.deltaTime);

			if ( !waitActionFinish ) {
				EndAction(false);
			}
		}
	}
}