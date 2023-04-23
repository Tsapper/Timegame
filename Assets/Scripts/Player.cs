using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Volume volume;
    private ChromaticAberration aberration;
    private float targetChromatic;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private float input;
    private bool grounded;

    private void Start()
    {
        aberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        StartCoroutine(AudioManager.GetAudioManager().PlaySound(2, 2f));
    }

    private void Update()
    {
        input = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        Vector2 velocity = rb.velocity;
        velocity.x = input * speed * 100f;
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocity.y = jumpSpeed;
            grounded = false;
        }
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShiftTime();
        }
        aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, targetChromatic, 2f * Time.deltaTime);
        volume.profile.TryGet<ChromaticAberration>(out aberration);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }

    private void ShiftTime()
    {
        if (TimeManager.present)
        {
            targetChromatic = 0.5f;
            timeManager.ToFuture();
            transform.position += new Vector3(0, 30f, 0);
        }
        else
        {
            targetChromatic = 0f;
            timeManager.ToPresent();
            transform.position += new Vector3(0, -30f, 0);
        }
    }

    public static Player GetPlayer()
    {
        return FindObjectOfType<Player>();
    }
}
