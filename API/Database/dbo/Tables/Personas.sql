CREATE TABLE [dbo].[Personas] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Nombre]    VARCHAR (50)     NOT NULL,
    [Apellido]  VARCHAR (150)    NOT NULL,
    [Direccion] VARCHAR (200)    NOT NULL,
    [Telefono]  VARCHAR (9)      NOT NULL,
    [Correo]    VARCHAR (150)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);