CREATE PROCEDURE deleteProducto
    @ProductoID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Producto
    WHERE ProductoID = @ProductoID;
END
