import dispatcher from './dispatcher';
import { FluxActionTypes } from './constants';

let nextFluxTaskId = 1;

export const FluxActions = {
	addTask(text) {
		dispatcher.dispatch({
			type: FluxActionTypes.ADD_TASK,
			payload: { id: nextFluxTaskId++, text }
		});
	},
	removeTask(id) {
		dispatcher.dispatch({
			type: FluxActionTypes.REMOVE_TASK,
			payload: { id }
		});
	},
	toggleTask(id) {
		dispatcher.dispatch({
			type: FluxActionTypes.TOGGLE_TASK,
			payload: { id }
		});
	}
}; 