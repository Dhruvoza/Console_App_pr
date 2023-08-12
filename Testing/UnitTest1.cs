using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;



namespace TaskPadApp.Tests
{
    [TestClass]
    public class TaskPadTests
    {
        [TestMethod]
        public void TestTaskItemToString()
        {
            // Arrange
            var task = new TaskItem
            {
                TaskID = 1,
                Title = "Sample Task",
                Description = "This is a sample task.",
                DueDate = DateTime.Parse("2023-08-01"),
                IsComplete = false
            };

            // Act
            string result = task.ToString();

            // Assert
            string expected = "Task ID: 1\nTitle: Sample Task\nDescription: This is a sample task.\nDue Date: 8/1/2023 12:00:00 AM\nCompletion Status: Incomplete";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestTodoManagerAddTask()
        {
            // Arrange
            var todoManager = new TodoManager();
            int initialCount = todoManager.GetTasks().Count;

            // Act
            todoManager.AddTask("New Task", "This is a new task.", DateTime.Now, false);

            // Assert
            int finalCount = todoManager.GetTasks().Count;
            Assert.AreEqual(initialCount + 1, finalCount);
        }

        [TestMethod]
        public void TestTodoManagerUpdateTask()
        {
            // Arrange
            var todoManager = new TodoManager();
            int taskId = 1;
            todoManager.AddTask("Task to Update", "This task will be updated.", DateTime.Now, false);

            // Act
            todoManager.UpdateTask(taskId, "Updated Task", "This task has been updated.", DateTime.Now, true);

            // Assert
            TaskItem updatedTask = todoManager.GetTaskById(taskId);
            Assert.IsNotNull(updatedTask);
            Assert.AreEqual("Updated Task", updatedTask.Title);
            Assert.AreEqual("This task has been updated.", updatedTask.Description);
            Assert.IsTrue(updatedTask.IsComplete);
        }

        [TestMethod]
        public void TestTodoManagerViewTasksByDueDate()
        {
            // Arrange
            var todoManager = new TodoManager();
            todoManager.AddTask("Task 1", "This is task 1.", DateTime.Parse("2023-08-01"), false);
            todoManager.AddTask("Task 2", "This is task 2.", DateTime.Parse("2023-07-15"), false);
            todoManager.AddTask("Task 3", "This is task 3.", DateTime.Parse("2023-09-10"), true);

            // Act
            List<TaskItem> sortedTasks = todoManager.ViewTasksByDueDate();

            // Assert
            Assert.AreEqual(3, sortedTasks.Count);
            Assert.AreEqual("Task 2", sortedTasks[0].Title);
            Assert.AreEqual("Task 1", sortedTasks[1].Title);
            Assert.AreEqual("Task 3", sortedTasks[2].Title);
        }

        [TestMethod]
        public void TestTodoManagerViewTasksByCompletionStatus()
        {
            // Arrange
            var todoManager = new TodoManager();
            todoManager.AddTask("Task 1", "This is task 1.", DateTime.Now, false);
            todoManager.AddTask("Task 2", "This is task 2.", DateTime.Now, true);
            todoManager.AddTask("Task 3", "This is task 3.", DateTime.Now, true);

            // Act
            List<TaskItem> completedTasks = todoManager.ViewTasksByCompletionStatus(true);

            // Assert
            Assert.AreEqual(2, completedTasks.Count);
            foreach (var task in completedTasks)
            {
                Assert.IsTrue(task.IsComplete);
            }
        }
    }
}