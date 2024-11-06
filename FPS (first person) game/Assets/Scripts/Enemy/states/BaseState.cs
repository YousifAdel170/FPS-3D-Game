public abstract class BaseState
{
    // Instance of enemy class
    public Enemy enemy;
    
    // Instance of statemachine class
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}