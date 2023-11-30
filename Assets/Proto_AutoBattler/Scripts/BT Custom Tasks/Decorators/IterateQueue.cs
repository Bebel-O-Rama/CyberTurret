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
    [Name("Iterate Queue")]
    [Category("CustomBT")]
    [Description(
        "Iterate through a queue while deleting each elements as it goes through. Keeps iterating until the Termination Policy is met or until the whole queue is iterated, in which case the last iteration child status is returned. If the queue starts empty, returns Failure.")]
    [ParadoxNotion.Design.Icon("List")]
    public class IterateQueue<T> : BTDecorator
    {
        [RequiredField]
        [BlackboardOnly]
        public BBParameter<Queue<T>> targetQueue;

        [RequiredField]
        [BlackboardOnly]
        public BBParameter<T> queueElement;

        public TerminationConditions terminationCondition = TerminationConditions.None;
        
        public enum TerminationConditions
        {
            FirstSuccess,
            FirstFailure,
            None
        }
        
        private Queue<T> queue => targetQueue != null ? targetQueue.value : null;
        
        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            if (decoratedConnection == null)
                return Status.Resting;

            if ( queue == null || queue.Count == 0 ) {
                return Status.Failure;
            }
            
            while (queue.Any())
            {
                queueElement.value = queue.Dequeue();
                status = decoratedConnection.Execute(agent, blackboard);

                if ( status == Status.Success && terminationCondition == TerminationConditions.FirstSuccess ) {
                    return Status.Success;
                }

                if ( status == Status.Failure && terminationCondition == TerminationConditions.FirstFailure ) {
                    return Status.Failure;
                }

                return queue.Any() ? Status.Running : status;
            }

            return Status.Failure;
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR
        protected override void OnNodeGUI()
        {
            GUILayout.Label("For Each " + queueElement + " in " + targetQueue);
            if ( terminationCondition != TerminationConditions.None ) {
                GUILayout.Label("Break on " + terminationCondition.ToString());
            }
            if (Application.isPlaying)
            {
                GUILayout.Label("There are " + (queue != null && queue.Any() ? (queue.Count - 1 ).ToString() : "?") + " element(s) to execute");
            }
        }
#endif
    }
}