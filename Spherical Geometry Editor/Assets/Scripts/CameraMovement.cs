using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  this.transform.Rotate(0, 0, 1);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    this.transform.Rotate(0.5f, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    this.transform.Rotate(-0.5f, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    this.transform.Rotate(0, 0.5f, 0);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    this.transform.Rotate(0, -0.5f, 0);
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    this.transform.Rotate(0, 0, 0.5f);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    this.transform.Rotate(0, 0, -0.5f);
        //}
    }

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
