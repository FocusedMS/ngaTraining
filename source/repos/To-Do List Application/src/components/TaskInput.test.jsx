import { render, screen, fireEvent } from '@testing-library/react';
import TaskInput from './TaskInput';

describe('TaskInput', () => {
  test('updates input value as the user types', () => {
    render(<TaskInput onAddTask={() => {}} />);
    const input = screen.getByPlaceholderText(/add a new task/i);
    fireEvent.change(input, { target: { value: 'Buy milk' } });
    expect(input).toHaveValue('Buy milk');
  });

  test('calls onAddTask and clears input on submit', () => {
    const handleAdd = jest.fn();
    render(<TaskInput onAddTask={handleAdd} />);

    const input = screen.getByPlaceholderText(/add a new task/i);
    const button = screen.getByRole('button', { name: /add task/i });

    fireEvent.change(input, { target: { value: 'Write tests' } });
    fireEvent.click(button);

    expect(handleAdd).toHaveBeenCalledTimes(1);
    expect(handleAdd).toHaveBeenCalledWith('Write tests');
    expect(input).toHaveValue('');
  });
}); 