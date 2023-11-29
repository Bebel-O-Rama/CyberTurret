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
    [Name("Iterate Hit Queue")]
    [Category("CustomBT")]
    [Description(
        "Iterate through the HitInstances queue of a unit and process each hit. Keeps iterating until the queue is empty or the unit is dead. Return Success if the hit killed the unit, otherwise return failure.")]
    [ParadoxNotion.Design.Icon("List")]
    public class IterateHitQueue : BTDecorator
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<Queue<HitInstance>> queue;

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<HitInstance> hitInstance;

        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            if (decoratedConnection == null)
                return Status.Resting;

            while (queue.value.Any())
            {
                hitInstance.value = queue.value.Dequeue();
                status = decoratedConnection.Execute(agent, blackboard);

                if (status == Status.Success)
                    return Status.Success;
                
                return Status.Running;
            }

            return Status.Failure;
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR
        protected override void OnNodeGUI()
        {
            GUILayout.Label("For Each HitInstance in " + queue);
            if (Application.isPlaying)
            {
                GUILayout.Label("PROCESSING HITS");
                GUILayout.Label("There are " + queue.value.Count + " other hit(s) to process");
            }
        }
#endif
    }
}