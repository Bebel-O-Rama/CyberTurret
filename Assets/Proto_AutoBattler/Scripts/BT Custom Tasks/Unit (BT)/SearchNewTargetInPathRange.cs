using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;


namespace CustomBT.Unit
{
    [Category("Unit")]
    [Description(
        "Look for the closest possible target using the range of the path")]
    public class SearchNewTargetInPathRange : ActionTask
    {
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
                Path path = ABPath.Construct(position.value, opponent.GetPosition());
                AstarPath.StartPath(path);
                AstarPath.BlockUntilCalculated(path);

                float distance = path.GetTotalLength();
                if (distance < newTargetDistance)
                {
                    newTarget = opponent;
                    newTargetDistance = distance;
                }
            }

            currentTarget.SetValue(newTarget);

            EndAction(currentTarget.value != null);
        }
    }
}