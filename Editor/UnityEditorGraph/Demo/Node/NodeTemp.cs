using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Graphs;
using UnityEditor;
using System;

public class NodeTemp : Node
{
    public GraphGUI m_graph;
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
        m_graph = host;
        base.NodeUI(host);
    }

    public override void BeginDrag()
    {
        base.BeginDrag();
        Debug.Log("BeginDrag" + this);
    }
    public override void InputEdgeChanged(Edge e)
    {
        base.InputEdgeChanged(e);
    }
    public override void ChangeSlotType(Slot s, Type toType)
    {
        base.ChangeSlotType(s, toType);
    }
    public override void EndDrag()
    {
        base.EndDrag();
        Debug.Log("BeginDrag:" + this.name);
    }
    public override void Dirty()
    {
        base.Dirty();
    }
    public override void RemoveSlot(Slot s)
    {
        base.RemoveSlot(s);
    }
    public override void RemovingFromGraph()
    {
        base.RemovingFromGraph();
    }
    public override void OnDrag()
    {
        base.OnDrag();
        Debug.Log("OnDrag" + this);
    }
    public override void RenameProperty(string oldName, string newName, Type newType)
    {
        base.RenameProperty(oldName, newName, newType);
    }
    public override void ResetGenericPropertyArgumentType()
    {
        base.ResetGenericPropertyArgumentType();
    }
    public override void SetGenericPropertyArgumentType(Type type)
    {
        base.SetGenericPropertyArgumentType(type);
    }
    public override string title
    {
        get
        {
            return base.title;
        }

        set
        {
            base.title = value;
        }
    }
    public override string windowTitle
    {
        get
        {
            return base.windowTitle;
        }
    }
}

