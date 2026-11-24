using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TurretPlacementManager : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private InputActionReference _mousePosition;
    [SerializeField] private InputActionReference _mouseClick;
    [SerializeField] private GameObject _constructionPrefab;
    [SerializeField] private GameObject _placementMenu;
    [SerializeField] private GameObject _editMenu;
    [SerializeField] private float _menuSize = 3.5f;
    [SerializeField] private SOConstruction[] _firstLevels;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Text _levelLabel;
    [SerializeField] private GameManager _gameManager;
    public event Action<int, int> OnMinePlaceOrUpgrade;
    private List<ConstructionBehaviour> _constructions = new List<ConstructionBehaviour>();
    private Vector3Int _selectedCell;
    private ConstructionBehaviour _selectedConstruction;
    private bool _clickPending;

    private void OnEnable()
    {
        _mouseClick.action.performed += OnClick;
        _mousePosition.action.Enable();
        _mouseClick.action.Enable();
    }

    private void OnDisable()
    {
        _mouseClick.action.performed -= OnClick;
        _mouseClick.action.Disable();
        _mousePosition.action.Disable();
        _clickPending = false;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        _clickPending = true;
    }

    void Start()
    {
        CloseMenus();
    }

    void Update()
    {
        if (!_clickPending)
        {
            return;
        }
        _clickPending = false;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; 
        }

        Vector2 mousePosition = _mousePosition.action.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int cell = _tilemap.WorldToCell(mouseWorldPosition);
        SelectCell(cell);   
    }

    public void SelectCell(Vector3Int cell)
    {
        CloseMenus();

        if (_tilemap.HasTile(cell))
        {
            return;
        }

        _selectedCell = cell;
        _selectedConstruction = null;
        foreach (ConstructionBehaviour construction in _constructions)
        {
            if (_tilemap.WorldToCell(construction.transform.position) == cell)
            {
                _selectedConstruction = construction;
                break;
            }
        }

        if (_selectedConstruction == null)
        {
            ShowMenu(_placementMenu);
        }
        else
        {
            _upgradeButton.interactable = _selectedConstruction.Level < 3;
            _levelLabel.text = "L." + _selectedConstruction.Level;
            ShowMenu(_editMenu);
        }
    }

    public void Place(int type)
    {
        if (_gameManager.TryBuyTurret(_firstLevels[type].Value))
        {
            Vector3 position = _tilemap.GetCellCenterWorld(_selectedCell);
            GameObject newConstruction = Instantiate(_constructionPrefab, position, Quaternion.identity);
            ConstructionBehaviour construction = newConstruction.GetComponent<ConstructionBehaviour>();
            construction.Init(_firstLevels[type]);
            _constructions.Add(construction);
            if (construction.Construction.Type == SOConstruction.ConstructionType.Mine)
            {
                OnMinePlaceOrUpgrade?.Invoke(0,construction.Level);
            }
            CloseMenus();
        }
        else
        {
            Debug.Log("Dinero insuficiente");
            CloseMenus();
            // Mostrar TOAST
        }
        
    }

    public void Upgrade()
    {
        if (_selectedConstruction == null || _selectedConstruction.Level == 3)
        {
            return;
        }
        if (_gameManager.TryBuyTurret(_selectedConstruction.Construction.Value+(_selectedConstruction.Level*10)))
        {
            int lastLevel = _selectedConstruction.Level;
            _selectedConstruction.Level++;
            _upgradeButton.interactable = _selectedConstruction.Level < 3;
            _levelLabel.text = "L." + _selectedConstruction.Level;
            if (_selectedConstruction.Construction.Type == SOConstruction.ConstructionType.Mine)
            {
                OnMinePlaceOrUpgrade?.Invoke(lastLevel,_selectedConstruction.Level);
            }
            CloseMenus();
        }
        else
        {
            CloseMenus();
            // Mostrar TOAST
        }
    }

    public void Remove()
    {
        if (_selectedConstruction == null)
        {
            return;
        }

        _constructions.Remove(_selectedConstruction);
        if (_selectedConstruction.Construction.Type == SOConstruction.ConstructionType.Mine)
        {
            OnMinePlaceOrUpgrade?.Invoke(_selectedConstruction.Level, 0);
        }
        Destroy(_selectedConstruction.gameObject);
        CloseMenus();
    }

    public void CloseMenus()
    {
        _placementMenu.SetActive(false);
        _editMenu.SetActive(false);
        _selectedConstruction = null;
    }

    private void ShowMenu(GameObject menu)
    {
        menu.transform.localScale = Vector3.one * (_menuSize / 100f);
        menu.transform.position = _tilemap.GetCellCenterWorld(_selectedCell);
        menu.SetActive(true);
    }
}
