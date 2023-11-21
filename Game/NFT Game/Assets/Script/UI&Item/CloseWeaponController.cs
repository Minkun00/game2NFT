//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class CloseWeaponController : MonoBehaviour
//{
//    // �̿ϼ� Ŭ���� = �߻� Ŭ����.


//    // ���� ������ Hand�� Ÿ�� ����.
//    [SerializeField]
//    protected CloseWeapon currentCloseWeapon;

//    // ������??
//    protected bool isAttack = false;
//    protected bool isSwing = false;

//    protected RaycastHit hitInfo;


//    //�ʿ��� ������Ʈ
//    private PlayerMove thePlayerController;

//    void Start()
//    {
//        thePlayerController = FindObjectOfType<PlayerMove>();
//    }

//    protected void TryAttack()
//    {
//        if (!Inventory.inventoryActivated)
//        {
//            if (Input.GetButton("Fire1"))
//            {
//                if (!isAttack)
//                {
//                    if (CheckObject())
//                    {
//                        if (currentCloseWeapon.isAxe && hitInfo.transform.tag == "Tree")
//                        {
//                            StartCoroutine(thePlayerController.TreeLookCoroutine(hitInfo.transform.GetComponent<TreeComponent>().GetTreeCenterPosition()));
//                            StartCoroutine(AttackCoroutine("Chop", currentCloseWeapon.workDelayA, currentCloseWeapon.workDelayB, currentCloseWeapon.workDelay));
//                            return;
//                        }
//                    }

//                    StartCoroutine(AttackCoroutine("Attack", currentCloseWeapon.attackDelayA, currentCloseWeapon.attackDelayB, currentCloseWeapon.attackDelay));
//                }
//            }
//        }
//    }

//    protected IEnumerator AttackCoroutine(string swingType, float _delayA, float _delayB, float _delayC)
//    {
//        isAttack = true;
//        currentCloseWeapon.anim.SetTrigger(swingType);

//        yield return new WaitForSeconds(_delayA);
//        isSwing = true;

//        StartCoroutine(HitCoroutine());

//        yield return new WaitForSeconds(_delayB);
//        isSwing = false;


//        yield return new WaitForSeconds(_delayC - _delayA - _delayB);
//        isAttack = false;
//    }


//    // �̿ϼ� = �߻� �ڷ�ƾ.
//    protected abstract IEnumerator HitCoroutine();


//    protected bool CheckObject()
//    {
//        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
//        {
//            return true;
//        }
//        return false;
//    }

//    // �ϼ� �Լ�������, �߰� ������ �Լ�.
//    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
//    {
//        if (WeaponManager.currentWeapon != null)
//            WeaponManager.currentWeapon.gameObject.SetActive(false);

//        currentCloseWeapon = _closeWeapon;
//        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
//        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;

//        currentCloseWeapon.transform.localPosition = Vector3.zero;
//        currentCloseWeapon.gameObject.SetActive(true);
//    }
//}
