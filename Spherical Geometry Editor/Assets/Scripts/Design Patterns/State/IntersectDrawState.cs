using UnityEngine;

public class IntersectDrawState : DrawingState
{
    public delegate GameObject CreatePointDelegate(GameObject prefab);
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
                Debug.Log("intersectable1: " + intersectable1.name + "intersectable2: " + intersectable2.name);
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircle)
                {
                    GreatCircleGreatCircleIntersectCommand command = new GreatCircleGreatCircleIntersectCommand(intersectable1 as GreatCircle, intersectable2 as GreatCircle, Instantiate, prefab);
                    manager.ExecuteCommand(command);
                    manager.SetState(manager.IdleState);
                }
                if (intersectable1 is GreatCircleSegment && intersectable2 is GreatCircleSegment)
                {
                    GreatCircleSegmentGreatCircleSegmentIntersectCommand command = new GreatCircleSegmentGreatCircleSegmentIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as GreatCircleSegment, Instantiate, prefab);
                    manager.ExecuteCommand(command);
                    manager.SetState(manager.IdleState);
                }
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircleSegment)
                {
                    GreatCircleGreatCircleSegmentIntersectCommand command = new GreatCircleGreatCircleSegmentIntersectCommand(intersectable2 as GreatCircleSegment, intersectable1 as GreatCircle, Instantiate, prefab);
                    manager.ExecuteCommand(command);
                    manager.SetState(manager.IdleState);
                }
                if (intersectable2 is GreatCircle && intersectable1 is GreatCircleSegment)
                {
                    GreatCircleGreatCircleSegmentIntersectCommand command = new GreatCircleGreatCircleSegmentIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as GreatCircle, Instantiate, prefab);
                    manager.ExecuteCommand(command);
                    manager.SetState(manager.IdleState);
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
