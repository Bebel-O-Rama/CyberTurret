using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Unit
{
    [Category("_CustomBT/Unit")]
    [Description("Checks if the current target is still targetable by the unit")]
    public class IsTargetInCircularRange : ActionTask
    {
        public BBParameter<bool> isCheckingCollision;
        public BBParameter<float> targetingRange;
        public BBParameter<Vector3> position;
        public BBParameter<UnitInstance> currentTarget;

        protected override void OnExecute()
        {
            float distance = Vector3.Distance(position.value, currentTarget.value.GetPosition());
            if (distance > targetingRange.value)
            {
                currentTarget.SetValue(null);
                EndAction(false);
            }
            if (isCheckingCollision.value)
            {
                Debug.Log("Missing collision check for the action IsTargetInRange");
            }

            EndAction(true);
        }
    }
}