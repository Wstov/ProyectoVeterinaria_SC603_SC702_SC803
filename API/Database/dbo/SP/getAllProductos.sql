﻿CREATE PROCEDURE getAllProductos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Producto;
END
