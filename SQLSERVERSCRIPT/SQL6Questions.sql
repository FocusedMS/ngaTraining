-- 1) Display Last Occurrence of given char in a string

declare @word Varchar(100) = 'Keerthi';
declare @char char = 'e';


select len(@word) - CHARINDEX(@char, Reverse(@word))+1
Go

--2) Take FullName as 'Venkata Narayana' and split them into firstName and LastName

declare @fullName nvarchar(100) = 'Venkata narayana'

select
	left(@fullName, charIndex(' ', @fullName)-1) as firstname,
	Right(@fullname, len(@fullName) - charIndex(' ', @fullName)) as lastname
	Go

-- 3) In Word 'misissipi' count no.of 'i' 
