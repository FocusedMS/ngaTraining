import dispatcher from './dispatcher';
import { FluxActionTypes } from './constants';

class MiniEmitter {
	constructor() {
		this.listeners = new Set();
	}
	on(fn) {
		this.listeners.add(fn);
	}
	off(fn) {
		this.listeners.delete(fn);
	}
	emit() {
		this.listeners.forEach(fn => fn());
	}
}

class TaskStore extends MiniEmitter {
	constructor() {
		super();
		this.tasks = [];
	}

	getTasks() {
		return this.tasks;
	}

	handleActions(action) {
		switch (action.type) {
			case FluxActionTypes.ADD_TASK: {
				const { id, text } = action.payload;
				if (!text || !text.trim()) return;
				this.tasks = [...this.tasks, { id, text: text.trim(), completed: false }];
				this.emit();
				break;
			}
			case FluxActionTypes.REMOVE_TASK: {
				const { id } = action.payload;
				this.tasks = this.tasks.filter(t => t.id !== id);
				this.emit();
				break;
			}
			case FluxActionTypes.TOGGLE_TASK: {
				const { id } = action.payload;
				this.tasks = this.tasks.map(t => t.id === id ? { ...t, completed: !t.completed } : t);
				this.emit();
				break;
			}
			default:
				break;
		}
	}
}

const taskStore = new TaskStore();
dispatcher.register(taskStore.handleActions.bind(taskStore));

export default taskStore; 