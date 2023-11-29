using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Unit
{
    [Category("_CustomBT/Unit")]
    [Description(
        "Search for a new opposing target in a circular range. Can also disable targeting for units behind a structure collider")]
    public class SearchNewTargetInCircularRange : ActionTask
    {
        public BBParameter<bool> isCheckingCollision;
        public BBParameter<float> targetingRange;
        public BBParameter<Vector3> position;
        public BBParameter<UnitType> unitType;
        public BBParameter<UnitInstance> currentTarget;
        
        protected override void OnExecute()
        {
            List<UnitInstance> spawnedOpponent = UnitTestingManager.Instance.GetOpposingUnits(unitType.value);
            UnitInstance newTarget = null;
            float newTargetDistance = targetingRange.value;

            foreach (var opponent in spawnedOpponent)
            {
                float distance = Vector3.Distance(position.value, opponent.GetPosition());
                if (distance < newTargetDistance)
                {
                    // TODO Add collision detection! 
                    if (isCheckingCollision.value)
                    {
                        Debug.Log("Missing collision check for the action SearchNewTargetInRange");
                    }
                    newTarget = opponent;
                    newTargetDistance = distance;
                }
            }

            currentTarget.SetValue(newTarget);

            EndAction(currentTarget.value != null);
        }
    }
}