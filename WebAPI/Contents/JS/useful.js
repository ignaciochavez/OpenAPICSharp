function getYear() {
    var fecha = new Date();
    return fecha.getFullYear();
}

window.onload = function () {
    document.getElementById('anio').innerHTML = getYear();
}