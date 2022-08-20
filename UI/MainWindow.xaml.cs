using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace UI;

public partial class MainWindow : Window
{
    TaskService taskService;

    public MainWindow()
    {
        InitializeComponent();

        taskService = new TaskService();

        CurrentTasks.ItemsSource = Process.GetProcesses();
        WatchTasks.ItemsSource = taskService.GetTasks().ToArray();
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

    private void UpdateList()
    {
        CurrentTasks.ItemsSource = Process.GetProcesses()
            .Where(x => x.ProcessName.ToLower().Contains(Input.Text.ToLower()));

        WatchTasks.ItemsSource = taskService.GetTasks()
            .Where(x => x.Name.ToLower().Contains(Input.Text.ToLower())).ToArray();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (Input.Text != "")
        {
            UpdateList();
        }

        else
        {
            CurrentTasks.ItemsSource = Process.GetProcesses();
            WatchTasks.ItemsSource = taskService.GetTasks().ToArray();
        }
    }

    private void Input_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        UpdateList();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        taskService.Add(NameInput.Text);
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        taskService.Delete(NameInput.Text);
    }
}