using UnityEngine;

public interface Observable
{
    void Subscirbe(Observer o);
    void Unsubscirbe(Observer o);
    void Notify();
}
