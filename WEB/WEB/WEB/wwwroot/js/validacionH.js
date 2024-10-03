document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const dateInput = document.querySelector("#appointmentDate");

    form.addEventListener("submit", function (event) {
        event.preventDefault();

        const selectedDate = new Date(dateInput.value);
        const currentDate = new Date();
        const currentDay = selectedDate.getDay();
        const currentHour = selectedDate.getHours();

        if (selectedDate < currentDate) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No puedes seleccionar una fecha en el pasado.'
            });
            return;
        }

        if (currentDay === 0) {
            Swal.fire({
                icon: 'error',
                title: 'Días Cerrados',
                text: 'Los domingos están cerrados.'
            });
            return;
        }

        if (currentDay === 6 && (currentHour < 7 || currentHour >= 17)) {
            Swal.fire({
                icon: 'error',
                title: 'Horario Cerrado',
                text: 'Los sábados solo se atiende entre las 7:00 y 17:00.'
            });
            return;
        }

        if ((currentDay >= 1 && currentDay <= 5) && (currentHour < 7 || currentHour >= 18)) {
            Swal.fire({
                icon: 'error',
                title: 'Horario Cerrado',
                text: 'De lunes a viernes solo se atiende entre las 7:00 y 18:00.'
            });
            return;
        }

        form.submit();
    });
});
