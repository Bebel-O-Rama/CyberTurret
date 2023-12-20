using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Actions {

	[Category("_CustomBT/Actions")]
	[Description("Draw a circle using a line renderer. Always return Success")]
	public class DrawDebugCircle : ActionTask {
		[RequiredField]
		[BlackboardOnly]
		public BBParameter<Vector3> centerPosition;
		
		[RequiredField]
		[BlackboardOnly]
		[Min(0f)] public BBParameter<float> radius;

		public BBParameter<float> lineLifespan = 0.5f;

		[RequiredField]
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
			spawnedLine.GetComponent<LineRenderedDebug>().StartCircle(centerPosition.value, radius.value, lineType.value, lineLifespan.value, isFading.value);

			EndAction(true);
		}
	}
}