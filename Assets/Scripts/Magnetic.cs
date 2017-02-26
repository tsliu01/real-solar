using UnityEngine;
using System.Collections;
public class Magnetic : MonoBehaviour
{
    public LayerMask m_MagneticLayers;
    public Vector3 m_Position;
    public float m_Radius;
    public float m_Force;
    void FixedUpdate()
    {
        Collider[] colliders;
        Rigidbody rigidbody;
        colliders = Physics.OverlapSphere(transform.position + m_Position, m_Radius, m_MagneticLayers);
        foreach (Collider collider in colliders)
        {
            rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            if (rigidbody == null)
            {
                continue;
            }
            rigidbody.AddExplosionForce(m_Force * -1, transform.position + m_Position, m_Radius);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + m_Position, m_Radius);
    }
}