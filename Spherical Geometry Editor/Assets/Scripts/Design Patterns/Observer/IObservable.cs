using UnityEngine;

public interface IObservable
{
    void Subscirbe(IObserver o);
    void Unsubscirbe(IObserver o);
    void Notify();
}
