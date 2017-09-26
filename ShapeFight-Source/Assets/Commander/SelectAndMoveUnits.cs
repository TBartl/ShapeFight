using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class SelectAndMoveUnits : NetworkBehaviour {
    [Header("Unit Selection")]
    public GameObject selectionCircle;

    UnitTeam team;

    public Vector2 sizeRange;
    float currentSize;
    public float sizeGrowSpeed;

    bool mouseWasDown = false;

    [HideInInspector] public List<UnitMove> selectedUnits = new List<UnitMove>();

    [Header("Unit Mover")]
    public GameObject arrow;
    Vector2 maxArrowSize;

    void Awake()
    {
        this.team = this.GetComponent<UnitTeam>();
    }

    void Update () {
        if (!isLocalPlayer)
            return;

        UpdateSelectorLogic();
        UpdateSelectorDisplay();        
	}

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        //Selection
        selectionCircle.transform.position = GetMousePosition() + Vector3.up * .05f;        
        if (Input.GetMouseButton(0))
            AddUnitsInCircle();

        //Mover
        if (Input.GetMouseButton(1))
        {
            arrow.SetActive(true);
            UpdateUnitsTarget();
        }
        else
        {
            arrow.SetActive(false);
        }
        
    }

    void UpdateSelectorLogic()
    {
        if (Input.GetMouseButton(0))
        {
            if (mouseWasDown)
                ClearSelectedUnits();
            currentSize += sizeGrowSpeed * Time.deltaTime;
        }
        else
        {
            currentSize -= sizeGrowSpeed * Time.deltaTime * 2;
        }
        currentSize = Mathf.Clamp(currentSize, sizeRange.x, sizeRange.y);
        mouseWasDown = Input.GetMouseButtonDown(0);
    }

    void UpdateSelectorDisplay()
    {
        selectionCircle.transform.localScale = Vector3.one * currentSize * GetSizeModifierForSelectionCircle();
    }

    void AddUnitsInCircle()
    {

        //TODO optimize this?
        Collider[] colliders = Physics.OverlapSphere(selectionCircle.transform.position, currentSize * GetSizeModifierForSelectionCircle(), 1 << 10);
        for (int index = 0; index < colliders.Length; index++)
        {
            UnitMove u = colliders[index].GetComponent<UnitMove>();
            if (u.CheckSameTeam(this.team.teamID) && !selectedUnits.Contains(u))
            {
                selectedUnits.Add(u);
                u.OnSelect();
            }
        }
    }

    void ClearSelectedUnits()
    {
        for (int index = 0; index < selectedUnits.Count; index++)
        {
            if (selectedUnits[index] != null)
                selectedUnits[index].OnDeselect();
        }
        selectedUnits.Clear();
    }

    Vector3 GetMousePosition()
    {
        Vector3 point = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, 200, 1 << 8))
        {
            point = info.point;
        }
        return point;
    }

    float GetSizeModifierForSelectionCircle()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            return 6;
        else if (Input.GetKey(KeyCode.LeftShift))
            return 3;
        return 1;
    }

    void UpdateUnitsTarget()
    {
        Vector3 mousePosition = GetMousePosition();
        arrow.transform.position = mousePosition;
        for (int index = 0; index < selectedUnits.Count; index++)
        {
            if (selectedUnits[index] != null)
                CmdSetUnitTarget(selectedUnits[index].gameObject, mousePosition);
        }
    }

    [Command]
    void CmdSetUnitTarget(GameObject unit, Vector3 position)
    {
        if (unit)
        {
            UnitMove tempMove = unit.GetComponent<UnitMove>();
            tempMove.SetTarget(position);
        }
    }
}

