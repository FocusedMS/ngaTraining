import { render, screen, fireEvent } from '@testing-library/react';
import TaskList from './TaskList';

const tasks = [
  { id: '1', title: 'Task A', completed: false },
  { id: '2', title: 'Task B', completed: true },
];

describe('TaskList', () => {
  test('renders all tasks', () => {
    render(
      <TaskList tasks={tasks} onToggle={() => {}} onRemove={() => {}} />
    );
    expect(screen.getByText('Task A')).toBeInTheDocument();
    expect(screen.getByText('Task B')).toBeInTheDocument();
  });

  test('fires events for toggle and remove through items', () => {
    const onToggle = jest.fn();
    const onRemove = jest.fn();

    render(
      <TaskList tasks={tasks} onToggle={onToggle} onRemove={onRemove} />
    );

    const checkboxes = screen.getAllByRole('checkbox');
    fireEvent.click(checkboxes[0]);
    expect(onToggle).toHaveBeenCalledWith('1');

    const removeButtons = screen.getAllByRole('button', { name: /remove/i });
    fireEvent.click(removeButtons[1]);
    expect(onRemove).toHaveBeenCalledWith('2');
  });
}); 