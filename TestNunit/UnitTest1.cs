using NUnit.Framework;
using System;
using System.Collections.Generic;
using Todo_Manager;

namespace TaskPadApp.Tests
{
    [TestFixture]
    public class TodoManagerTests
    {
        private TodoManager _todoManager;

        [SetUp]
        public void Setup()
        {
            _todoManager = new TodoManager();
        }

        [Test]
        public void TestAddTask()
        {

            int initialCount = _todoManager.GetTasks().Count;


            _todoManager.AddTask("New Task", "This is a new task.", DateTime.Now, false);


            int finalCount = _todoManager.GetTasks().Count;
            Assert.AreEqual(initialCount + 1, finalCount);
        }

        [Test]
        public void TestUpdateTask()
        {

            int taskId = 1;
            _todoManager.AddTask("Task to Update", "This task will be updated.", DateTime.Now, false);


            _todoManager.UpdateTask(taskId, "Updated Task", "This task has been updated.", DateTime.Now, true);


            TaskItem updatedTask = _todoManager.GetTaskByID(taskId);
            Assert.IsNotNull(updatedTask);
            Assert.AreEqual("Updated Task", updatedTask.Title);
            Assert.AreEqual("This task has been updated.", updatedTask.Description);
            Assert.IsTrue(updatedTask.Completed);
        }



        [Test]
        public void TestViewTasksByCompletionStatus()
        {

            _todoManager.AddTask("Task 1", "This is task 1.", DateTime.Now, false);
            _todoManager.AddTask("Task 2", "This is task 2.", DateTime.Now, true);
            _todoManager.AddTask("Task 3", "This is task 3.", DateTime.Now, true);


            List<TaskItem> completedTasks = _todoManager.ViewTasksByCompletion(true);


            Assert.AreEqual(2, completedTasks.Count);
            foreach (var task in completedTasks)
            {
                Assert.IsTrue(task.Completed);
            }
        }
    }
}