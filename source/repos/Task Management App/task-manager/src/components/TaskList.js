import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { removeTask, toggleTask } from '../redux/actions';

export default function TaskList() {
  const tasks = useSelector(state => state.tasks);
  const dispatch = useDispatch();

  if (!tasks.length) {
    return <p className="empty">No tasks yet. Add one above.</p>;
  }

  return (
    <ul className="task-list">
      {tasks.map(task => (
        <li key={task.id} className={task.completed ? 'completed' : ''}>
          <span onClick={() => dispatch(toggleTask(task.id))} role="button" tabIndex={0}>
            {task.text}
          </span>
          <button onClick={() => dispatch(removeTask(task.id))} aria-label={`Remove ${task.text}`}>
            Remove
          </button>
        </li>
      ))}
    </ul>
  );
} 