using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IInputHandler
    {
        public event Action<IGeometryObject, Vector3> OnLeftMouseButtonDown;
        public event Action<IGeometryObject, Vector3> OnLeftMouseButtonUp;
        public event Action<IGeometryObject, Vector3> OnLeftMouseButtonHold;
        public event Action OnWHoldDown;
        public event Action OnSHoldDown;
        public event Action OnDHoldDown;
        public event Action OnAHoldDown;
        public event Action OnEHoldDown;
        public event Action OnQHoldDown;
        public event Action<IGeometryObject> OnHover;
        public event Action OnNotHover;
        public event Action OnEscapeKeyDown;
    }

