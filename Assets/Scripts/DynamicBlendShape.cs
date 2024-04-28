using UnityEngine;

public class DynamicBlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        Mesh mesh = skinnedMeshRenderer.sharedMesh;
        int vertexCount = mesh.vertexCount;

        Vector3[] deltaVertices = new Vector3[vertexCount];
        Vector3[] deltaNormals = new Vector3[vertexCount];
        Vector3[] deltaTangents = new Vector3[vertexCount];

        int shapeIndex = mesh.blendShapeCount;
        skinnedMeshRenderer.SetBlendShapeWeight(shapeIndex, 0);
        mesh.AddBlendShapeFrame("", 1.0f, deltaVertices, deltaNormals, deltaTangents);

        mesh.RecalculateNormals();
    }
}
