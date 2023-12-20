using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Actions {

	[Category("_CustomBT/Actions")]
	[Description("Wait for x seconds and returns Running until the cooldown is over and return Success. Also dispays a loading bar under the unit for better feedback (can be disabled).")]
	public class CooldownTimer : ActionTask {

		[RequiredField]
		[Min(0f)] public BBParameter<float> waitTime = 1f;
		
		[RequiredField]
		public BBParameter<string> waitName = "";
		
		public BBParameter<bool> showLoadingBar = true;

		private LoadingBarControler lbControler;
		private GameObject loadingBarPF;
		
		protected override string info {
			get { return string.Format("Wait {0} sec.", waitTime); }
		}

		protected override string OnInit() {
			if (showLoadingBar.value)
			{
				loadingBarPF = Resources.Load<GameObject>("Prefabs/PF_CooldownProgressBar");
				if (loadingBarPF == null)
					return "Can't load the CooldownProgressBar prefab";
			}
			return null;
		}

		protected override void OnExecute()
		{
			if (showLoadingBar.value)
			{
				var cooldownUI = Object.Instantiate(loadingBarPF, agent.transform);
				lbControler = cooldownUI.GetComponent<LoadingBarControler>();
				lbControler.StartLoadingBarUI(waitTime.value, waitName.value);
			}
		}
		
		protected override void OnUpdate() {
			if ( elapsedTime >= waitTime.value ) {
				EndAction(true);
			}
			else if (showLoadingBar.value)
			{
				lbControler.UpdateLoadingBarUI(elapsedTime);
			}
		}

		protected override void OnStop()
		{
			if (showLoadingBar.value)
			{
				Object.Destroy(lbControler.gameObject);
			}
		}
	}
}