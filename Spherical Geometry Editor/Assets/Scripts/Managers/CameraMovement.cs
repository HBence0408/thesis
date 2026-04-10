using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public void MoveUp()
    {
        this.transform.Rotate(0.5f, 0, 0);
    }

    public void MoveDown() 
    {
        this.transform.Rotate(-0.5f, 0, 0);
    }

    public void MoveRight()
    {
        this.transform.Rotate(0, -0.5f, 0);
    }

    public void MoveLeft()
    {
        this.transform.Rotate(0, 0.5f, 0);
    }

    public void TiltLeft()
    {
        this.transform.Rotate(0, 0, 0.5f);
    }

    public void TiltRight()
    {
        this.transform.Rotate(0, 0, -0.5f);
    }
}
