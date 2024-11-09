using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ThrowingUp : MonoBehaviour, IInteractable
{
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private LayerMask playerLayer;


    private bool isThrowing = false;
    private float jumpStartTime = 0f;
    private float yPos;
    private float jumpHeight = 0f;


    private void OnCollisionEnter(Collision collision)
    {
        if(IsPlayerLayer(collision.gameObject) && isThrowing)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void Interact()
    {
        var player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            yPos = player.transform.position.y;
            isThrowing= true;
            jumpStartTime= Time.time;
            jumpHeight= 0f;
            Vector3 newPosition = player.transform.position;
            newPosition.y+= jumpForce;
            newPosition.z += 0.1f;
            player.transform.position = newPosition;
        }
    }

    private void Update()
    {
        if (isThrowing)
        {
            var player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                float timeElapsed = Time.time - jumpStartTime;

                jumpHeight = Mathf.Lerp(0f, jumpForce, timeElapsed / duration);
                player.transform.position = new Vector3(player.transform.position.x, yPos + jumpHeight, player.transform.position.z);

                if (timeElapsed >= duration)
                {
                    isThrowing= false;
                }
            }
        }
    }

    private bool IsPlayerLayer(GameObject collision)
    {
        return ((1 << collision.gameObject.layer) & playerLayer) != 0;
    }
}
