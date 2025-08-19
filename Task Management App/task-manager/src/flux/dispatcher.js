class Dispatcher {
	constructor() {
		this.callbacks = [];
	}

	register(callback) {
		this.callbacks.push(callback);
		return this.callbacks.length - 1;
	}

	dispatch(payload) {
		for (let i = 0; i < this.callbacks.length; i++) {
			this.callbacks[i](payload);
		}
	}
}

const dispatcher = new Dispatcher();
export default dispatcher; 