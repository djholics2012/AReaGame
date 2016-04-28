using UnityEngine;
using System.Collections.Generic;

public class StateManager<T>
{
    private T _owner;
    private Dictionary<int, State<T>> _stateList = new Dictionary<int,State<T>>();
    private State<T> _curState = null;
    private CHARACTER_STATE _curStateID = CHARACTER_STATE.IDLE;

    public CHARACTER_STATE CurStateID { get { return _curStateID; } }

    public StateManager(T owner)
    {
        _owner = owner;
    }

    public void AddState(int stateID, State<T> state)
    {
        _stateList.Add(stateID, state);
    }

    public void ChangeState(int stateID)
    {
        if (!_stateList.ContainsKey(stateID))
        {
            Debug.LogError("State를 찾을 수 없습니다.");
            return;
        }

        State<T> newState = _stateList[stateID];

        if (newState == null)
            return;

        if (_curState == newState)
            return;

        if (_curState != null)
            _curState.Exit(_owner);

        newState.Enter(_owner);

        _curState = newState;
        _curStateID = (CHARACTER_STATE)stateID;
    }

    public void Update( )
    {
        if (_curState == null)
            return;

        _curState.Update(_owner);
    }

    public void FixedUpdate( )
    {
        if (_curState == null)
            return;

        _curState.FixedUpdate(_owner);
    }
}
