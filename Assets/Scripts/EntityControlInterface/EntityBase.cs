using System;
using UnityEngine;

namespace EntityControlInterface
{
	public abstract class EntityBase<TStateEnumerations> : MonoBehaviour where TStateEnumerations : Enum
	{
		public abstract void UpdateEntity ();
		public abstract void TransitionTo (TStateEnumerations newState);
		public abstract float GetScoreForState (TStateEnumerations potentialState);
		public abstract TStateEnumerations GetCurrentState ();

		internal abstract void ExecuteCurrentStateLogic ();
	}
} // namespace EntityControlInterface