using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IHighlighter
    {
    public void Highlight(IGeometryObject geometryObject);

    public void UnHighlight();

    public void HighlightEverythingState();

    public void HighlightControllPointsState();

    public void HighlightCurvesState();

    public void HighlightMoveAblePointsState();

    public void HighlightGreatCirclesState();
}

