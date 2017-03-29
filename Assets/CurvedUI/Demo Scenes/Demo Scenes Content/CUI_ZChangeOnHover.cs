using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace CurvedUI
{
    public class CUI_ZChangeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static bool hovering;
        public static Button currentHoverButton;


        private Button myButton;

        public float restZ = 0;
        public float OnHoverZ = -50;

        bool Zoomed = false;

        private void Start()
        {
            myButton = GetComponent<Button>();
        } 

        // Update is called once per frame
        void Update()
        {

            (transform as RectTransform).anchoredPosition3D = (transform as RectTransform).anchoredPosition3D.ModifyZ(Mathf.Clamp((Zoomed ?
                (transform as RectTransform).anchoredPosition3D.z + Time.deltaTime * (OnHoverZ - restZ) * 6 :
                (transform as RectTransform).anchoredPosition3D.z - Time.deltaTime * (OnHoverZ - restZ) * 6), OnHoverZ, restZ));

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Zoomed = true;
            hovering = true;
            currentHoverButton = myButton;

        }

        public void OnPointerExit(PointerEventData eventData)
        {

            Zoomed = false;
            hovering = false;
            currentHoverButton = null;
        }
    }
}
