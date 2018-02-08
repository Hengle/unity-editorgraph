using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI;
using UnityEditor.Graphs;
using UnityEditor;
using System;
using UnityEngine.Experimental.UIElements;

namespace UnityEditorGraph
{

    public class UnityEditorGraphWindow : EditorWindow
    {
        private ZoomManipulator m_ZoomManipulator;
        private UnityEditorGrapGUI graphGUI;
        private float zoom = 1;
        private Rect graphRect;

        [MenuItem("Window/UnityEditorGraph")]
        static void Main()
        {
            GetWindow<UnityEditorGraphWindow>();
        }
       
        private void OnEnable()
        {
            this.SetupGUI();
        }

        private void OnGUI()
        {
            if(graphGUI == null) InitGraph(CreateInstance<Graph>());
            DrawHeader();
            DrawScrollView();
        }

        [OnOpenAsset]
        private static bool OnOpenGraph(int arg0,int arg1)
        {
            var obj = EditorUtility.InstanceIDToObject(arg0);
            if(obj is Graph)
            {
                var window =  GetWindow<UnityEditorGraphWindow>();
                window.InitGraph(obj as Graph);
                return true;
            }
            return false;
        }

        public void SetupGUI()
        {
            VisualElement rootVisualContainer = new VisualElement(); 
            this.m_ZoomManipulator = new ZoomManipulator(rootVisualContainer, this);
            VisualElementExtensions.AddManipulator(rootVisualContainer, m_ZoomManipulator);


            rootVisualContainer.RegisterCallback<WheelEvent>((x) => { Debug.Log(x); rootVisualContainer.HandleEvent(x); });
        }

        private void DefultNode()
        {
            //graphGUI.AddTools();
            var node = CreateInstance<NodeTemp>();
            node.position = new Rect(200, 0, 100, 100);
            //node.AddedToGraph();
            var solt = node.AddInputSlot("in");
            graphGUI.graph.AddNode(node);

            //graphGUI.AddTools();
            node = CreateInstance<NodeTemp>();
            node.position = new Rect(0, 0, 100, 100);
            //node.AddedToGraph();
            solt = node.AddInputSlot("in");
            graphGUI.graph.AddNode(node);
        }

        private void InitGraph(Graph graph)
        {
            graphGUI = GraphGUI.CreateInstance<UnityEditorGrapGUI>();
            graphGUI.graph = graph;
            graphGUI.drawSelectionRectCallback += CallBack;
            DefultNode();
        }

        #region GUI
        private void DrawScrollView()
        {
            graphRect = new Rect(0, EditorGUIUtility.singleLineHeight, position.width, position.height - 2 * EditorGUIUtility.singleLineHeight);

            graphGUI.BeginGraphGUI(this, graphRect);
            graphGUI.OnGraphGUI();
            graphGUI.EndGraphGUI();
        }
        private void DrawHeader()
        {
            graphGUI.BeginToolbarGUI(new Rect(0, 0, position.width, EditorGUIUtility.singleLineHeight));
            graphGUI.OnToolbarGUI();
            graphGUI.EndToolbarGUI();
        }
        #endregion
        private void CallBack(Rect selectionRect)
        {
            //Debug.Log(selectionRect);
        }

        internal void OnGraphScroll()
        {
            Debug.Log("OnGraphScroll");
        }
    }
}