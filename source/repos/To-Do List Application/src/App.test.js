import { render, screen, fireEvent } from '@testing-library/react';
import App from './App';

describe('To-Do App integration', () => {
  test('adds a task and displays it in the list', () => {
    render(<App />);
    const input = screen.getByPlaceholderText(/add a new task/i);
    const addButton = screen.getByRole('button', { name: /add task/i });

    fireEvent.change(input, { target: { value: 'New Task' } });
    fireEvent.click(addButton);

    expect(screen.getByText('New Task')).toBeInTheDocument();
  });

  test('toggles and removes a task', () => {
    render(<App />);
    const input = screen.getByPlaceholderText(/add a new task/i);
    const addButton = screen.getByRole('button', { name: /add task/i });

    fireEvent.change(input, { target: { value: 'Task to Toggle' } });
    fireEvent.click(addButton);

    const checkbox = screen.getByRole('checkbox');
    expect(checkbox).not.toBeChecked();
    fireEvent.click(checkbox);
    expect(checkbox).toBeChecked();

    const removeButton = screen.getByRole('button', { name: /remove/i });
    fireEvent.click(removeButton);

    expect(screen.queryByText('Task to Toggle')).not.toBeInTheDocument();
  });
});
