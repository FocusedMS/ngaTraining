import { ADD_TASK, REMOVE_TASK, TOGGLE_TASK } from './actionTypes';

let nextTaskId = 1;

export const addTask = (text) => ({
  type: ADD_TASK,
  payload: { id: nextTaskId++, text }
});

export const removeTask = (id) => ({
  type: REMOVE_TASK,
  payload: { id }
});

export const toggleTask = (id) => ({
  type: TOGGLE_TASK,
  payload: { id }
}); 