using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFaceMaterialAssigner : MonoBehaviour
{
    public Material frontFaceMaterial;
    public Material otherFacesMaterial;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;

        Material[] materials = new Material[mesh.subMeshCount];
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            materials[i] = otherFacesMaterial;
        }

        for (int i = 0; i < normals.Length; i++)
        {
            if (normals[i] == Vector3.forward)
            {
                materials[0] = frontFaceMaterial;
                break;
            }
        }

        GetComponent<MeshRenderer>().materials = materials;
    }
}
