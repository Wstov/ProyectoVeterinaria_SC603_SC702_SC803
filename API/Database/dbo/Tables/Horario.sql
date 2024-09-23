CREATE TABLE Horario (
    HorarioID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    HoraEntrada dateTIME NOT NULL,
    HoraSalida dateTIME NOT NULL,
    HorasTrabajadas INT NOT NULL
);