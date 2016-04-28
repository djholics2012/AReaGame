///<summary>
/// 상태 클래스의 부모 클래스(추상 클래스)
/// </summary>
/// 
public abstract class State<T>
{
    public abstract void Enter(T owner);
    public virtual void Update(T owner) { }
    public virtual void FixedUpdate(T owner) { }
    public abstract void Exit(T owner);
}