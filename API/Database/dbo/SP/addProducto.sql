CREATE PROCEDURE addProducto
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

    INSERT INTO Producto (Nombre, Categoria, Presentacion, Precio, Cantidad, Descripcion, Imagen)
    VALUES (@Nombre, @Categoria, @Presentacion, @Precio, @Cantidad, @Descripcion, @Imagen);
END
