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
            m_Tools.Add(new NodeTool("Copy", "B", () => { return null; }));
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
            Selection.activeObject = this;
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
            n.title = "123456789";
            n.name  = "123456789";
            if (n.inputSlots.Count() < 5)
            {
                var slot = new Slot(SlotType.InputSlot);
                slot.title = "我是输入";
                n.AddSlot(slot);
            }
            if (n.outputSlots.Count() < 5)
            {
                var slot = new Slot(SlotType.OutputSlot);
                slot.title = "我是输出";
                n.AddSlot(slot);
            }
            EditorGUI.LabelField(n.position, "哈哈");
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
            m_ScrollPosition = Vector2.one;
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
            Debug.Log("UpdateUnitySelection");
            base.UpdateUnitySelection();
        }
    }
}