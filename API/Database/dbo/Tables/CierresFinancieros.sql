CREATE TABLE CierresFinancieros (
    CierreID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
    FechaInicio DATE NOT NULL,               
    FechaFin DATE NOT NULL,                  
    TotalIngresos DECIMAL(18, 2) NOT NULL,   
    TotalGastos DECIMAL(18, 2) NOT NULL,     
    BalanceFinal AS (TotalIngresos - TotalGastos) PERSISTED
);
