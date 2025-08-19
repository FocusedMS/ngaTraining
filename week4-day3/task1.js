
// Function to fetch data from the API
async function fetchEmployeeData() {
    try {
        console.log('Fetching employee data...');
        
        // Make a GET request to the API endpoint
        const response = await fetch('https://dummy.restapiexample.com/api/v1/employees');
        
        // Check if the request was successful
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        // Parse the JSON response
        const data = await response.json();
        
        console.log('✅ Data retrieved successfully!');
        console.log('📊 API Response:', data);
        
        // Display specific information about employees
        if (data.data && Array.isArray(data.data)) {
            console.log(`\n👥 Total employees: ${data.data.length}`);
            
            // Display first few employees as examples
            console.log('\n📋 First 3 employees:');
            data.data.slice(0, 3).forEach((employee, index) => {
                console.log(`\nEmployee ${index + 1}:`);
                console.log(`  ID: ${employee.id}`);
                console.log(`  Name: ${employee.employee_name}`);
                console.log(`  Salary: $${employee.employee_salary}`);
                console.log(`  Age: ${employee.employee_age}`);
            });
        }
        
    } catch (error) {
        console.error('❌ Error fetching data:', error.message);
    }
}

// Call the function to execute the fetch
console.log('🚀 Starting Week 4 Day 3 - Task 1');
fetchEmployeeData();
