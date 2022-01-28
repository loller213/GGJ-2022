using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;

		// Patrol AI
		public Transform[] targets;

		/// <summary>Time in seconds to wait at each target</summary>
		public float delay = 0;

		/// <summary>Current target index</summary>
		int index;

		float switchTime = float.PositiveInfinity;

		//Safe Area
		private BoxCollider2D[] safeArea;

		private bool notInSafeArea;

		public int safeAreaQty;

		void OnEnable () {
			ai = GetComponent<IAstarAI>();

            for (int i = 0; i < safeAreaQty; i++)
            {
				safeArea[i] = GameObject.FindWithTag("Safe Area").GetComponent<BoxCollider2D>();
			}

			//safeArea[] = GameObject.FindWithTag("Safe Area").GetComponent<BoxCollider2D>();

			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {

            for (int i = 0; i < safeAreaQty; i++)
            {
				notInSafeArea = !safeArea[].bounds.Contains(target.position);
			}

			//bool notInSafeArea = !safeArea[].bounds.Contains(target.position);


			if (target != null && ai != null && notInSafeArea) ai.destination = target.position;
			else
            {
                if (targets.Length == 0) return;

                bool search = false;

                // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
                // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
                if (ai.reachedEndOfPath && !ai.pathPending && float.IsPositiveInfinity(switchTime))
                {
                    switchTime = Time.time + delay;
                }

                if (Time.time >= switchTime)
                {
                    index = index + 1;
                    search = true;
                    switchTime = float.PositiveInfinity;
                }

                index = index % targets.Length;
				Debug.Log("index: " + index);
				
                ai.destination = targets[index].position;
				Debug.Log("ai dest: " + ai.destination);
                
				//if (search) ai.SearchPath();
            }
        }
	}
}
