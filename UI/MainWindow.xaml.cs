using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UI.Models;
using UI.Services;

namespace UI;

public partial class MainWindow : Window
{
    private readonly TaskService taskService;
    private readonly ProcessService processService;

    public MainWindow()
    {
        InitializeComponent();
        taskService = new TaskService();
        processService = new ProcessService();

        UpdateList(false);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        UpdateList(false);

        if (this.Visibility != Visibility.Hidden)
        {
            e.Cancel = true;

            this.Hide();

            base.OnClosing(e);
        }
    }

    private void UpdateList(bool check)
    {
        if (check && Input.Text != "")
        {
            CurrentTasks.ItemsSource = processService.GetProcess()
                .Where(x => x.Name.ToLower().Contains(Input.Text.ToLower()));

            WatchTasks.ItemsSource = taskService.GetTasks()
                .Where(x => x.Name.ToLower().Contains(Input.Text.ToLower()));
        }

        else
        {
            CurrentTasks.ItemsSource = processService.GetProcess();
            WatchTasks.ItemsSource = taskService.GetTasks();
        }

        taskService.CheckState();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        UpdateList(true);
    }

    private void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateList(true);
    }

    private void WatchTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Check.IsChecked == true && e.AddedItems.Count > 0)
        {
            taskService.Delete(((TaskDTO)e.AddedItems[0]!).Name);
            UpdateList(false);
            Input.Text = "";
        }
    }

    private void CurrentTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0 && Check.IsChecked == true)
        {
            taskService.Add(((ProcessDTO)e.AddedItems[0]!).Name);
            UpdateList(false);
            Input.Text = "";
        }
    }
}