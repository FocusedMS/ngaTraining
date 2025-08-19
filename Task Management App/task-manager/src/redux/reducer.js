import { ADD_TASK, REMOVE_TASK, TOGGLE_TASK } from './actionTypes';

const initialState = {
  tasks: []
};

export default function rootReducer(state = initialState, action) {
  switch (action.type) {
    case ADD_TASK: {
      const { id, text } = action.payload;
      if (!text || !text.trim()) return state;
      const newTask = { id, text: text.trim(), completed: false };
      return { ...state, tasks: [...state.tasks, newTask] };
    }
    case REMOVE_TASK: {
      const { id } = action.payload;
      return { ...state, tasks: state.tasks.filter(t => t.id !== id) };
    }
    case TOGGLE_TASK: {
      const { id } = action.payload;
      return {
        ...state,
        tasks: state.tasks.map(t => t.id === id ? { ...t, completed: !t.completed } : t)
      };
    }
    default:
      return state;
  }
} 