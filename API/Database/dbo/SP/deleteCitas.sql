﻿CREATE PROCEDURE deleteCitas
    @CitaID UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM RegistroCitas
    WHERE CitaID = @CitaID;
END;