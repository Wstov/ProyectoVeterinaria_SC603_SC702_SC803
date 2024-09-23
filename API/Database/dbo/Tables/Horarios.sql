CREATE TABLE [dbo].[Horarios]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [HoraEntrada] DATETIME NOT NULL, 
    [HoraSalida] DATETIME NOT NULL, 
    [HorasTrabajadas] INT NOT NULL 
)
