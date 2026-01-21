using UnityEngine;

public class IntersectDrawState : DrawingState
{
    private ParametricCurve intersectable1;
    private ParametricCurve intersectable2;
    private GameObject prefab;
    
    public void SetUp(DrawManager manager, GameObject prefab)
    {
        base.SetUp(manager);
        this.prefab = prefab;
    }

    public override void OnLeftMouseUp()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");

            if (hit.transform.gameObject.tag == "intersectable" && intersectable1 == null)
            {
                // manager.SelectControllPoint(hit.transform.gameObject);
                intersectable1 = hit.transform.gameObject.GetComponent<ParametricCurve>();
            }
            else if (hit.transform.gameObject.tag == "intersectable" && intersectable1 != null)
            {
                intersectable2 = hit.transform.gameObject.GetComponent<ParametricCurve>();
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircle)
                {
                    Vector3 dir = Vector3.Cross(intersectable1.NormalOfPlane, intersectable2.NormalOfPlane);
                    GameObject point1 = Instantiate(prefab);
                    GameObject point2 = Instantiate(prefab);
                    Place2PointsCommand command = new Place2PointsCommand(dir.normalized, point1.GetComponent<ControllPoint>(), -dir.normalized, point2.GetComponent<ControllPoint>());
                    manager.ExecuteCommand(command);
                    manager.SetState(manager.IdleState);
                }
                if (intersectable1 is GreatCircleSegment && intersectable2 is GreatCircleSegment)
                {

                }
                if ((intersectable1 is GreatCircle && intersectable2 is GreatCircleSegment) || (intersectable2 is GreatCircle && intersectable1 is GreatCircleSegment))
                {

                }
                if ((intersectable1 is GreatCircle && intersectable2 is SmallCircle) || (intersectable2 is GreatCircle && intersectable1 is SmallCircle))
                {

                }
                if ((intersectable1 is SmallCircle && intersectable2 is GreatCircleSegment) || (intersectable2 is SmallCircle && intersectable1 is GreatCircleSegment))
                {

                }
                if (intersectable1 is SmallCircle && intersectable2 is SmallCircle)
                {

                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        intersectable1 = null;
        intersectable2 = null;
    }
}
