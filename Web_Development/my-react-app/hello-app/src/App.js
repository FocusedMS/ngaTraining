import React from 'react';
import './App.css';
import { 
  First, 
  Second, 
  Third, 
  Four, 
  Five, 
  Six, 
  Seven, 
  Eight, 
  ButtonEx 
} from './components';

function App() {
  return (
    <div className="App">
      <h1>React Components Demo</h1>
      
      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>First Component</h2>
        <First />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Second Component</h2>
        <Second />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Third Component (with props)</h2>
        <Third firstName="John" lastName="Doe" company="Tech Corp" />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Four Component (useState with buttons)</h2>
        <Four />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Five Component (Counter)</h2>
        <Five />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Six Component (Input form)</h2>
        <Six />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Seven Component (Full name form)</h2>
        <Seven />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>Eight Component (Calculator)</h2>
        <Eight />
      </div>

      <div style={{ margin: '20px', padding: '20px', border: '1px solid #ccc' }}>
        <h2>ButtonEx Component (Alert buttons)</h2>
        <ButtonEx />
      </div>
    </div>
  );
}

export default App;
