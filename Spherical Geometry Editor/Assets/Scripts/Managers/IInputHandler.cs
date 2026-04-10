using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface IInputHandler
    {
        public event Action OnLeftMouseButtonDown;
        public event Action OnLeftMouseButtonUp;
        public event Action OnLeftMouseButtonHold;
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

