﻿function getYear() {
    var fecha = new Date();
    return fecha.getFullYear();
}

window.onload = function ()
{
    document.getElementById('anio').innerHTML = getYear();

    var pageIndex = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1, window.location.pathname.length).toLowerCase();
    if (pageIndex == 'index.html' || pageIndex == '') {
        var host = window.location.toString().substring(0, window.location.toString().lastIndexOf('/'));
        var swaggerPage = host + '/swagger/docs/v1';
        var request = new XMLHttpRequest();
        request.open('GET', swaggerPage, false);
        request.send();

        if (request.status == 200) {
            var pReadMore = document.getElementById('pReadMore');
            var btnReadMore = document.getElementById('btnReadMore');
            if (pReadMore != undefined && btnReadMore != undefined) {
                pReadMore.style.display = '';
                btnReadMore.style.display = '';
            }
        }
    }
}