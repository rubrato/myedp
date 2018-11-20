var mymap = L.map('mapid').fitWorld();

mymap.locate({ setView: true, maxZoom: 16 });

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
    maxZoom: 18,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
        '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
        'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    id: 'mapbox.streets'
}).addTo(mymap);

L.marker([-20.308811, -40.321348]).addTo(mymap);

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/marcador")
    .configureLogging(signalR.LogLevel.Information)
    .build();
connection.start().catch(err => console.error(err.toString()));

connection.on("getMarcador", (lttd, lngtd) => {
    console.log(lttd, lngtd);
    L.marker([lttd, lngtd]).addTo(mymap);
});

connection.on("sendMarcador", (lttd, lngtd, obs) => {
    L.marker([lttd, lngtd]).addTo(mymap).bindPopup(obs);
});

connection.on("sendConsulta", (consulta) => {
    console.log(consulta);
});


var popup = L.popup();
var sndlttd;
var sndlngtd;

function onMapClick(e) {
    //popup
    //    .setLatLng(e.latlng)
    //    .setContent("You clicked the map at " + e.latlng.lat + ", " + e.latlng.lng)
     //   .openOn(mymap);
    sndlttd = e.latlng.lat;
    sndlngtd = e.latlng.lng;
    //connection.invoke('GetMarcador', e.latlng.lat, e.latlng.lng);
}
mymap.on('click', onMapClick);


var botaoAdicionar = document.querySelector("#adicionar-servico")

botaoAdicionar.addEventListener("click", function () {
    event.preventDefault(); //evita a pagina ser recarregada ao clicar no botao
    var form = document.querySelector("#form-adicionar");
    var observacoes = form.observacoes.value;
    console.log(observacoes);
    connection.invoke('SendMarcador', sndlttd, sndlngtd, observacoes);
});