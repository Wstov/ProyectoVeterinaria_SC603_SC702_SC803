CREATE PROCEDURE getCitas
AS
BEGIN
    SELECT CitaID, CodigoCita, FechaHora, Dueño, MascotaID, Motivo, VeterinarioAsignadoID
    FROM RegistroCitas;
END;