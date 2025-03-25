using UnityEngine;
using UnityEngine.Serialization;

namespace EntityControlInterface
{
	public class DefaultEntityImpl : EntityBase<DefaultEntityStates>
	{
		public DefaultStateMachineImpl stateMachine;

		private DefaultEntityStates _state = DefaultEntityStates.Idle;

		public override void TransitionTo(DefaultEntityStates newState)
		{
			Debug.Log("Transitioning to " + newState);
			_state = newState;
		}

		public override void UpdateEntity()
		{
			stateMachine?.UpdateEntity(this);
		}


		public void Update()
		{
			UpdateEntity();
		}
	}
}  // namespace EntityControlInterface