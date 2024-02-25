CREATE TABLE Classes (
    Id VARCHAR(32) PRIMARY KEY,
    Name NVARCHAR(255),
    Code VARCHAR(32),
    CreateTime DATETIME
);

Go

CREATE TABLE Subjects (
    Id VARCHAR(32) PRIMARY KEY,
    Name NVARCHAR(255),
    Code VARCHAR(32),
    CreateTime DATETIME
)
Go

CREATE TABLE Teachers (
    Id VARCHAR(32) PRIMARY KEY,
    Name NVARCHAR(255),
    Code VARCHAR(32),
    CreateTime DATETIME
)


Go

CREATE TABLE Rooms (
    Id VARCHAR(32) PRIMARY KEY,
    Code VARCHAR(32),
    CreateTime DATETIME
)

Go

CREATE TABLE Slots (
    Id VARCHAR(32) PRIMARY KEY,
    Code VARCHAR(32),
    CreateTime DATETIME
)
GO
ALTER TABLE Classes
ADD CONSTRAINT UC_Classes_Code UNIQUE (Code);

GO
ALTER TABLE Subjects
ADD CONSTRAINT UC_Subjects_Code UNIQUE (Code);

GO
ALTER TABLE Teachers
ADD CONSTRAINT UC_Teachers_Code UNIQUE (Code);

GO
ALTER TABLE Rooms
ADD CONSTRAINT UC_Rooms_Code UNIQUE (Code);

GO
ALTER TABLE Slots
ADD CONSTRAINT UC_Slots_Code UNIQUE (Code);


Go
CREATE TABLE Timetables (
    Id INT PRIMARY KEY,
    ClassCode VARCHAR(32),
    TeacherCode VARCHAR(32),
    SubjectCode VARCHAR(32),
    RoomCode VARCHAR(32),
    SlotCode VARCHAR(32),
	CreateTime DATETIME,
    FOREIGN KEY (ClassCode) REFERENCES Classes(Code),
    FOREIGN KEY (TeacherCode) REFERENCES Teachers(Code),
    FOREIGN KEY (SubjectCode) REFERENCES Subjects(Code),
    FOREIGN KEY (RoomCode) REFERENCES Rooms(Code),
    FOREIGN KEY (SlotCode) REFERENCES Slots(Code)
);