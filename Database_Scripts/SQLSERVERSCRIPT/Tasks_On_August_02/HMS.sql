create Database hms_db;
Go

USE hms_db;
GO

create table Doctor_Master (
	doctor_id varchar(15) primary key,
	doctor_name varchar(15) not null,
	dept varchar(15) not null
);
GO

create table room_master (
	room_no varchar(15) primary key,
	room_type varchar(15) not null,
	status varchar(15) not null
);
GO

create table patient_master (
	pid varchar(15) primary key,
	name varchar(15) not null,
	age int not null,
	weight int not null,
	gender varchar(10) not null,
	address varchar(50) not null,
	phoneno varchar(10) not null,
	disease varchar(50) not null,
	doctor_id varchar(15) not null,
	foreign key (doctor_id) references Doctor_Master(doctor_id)
);
GO

create table room_allocation (
	room_no varchar(15),
	pid varchar(15),
	admission_date date not null,
	release_date date,
	foreign key (room_no) references room_master(room_no),
	foreign key (pid) references patient_master(pid)
);
GO

insert into Doctor_Master values
('D0001', 'Ram', 'ENT'),
('D0002', 'Rajan', 'ENT'),
('D0003', 'Smita', 'Eye'),
('D0004', 'Smita', 'Surgery'),
('D0005', 'Sheela', 'Surgery'),
('D0006', 'Nethra', 'Surgery');
GO

insert into room_master values
('R0001', 'AC', 'occupied'),
('R0002', 'Suite', 'vacant'),
('R0003', 'NonAC', 'vacant'),
('R0004', 'NonAC', 'occupied'),
('R0005', 'AC', 'vacant'),
('R0006', 'AC', 'occupied');
GO

INSERT INTO PATIENT_MASTER VALUES 
('P0001', 'Gita', 35, 65, 'F', 'Chennai', '9867145678', 'Eye Infection', 'D0003'),
('P0002', 'Ashish', 40, 70, 'M', 'Delhi', '9845675678', 'Asthma', 'D0003'),
('P0003', 'Radha', 25, 60, 'F', 'Chennai', '9867166678', 'Pain in heart', 'D0005'),
('P0004', 'Chandra', 28, 55, 'F', 'Bangalore', '9978675567', 'Asthma', 'D0001'),
('P0005', 'Goyal', 42, 65, 'M', 'Delhi', '8967533223', 'Pain in Stomach', 'D0004');
GO

INSERT INTO ROOM_ALLOCATION VALUES 
('R0001', 'P0001', '2016-10-15', '2016-10-26'),
('R0002', 'P0002', '2016-11-15', '2016-11-26'),
('R0002', 'P0003', '2016-12-01', '2016-12-30'),
('R0004', 'P0001', '2017-01-01', '2017-01-30');
GO


select pid
from room_allocation
where MONTH(admission_date) = 1;
GO

select * from patient_master
where gender = 'F' and lower(disease) <> 'asthma'
GO

select gender, count(*) as total_count
from patient_master
group by gender;
GO

select room_no
from room_master
where room_no not in(
	select distinct room_no from room_allocation
);
GO

select room_no, count(*) as times_allocated
from room_allocation
group by room_no
having count(*) >1;
GO