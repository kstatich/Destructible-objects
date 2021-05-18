using UnityEngine;

public class ChangeToDestructible : MonoBehaviour
{
    [SerializeField] private Transform _destructiblePrefab;

    public void EnableDestruction()
    {         
        Instantiate(_destructiblePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
