using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ThrowingUp : MonoBehaviour, IInteractable
{
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask aiLayer;



    private bool isThrowing = false;
    private float jumpStartTime = 0f;
    private float playerPos;
    private float aiPos;
    private float jumpHeight = 0f;


    private void OnCollisionEnter(Collision collision)
    {
        if(IsPlayerLayer(collision.gameObject) && isThrowing || IsAILayer(collision.gameObject) && isThrowing)
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
        var ai = GameObject.FindWithTag("AI");

        if(player != null)
        {
            playerPos = player.transform.position.y;
            

            isThrowing= true;
            jumpStartTime= Time.time;
            jumpHeight= 0f;

            Vector3 newPlayerPosition = player.transform.position;
            newPlayerPosition.y+= jumpForce;
            newPlayerPosition.z += 0.1f;
            player.transform.position = newPlayerPosition;

            
        }

        if(ai != null)
        {
            aiPos = ai.transform.position.y;

            isThrowing = true;
            jumpStartTime = Time.time;
            jumpHeight = 0f;

            Vector3 newAiPosition = ai.transform.position;
            newAiPosition.y += jumpForce;
            newAiPosition.z += 0.1f;
            ai.transform.position = newAiPosition;
        }
    }

    private void Update()
    {
        if (isThrowing)
        {
            var player = GameObject.FindWithTag("Player");
            var ai = GameObject.FindWithTag("AI");

            if (player != null)
            {
                float timeElapsed = Time.time - jumpStartTime;

                jumpHeight = Mathf.Lerp(0f, jumpForce, timeElapsed / duration);

                player.transform.position = new Vector3(player.transform.position.x, playerPos + jumpHeight, player.transform.position.z);
                

                if (timeElapsed >= duration)
                {
                    isThrowing= false;
                }
            }

            if (ai != null)
            {
                float timeElapsed = Time.time - jumpStartTime;

                jumpHeight = Mathf.Lerp(0f, jumpForce, timeElapsed / duration);

                
                ai.transform.position = new Vector3(ai.transform.position.x, aiPos + jumpHeight, ai.transform.position.z);

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
    private bool IsAILayer(GameObject collision)
    {
        return ((1 << collision.gameObject.layer) & aiLayer) != 0;
    }
}
