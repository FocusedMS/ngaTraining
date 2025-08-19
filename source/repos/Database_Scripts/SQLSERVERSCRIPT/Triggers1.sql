create table student
(
	sno int constraint pk_student_sno primary key,
	nam varchar(30),
	sub1 int,
	sub2 int,
	sub3 int,
	tot int,
	avg float,
	InsertedOn DATETIME,
	InsertedBy varchar(100)
);
GO

CREATE TRIGGER trgStudentInsert
ON student
AFTER INSERT
AS
BEGIN
    UPDATE S
    SET
        tot = I.sub1 + I.sub2 + I.sub3,
        avg = (I.sub1 + I.sub2 + I.sub3) / 3.0,
        InsertedOn = GETDATE(),
        InsertedBy = SYSTEM_USER
    FROM student S
    INNER JOIN inserted I ON S.sno = I.sno;
END;
GO

insert into student(sno, nam, sub1, sub2, sub3)
values (1, 'Kabir', 80, 90, 85);

select * from student;

