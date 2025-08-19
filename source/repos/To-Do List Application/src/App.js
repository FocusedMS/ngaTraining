import './App.css';
import React, { useState, useCallback } from 'react';
import TaskInput from './components/TaskInput';
import TaskList from './components/TaskList';

function App() {
  const [tasks, setTasks] = useState([]);

  const addTask = useCallback((title) => {
    setTasks((prev) => [
      ...prev,
      { id: Date.now().toString(), title, completed: false }
    ]);
  }, []);

  const toggleTask = useCallback((id) => {
    setTasks((prev) => prev.map(t => t.id === id ? { ...t, completed: !t.completed } : t));
  }, []);

  const removeTask = useCallback((id) => {
    setTasks((prev) => prev.filter(t => t.id !== id));
  }, []);

  return (
    <div className="App">
      <h1>To-Do List</h1>
      <TaskInput onAddTask={addTask} />
      <TaskList tasks={tasks} onToggle={toggleTask} onRemove={removeTask} />
    </div>
  );
}

export default App;
