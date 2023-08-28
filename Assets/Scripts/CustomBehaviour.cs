using UnityEngine;

/// <summary>
/// Ubh mono behaviour.
/// </summary>
public abstract class CustomBehaviour : MonoBehaviour
{
    protected GameObject m_gameObject;
    protected Transform m_transform;
    protected Renderer m_renderer;
    protected Rigidbody m_rigidbody;
    protected Animator m_animator;

    public new GameObject gameObject
    {
        get
        {
            if (m_gameObject == null)
            {
                m_gameObject = base.gameObject;
            }
            return m_gameObject;
        }
    }

   

    public new Transform transform
    {
        get
        {
            if (m_transform == null)
            {
                m_transform = GetComponent<Transform>();
            }
            return m_transform;
        }
    }

   


    public new Renderer renderer
    {
        get
        {
            if (m_renderer == null)
            {
                m_renderer = GetComponent<Renderer>();
            }
            return m_renderer;
        }
    }

    public new Rigidbody rigidbody
    {
        get
        {
            if (m_rigidbody == null)
            {
                m_rigidbody = GetComponent<Rigidbody>();
               
            }
            return m_rigidbody;
        }
    }

  
}
