﻿CREATE TABLE [dbo].[Usuarios]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nombre] VARCHAR(50) NOT NULL, 
    [Direccion] VARCHAR(150) NOT NULL, 
    [Telefono] INT NOT NULL, 
	[Correo] VARCHAR(50) NOT NULL
)