﻿CREATE PROCEDURE editProducto
    @ProductoID UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Categoria VARCHAR(100),
    @Presentacion VARCHAR(50),
    @Precio DECIMAL(25, 2),
    @Cantidad INT,
    @Descripcion VARCHAR(MAX),
    @Imagen VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Producto
    SET Nombre = @Nombre,
        Categoria = @Categoria,
        Presentacion = @Presentacion,
        Precio = @Precio,
        Cantidad = @Cantidad,
        Descripcion = @Descripcion,
        Imagen = @Imagen
    WHERE ProductoID = @ProductoID;
END
