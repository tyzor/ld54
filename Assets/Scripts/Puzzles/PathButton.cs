using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathButton : MonoBehaviour
{
    public enum PathNodeType {
        Node,
        Start,
        End
    }

    private List<bool> _edges;

    public int GetEdges() {
        int result = 0;
        for(int i=0;i<_edges.Count;i++)
        {
            if(_edges[i])
                result += (1 << i);
        }
        return result;
    }
    public bool GetEdge(int i) => _edges[i];

    public PathNodeType nodeType;

    [SerializeField]
    private TextMeshProUGUI textLabel;
    
    public void SetType(PathNodeType type)
    {
        this.nodeType = type;
        if(nodeType != PathNodeType.Node)
        {
            textLabel.SetText("O");
        }
    }

    public void SetEdges(int edgeBits)
    {
        if(_edges == null)
        {
            _edges = new List<bool>();
            for(int i=0;i<4;i++)
                _edges.Add(false);

        }

        for(int i=0;i<4;i++)
        {
            _edges[i] = (edgeBits & (1 << i)) != 0;
        }
        RenderEdges();
    }

    public void Rotate(bool clockwise = true)
    {
        if(clockwise)
        {
            bool edge = _edges[_edges.Count-1];
            _edges.RemoveAt(_edges.Count-1);
            _edges.Insert(0,edge);
            //transform.Rotate(Vector3.right, 90f, Space.Self);
        } else {
            bool edge = _edges[0];
            _edges.RemoveAt(0);
            _edges.Add(edge);
            //transform.Rotate(Vector3.right, -90f, Space.Self);
        }
        RenderEdges();
        
    }

    // Display a visual representation of the edges
    public void RenderEdges()
    {
        if(nodeType != PathNodeType.Node)
        {        
            if(_edges[0]) {
                textLabel.SetText("^");
            } else if(_edges[1])
            {
                textLabel.SetText(">");
            } else if(_edges[2])
            {
                textLabel.SetText("v");
            } else if(_edges[3])
            {
                textLabel.SetText("<");
            }
            return;
        }

        textLabel.SetText("");
        if(_edges[0] == true)
        {
            if(_edges[1] == true) {
                textLabel.SetText("╚");
            } else if(_edges[2] == true) {
                textLabel.SetText("║");
            } else if(_edges[3] == true) {
                textLabel.SetText("╝");
            }
        } else if(_edges[1] == true)
        {
            if(_edges[2] == true)
            {
                textLabel.SetText("╔");   
            } else if(_edges[3] == true)
            {
                textLabel.SetText("═");
            }
        } else if(_edges[2] == true)
        {
            if(_edges[3] == true)
            {
                textLabel.SetText("╗");
            }
        }

    }

}
