using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Graphs;

namespace UnityEditorGraph
{
    [CreateAssetMenu(menuName ="GraphObject")]
    public class GraphObject : Graph
    {
        public override void AddNode(Node node)
        {
            base.AddNode(node);
            Debug.Log("Add Node:" + node);
        }
        public override void AddNodes(params Node[] nodes)
        {
            base.AddNodes(nodes);
        }
        public override bool CanConnect(Slot fromSlot, Slot toSlot)
        {
            return base.CanConnect(fromSlot, toSlot);
        }
        public override void Clear(bool destroyNodes = false)
        {
            base.Clear(destroyNodes);
        }
        public override Edge Connect(Slot fromSlot, Slot toSlot)
        {
            return base.Connect(fromSlot, toSlot);
        }
        public override bool Connected(Slot fromSlot, Slot toSlot)
        {
            return base.Connected(fromSlot, toSlot);
        }
        public override void DestroyNode(Node node)
        {
            base.DestroyNode(node);
        }
        public override void Dirty()
        {
            base.Dirty();
        }
        public override void OnEnable()
        {
            base.OnEnable();
        }
        public override void RemoveEdge(Edge e)
        {
            base.RemoveEdge(e);
        }
        public override void RemoveNode(Node node, bool destroyNode = false)
        {
            base.RemoveNode(node, destroyNode);
        }
        public override void RemoveNodes(List<Node> nodesToRemove, bool destroyNodes = false)
        {
            base.RemoveNodes(nodesToRemove, destroyNodes);
        }
        public override void WakeUp(bool force)
        {
            base.WakeUp(force);
        }
        protected override void WakeUpNodes()
        {
            base.WakeUpNodes();
        }
    }
}