const EMPLOYEES_URL = 'https://dummy.restapiexample.com/api/v1/employees';

async function fetchEmployeeData() {
  console.log('Fetching employees…');

  let response;
  try {
    response = await fetch(EMPLOYEES_URL);
  } catch (err) {
    console.error('Network error:', err);
    return;
  }

  if (!response.ok) {
    console.error(`Request failed: ${response.status} ${response.statusText}`);
    return;
  }

  const payload = await response.json();
  console.log('API payload:', payload);

  const employees = Array.isArray(payload?.data) ? payload.data : [];
  console.log(`Total employees: ${employees.length}`);

  const n = Math.min(3, employees.length);
  for (let i = 0; i < n; i++) {
    const e = employees[i];
    console.log(`\nEmployee ${i + 1}`);
    console.log(`  ID: ${e.id}`);
    console.log(`  Name: ${e.employee_name}`);
    console.log(`  Salary: $${e.employee_salary}`);
    console.log(`  Age: ${e.employee_age}`);
  }
}

console.log('Starting Task 1');
fetchEmployeeData();
