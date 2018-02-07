using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI;
using UnityEditor.WebGL;
using UnityEditor.Graphs;
using UnityEditor;
using System;

public class NodeTemp : Node
{
    public string form;
    public override void AddedToGraph()
    {
        base.AddedToGraph();
    }
    public override void AddSlot(Slot s, int index)
    {
        base.AddSlot(s, index);
    }
    public override void NodeUI(GraphGUI host)
    {
        base.NodeUI(host);
        GUI.Box(position, "");
        EditorGUI.LabelField(position, "哈哈");
    }
    public override void BeginDrag()
    {
        base.BeginDrag();
        Selection.activeObject = this;
        Debug.Log(this);
    }
    public override void InputEdgeChanged(Edge e)
    {
        base.InputEdgeChanged(e);
    }
}
public class GUITemp : GraphGUI
{
    protected new const float kGraphPaddingMultiplier = 2;
    public void SetZoom(float value)
    {
        CeilValueToGrid(value);
        //kGraphPaddingMultiplier = value;
        m_GraphClientArea = new Rect();
    }
    protected override void AddNode(Node node)
    {
        base.AddNode(node);
    }
}

public class GraphWindow : EditorWindow
{
    Graph graph;
    GraphGUI graphGUI;
    Node node;
    private float zoom = 1;
    private Vector2 scrollPos;
    [MenuItem("Test/Graph")]
    static void Main()
    {
        GetWindow<GraphWindow>();
    }
    private void OnEnable()
    {
        graph = CreateInstance<Graph>();
        node = CreateInstance<NodeTemp>();
        node.name = "test";
        node.position = new Rect(10, 10, 100, 100);
        graph.AddNode(node);
        graphGUI = GraphGUI.CreateInstance<GUITemp>();
        graphGUI.graph = graph;
        GUIScaleUtility.Init();
    }
    private void OnGUI()
    {
        //if (graphGUI != null)
        //{
        //    graphGUI.BeginToolbarGUI(new Rect(0, 0, position.width, EditorGUIUtility.singleLineHeight));

        //    zoom = GUILayout.HorizontalSlider(zoom, 0, 10);
        //    graphGUI.drawSelectionRectCallback += CallBack;
        //    Debug.Log(graphGUI.zoomLevel);
        //    graphGUI.EndToolbarGUI();
        //    graphGUI.BeginGraphGUI(this, new Rect(0, EditorGUIUtility.singleLineHeight, position.width, position.height));
        //    graphGUI.NodeGUI(node);
        //    graphGUI.EndGraphGUI();
        //}
        DrawScrollView();
    }
    float maxCanvasSize = 2.5f;
    void DrawScrollView()
    {
        zoom = GUILayout.HorizontalSlider(zoom, 0.5f, 2);
        var width = position.width;
        Debug.Log(width);
        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPos))
        {
            scrollPos = scrollScope.scrollPosition;
            var canvasRect = new Rect(-scrollPos.x, -scrollPos.y, position.width * maxCanvasSize, position.height * maxCanvasSize);
            var viewRect = new Rect(0, 0, (scrollPos.x + width - 20) / zoom, 120);
            //GUIUtility.ScaleAroundPivot(Vector2.one * zoom, Vector2.zero);
            GUI.BeginClip(viewRect);
            using (var clipScore = new GUI.ClipScope(viewRect))
            {
                var vect = GUIScaleUtility.BeginScale(ref canvasRect, canvasRect.size * 0.5f, zoom, true, false);

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        var rect = new Rect(i * 100, j * 100, 100, 100);
                        GUI.Button(rect, i + ":" + j);
                    }
                }
                GUIScaleUtility.EndScale();
            }
            GUI.EndClip();
            GUI.matrix = Matrix4x4.identity;
            GUILayoutUtility.GetRect(600/zoom,600 / zoom);
        }
        var rect0 = GUILayoutUtility.GetRect(0, 100);
        EditorGUI.LabelField(rect0, "Label");
    }
    private void CallBack(Rect selectionRect)
    {
        Debug.Log(selectionRect);
    }
}
