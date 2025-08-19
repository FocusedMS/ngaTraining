import { createStore } from 'redux';
import rootReducer from './reducer';

const enhancer = typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION__
  ? window.__REDUX_DEVTOOLS_EXTENSION__()
  : undefined;

const store = createStore(rootReducer, enhancer);

export default store; 