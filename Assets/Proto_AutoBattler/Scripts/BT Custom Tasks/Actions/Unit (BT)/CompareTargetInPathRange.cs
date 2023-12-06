using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;


namespace CustomBT.Unit
{
    [Category("_CustomBT/Unit")]
    [Description(
        "Using a target iterator, update the currentTarget if the targetRate of the new one is better. Return Success if the target has been updated, otherwise return Failure.")]
    public class CompareTargetInPathRange : ActionTask
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> targetingRange;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<Vector3> position;
        
        [BlackboardOnly]
        public BBParameter<UnitInstance> currentTarget;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> targetToTest;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> bestTargetRate;

        protected override void OnExecute()
        {
            Path path = ABPath.Construct(position.value, targetToTest.value.GetPosition());
            AstarPath.StartPath(path);
            AstarPath.BlockUntilCalculated(path);
            float distance = path.GetTotalLength();

            // The idea is to get a growing rating to find a target. Maybe we could add multiple rating later on to get more targeting nuance...
            // For now, we do targetingRange - distance = newRate
            float newTargetRate = targetingRange.value - distance;
            
            if (newTargetRate > bestTargetRate.value)
            {
                currentTarget.value = targetToTest.value;
                bestTargetRate.value = newTargetRate;
                EndAction(true);
            }
            
            EndAction(false);
        }
    }
}