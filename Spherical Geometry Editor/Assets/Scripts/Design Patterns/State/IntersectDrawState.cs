using UnityEngine;

public class IntersectDrawState : DrawingState
{
    public delegate GameObject CreatePointDelegate(GameObject prefab);
    private ParametricCurve intersectable1;
    private ParametricCurve intersectable2;
    private GameObject prefab;

    public IntersectDrawState(DrawManager manager, GameObject prefab) : base(manager)
    {
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
                ICommand command = null;
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircle)
                {
                    command = new GreatCircleGreatCircleIntersectCommand(intersectable1 as GreatCircle, intersectable2 as GreatCircle, prefab);
                }
                if (intersectable1 is GreatCircleSegment && intersectable2 is GreatCircleSegment)
                {
                    command = new GreatCircleSegmentGreatCircleSegmentIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as GreatCircleSegment, prefab);
                }
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircleSegment)
                {
                    command = new GreatCircleGreatCircleSegmentIntersectCommand(intersectable2 as GreatCircleSegment, intersectable1 as GreatCircle, prefab);
                }
                if (intersectable2 is GreatCircle && intersectable1 is GreatCircleSegment)
                {
                    command = new GreatCircleGreatCircleSegmentIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as GreatCircle, prefab);
                }
                if (intersectable1 is GreatCircle && intersectable2 is SmallCircle)
                {
                    command = new GreatCircleSmallCircleIntersectCommand(intersectable1 as GreatCircle, intersectable2 as SmallCircle, prefab);
                }
                if (intersectable2 is GreatCircle && intersectable1 is SmallCircle)
                {
                    command = new GreatCircleSmallCircleIntersectCommand(intersectable2 as GreatCircle, intersectable1 as SmallCircle, prefab);
                }
                if (intersectable1 is SmallCircle && intersectable2 is GreatCircleSegment)
                {
                    command = new GreatCircleSegmentSmallCircleIntersectCommand(intersectable2 as GreatCircleSegment, intersectable1 as SmallCircle, prefab);
                }
                if (intersectable2 is SmallCircle && intersectable1 is GreatCircleSegment)
                {
                    command = new GreatCircleSegmentSmallCircleIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as SmallCircle, prefab);
                }
                if (intersectable1 is SmallCircle && intersectable2 is SmallCircle)
                {
                    command = new SmallCircleSmallCircleIntersectCommand(intersectable1 as SmallCircle, intersectable2 as SmallCircle, prefab);
                }
                if (command != null)
                {
                    manager.ExecuteCommand(command);
                    manager.SetState(manager.IdleState);
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
