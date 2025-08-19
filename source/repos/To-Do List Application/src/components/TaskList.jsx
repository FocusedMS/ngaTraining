import React from 'react';
import TaskItem from './TaskItem';

function TaskList({ tasks, onToggle, onRemove, className = '' }) {
  return (
    <ul className={className}>
      {tasks.map((task) => (
        <TaskItem key={task.id} task={task} onToggle={onToggle} onRemove={onRemove} />
      ))}
    </ul>
  );
}

export default TaskList; 