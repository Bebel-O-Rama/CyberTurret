using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomBT.Unit
{
    [Category("_CustomBT/Unit")]
    [Description(
        "Using a target iterator, update the currentTarget if the targetRate of the new one is better. Return Success if the target has been updated, otherwise return Failure.")]
    public class GetTargetRateInCircularRange : ActionTask
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

        public bool failureIfRateZero = true;

        protected override void OnExecute()
        {
            float distance = Vector3.Distance(position.value, targetToTest.value.GetPosition());
            // The idea is to get a growing rating to find a target. Maybe we could add multiple rating later on to get more targeting nuance...
            // For now, we do targetingRange - distance = newRate
            float newTargetRate = targetingRange.value - distance;

            if (newTargetRate <= 0 && failureIfRateZero)
                EndAction(false);
            EndAction(true);
        }
    }
}