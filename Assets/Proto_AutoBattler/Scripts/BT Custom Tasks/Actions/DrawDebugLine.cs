using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.Events;


namespace CustomBT.Actions {

	[Category("_CustomBT/Actions")]
	[Description("Draw a line between two positions. Can also edit the color, thickness and duration. Always return Success.")]
	public class DrawDebugLine : ActionTask
	{
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Vector3> startLinePos;
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Vector3> endLinePos;

		public BBParameter<float> lineLifespan = 0.5f;

		[RequiredField]
		public BBParameter<float> aWidth = 1;
		public BBParameter<float> bWidth = 1;

		[RequiredField]
		public BBParameter<Color> aColor;
		public BBParameter<Color> bColor;
		
		private LineRenderer lr;
		
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
		{
			lr = (new GameObject("line")).AddComponent<LineRenderer>();
			lr.enabled = false;
			if (bColor.isNull)
				bColor.value = aColor.value;
			if (bWidth.isNull)
				bWidth.value = aWidth.value;
			if (lineLifespan.isNull || lineLifespan.value <= 0f)
				lineLifespan.value = 0.5f;
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute()
		{
			lr.SetPosition(0, startLinePos.value);
			lr.SetPosition(1, endLinePos.value);
			lr.startColor = aColor.value;
			lr.endColor = bColor.value;
			lr.startWidth = aWidth.value;
			lr.endWidth = bWidth.value;
			lr.enabled = true;

			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		void DeleteLineDraw()
		{
			
		}
	}
}