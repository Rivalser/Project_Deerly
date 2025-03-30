using System;
using UnityEngine;

namespace EntityControlInterface
{
	public abstract class IStateMachine<TStateEnumerations> : MonoBehaviour where TStateEnumerations : Enum
	{
		public abstract void UpdateEntity(EntityBase<TStateEnumerations> entity);
	}
} // namespace EntityControlInterface