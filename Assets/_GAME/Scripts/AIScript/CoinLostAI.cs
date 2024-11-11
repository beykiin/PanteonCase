using UnityEngine;

public class CoinLostAI : MonoBehaviour
{
    [SerializeField] LayerMask aiLayer;

    private void OnTriggerEnter(Collider other)
    {

        if (IsAICollider(other))
        {
            Destroy(gameObject);
        }
    }
    private bool IsAICollider(Collider collider) {

        return ((aiLayer.value & (1 << collider.gameObject.layer)) != 0);

    }
        
 
}
