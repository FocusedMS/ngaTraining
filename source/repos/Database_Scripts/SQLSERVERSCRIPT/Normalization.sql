CREATE TABLE Course (
    Ccode VARCHAR(10) PRIMARY KEY,
    Coursename VARCHAR(50),
    Duration VARCHAR(20),
    Fee NUMERIC(9,2),
    FacultyCode VARCHAR(10)
);


CREATE TABLE Faculty (
    FacultyCode VARCHAR(10) PRIMARY KEY,
    FacultyName VARCHAR(50),
    Qualification VARCHAR(50)
);

CREATE TABLE Student1 (
    StudentNo INT PRIMARY KEY,
    StudentName VARCHAR(50),
    City VARCHAR(50),
    Gender VARCHAR(10) CHECK (Gender IN ('MALE', 'FEMALE'))
);


CREATE TABLE Batch (
    BatchCode VARCHAR(10) PRIMARY KEY,
    BatchName VARCHAR(50),
    StartDate DATE,
    EndDate DATE,
    Timing VARCHAR(20)
);


CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY,
    StudentNo INT,
    Ccode VARCHAR(10),
    PaymentDate DATE,
    FOREIGN KEY (StudentNo) REFERENCES Student1(StudentNo),
    FOREIGN KEY (Ccode) REFERENCES Course(Ccode)
);


CREATE TABLE Enrollment (
    StudentNo INT,
    Ccode VARCHAR(10),
    BatchCode VARCHAR(10),
    PRIMARY KEY (StudentNo, Ccode),
    FOREIGN KEY (StudentNo) REFERENCES Student1(StudentNo),
    FOREIGN KEY (Ccode) REFERENCES Course(Ccode),
    FOREIGN KEY (BatchCode) REFERENCES Batch(BatchCode)
);
