import React from 'react';

function TaskItem({ task, onToggle, onRemove, className = '' }) {
  const handleToggle = () => {
    if (onToggle) onToggle(task.id);
  };

  const handleRemove = () => {
    if (onRemove) onRemove(task.id);
  };

  return (
    <li className={className}>
      <label>
        <input type="checkbox" checked={Boolean(task.completed)} onChange={handleToggle} />
        <span style={{ textDecoration: task.completed ? 'line-through' : 'none' }}>{task.title}</span>
      </label>
      <button type="button" onClick={handleRemove}>Remove</button>
    </li>
  );
}

export default TaskItem; 