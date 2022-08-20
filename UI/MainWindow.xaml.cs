using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UI;

public partial class MainWindow : Window
{
    TaskService taskService;

    public MainWindow()
    {
        InitializeComponent();

        taskService = new TaskService();

        CurrentTasks.ItemsSource = taskService.MapProcesses(Process.GetProcesses());
        WatchTasks.ItemsSource = taskService.GetTasks();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
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
            CurrentTasks.ItemsSource = taskService.MapProcesses(Process.GetProcesses())
                .Where(x => x.Name.ToLower().Contains(Input.Text.ToLower()));

            WatchTasks.ItemsSource = taskService.GetTasks()
                .Where(x => x.Name.ToLower().Contains(Input.Text.ToLower())).ToArray();
        }

        else
        {
            CurrentTasks.ItemsSource = taskService.MapProcesses(Process.GetProcesses());

            WatchTasks.ItemsSource = taskService.GetTasks();
        }

        taskService.CheckState();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        UpdateList(true);
    }

    private void Input_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        UpdateList(true);
    }

    private void WatchTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0 && Check.IsChecked == true)
        {
            taskService.Delete(((Task)e.AddedItems[0]!).Name);
            UpdateList(false);
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