import React, { useState } from 'react';

function TaskInput({ onAddTask, placeholder = 'Add a new task', buttonLabel = 'Add Task', className = '' }) {
  const [value, setValue] = useState('');

  const handleSubmit = (event) => {
    event.preventDefault();
    const trimmed = value.trim();
    if (!trimmed) return;
    if (onAddTask) onAddTask(trimmed);
    setValue('');
  };

  return (
    <form onSubmit={handleSubmit} className={className}>
      <input
        aria-label="task-input"
        placeholder={placeholder}
        value={value}
        onChange={(e) => setValue(e.target.value)}
      />
      <button type="submit">{buttonLabel}</button>
    </form>
  );
}

export default TaskInput; 