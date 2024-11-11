using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintWall : MonoBehaviour
{
    public Color brushColor = Color.red;  
    public float brushSize = 1f;       
    private Mesh wallMesh;
    private Color[] vertexColors;

    private Camera paintingCamera;

    private int totalVertices;
    private int paintedVertices = 0;

    public delegate void OnPaintedPercentageChanged(float percentage);
    public event OnPaintedPercentageChanged PaintedPercentageChanged;

    private void Awake()
    {
        paintingCamera = Camera.main;

        
        wallMesh = GetComponent<MeshFilter>().mesh;
        totalVertices = wallMesh.vertexCount;
        vertexColors = new Color[totalVertices];


        for (int i = 0; i < vertexColors.Length; i++)
        {
            vertexColors[i] = Color.white;
        }

        wallMesh.colors = vertexColors;

        if (paintingCamera == null)
        {
            Debug.LogError("Painting camera is not assigned!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Paint();
        }
    }

    private void Paint()
    {
        if (paintingCamera == null || wallMesh == null)
        {
            Debug.LogError("Camera or Wall Mesh not found!");
            return;
        }

        Ray ray = paintingCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("PaintingWall");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 hitPoint = hit.point;

            
            Vector3[] vertices = wallMesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 worldPos = transform.TransformPoint(vertices[i]);
                float distance = Vector3.Distance(worldPos, hitPoint);

                
                if (distance < brushSize)
                {
                    
                    if (vertexColors[i] == Color.white)
                    {
                        paintedVertices++;
                        UpdatePaintedPercentage();
                    }

                    vertexColors[i] = brushColor;
                }
            }


            wallMesh.colors = vertexColors;
        }
    }

    private void UpdatePaintedPercentage()
    {
        float percentage = (float)paintedVertices / totalVertices * 100f;

        
        PaintedPercentageChanged?.Invoke(percentage);
    }


    public void SetBrushColor(Color newColor)
    {
        brushColor = newColor;
    }

    
    public void SetBrushSize(float newSize)
    {
        brushSize = newSize;
    }
}
