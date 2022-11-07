using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    [SerializeField] UIGridRenderer _grid;

    [SerializeField] Vector2Int _gridSize;

    [SerializeField] List<Vector2> _points;

    [SerializeField] List<GameObject> _Images;
    [SerializeField] float _ImageHalfWidth = 25;

    float _width;
    float _height;
    float _unitWidth;
    float _unitHeight;

    [SerializeField] float _thickness = 10f;

    private void Update()
    {
        if (_grid != null)
        {
            if (_gridSize != _grid._gridSize)
            {
                _gridSize = _grid._gridSize;
                SetVerticesDirty();
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        _width = rectTransform.rect.width;
        _height = rectTransform.rect.height;

        _unitWidth = _width / (float)_gridSize.x;
        _unitHeight = _height / (float)_gridSize.y;

        if(_points.Count < 2)
        {
            return;
        }
        float angle = 0;

        for (int i = 0; i < _points.Count - 1; i++)
        {
            Vector2 point = _points[i];
            Vector2 point2 = _points[i+1];

            if(i < _points.Count - 1)
            {
                angle = GetAngle(_points[i], _points[i + 1]) + 90f;
            }

            DrawVerticesForPoint(point, point2, vh, angle);

            _Images[i].transform.localPosition = MoveImages(point);
            if (i + 1 == _points.Count - 1)
            {
                _Images[i + 1].transform.localPosition = MoveImages(point2);
            }
        }

        for (int i = 0; i < _points.Count -1; i++)
        {
            int index = i * 4;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 1, index + 2, index + 3);
        }
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    void DrawVerticesForPoint(Vector2 point, Vector2 point2, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0,0,angle) * new Vector3(-_thickness / 2, 0);
        vertex.position += new Vector3(_unitWidth * point.x, _unitHeight * point.y);
        vh.AddVert(vertex);
        //Debug.Log(vertex.position);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(_thickness / 2, 0);
        vertex.position += new Vector3(_unitWidth * point.x, _unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-_thickness / 2, 0);
        vertex.position += new Vector3(_unitWidth * point2.x, _unitHeight * point2.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(_thickness / 2, 0);
        vertex.position += new Vector3(_unitWidth * point2.x, _unitHeight * point2.y);
        vh.AddVert(vertex);
    }

    Vector3 MoveImages(Vector2 point)
    {
        UIVertex vertex = UIVertex.simpleVert;

        vertex.position = new Vector3(_unitWidth * point.x -_ImageHalfWidth, _unitHeight * point.y - _ImageHalfWidth);

        return vertex.position;
    }
}
