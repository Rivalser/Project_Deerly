using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace EntityControlInterface
{
    public class EnemyStateMachineImpl<TStateEnumerations> : StateMachineBase<TStateEnumerations>
        where TStateEnumerations : Enum
    {
        private readonly TStateEnumerations[] _stateValues;

        public EnemyStateMachineImpl ()
        {
            _stateValues = (TStateEnumerations[])Enum.GetValues (typeof (TStateEnumerations));

            if (_stateValues.Length == 0)
                Debug.LogError ($"Enum {typeof (TStateEnumerations).Name} has no values!");
        }

        public override void UpdateEntity (EntityBase<TStateEnumerations> entity)
        {
            if (entity == null) throw new ArgumentNullException (nameof (entity));

            if (_stateValues == null || _stateValues.Length == 0) return;

            var bestState = _stateValues
                .OrderByDescending (state => entity.GetScoreForState (state))
                .First ();

            if (!EqualityComparer<TStateEnumerations>.Default.Equals (bestState, entity.GetCurrentState ()))
                entity.TransitionTo (bestState);
        }
    }
}
