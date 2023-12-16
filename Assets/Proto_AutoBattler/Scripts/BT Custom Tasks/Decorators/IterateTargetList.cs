using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomBT.Decorators
{
    [Name("Iterate Target List")]
    [Category("CustomBT")]
    [Description(
        "Iterate through the every target available. Keeps iterating until the list is empty. Return Success a target has been found, otherwise return Failure.")]
    [ParadoxNotion.Design.Icon("List")]
    public class IterateTargetList : BTDecorator
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> thisUnit;

        public bool isLookingForAllies = false;
        public bool canTargetItself = false;
        
        [BlackboardOnly]
        public BBParameter<UnitInstance> bestTarget;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> bestTargetRate;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> targetToTest;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> targetToTestRate;

        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            if (decoratedConnection == null)
                return Status.Resting;
            UnitType type = thisUnit.value.GetUnitType();
                
            List<UnitInstance> spawnedTargets = UnitTestingManager.Instance.GetUnitTargetsFromType(type, isLookingForAllies);
            if (!canTargetItself)
                spawnedTargets.Remove(thisUnit.value);

            bestTarget.value = null;
            bestTargetRate.value = 0;

            foreach (var target in spawnedTargets)
            {
                targetToTestRate.value = 0;
                targetToTest.value = target;
                status = decoratedConnection.Execute(agent, blackboard);
                decoratedConnection.Reset();
            }

            if (bestTarget.value != null)
                return Status.Success;
            return Status.Failure;
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR
        protected override void OnNodeGUI()
        {
            GUILayout.Label("For each potential targets");
            if (Application.isPlaying)
            {
                GUILayout.Label("PROCESSING OPPONENTS");
            }
        }
#endif
    }
}