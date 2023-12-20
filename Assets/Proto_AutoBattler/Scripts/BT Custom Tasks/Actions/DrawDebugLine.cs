using System.Collections;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
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
		public BBParameter<Vector3> startPosition;
		
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Vector3> endPosition;

		public BBParameter<float> lineLifespan = 0.5f;

		public BBParameter<LineRenderedDebug.LineType> lineType;
		
		public BBParameter<bool> isFading = true;

		private GameObject lineDebugPrefab;
		
		protected override string OnInit()
		{
			lineDebugPrefab = Resources.Load<GameObject>("Prefabs/PF_LineRendererDebug");
			if (lineDebugPrefab == null)
				return "Can't load the lineDebugPrefab";
			return null;
		}

		protected override void OnExecute()
		{
			var spawnedLine = Object.Instantiate(lineDebugPrefab, agent.transform);
			spawnedLine.GetComponent<LineRenderedDebug>().StartLine(startPosition.value, endPosition.value, lineType.value, lineLifespan.value, isFading.value);

			EndAction(true);
		}
	}
}