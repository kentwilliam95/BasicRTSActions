using System.Buffers;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    private int layerMaskGround;
    private int layerMaskInteractable;
    private bool _isTouchEnable;

    [SerializeField] private Camera _cam;
    [SerializeField] private Player _player;

    [Header("User Interface")]
    [SerializeField] private GameUIController _uiController;

    private void Start()
    {
        layerMaskGround = LayerMask.GetMask("Ground");
        layerMaskInteractable = LayerMask.GetMask("Interactable");
        ArrayPool<RaycastHit>.Create(32, 16);

        var objecctSpawners = GetComponentsInChildren<ObjectSpawner>();
        foreach (var item in objecctSpawners)
            item.Initialize();

        Global.InitializeAnimationID();
        _isTouchEnable = true;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        bool isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
        if (!Input.GetMouseButtonDown(0) || isPointerOverUI)
            return;

        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        var hitResultGround = ArrayPool<RaycastHit>.Shared.Rent(1);
        var hitResultInteractable = ArrayPool<RaycastHit>.Shared.Rent(8);

        int hitCountGround = Physics.RaycastNonAlloc(ray, hitResultGround, 100, layerMaskGround);
        int hitCountInteractable = Physics.RaycastNonAlloc(ray, hitResultInteractable, 100, layerMaskInteractable);
        var position = Vector3.zero;

        bool isFoundInteractable = false;
        for (int i = 0; i < hitCountInteractable; i++)
        {
            var iDamageAble = hitResultInteractable[i].collider.GetComponentInParent<IDamage>();
            if (iDamageAble != null)
            {
                _player.Attack(iDamageAble);
                isFoundInteractable = true;
                break;
            }

            var iPickable = hitResultInteractable[i].collider.GetComponentInParent<IPickable>();
            if (iPickable != null)
            {
                _player.Pick(iPickable);
                isFoundInteractable = true;
                break;
            }
        }

        if (!isFoundInteractable)
        {
            _player.Move(hitResultGround[0].point);
        }

        ArrayPool<RaycastHit>.Shared.Return(hitResultInteractable);
        ArrayPool<RaycastHit>.Shared.Return(hitResultGround);
    }

    public void OpenInventory()
    {
        _uiController.SetupInventory(_player.Inventory);
        _uiController.OpenInventory();
        _isTouchEnable = false;
    }

    public void CloseInventory()
    {
        _uiController.CloseInventory();
        _isTouchEnable = true;
    }
}
