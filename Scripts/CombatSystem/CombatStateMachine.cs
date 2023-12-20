
public class CombatStateMachine
{
    public CombatState CurrentState;
    public void ChangeState(CombatState state)
    {
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
