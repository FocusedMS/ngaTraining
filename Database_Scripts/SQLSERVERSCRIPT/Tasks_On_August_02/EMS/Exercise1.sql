-- 1. List of Employees who have a one part name
SELECT * FROM tblEmployees WHERE Name NOT LIKE '% %';

-- 2. List of Employees who have a three part name
SELECT * FROM tblEmployees 
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 2;

select emp.Name
from dbo.tblEmployees emp
where emp.Name like '[a-z]%[ ][a-z]%[ ][a-z]%' and emp.Name not like '[a-z]%[ ][a-z]%[ ][a-z]%[ ][a-z]%';

-- 3. Employees with First, Middle, or Last name as 'Ram' and only 'Ram'
SELECT * FROM tblEmployees
WHERE (
    Name LIKE 'Ram'
    OR Name LIKE 'Ram %'
    OR Name LIKE '% Ram'
    OR Name LIKE '% Ram %'
) 
AND LEN(REPLACE(Name, 'Ram', '')) = LEN(Name) - 3;

-- 4. Bitwise Operations
SELECT emp.EmployeeNumber, emp.Name, emp.CentreCode
FROM dbo.tblEmployees emp
WHERE emp.CentreCode IN (65, 11);

-- b
SELECT COUNT(*)
FROM dbo.tblEmployees emp
WHERE (emp.CategoryCode = 65 OR emp.CentreCode = 11)
  AND NOT (emp.CategoryCode = 65 AND emp.CentreCode = 11);


--c
SELECT emp.EmployeeNumber, emp.Name, emp.CentreCode, emp.CategoryCode
FROM tblEmployees emp
WHERE emp.CategoryCode = 12 AND emp.CentreCode = 4;

--d
SELECT emp.EmployeeNumber, emp.Name, emp.CentreCode, emp.CategoryCode
FROM dbo.tblEmployees emp
WHERE (emp.CategoryCode, emp.CentreCode) IN ((12, 4), (13, 1));

--e
SELECT emp.EmployeeNumber, emp.Name
FROM dbo.tblEmployees emp
WHERE emp.EmployeeNumber IN (127, 64);

--f
SELECT emp.EmployeeNumber, emp.Name
FROM dbo.tblEmployees emp
WHERE (emp.CategoryCode = 127 OR emp.CentreCode = 64)
  AND NOT (emp.CategoryCode = 127 AND emp.CentreCode = 64);

--g
SELECT emp.EmployeeNumber, emp.Name
FROM dbo.tblEmployees emp
WHERE (emp.CategoryCode = 127 OR emp.CentreCode = 128)
  AND NOT (emp.CategoryCode = 127 AND emp.CentreCode = 128);


--h
SELECT emp.EmployeeNumber, emp.Name
FROM dbo.tblEmployees emp
WHERE emp.EmployeeNumber = 127 AND emp.AreaCode = 64;


--i
SELECT emp.EmployeeNumber, emp.Name
FROM dbo.tblEmployees emp
WHERE emp.EmployeeNumber = 127 AND emp.AreaCode = 128;


-- 5. All data from tblCentreMaster
SELECT * FROM dbo.tblCentreMaster;

-- 6. Distinct employee types
SELECT DISTINCT EmployeeType FROM tblEmployees;

-- 7. Employees with PresentBasic conditions
-- a. Greater than 3000
SELECT Name, FatherName, DOB FROM tblEmployees WHERE PresentBasic > 3000;
-- b. Less than 3000
SELECT Name, FatherName, DOB FROM tblEmployees WHERE PresentBasic < 3000;
-- c. Between 3000 and 5000
SELECT Name, FatherName, DOB FROM tblEmployees WHERE PresentBasic BETWEEN 3000 AND 5000;

-- 8. Employees with name-based filters
-- a. Ends with 'KHAN'
SELECT * FROM tblEmployees WHERE Name LIKE '%KHAN';
-- b. Starts with 'CHANDRA'
SELECT * FROM tblEmployees WHERE Name LIKE 'CHANDRA%';
-- c. Is 'RAMESH' and initials in range A-T
SELECT * FROM tblEmployees 
WHERE Name LIKE '_%.RAMESH' 
AND UPPER(LEFT(Name, 1)) BETWEEN 'A' AND 'T';
