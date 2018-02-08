using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace UnityEditorGraph
{
    public class ZoomManipulator : MouseManipulator,IManipulator
    {
        private VisualElement m_GraphUI;

        private UnityEditorGraphWindow m_Tool;

        private bool m_Active;

        private Vector2 m_Start;

        private Vector2 m_Last;

        private Vector2 m_ZoomCenter;

        public float zoomStep
        {
            get;
            set;
        }

        public ZoomManipulator(VisualElement graphUI, UnityEditorGraphWindow tool)
        {
            this.m_GraphUI = graphUI;
            this.m_Tool = tool;
            this.zoomStep = 0.05f;
            base.activators.Add(new ManipulatorActivationFilter
            {
                button = (MouseButton)1,
                modifiers = EventModifiers.Alt
            });
        }

        protected override void RegisterCallbacksOnTarget()
        {
            Debug.Log("RegisterCallbacksOnTarget");
            base.target.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScroll), (Capture)1);
            base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), (Capture)1);
            base.target.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), (Capture)1);
            base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), (Capture)1);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            Debug.Log("UnregisterCallbacksFromTarget");
            base.target.UnregisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScroll), (Capture)1);
            base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), (Capture)1);
            base.target.UnregisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), (Capture)1);
            base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), (Capture)1);
        }

        private void OnScroll(WheelEvent e)
        {
            Vector2 zoomCenter = VisualElementExtensions.ChangeCoordinatesTo(base.target, this.m_GraphUI, e.localMousePosition);
            float zoomScale = 1f - e.delta.y * this.zoomStep;
            this.Zoom(zoomCenter, zoomScale);
            e.StopPropagation();
            Debug.Log(e);
        }

        protected void OnMouseDown(MouseDownEvent e)
        {
            if (base.CanStartManipulation(e))
            {
                this.m_Start = (this.m_Last = e.localMousePosition);
                this.m_ZoomCenter = VisualElementExtensions.ChangeCoordinatesTo(base.target, this.m_GraphUI, this.m_Start);
                this.m_Active = true;
                MouseCaptureController.TakeMouseCapture(base.target);
                e.StopPropagation();
            }
            Debug.Log(e);
        }

        protected void OnMouseMove(MouseMoveEvent e)
        {
            if (this.m_Active && MouseCaptureController.HasMouseCapture(base.target))
            {
                Vector2 vector = e.localMousePosition - this.m_Last;
                float zoomScale = 1f + (vector.x + vector.y) * this.zoomStep;
                this.Zoom(this.m_ZoomCenter, zoomScale);
                e.StopPropagation();
                this.m_Last = e.localMousePosition;
            }
        }

        protected void OnMouseUp(MouseUpEvent e)
        {
            if (this.m_Active)
            {
                if (base.CanStopManipulation(e))
                {
                    this.m_Active = false;
                    MouseCaptureController.ReleaseMouseCapture(base.target);
                    e.StopPropagation();
                }
            }
        }

        private void Zoom(Vector2 zoomCenter, float zoomScale)
        {
            Vector3 vector = this.m_GraphUI.transform.position;
            Vector3 vector2 = this.m_GraphUI.transform.scale;
            Vector2 min = this.m_GraphUI.layout.min;
            float x = zoomCenter.x + min.x;
            float y = zoomCenter.y + min.y;
            vector += Vector3.Scale(new Vector3(x, y, 0f), vector2);
            vector2 = Vector3.Scale(vector2, new Vector3(zoomScale, zoomScale, 1f));
            vector2.x = Mathf.Clamp(vector2.x, 0.1f, 3f);
            vector2.y = Mathf.Clamp(vector2.y, 0.1f, 3f);
            vector -= Vector3.Scale(new Vector3(x, y, 0f), vector2);
            this.m_GraphUI.transform.position = vector;
            this.m_GraphUI.transform.scale = vector2;
            this.m_Tool.OnGraphScroll();
        }
    }

}