//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestSpwaner : MonoBehaviour
//{
//    public GameObject ItemPrefab;

//    private Camera _mainCam;
//    // Start is called before the first frame update
//    void Start()
//    {
//        _mainCam = Camera.main;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // 1. ���콺 Primary��ư�� Ŭ���ϸ�
//        if (Input.GetMouseButton(0))
//        {
//            // 2. �� ������ ����
//            Ray mouseRay = _mainCam.ScreenPointToRay(Input.mousePosition);

//            LayerMask targetLayer = LayerMask.NameToLayer("Ground");
//            int layerMask = (1 << targetLayer.value);

//            RaycastHit hit;
//            bool isHit = Physics.Raycast(mouseRay, out hit, 100f);
//            if (isHit)
//            {
//                if(hit.collider.gameObject.layer == )
//                // 3. �� ���� �������� ����
//                Vector3 spawnPosition = hit.point;
//                spawnPosition.y += 0.5f;
//                GameObject item = Instantiate(ItemPrefab, spawnPosition, Quaternion.identity);
//                Destroy(item, 5f);
//            }
//        }
//    }
//}
