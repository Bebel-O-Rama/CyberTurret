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
        "Get the targeting rate based on its path distance. Returns Success, or Failure if the rate is <= 0 and failureIfRateZero is set to true.")]
    public class GetTargetRateInPathRange : ActionTask
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> targetingRange;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<Vector3> position;

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> targetToTest;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> targetToTestRate;
        
        public bool failureIfRateZero = true;

        protected override void OnExecute()
        {
            Path path = ABPath.Construct(position.value, targetToTest.value.GetPosition());
            AstarPath.StartPath(path);
            AstarPath.BlockUntilCalculated(path);
            float distance = path.GetTotalLength();

            // The idea is to get a growing rating to find a target. Maybe we could add multiple rating later on to get more targeting nuance...
            // For now, we do targetingRange - distance = newRate
            float newTargetRate = targetingRange.value - distance;
            
            if (newTargetRate <= 0 && failureIfRateZero)
                EndAction(false);
            targetToTestRate.value += newTargetRate;
            EndAction(true);
        }
    }
}