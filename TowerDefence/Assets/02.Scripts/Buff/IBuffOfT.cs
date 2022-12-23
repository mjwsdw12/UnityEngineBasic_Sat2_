public interface IBuff<T>
{
    void OnActive(T target);
    void OnDuration(T target);
    void OnDeactive(T target);
}
