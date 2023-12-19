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
    [Name("Iterate Through Available Units")]
    [Category("CustomBT")]
    [Description(
        "Iterate through the every unit available in the game. Keeps iterating until the list is empty or a TerminationCondition has been met.")]
    [ParadoxNotion.Design.Icon("List")]
    public class IterateAvailableUnits : BTDecorator
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<UnitInstance> thisUnit;

        public BBParameter<UnitInstance> iUnit;

        public BBParameter<bool> isLookingForAllies = false;
        public BBParameter<bool> canTargetItself = false;
        public TerminationConditions terminationCondition = TerminationConditions.None;

        public enum TerminationConditions
        {
            FirstSuccess,
            FirstFailure,
            None
        }

        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            if (decoratedConnection == null)
                return Status.Resting;
            UnitType type = thisUnit.value.GetUnitType();
                
            List<UnitInstance> spawnedTargets = UnitTestingManager.Instance.GetUnitTargetsFromType(type, isLookingForAllies.value);
            if (!canTargetItself.value)
                spawnedTargets.Remove(thisUnit.value);

            if (!spawnedTargets.Any())
                return Status.Failure;
            
            while (spawnedTargets.Any())
            {
                iUnit.value = spawnedTargets.First();
                spawnedTargets.RemoveAt(0);
                
                status = decoratedConnection.Execute(agent, blackboard);
                
                if (status == Status.Success && terminationCondition == TerminationConditions.FirstSuccess)
                    return Status.Success;

                if (status == Status.Failure && terminationCondition == TerminationConditions.FirstFailure)
                    return Status.Failure;

                decoratedConnection.Reset();
            }

            return status;
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR
        protected override void OnNodeGUI()
        {
            GUILayout.Label("For each available units");
            if (Application.isPlaying)
            {
                GUILayout.Label("PROCESSING UNITS");
            }
        }
#endif
    }
}