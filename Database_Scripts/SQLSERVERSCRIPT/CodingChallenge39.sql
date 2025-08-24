USE WiprojulyTasks;

-- See users & roles (grab the Ids if you want to be explicit)
SELECT Id, Email FROM AspNetUsers;
SELECT Id, Name  FROM AspNetRoles;

-- Put someone in the Admin role
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM AspNetUsers u
JOIN AspNetRoles r ON r.Name = 'Admin'
WHERE u.Email = 'admin@local.test';

-- Put someone in the User role
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM AspNetUsers u
 JOIN AspNetRoles r ON r.Name = 'User'
WHERE u.Email = 'user@local.test';

-- Give the regular user the edit claim so they can edit their own tasks
INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue)
SELECT u.Id, 'perm', 'CanEditTask'
FROM AspNetUsers u
WHERE u.Email = 'user@local.test';
