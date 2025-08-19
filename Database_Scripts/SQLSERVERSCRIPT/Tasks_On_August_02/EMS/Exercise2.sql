-- Exercise 2

-- 1. Total Present Basic > 30000 grouped by department
SELECT DepartmentCode, SUM(PresentBasic) AS TotalBasic
FROM dbo.tblEmployees
GROUP BY DepartmentCode
HAVING SUM(PresentBasic) > 30000
ORDER BY DepartmentCode;


-- 2. Max, Min, Avg age by serviceType, serviceStatus, Centre
SELECT CentreCode, ServiceType, ServiceStatus,
       MAX(DATEDIFF(YEAR, DOB, GETDATE())) AS MaxAgeInYears,
       MIN(DATEDIFF(YEAR, DOB, GETDATE())) AS MinAgeInYears,
       AVG(DATEDIFF(YEAR, DOB, GETDATE() * 1.0)) AS AvgAgeInYears
FROM dbo.tblEmployees
GROUP BY CentreCode, ServiceType, ServiceStatus
ORDER BY CentreCode, ServiceType, ServiceStatus;

-- 3. Max, Min, Avg service by serviceType, serviceStatus, Centre
SELECT CentreCode, ServiceType, ServiceStatus,
       MAX(DATEDIFF(YEAR, DOJ, GETDATE())) AS MaxServiceInYears,
       MIN(DATEDIFF(YEAR, DOJ, GETDATE())) AS MinServiceInYears,
       AVG(DATEDIFF(YEAR, DOJ, GETDATE() * 1.0)) AS AvgServiceInYears
FROM dbo.tblEmployees
GROUP BY CentreCode, ServiceType, ServiceStatus
ORDER BY CentreCode, ServiceType, ServiceStatus;

-- 4. Departments where total salary > 3 * avg salary
SELECT department
FROM Employee
GROUP BY department
HAVING SUM(Salary) > 3 * AVG(Salary);

-- 5. Departments where total salary > 2 * avg salary AND max basic >= 3 * min basic
SELECT DepartmentCode, SUM(PresentBasic) AS TotalBasic
FROM EMP_ShriramDB.dbo.tblEmployees
GROUP BY DepartmentCode
HAVING SUM(PresentBasic) > 2 * (SELECT AVG(PresentBasic) FROM EMP_ShriramDB.dbo.tblEmployees)
   AND MAX(PresentBasic) >= 3 * MIN(PresentBasic);

-- 6. Centers where max name length is twice min length
SELECT Centre
FROM Employee
GROUP BY Centre
HAVING MAX(LEN(RTRIM(LTRIM(Name)))) >= 2 * MIN(LEN(RTRIM(LTRIM(Name))));

-- 7. Service stats in milliseconds
SELECT Centre, ServiceType, ServiceStatus,
       MAX(DATEDIFF(MILLISECOND, DOJ, GETDATE())) AS MaxService_ms,
       MIN(DATEDIFF(MILLISECOND, DOJ, GETDATE())) AS MinService_ms,
       AVG(DATEDIFF(MILLISECOND, DOJ, GETDATE())) AS AvgService_ms
FROM Employee
GROUP BY Centre, ServiceType, ServiceStatus;

-- 8. Names with leading/trailing spaces
SELECT * FROM Employee
WHERE Name LIKE ' %' OR Name LIKE '% ';

-- 9. Names with multiple spaces between words
SELECT * FROM Employee
WHERE Name LIKE '%  %';

-- 10. Cleaned names with single space, no trailing/leading, no multiple spaces or dots
SELECT empid, 
       RTRIM(LTRIM(REPLACE(REPLACE(Name, '.', ''), '  ', ' '))) AS CleanedName
FROM Employee;

-- 11. Max number of words in names
SELECT MAX(LEN(CleanedName) - LEN(REPLACE(CleanedName, ' ', '')) + 1) AS MaxWords
FROM (
    SELECT RTRIM(LTRIM(REPLACE(REPLACE(Name, '.', ''), '  ', ' '))) AS CleanedName
    FROM Employee
) AS cleaned;

-- 12. Names starting and ending with same char
SELECT * FROM Employee
WHERE LEFT(Name, 1) = RIGHT(RTRIM(Name), 1);

-- 13. First and second name start with same char
SELECT * FROM Employee
WHERE LEFT(Name, 1) = SUBSTRING(Name, CHARINDEX(' ', Name) + 1, 1);

-- 14. First letters of all words same
SELECT * FROM Employee
WHERE NOT EXISTS (
  SELECT 1
  FROM STRING_SPLIT(REPLACE(REPLACE(Name, '.', ''), '  ', ' '), ' ')
  WHERE LEFT(value, 1) != LEFT(Name, 1)
);

-- 15. Any word (excluding initials) starts and ends with same char
SELECT * FROM Employee
WHERE EXISTS (
  SELECT 1
  FROM STRING_SPLIT(REPLACE(REPLACE(Name, '.', ''), '  ', ' '), ' ') AS parts
  WHERE LEN(parts.value) > 1 AND LEFT(parts.value,1) = RIGHT(parts.value,1)
);

-- 16. Present Basic rounded to 100
-- a) ROUND
SELECT * FROM Employee
WHERE ROUND(present_basic, -2) = present_basic;
-- b) FLOOR
SELECT * FROM Employee
WHERE FLOOR(present_basic/100.0)*100 = present_basic;
-- c) MOD
SELECT * FROM Employee
WHERE present_basic % 100 = 0;
-- d) CEILING
SELECT * FROM Employee
WHERE CEILING(present_basic/100.0)*100 = present_basic;

-- 17. Departments where all employees have basic rounded to 100
SELECT department
FROM Employee
GROUP BY department
HAVING COUNT(*) = SUM(CASE WHEN present_basic % 100 = 0 THEN 1 ELSE 0 END);

-- 18. Departments where no employee has basic rounded to 100
SELECT department
FROM Employee
GROUP BY department
HAVING SUM(CASE WHEN present_basic % 100 = 0 THEN 1 ELSE 0 END) = 0;

-- 19. Eligibility for bonus and age on bonus date
SELECT empid, Name,
       DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ)))) AS BonusDate,
       DATEDIFF(YEAR, DateOfBirth, DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ))))) AS AgeOnBonus_Years,
       DATENAME(WEEKDAY, BonusDate) AS Weekday,
       DATEPART(WEEK, BonusDate) AS WeekOfYear,
       DATEPART(DAYOFYEAR, BonusDate) AS DayOfYear,
       (DATEPART(DAY, BonusDate) - 1)/7 + 1 AS WeekOfMonth
FROM Employee;

-- 20. Bonus eligibility by service type & rules
SELECT empid, Name
FROM Employee
WHERE (
    (ServiceType = 1 AND EmpType = 1 AND service_years >= 10 AND service_left >= 15 AND age <= 60)
 OR (ServiceType = 1 AND EmpType = 2 AND service_years >= 12 AND service_left >= 14 AND age <= 55)
 OR (ServiceType = 1 AND EmpType = 3 AND service_years >= 12 AND service_left >= 12 AND age <= 55)
 OR (ServiceType IN (2,3,4) AND service_years >= 15 AND service_left >= 20 AND age <= 65)
);

-- 21. Display current date in various formats
SELECT 
  CONVERT(VARCHAR, GETDATE(), 0) AS Format0,
  CONVERT(VARCHAR, GETDATE(), 1) AS Format1,
  CONVERT(VARCHAR, GETDATE(), 101) AS Format101,
  CONVERT(VARCHAR, GETDATE(), 113) AS Format113,
  CONVERT(VARCHAR, GETDATE(), 120) AS Format120;

-- 22. Employees with net payment < expected basic
SELECT e.empid, e.Name, p.NetPayment, e.present_basic
FROM Employee e
JOIN TablpayemployeeParamDetails p ON e.empid = p.empid
WHERE p.ParamCode = 'NETPAY' AND p.NetPayment < e.present_basic;