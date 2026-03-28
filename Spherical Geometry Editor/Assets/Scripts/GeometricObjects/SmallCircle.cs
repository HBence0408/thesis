using UnityEngine;

public class SmallCircle : ParametricCurve
{
    public override Vector3 GetClosestPoint(Vector3 pos)
    {

        float distance = normaleOfPlane.x * pos.x + normaleOfPlane.y * pos.y + normaleOfPlane.z * pos.z - (normaleOfPlane.x * center.x + normaleOfPlane.y * center.y + normaleOfPlane.z * center.z);
        distance = distance / normaleOfPlane.magnitude;
        Vector3 pointOnPlane = pos - distance * normaleOfPlane.normalized;

        ////newPos.Normalize();
        //newPos = newPos - center;
        ////newPos =  newPos.normalized * Vector3.Distance(center, PointsOnCurve[0]);
        //return center + Vector3.Distance(center, PointsOnCurve[0]) * ((newPos)/(newPos.magnitude));

        // 1. Kiszámoljuk a vektor irányát a középpontból a pont felé
      //  Vector3 v = pos - center;

        // 2. Kiszámoljuk a távolságot a síktól a normálvektor segítségével (Skaláris szorzat)
        // Feltételezzük, hogy a normaleOfPlane normalizált!
       // float distFromPlane = Vector3.Dot(v, normaleOfPlane);

        // 3. Vetítjük a pontot a kör síkjára
       // Vector3 pointOnPlane = pos - distFromPlane * normaleOfPlane;

        // 4. Meghatározzuk a kör sugarát (az első pont távolsága a középponttól)
        float radius = Vector3.Distance(center, PointsOnCurve[0]);

        // 5. A síkon lévő pont irányát vesszük a középponthoz képest
        Vector3 directionFromCenter = (pointOnPlane - center).normalized;

        // 6. Visszaadjuk a kör ívén lévő legközelebbi pontot
        return center + directionFromCenter * radius;
    }

    public override void OnChanged()
    {
        ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
        Notify();
    }
}
