using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinePoints : MonoBehaviour
{
    UILineRenderer _parent;
    List<GameObject> _images = new List<GameObject>();

    int _count;
    float _imageWidth = 50f;

    private void Awake()
    {
        _parent = GetComponent<UILineRenderer>();
        _count = _parent._points.Count;
        _imageWidth = _parent._imageWidth;

        for (int i = 0; i < _count; i++)
        {
            _images.Add(new GameObject("Point", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)));

            Transform trans = _images[i].transform;
            trans.SetParent(transform);
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.Euler(0, 0, 0);

            RectTransform rect = _images[i].transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_imageWidth, _imageWidth);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _images.Count -1; i++)
        {
            Vector2 point = _parent._points[i];
            Vector2 point2 = _parent._points[i + 1];

            _images[i].transform.localPosition = _parent.MoveImages(point);
            if (i + 1 == _parent._points.Count - 1)
            {
                _images[i + 1].transform.localPosition = _parent.MoveImages(point2);
            }
        }
    }

}
