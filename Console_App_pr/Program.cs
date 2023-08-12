using System;
using Todo_Manager;

class Program
{
    static void Main(string[] args)
    {
        TodoManager todoManager = new TodoManager();

        while (true)
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("a. Add a task");
            Console.WriteLine("b. View all tasks");
            Console.WriteLine("c. View a specific task");
            Console.WriteLine("d. Mark a task as completed");
            Console.WriteLine("e. Update a task");
            Console.WriteLine("f. Delete a task");
            Console.WriteLine("g. Save tasks to a file");
            Console.WriteLine("h. Load tasks from a file");
            Console.WriteLine("s. View the task for Completion status");
            Console.WriteLine("t. View task by date priority");
            Console.WriteLine("i. Exit");
            
            Console.Write("Select an option: ");
            char choice = Char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            switch (choice)
            {
                case 'a':
                    
                    Console.Write("Enter task title: ");
                    string title = Console.ReadLine();

                    while (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Title cannot be null or empty. Please try again.");
                        Console.WriteLine(("Enter Title of TaskID: "));
                        title = Console.ReadLine();
                    }


                    Console.Write("Enter task Description: ");                    
                    
                    string description = Console.ReadLine();

                    while (string.IsNullOrEmpty(description))
                    {
                        Console.WriteLine("Description cannot be null or empty. Please try again.");
                        Console.WriteLine(("Enter Description of TaskID: "));
                        description = Console.ReadLine();
                    }

                    Console.Write("Enter due date (yyyy-MM-dd HH:mm:ss): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                    {                        
                        
                        todoManager.AddTask(title, description, dueDate, false);
                        
                        todoManager.SaveTasksToJson("tasks.json");
                        

                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Task not added.");
                    }
                    break;


                case 'b':
                    todoManager.ViewAllTasks();
                    break;

                case 'c':
                    Console.Write("Enter task ID: ");
                    if (int.TryParse(Console.ReadLine(), out int taskID))
                    {
                        todoManager.ViewTaskByID(taskID);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                case 'd':
                    Console.Write("Enter task ID to mark as completed: ");
                    if (int.TryParse(Console.ReadLine(), out int completeTaskID))
                    {
                        todoManager.MarkTaskAsCompleted(completeTaskID);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                case 'e':
                    Console.Write("Enter task ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateTaskID))
                    {
                        Console.Write("Enter updated title: ");
                        string updatedTitle = Console.ReadLine();
                        Console.Write("Enter updated description: ");
                        string updatedDescription = Console.ReadLine();
                        Console.Write("Enter updated due date (yyyy-MM-dd HH:mm:ss): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime updatedDueDate))
                        {
                            Console.Write("Is the task completed? (true/false): ");
                            if (bool.TryParse(Console.ReadLine(), out bool updatedIsComplete))
                            {
                                todoManager.UpdateTask(updateTaskID, updatedTitle, updatedDescription, updatedDueDate, updatedIsComplete);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Task not updated.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Task not updated.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                case 'f':
                    Console.Write("Enter task ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteTaskID))
                    {
                        todoManager.DeleteTask(deleteTaskID);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                case 'g':
                    todoManager.SaveTasksToJson("tasks.json");
                    break;

                case 'h':
                    todoManager.LoadTasksFromFile("tasks.json");
                    break;

                case 's':
                    todoManager.ViewTasksByCompletionStatus(false);
                    break;

                case 't':
                    todoManager.ViewTasksByDueDate();
                    break;

                case 'i':
                    Environment.Exit(0);
                    break;

                case 'j':
                    Console.WriteLine("sort Task by Due Date:");
                    todoManager.ViewTasksByDueDate();
                    break;

                case 'k':
                    Console.Write("Enter completion status (true/false): ");
                    if (bool.TryParse(Console.ReadLine(), out bool filterStatus))
                    {
                        todoManager.ViewTasksByCompletionStatus(filterStatus);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}