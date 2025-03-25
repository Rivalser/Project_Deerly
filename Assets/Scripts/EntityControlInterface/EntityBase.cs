using System;
using UnityEngine;

namespace EntityControlInterface
{
	public abstract class EntityBase<TStateEnumerations> : MonoBehaviour where TStateEnumerations : Enum
	{
		public abstract void TransitionTo(TStateEnumerations newState);
		public abstract void UpdateEntity();
	}
}  // namespace EntityControlInterface