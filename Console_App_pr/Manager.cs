using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Todo_Manager;

public class TodoManager
{
    private List<TaskItem> tasks = new List<TaskItem>();


    public void AddTask(string title, string description, DateTime dueDate, bool isComplete = false)
    {
        try
        {
            int newTaskID = tasks.Count + 1;
            if (dueDate < DateTime.Now)
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            tasks.Add(new TaskItem
            {
                TaskID = newTaskID,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Completed = isComplete
            });

            Console.WriteLine("Task added successfully.");
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error adding task: " + ex.Message);
        }
    }

    public void ViewAllTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        Console.WriteLine("All Tasks:");
        foreach (TaskItem task in tasks)
        {
            DisplayTaskInfo(task);
        }
    }

    public void ViewTaskByID(int taskID)
    {
        TaskItem task = tasks.FirstOrDefault(t => t.TaskID == taskID);
        if (task != null)
        {
            DisplayTaskInfo(task);
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    public void MarkTaskAsCompleted(int taskID)
    {
        TaskItem task = tasks.FirstOrDefault(t => t.TaskID == taskID);
        if (task != null)
        {
            task.Completed = true;
            Console.WriteLine("Task marked as completed.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    public void UpdateTask(int taskID, string title, string description, DateTime dueDate, bool isComplete)
    {
        TaskItem task = tasks.FirstOrDefault(t => t.TaskID == taskID);
        if (task != null)
        {
            task.Title = title;
            task.Description = description;
            task.DueDate = dueDate;
            task.Completed = isComplete;
            Console.WriteLine("Task updated successfully.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    public void DeleteTask(int taskID)
    {
        TaskItem task = tasks.FirstOrDefault(t => t.TaskID == taskID);
        if (task != null)
        {
            tasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    public void SaveTasksToJson(string fileName)
    {
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        System.IO.File.WriteAllText(fileName, json);
        Console.WriteLine("Tasks saved to JSON file.");
    }

    public void LoadTasksFromFile(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            Console.WriteLine("Loaded tasks from file:");
            Console.WriteLine(json);

            // Deserialize the JSON and update the task list
            tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading tasks from file: " + ex.Message);
        }
    }

    public void ViewTasksByDueDate()
    {
        var sortedTasks = tasks.OrderBy(t => t.DueDate);
        DisplayTasks(sortedTasks);
    }

    // View tasks filtered by completion status
    public void ViewTasksByCompletionStatus(bool isComplete)
    {
        var filteredTasks = tasks.Where(task => task.Completed == isComplete).ToList();

        if (filteredTasks.Count == 0)
        {
            Console.WriteLine("No tasks found with the specified completion status.");
        }
        else
        {
            Console.WriteLine($"Tasks with Completion Status: {(isComplete ? "Completed" : "Not Completed")}");
            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"Task ID: {task.TaskID}");
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate}");
                
                Console.WriteLine($"Is Complete: {task.Completed}");
                Console.WriteLine();
            }
        }
    }

    private void DisplayTasks(IEnumerable<TaskItem> taskList)
    {
        foreach (var task in taskList)
        {
            Console.WriteLine($"Task ID: {task.TaskID}");
            Console.WriteLine($"Title: {task.Title}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Due Date: {task.DueDate}");
            Console.WriteLine($"Completion Status: {(task.Completed ? "Complete" : "Incomplete")}");
            Console.WriteLine("-----------------------------------");
        }
    }
    public List<TaskItem> GetTasks()
    {
        return tasks.ToList();
    }
    public TaskItem GetTaskByID(int taskID)
    {
        return tasks.FirstOrDefault(t => t.TaskID == taskID);
    }
    public List<TaskItem> ViewTasksByCompletion(bool isComplete)
    {
        return tasks.Where(t => t.Completed == isComplete).ToList();
    }


    private void DisplayTaskInfo(TaskItem task)
    {
        Console.WriteLine($"ID: {task.TaskID}");
        Console.WriteLine($"Title: {task.Title}");
        Console.WriteLine($"Description: {task.Description}");
        Console.WriteLine($"Completed: {task.Completed}");
        Console.WriteLine($"Due Date: {task.DueDate}");
        Console.WriteLine();
    }
}