using System;

namespace EntityControlInterface
{
	public class DefaultStateMachineImpl : IStateMachine<DefaultEntityStates>
	{
		public override void UpdateEntity(EntityBase<DefaultEntityStates> entity)
		{
			if (entity == null) throw new NullReferenceException();

			var rand = new System.Random();
			if (rand.Next(2) == 1)
			{
				entity.TransitionTo(DefaultEntityStates.Walking);
			}
			else
			{
				entity.TransitionTo(DefaultEntityStates.Idle);
			}
		}
	}
}  // namespace EntityControlInterface