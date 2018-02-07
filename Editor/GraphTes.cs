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
public class EditorGrapGUI : GraphGUI
{
    protected new const float kGraphPaddingMultiplier = 2;
   
    protected override void AddNode(Node node)
    {
        base.AddNode(node);
    }
}

public class GraphWindow : EditorWindow
{
    Graph graph;
    EditorGrapGUI graphGUI;
    Node node;
    private float zoom = 1;
    float zoomMax = 2f;

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
        graphGUI = GraphGUI.CreateInstance<EditorGrapGUI>();
        graphGUI.graph = graph;
        graphGUI.drawSelectionRectCallback += CallBack;
        GUIScaleUtility.Init();
    }
    private void OnGUI()
    {
        DrawHeader();
        DrawScrollView();
    }
    void DrawScrollView()
    {
        var windowRect = new Rect(0, 0, position.width - 15, position.height - 35);

        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPos))
        {
            scrollPos = scrollScope.scrollPosition;
            //分配滑动区尺寸
            var canvasRect = GUILayoutUtility.GetRect(windowRect.width * zoomMax / zoom, windowRect.height * zoomMax / zoom);
            //绘制canvas
            graphGUI.BeginGraphGUI(this, canvasRect);
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
        zoom = GUILayout.HorizontalSlider(zoom, 0.5f, 2);
        graphGUI.EndToolbarGUI();
    }
    private void DrawBackGround()
    {
    }
    private void DrawGraph()
    {
        if (graphGUI != null)
        {
            graphGUI.NodeGUI(node);
        }
    }
    private void CallBack(Rect selectionRect)
    {
        Debug.Log(selectionRect);
    }
}
