using UnityEngine;
using System.Collections;

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            CombineMeshes();
        }
    }

    void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];

        for (int i = 0, j = 0; i < meshFilters.Length; i++)
        {
            if(meshFilters[i].gameObject != gameObject)
            {
                combine[j].mesh = meshFilters[i].sharedMesh;
                combine[j].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
                j++;
            }
        }
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        Debug.Log(meshFilter.mesh.triangles.Length);
    }
}