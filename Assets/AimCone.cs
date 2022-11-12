using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AimCone : MonoBehaviour
{
    public SpriteRenderer cone;
    public Transform t;
    public Vector2 center;
    public float driftSpeed;
    public float moveSpeed;
    //[HideInInspector]
    public Vector3 normal = new Vector3(0,0,-.2f);
    //[HideInInspector]
    public Vector2 directionMove;
    public Vector2 cache = Vector2.zero;
    public WaitForSeconds wait;
    public Coroutine swap;
    public LayerMask layer;

    public InputActionReference move;
    public InputActionReference fire;


    public void begin()
    {
        wait = new WaitForSeconds(3);
        swap = StartCoroutine(direction());
        cone.enabled = true;
        enabled = true;
        normal.x = 0;
        normal.y = 0;
        normal.z = -.2f;
        move.action.Enable();
        move.action.performed += onMove;
        move.action.canceled += off;
        fire.action.Enable();
        fire.action.performed += shoot;

    }


    public void OnDisable()
    {
        StopCoroutine(swap);
        cone.enabled = false;

        move.action.Disable();
        move.action.performed -= onMove;
        move.action.canceled -= off;

        fire.action.Disable();
        fire.action.performed -= shoot;
    }

    public void Update()
    {
        normal.y += (directionMove.y + cache.y ) * Time.deltaTime ;
        normal.x += (directionMove.x + cache.x) * Time.deltaTime;
        t.localPosition = normal;
        
        //normal.x = t.localPosition.x;
        //normal.y = t.localPosition.y;
        
    }

    public void shoot(InputAction.CallbackContext c)
    {

        if(Physics.Raycast(t.localPosition, Vector3.forward, out RaycastHit hit, layer))
        {
            Debug.DrawLine(t.localPosition, hit.point, Color.black, 10);
            hit.collider.gameObject.GetComponent<BoardCollider>().hit();
        }

        enabled = false;
    }

    public IEnumerator direction()
    {
        while (true)
        {
            directionMove.x = Random.Range(-driftSpeed, driftSpeed);
            directionMove.y = Random.Range(-driftSpeed, driftSpeed);
            yield return wait;

        }
    }

    public IEnumerator Recover()
    {

        directionMove.x = center.x - normal.x;
        directionMove.y = center.y - normal.y;

        directionMove.x = Mathf.Clamp(directionMove.x,-moveSpeed, moveSpeed);
        directionMove.y = Mathf.Clamp(directionMove.y, -moveSpeed, moveSpeed);

        //Debug.Log("dir " + directionMove);
        //throw new System.AccessViolationException();
        yield return wait;
        swap = StartCoroutine(direction());
    }

    public void off(InputAction.CallbackContext c)
    {
        cache = Vector2.zero;
    }

    public void onMove(InputAction.CallbackContext c)
    {
      
        cache = c.ReadValue<Vector2>();
        cache.x  *= moveSpeed;
        cache.y *= moveSpeed;
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Out\a");
        StopCoroutine(swap);
      
        StartCoroutine(Recover());
    }
}
