namespace OrderService.IntegrationTests.Builder;

public abstract class BaseBuilder<T> where T : class, new()
{
    protected T Model { get; }
    
    protected BaseBuilder()
    {
        Model = new T();
    }

    private Queue<Action<T>> _propertySettingActions = new();

    public BaseBuilder<T> With(Action<T> setPropertyAction)
    {
        _propertySettingActions.Enqueue(setPropertyAction);

        return this;
    }

    public virtual T Build()
    {
        foreach (var action in _propertySettingActions)
        {
            action(Model);
        }

        _propertySettingActions = new Queue<Action<T>>();
        
        return Model;
    }
}