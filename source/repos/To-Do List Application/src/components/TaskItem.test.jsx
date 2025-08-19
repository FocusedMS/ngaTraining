import { render, screen, fireEvent } from '@testing-library/react';
import TaskItem from './TaskItem';

describe('TaskItem', () => {
  const sampleTask = { id: '1', title: 'Read book', completed: false };

  test('toggles completion when checkbox is clicked', () => {
    const onToggle = jest.fn();
    render(
      <TaskItem task={sampleTask} onToggle={onToggle} onRemove={() => {}} />
    );

    const checkbox = screen.getByRole('checkbox');
    expect(checkbox).not.toBeChecked();
    fireEvent.click(checkbox);
    expect(onToggle).toHaveBeenCalledWith('1');
  });

  test('removes task when remove button is clicked', () => {
    const onRemove = jest.fn();
    render(
      <TaskItem task={sampleTask} onToggle={() => {}} onRemove={onRemove} />
    );

    const removeButton = screen.getByRole('button', { name: /remove/i });
    fireEvent.click(removeButton);
    expect(onRemove).toHaveBeenCalledWith('1');
  });
}); 