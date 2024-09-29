CREATE PROCEDURE getOneProducto
    @ProductoID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Producto
    WHERE ProductoID = @ProductoID;
END
