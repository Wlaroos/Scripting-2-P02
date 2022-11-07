using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinePointsTest : MonoBehaviour
{
    // Array of Line Objects
    [SerializeField] UILineRenderer[] _lines;

    // List of Point Objects
    List<GameObject> _images = new List<GameObject>();
    // List of points in ALL Line Objects
    List<Vector2> _localPoints = new List<Vector2>();

    int _totalPointCount;
    float _imageWidth = 50f;

    RectTransform _rectTransRef;

    private void Awake()
    {
        _rectTransRef = GetComponent<RectTransform>();

        // For every line object...
        for (int i = 0; i < _lines.Length; i++)
        {
            // Set References
            _lines[i] = transform.GetChild(i).GetComponent<UILineRenderer>();
            // Add to total point count from each line's point count
            _totalPointCount += _lines[i]._points.Count;
            // All width values should be the same, so grab from whichever
            _imageWidth = _lines[0]._imageWidth;

            // Add every Vector2 from each line object to the Vector2 List
            for (int j = 0; j < _lines[i]._points.Count; j++)
            {
                _localPoints.Add(_lines[i]._points[j]);
            }
        }

        // Create and set default values for every point needed -- Also, add each gameobject to the _images List
        for (int j = 0; j < _totalPointCount; j++)
        {
            _images.Add(new GameObject("Point", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)));

            Transform trans = _images[j].transform;
            trans.SetParent(transform);
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.Euler(0, 0, 0);

            RectTransform rect = _images[j].transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_imageWidth, _imageWidth);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);

        }
    }

    // Gives the LineRenderer script time to setup
    private void Start()
    {
        Invoke("Delay", .01f);
    }

    private void Delay()
    {
        // For each game object created, set the transform values to the vertex points of the line
        // This is done through the MoveImages method -- Have to subtract half of the rect transform's size becuase of the way the achors are set up
        int i = 1;
        for (int j = 0; j < _images.Count - 1; j++)
        {
            Vector2 point = _localPoints[j];
            Vector2 point2 = _localPoints[j + 1];

            _images[j].transform.localPosition = _lines[0].MoveImages(point) - new Vector3(_rectTransRef.sizeDelta.x/2, _rectTransRef.sizeDelta.y/2, 0);

            // This catches the final points in each line's list of points
            if (j + 1 == _lines[0]._points.Count*i - 1)
            {
                _images[j + 1].transform.localPosition = _lines[0].MoveImages(point2) - new Vector3(_rectTransRef.sizeDelta.x/2,_rectTransRef.sizeDelta.y/2,0);
                i++;
            }
        }

        // Remove duplicate images
        RemoveDuplicatesFrom(_images);
    }

    // Checking list for duplicates
    private void RemoveDuplicatesFrom(List<GameObject> collection)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            for (int j = i + 1; j < collection.Count; j++)
            {
                // If duplicate is found, destroy and remove from list
                if (!Match(collection[i], collection[j]))
                    continue;

                Destroy(collection[j].gameObject);
                collection.RemoveAt(j);
                j--;
            }
        }
    }

    // Test to see if transforms match
    private bool Match(GameObject object1, GameObject object2)
    {
        return object1.transform.position.x == object2.transform.position.x && object1.transform.position.y == object2.transform.position.y;
    }

    private void FixedUpdate()
    {
        /*        for (int i = 0; i < _images.Count -1; i++)
                {
                    Vector2 point = _parent._points[i];
                    Vector2 point2 = _parent._points[i + 1];

                    _images[i].transform.localPosition = _parent.MoveImages(point);
                    if (i + 1 == _parent._points.Count - 1)
                    {
                        _images[i + 1].transform.localPosition = _parent.MoveImages(point2);
                    }
                }*/
    }

}
