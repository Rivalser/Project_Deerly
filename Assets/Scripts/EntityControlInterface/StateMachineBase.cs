using System;

namespace EntityControlInterface
{
	public abstract class StateMachineBase<TStateEnumerations> where TStateEnumerations : Enum
	{
		public abstract void UpdateEntity(EntityBase<TStateEnumerations> entity);
	}
} // namespace EntityControlInterface