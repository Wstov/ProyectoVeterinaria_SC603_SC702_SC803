﻿CREATE TABLE Campania (
    CampaniaID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    NombreCampania VARCHAR(100) NOT NULL,
    Proposito TEXT NOT NULL,
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    CostoCampania DECIMAL(10, 2) NOT NULL
);