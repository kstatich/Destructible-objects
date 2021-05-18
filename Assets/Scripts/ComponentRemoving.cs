using UnityEngine;

public class ComponentRemoving : MonoBehaviour
{  
    public void DestroyComponent()
    {
        foreach(Transform child in transform)
        {
            var body = child.gameObject.GetComponent<Rigidbody>();

            if (body != null)
            {
                Destroy(body);
                Destroy(body.gameObject.GetComponent<MeshCollider>());                
            }
        }
    }
}
