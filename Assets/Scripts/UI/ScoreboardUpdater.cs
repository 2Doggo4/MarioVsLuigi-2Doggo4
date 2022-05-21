using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreboardUpdater : MonoBehaviour {

    public static ScoreboardUpdater instance;
    private static IComparer<ScoreboardEntry> entryComparer;

    [SerializeField] GameObject entryTemplate;
    
    private readonly List<ScoreboardEntry> entries = new();
    private bool toggled = false;
    private Animator animator;

    public void OnEnable() {
        InputSystem.controls.UI.Scoreboard.performed += OnToggle;
    }
    public void OnDisable() {
        InputSystem.controls.UI.Scoreboard.performed -= OnToggle;
    }

    private void OnToggle(InputAction.CallbackContext context) {
        toggled = !toggled;
        animator.SetFloat("speed", toggled ? 1 : -1);
        animator.Play("toggle", 0, Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
    }

    public void Awake() {
        instance = this;
        animator = GetComponent<Animator>();
        if (entryComparer == null)
            entryComparer = new ScoreboardEntry.EntryComparer();
    }

    public void Reposition() {
        entries.Sort(entryComparer);
        entries.ForEach(se => se.transform.SetAsLastSibling());
    }

    public void Populate(PlayerController[] players) {
        foreach (PlayerController player in players) {
            GameObject entryObj = Instantiate(entryTemplate, transform);
            entryObj.SetActive(true);
            entryObj.name = player.photonView.Owner.NickName;
            ScoreboardEntry entry = entryObj.GetComponent<ScoreboardEntry>();
            entry.target = player;

            entries.Add(entry);
        }

        Reposition();
    }
}