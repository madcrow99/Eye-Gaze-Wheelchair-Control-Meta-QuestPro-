using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{
    public bool IsHovered { get; set; }
    private bool HoverActive = false;

    private float HoverTimer = 0.0f;
    private float HoverTimeLimit = 0.5f;
    
    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    private Material OnHoverActiveMaterial;

    [SerializeField]
    private Material OnHoverInactiveMaterial;

    [SerializeField]
    private string InteractableCMD;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //if (tag == "pause") HoverTimeLimit = 1.0f;
    }

    private void Update()
    {
        if (IsHovered)
        {
            OnObjectHover?.Invoke(gameObject);
            
            if (!HoverActive)
            {
                HoverActive= true;
                HoverTimer= Time.time;
            }
            else if(Time.time - HoverTimer > HoverTimeLimit)
            {
                HoverAction();
                HoverActive= false;
            }

         meshRenderer.material = OnHoverActiveMaterial;
        }
        else
        {
            meshRenderer.material = OnHoverInactiveMaterial;
            HoverActive = false;
        }
    }

    private void HoverAction()
    {
        if(InteractableCMD == "pause") PowerChairClient.Instance.pauseActive = !PowerChairClient.Instance.pauseActive;
        if (!PowerChairClient.Instance.pauseActive)
        {
            byte[] sendbuf = Encoding.ASCII.GetBytes(InteractableCMD);

            PowerChairClient.Instance.getSock().SendTo(sendbuf, PowerChairClient.Instance.getEP());
         
        }
    

    }
}
