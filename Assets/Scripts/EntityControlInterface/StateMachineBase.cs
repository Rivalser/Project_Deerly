using System;
using UnityEngine;

namespace EntityControlInterface
{
	public abstract class StateMachineBase<TStateEnumerations> : MonoBehaviour where TStateEnumerations : Enum
	{
		public abstract void UpdateEntity(EntityBase<TStateEnumerations> entity);
	}
} // namespace EntityControlInterface