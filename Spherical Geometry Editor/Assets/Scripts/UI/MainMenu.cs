using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private Module module;
    private VisualElement ui;

    [SerializeField] private VisualTreeAsset VisualTreeAsset;

    public Action<string> OnButtonClicked;
    public Action OnNewButtonClicked;

    public void SetButtons(string[] names)
    {
        module = new Module();
        List<ItemData> list = new List<ItemData>();
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log("Adding " + names[i]);
            ItemData item = new ItemData();
            item.Name = names[i];
            list.Add(item);
        }
        module.data = list;
    }

    private void Start()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        ui.Q<Button>("NewButton").clicked += OnNewButtonClick;

        var holder = ui.Q<VisualElement>("holder");

        List<Button> buttons = new List<Button>();

        if (module is null)
        {
            return;
        }

        ui.dataSource = module;

        for (int i = 0; i < module.data.Count; i++)
        {
            var instance = VisualTreeAsset.CloneTree();
            instance.name = module.data[i].Name;
            instance.dataSource = module.data[i];

            Button btn = instance.Q<Button>();
            buttons.Add(btn);

            btn.clicked += () =>
            {
                OnButtonClicked?.Invoke(btn.text);
            };

            holder.Add(instance);
        }
    }


    private void OnNewButtonClick()
    {
        Debug.Log("new button clicked");
        OnNewButtonClicked?.Invoke();
    }

    public void Hide()
    {
        ui.visible = false;
    }

    public void Reveal()
    {
        ui.visible = true;
    }
}
