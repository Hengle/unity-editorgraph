using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Graphs;
using UnityEditor;
using System.Linq;

namespace UnityEditorGraph
{
    public class UnityEditorGrapGUI : GraphGUI
    {
        public override void OnEnable()
        {
            base.OnEnable();
        }
        public override void AddTools()
        {
            base.AddTools();
            m_Tools.Add(new NodeTool("HH", "B", () => { return null; }));
        }
        protected override void AddNode(Node node)
        {
            base.AddNode(node);
        }
        public override IEdgeGUI edgeGUI
        {
            get
            {
                return base.edgeGUI;
            }
        }
        protected override void CopyNodesToPasteboard()
        {
            base.CopyNodesToPasteboard();
        }
        public override void DoBackgroundClickAction()
        {
            base.DoBackgroundClickAction();
            Debug.Log("DoBackgroundClickAction");

        }
        protected override void DuplicateNodesThroughPasteboard()
        {
            base.DuplicateNodesThroughPasteboard();
        }
        protected override void OnScroll()
        {
            base.OnScroll();
            //Debug.Log("OnScroll");
        }
        public override void NodeGUI(Node n)
        {
            base.NodeGUI(n);
            Debug.Log("write group:" + n);
        }
        protected override Vector2 GetCenterPosition()
        {
            Debug.Log("GetCenterPosition");
            return base.GetCenterPosition();
        }
        public override void OnNodeLibraryGUI(EditorWindow host, Rect position)
        {
            base.OnNodeLibraryGUI(host, position);
            Debug.Log("OnNodeLibraryGUI");
        }
        public override void ClearSelection()
        {
            base.ClearSelection();
        }
        public override void OnGraphGUI()
        {
            base.OnGraphGUI();
        }

        public override void OnToolbarGUI()
        {
            base.OnToolbarGUI();
        }
        protected override void PasteNodesFromPasteboard()
        {
            base.PasteNodesFromPasteboard();
        }
        protected override void PasteNodesPasteboardData(Graph dummyGraph)
        {
            base.PasteNodesPasteboardData(dummyGraph);
        }
        public override void SyncGraphToUnitySelection(bool force = false)
        {
            base.SyncGraphToUnitySelection(force);
        }
        protected override void UpdateUnitySelection()
        {
            //Debug.Log("UpdateUnitySelection");
            base.UpdateUnitySelection();
        }
    }
}