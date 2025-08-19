import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { addTask } from '../redux/actions';

export default function TaskInput() {
  const [text, setText] = useState('');
  const dispatch = useDispatch();

  const handleAdd = (e) => {
    e.preventDefault();
    const value = text.trim();
    if (!value) return;
    dispatch(addTask(value));
    setText('');
  };

  return (
    <form onSubmit={handleAdd} className="task-input">
      <input
        type="text"
        placeholder="Add a new task..."
        value={text}
        onChange={(e) => setText(e.target.value)}
        aria-label="Task text"
      />
      <button type="submit">Add</button>
    </form>
  );
} 