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
        
        // For now, I'll hard set them. We could show them once it becomes useful. Don't forget to add .value to the variables!
        //
        // public BBParameter<bool> isLookingForAllies = false;
        // public BBParameter<bool> canTargetItself = false;
        public bool isLookingForAllies = false;
        public bool canTargetItself = false;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> currentTarget;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> targetToTest;
        
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<float> bestTargetRate;

        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            if (decoratedConnection == null)
                return Status.Resting;
            UnitType type = thisUnit.value.GetUnitType();
                
            List<UnitInstance> spawnedTargets = UnitTestingManager.Instance.GetUnitTargetsFromType(type, isLookingForAllies);
            if (!canTargetItself)
                spawnedTargets.Remove(thisUnit.value);
            
            if (currentTarget.value != null)
            {
                Debug.LogWarning("A unit is trying to find a new unit to target when it's already targeting " +
                                 currentTarget.value.name +
                                 ". The previous target has been removed to find a new one.");
                currentTarget.value = null;
            }
            bestTargetRate.value = 0;

            foreach (var target in spawnedTargets)
            {
                targetToTest.value = target;
                status = decoratedConnection.Execute(agent, blackboard);
                
                return Status.Running;
            }

            if (currentTarget.value != null)
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