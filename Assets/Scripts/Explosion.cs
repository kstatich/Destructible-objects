using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force; 

    private void Start() => Destroy(gameObject, 2f);

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<ChangeToDestructible>())
            {
                hitColliders[i].GetComponent<ChangeToDestructible>().EnableDestruction();
            }
            //if(hitColliders[i].TryGetComponent(out Destructible d))
            if (hitColliders[i].GetComponent<Destructible>()) 
            {
                if (!hitColliders[i].GetComponent<Rigidbody>())
                {
                    hitColliders[i].gameObject.AddComponent<Rigidbody>();
                    var body = hitColliders[i].gameObject.GetComponent<Rigidbody>();
                    body.AddExplosionForce(_force, transform.position, _radius);                     
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }   
}
