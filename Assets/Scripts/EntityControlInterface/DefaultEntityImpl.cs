using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

namespace EntityControlInterface
{
	public class DefaultEntityImpl : EntityBase<DefaultEntityStates>
	{
		[SerializeField] private DefaultStateMachineImpl<DefaultEntityStates> stateMachine;

		private DefaultEntityStates _state = DefaultEntityStates.Idle;

		// Mono Behaviour Methods
		private void Awake ()
		{
			if (stateMachine == null) stateMachine = new DefaultStateMachineImpl<DefaultEntityStates> ();
		}

		public void Update ()
		{
			ExecuteCurrentStateLogic ();

			UpdateEntity ();
		}
		// ... End Mono Behaviour Methods

		public override void TransitionTo (DefaultEntityStates newState)
		{
			Debug.Log ("Transitioning to " + newState);
			_state = newState;
		}

		public override float GetScoreForState (DefaultEntityStates potentialState)
		{
			var rand = new Random ();
			var value = rand.Next (100);

			switch (potentialState)
			{
				case DefaultEntityStates.Idle:
					return math.abs (value - 100);
				case DefaultEntityStates.Walking:
					return value;
				default:
					return 0;
			}
		}

		public override DefaultEntityStates GetCurrentState ()
		{
			return _state;
		}

		internal override void ExecuteCurrentStateLogic ()
		{
			switch (_state)
			{
				case DefaultEntityStates.Idle:
					Debug.Log ("Idle");
					break;
				case DefaultEntityStates.Walking:
					Debug.Log ("Walking");
					break;
			}
		}

		public override void UpdateEntity ()
		{
			stateMachine?.UpdateEntity (this);
		}
	}
} // namespace EntityControlInterface