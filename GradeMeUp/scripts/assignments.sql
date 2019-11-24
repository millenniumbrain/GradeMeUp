CREATE TABLE IF NOT EXISTS assignments (
	ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	"Name" VARCHAR(255),
	PointsPossible INT,
	Points INT,
	StudentID INTEGER,
	AssignmentTypeID INTEGER,
	FOREIGN KEY(StudentID) REFERENCES students(ID),
	FOREIGN KEY(AssignmentTypeID) REFERENCES assignment_types(ID)
);