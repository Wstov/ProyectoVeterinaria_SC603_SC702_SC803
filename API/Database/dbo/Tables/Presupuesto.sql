﻿CREATE TABLE Presupuesto (
    PresupuestoID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Presupuesto DECIMAL(10, 2) NOT NULL
);