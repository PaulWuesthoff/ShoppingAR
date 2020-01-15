using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ValuesRadarChart : MonoBehaviour
{
    [SerializeField] private Material radarMaterial;
    [SerializeField] private Texture2D radarTexture2D;

    private Values value;
    private CanvasRenderer radarMeshCanvasRenderer;
    public Transform radarMesh; 
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        radarMeshCanvasRenderer = transform.Find("radarMesh").GetComponent<CanvasRenderer>();
        meshFilter = radarMesh.GetComponent<MeshFilter>();
        meshRenderer = radarMesh.GetComponent<MeshRenderer>();
    }

    public void setValues(Values values)
    {
        this.value = values;
        values.OnValuesChanged += Values_OnValuesChanged;
        UpdateValuesVisual();
    }

    private void Values_OnValuesChanged(object sender, System.EventArgs e)
    {
        UpdateValuesVisual();
    }

    private void UpdateValuesVisual()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[3 * 5];

        float angleIncrement = 360f / 5;
        float radarChartSize = 50f;

        Vector3 sugarVertex = Quaternion.Euler(0, 0, -angleIncrement * 0) * transform.up * radarChartSize * value.getValueAmountNormalized(Values.Type.Sugar);
        int sugarVertexIndex = 1;
        Vector3 carboonFootprintVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * transform.up * radarChartSize * value.getValueAmountNormalized(Values.Type.CarbonFootprint);
        int carbonFootprintVertexIndex = 2;
        Vector3 fatVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * transform.up * radarChartSize * value.getValueAmountNormalized(Values.Type.Fat);
        int fatVertexIndex = 3;
        Vector3 carbohydratesVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * transform.up * radarChartSize * value.getValueAmountNormalized(Values.Type.Carbohydrates);
        int carbohydratesVertexIndex = 4;
        Vector3 saltVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * transform.up * radarChartSize * value.getValueAmountNormalized(Values.Type.Salt);
        int saltVertexIndex = 5;

        vertices[0] = Vector3.zero;
        vertices[sugarVertexIndex] = sugarVertex;
        vertices[carbonFootprintVertexIndex] = carboonFootprintVertex;
        vertices[fatVertexIndex] = fatVertex;
        vertices[carbohydratesVertexIndex] = carbohydratesVertex;
        vertices[saltVertexIndex] = saltVertex;

        uv[0] = Vector2.zero;
        uv[sugarVertexIndex] = Vector2.one;
        uv[carbonFootprintVertexIndex] = Vector2.one;
        uv[fatVertexIndex] = Vector2.one;
        uv[carbohydratesVertexIndex] = Vector2.one;
        uv[saltVertexIndex] = Vector2.one;

        triangles[0] = 0;
        triangles[1] = sugarVertexIndex;
        triangles[2] = carbonFootprintVertexIndex;

        triangles[3] = 0;
        triangles[4] = carbonFootprintVertexIndex;
        triangles[5] = fatVertexIndex;

        triangles[6] = 0;
        triangles[7] = fatVertexIndex;
        triangles[8] = carbohydratesVertexIndex;

        triangles[9] = 0;
        triangles[10] = carbohydratesVertexIndex;
        triangles[11] = saltVertexIndex;

        triangles[12] = 0;
        triangles[13] = saltVertexIndex;
        triangles[14] = sugarVertexIndex;


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        radarMeshCanvasRenderer.SetMesh(mesh);
        radarMeshCanvasRenderer.SetMaterial(radarMaterial, radarTexture2D);
        meshFilter.mesh = mesh;
        meshRenderer.material = radarMaterial;
    }
}
