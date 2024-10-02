CREATE PROCEDURE editProducto
    @ProductoID UNIQUEIDENTIFIER,
    @Nombre VARCHAR(100),
    @Categoria VARCHAR(100),
    @Presentacion VARCHAR(50),
    @Precio DECIMAL(25, 2),
    @Cantidad INT,
    @Descripcion VARCHAR(MAX),
    @Imagen VARBINARY(MAX) = NULL -- Parámetro por defecto
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
        Imagen = CASE 
                    WHEN @Imagen IS NOT NULL THEN @Imagen 
                    ELSE Imagen 
                 END
    WHERE ProductoID = @ProductoID;
END
