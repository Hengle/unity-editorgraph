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

namespace UnityEditorGraph
{
    public class UnityEditorGraphWindow : EditorWindow
    {
        private UnityEditorGrapGUI graphGUI;
        private float zoom = 1;
        private float zoomMax = 2f;

        private Vector2 scrollPos;
        [MenuItem("Window/UnityEditorGraph")]
        static void Main()
        {
            GetWindow<UnityEditorGraphWindow>();
        }
       
        private void OnEnable()
        {
            GUIScaleUtility.Init();
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
            graphGUI.AddTools();
            DefultNode();
        }

        #region GUI
        private void DrawScrollView()
        {
            var windowRect = new Rect(0, 0, position.width - 15, position.height - 35);

            using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPos,true,true))
            {
                //绘制canvas
                graphGUI.BeginGraphGUI(this, new Rect(scrollScope.scrollPosition,new Vector2( windowRect.width * zoomMax, windowRect.height * zoomMax)));
                scrollPos = scrollScope.scrollPosition;
                //分配滑动区尺寸
                var canvasRect = GUILayoutUtility.GetRect(windowRect.width * zoomMax / zoom, windowRect.height * zoomMax / zoom);
              
                //开始进行缩放并去除以前的裁切
                var vect = GUIScaleUtility.BeginScale(ref windowRect, windowRect.size * 0.5f, zoom, false);

                var viewRect = new Rect(-scrollPos, 2 * vect + scrollPos);
                //进行新的视角裁切
                GUI.BeginClip(viewRect);
                //中间的信息
                DrawGraph();
                //结束裁切
                GUI.EndClip();
                //结束尺寸变化
                GUIScaleUtility.EndScale();
                graphGUI.EndGraphGUI();
            }
        }
        private void DrawHeader()
        {
            graphGUI.BeginToolbarGUI(new Rect(0, 0, position.width, EditorGUIUtility.singleLineHeight));
            //graphGUI.OnToolbarGUI();
            zoom = GUILayout.HorizontalSlider(zoom, 0.5f, 2);
            graphGUI.EndToolbarGUI();
        }
        private void DrawBackGround()
        {
        }
        private void DrawGraph()
        {
            graphGUI.OnGraphGUI();
        }
        #endregion
        private void CallBack(Rect selectionRect)
        {
            Debug.Log(selectionRect);
        }
    }
}